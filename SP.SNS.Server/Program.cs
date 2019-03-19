using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RedisCache.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SP.SNS.Server
{
    class Program
    {
        private static string channelTemplate = "channel-{0}";
        private static Dictionary<string, TcpClient> clientDic = new Dictionary<string, TcpClient>();
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            AssemblyRegistHelper.Register(configuration);
            RedisCacheRegisteConfig.Register(IocManager.Instance);

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            
            server.Start(1000);
            Console.WriteLine("Server has started on 127.0.0.1:8888.{0}Waiting for a connection...", Environment.NewLine);

            //enter to an infinite cycle to be able to handle every change in stream
            AsyncAcceptTcpClient(server);
        }

        async static void AsyncAcceptTcpClient(TcpListener server)
        {
            while (true)
            {
                var client = server.AcceptTcpClient();
                var task = Task.Factory.StartNew(() =>
                {
                    AsyncConnetct(client);
                });                
            }
        }
        async static Task AsyncConnetct(TcpClient client)
        {            
            BinaryReader reader = new BinaryReader(client.GetStream());
            BinaryWriter writer = new BinaryWriter(client.GetStream());
            bool isPack = false;
            while (true)
            {
                try
                {
                    if (!isPack)
                    {
                        if (client.Available > 0)
                        {
                            byte[] buffer = new byte[client.Available];
                            reader.Read(buffer, 0, client.Available);
                            String result = Encoding.UTF8.GetString(buffer);
                            await RegisterChannel(result, client);
                            Console.WriteLine(result);
                            if (result.Contains("Sec-WebSocket-Key"))
                            {
                                writer.Write(PackHandShakeData(GetSecKeyAccetp(result)));
                                writer.Flush();
                                isPack = true;
                            }
                        }
                    }
                    else
                    {
                        if (client.Available > 0)
                        {
                            byte[] buffer = new byte[client.Available];
                            reader.Read(buffer, 0, client.Available);
                            String result = AnalyticData(buffer, buffer.Length);
                            Console.WriteLine("result =" + result);
                            var msgObject = JsonConvert.DeserializeObject<Msg>(result);
                            var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
                            var accountId = msgObject.To;
                            var channel = string.Format(channelTemplate, accountId);
                            cache.Publish(channel, result);
                        }
                    }
                }
                catch (Exception exp)
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                    if (writer != null)
                    {
                        writer.Dispose();
                    }
                    if (client != null)
                    {
                        client.Close();
                    }
                    break;
                }
            }            
        }
        static async Task RegisterChannel(string handShakeText,TcpClient client)
        {            
            var dic = GetParameter(handShakeText);
            if(dic.ContainsKey("aid"))
            {
                var accountId = dic["aid"];
                var channel = string.Format(channelTemplate, accountId);
                var cache = IocManager.Instance.Resolve<ICacheManager>().GetCache<string, string>("CacheItems");
                if (clientDic.ContainsKey(accountId))
                {
                    cache.Unsubscribe(channel, null);
                    clientDic.Remove(accountId);
                }
                clientDic.Add(accountId, client);
                
                cache.Subscribe(channel,(redisChannel,msg)=>
                {
                    var aid = redisChannel.Replace("channel-", "");
                    if (clientDic.ContainsKey(aid))
                    {
                        var writerClient = clientDic[aid];
                        BinaryWriter writer = new BinaryWriter(client.GetStream());
                        writer.Write(PackData(msg));
                        writer.Flush();
                    }
                });
            }
        }
        /// <summary>
        /// 打包握手信息
        /// </summary>
        /// <param name="secKeyAccept">Sec-WebSocket-Accept</param>
        /// <returns>数据包</returns>
        private static byte[] PackHandShakeData(string secKeyAccept)
        {
            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + Environment.NewLine);
            responseBuilder.Append("Upgrade: websocket" + Environment.NewLine);
            responseBuilder.Append("Connection: Upgrade" + Environment.NewLine);
            responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine + Environment.NewLine);
            //如果把上一行换成下面两行，才是thewebsocketprotocol-17协议，但居然握手不成功，目前仍没弄明白！
            //responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine);
            //responseBuilder.Append("Sec-WebSocket-Protocol: chat" + Environment.NewLine);

            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }
        /// <summary>
        /// 解析客户端数据包
        /// </summary>
        /// <param name="recBytes">服务器接收的数据包</param>
        /// <param name="recByteLength">有效数据长度</param>
        /// <returns></returns>
        private static string AnalyticData(byte[] recBytes, int recByteLength)
        {
            if (recByteLength < 2) { return string.Empty; }

            bool fin = (recBytes[0] & 0x80) == 0x80; // 1bit，1表示最后一帧  
            if (!fin)
            {
                return string.Empty;// 超过一帧暂不处理 
            }

            bool mask_flag = (recBytes[1] & 0x80) == 0x80; // 是否包含掩码  
            if (!mask_flag)
            {
                return string.Empty;// 不包含掩码的暂不处理
            }

            int payload_len = recBytes[1] & 0x7F; // 数据长度  

            byte[] masks = new byte[4];
            byte[] payload_data;

            if (payload_len == 126)
            {
                Array.Copy(recBytes, 4, masks, 0, 4);
                payload_len = (UInt16)(recBytes[2] << 8 | recBytes[3]);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 8, payload_data, 0, payload_len);

            }
            else if (payload_len == 127)
            {
                Array.Copy(recBytes, 10, masks, 0, 4);
                byte[] uInt64Bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    uInt64Bytes[i] = recBytes[9 - i];
                }
                UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                payload_data = new byte[len];
                for (UInt64 i = 0; i < len; i++)
                {
                    payload_data[i] = recBytes[i + 14];
                }
            }
            else
            {
                Array.Copy(recBytes, 2, masks, 0, 4);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 6, payload_data, 0, payload_len);

            }

            for (var i = 0; i < payload_len; i++)
            {
                payload_data[i] = (byte)(payload_data[i] ^ masks[i % 4]);
            }

            return Encoding.UTF8.GetString(payload_data);
        }


        /// <summary>
        /// 生成Sec-WebSocket-Accept
        /// </summary>
        /// <param name="handShakeText">客户端握手信息</param>
        /// <returns>Sec-WebSocket-Accept</returns>
        private static string GetSecKeyAccetp(String handShakeText)
        {
            string key = string.Empty;
            Regex r = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n");
            Match m = r.Match(handShakeText);
            if (m.Groups.Count != 0)
            {
                key = Regex.Replace(m.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }
            byte[] encryptionString = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"));
            return Convert.ToBase64String(encryptionString);
        }
        private static Dictionary<string, string> GetParameter(String handShakeText)
        {
            Dictionary<string,string> keyValues = new Dictionary<string, string>();
            Regex r = new Regex(@"GET(.*?)\r\n");
            Match m = r.Match(handShakeText);
            if (m.Groups.Count != 0)
            {
                var temp  = m.Value.Split(' ');
                if(temp.Length > 2)
                {
                    var index = temp[1].IndexOf("?");
                    var str = temp[1].Substring(index + 1);
                    var paramters = str.Split("&");
                    foreach(var item in paramters)
                    {
                        var paramter = item.Split("=");
                        if (paramter.Length >= 2)
                        {
                            keyValues.Add(paramter[0], paramter[1]);
                        }
                    }
                }
            }            
            return keyValues;
        }

        /// <summary>
        /// 打包服务器数据
        /// </summary>
        /// <param name="message">数据</param>
        /// <returns>数据包</returns>
        private static byte[] PackData(string message)
        {
            byte[] contentBytes = null;
            byte[] temp = Encoding.UTF8.GetBytes(message);

            if (temp.Length < 126)
            {
                contentBytes = new byte[temp.Length + 2];
                contentBytes[0] = 0x81;
                contentBytes[1] = (byte)temp.Length;
                Array.Copy(temp, 0, contentBytes, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                contentBytes = new byte[temp.Length + 4];
                contentBytes[0] = 0x81;
                contentBytes[1] = 126;
                contentBytes[2] = (byte)(temp.Length & 0xFF);
                contentBytes[3] = (byte)(temp.Length >> 8 & 0xFF);
                Array.Copy(temp, 0, contentBytes, 4, temp.Length);
            }
            else
            {
                // 暂不处理超长内容  
            }

            return contentBytes;
        }
    }
}

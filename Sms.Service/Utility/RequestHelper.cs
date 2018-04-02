using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sms.Service.Utility
{
    public class RequestHelper
    {
        /// <summary>
        /// 执行Get请求
        /// </summary>
        public static string GetRequestServer(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            return GetRequestServer(request);
        }

        public static string GetRequestServer(HttpWebRequest request)
        {
            string responseStr = string.Empty;
            HttpWebResponse response = null;

            try
            {
                request.Method = "GET";
                request.Timeout = 10000;
                response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                responseStr = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteFile("GET--url:" + request.RequestUri + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, "requestError.txt");
            }
            finally
            {
                response?.Close();

                request?.Abort();
            }
            return responseStr;
        }

        /// <summary>
        /// 执行Post请求(传入url)
        /// </summary>
        public static string PostRequestServer(string url, string json,string contentType= "application/json")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            return PostRequestServer(request, json);
        }

        /// <summary>
        /// 执行Post请求(传入url)
        /// </summary>
        public static string PostRequestServer(string url, byte[] bData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            return PostRequestServer(request, bData);
        }

        /// <summary>
        /// 执行Post请求(传入自定义的request)
        /// </summary>
        public static string PostRequestServer(HttpWebRequest request, string json)
        {
            var responseStr = string.Empty;
            // 解决：服务器提交了协议冲突. section=responsestatusline
            request.KeepAlive = false;
            request.AllowAutoRedirect = true;
            request.CookieContainer = new System.Net.CookieContainer();
            HttpWebResponse response = null;

            try
            {
                request.Method = "POST";
                request.Timeout = 10000;
                var myStreamWriter = new StreamWriter(request.GetRequestStream());
                myStreamWriter.Write(json);
                myStreamWriter.Close();
                response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                responseStr = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteFile("POST--url:" + request.RequestUri + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + json, "requestError.txt");
            }
            finally
            {
                response?.Close();

                request?.Abort();
            }
            return responseStr;
        }

        /// <summary>
        /// 执行Post请求(传入自定义的request)
        /// </summary>
        public static string PostRequestServer(HttpWebRequest request, byte[] bData)
        {
            HttpWebResponse response = null;

            string strResult = string.Empty;
            try
            {
                request.Method = "POST";
                request.Timeout = 10000;
                request.ContentLength = bData.Length;
                Stream smWrite = request.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();

                response = (HttpWebResponse)request.GetResponse();
                Encoding encode = Encoding.ASCII;
                if (response.CharacterSet.Contains("utf-8"))
                    encode = Encoding.UTF8;
                StreamReader srReader = new StreamReader(response.GetResponseStream(), encode);
                strResult = srReader.ReadToEnd();
                srReader.Close();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteFile("POST--url:" + request.RequestUri + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + Encoding.Default.GetString(bData), "requestError.txt");
            }
            finally
            {
                response?.Close();

                request?.Abort();
            }

            return strResult;
        }

    }
}

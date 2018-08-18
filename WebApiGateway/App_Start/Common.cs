using AccountGRPCInterface;
using Aop.Api;
using SP.Api.Model.Account;
using SP.Api.Model.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WeddingCarService.WXPay;

namespace WebApiGateway.App_Start
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Common
    {
        #region 判断是否移动端

        public static bool IsMobileDevice(HttpRequest request)
        {
            if (request != null)
            {
                var u = request.ServerVariables["HTTP_USER_AGENT"];
                if (!string.IsNullOrEmpty(u))
                {
                    var b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    var v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    return (b.IsMatch(u) || v.IsMatch(u.Substring(0, 4)));
                }
            }
            return false;
        }

        # endregion
        #region 密码规则验证
        /// <summary>
        /// 密码规则验证
        /// </summary>
        public static bool ValidatePassword(string str)
        {
            bool flag = false;
            if (str.Length >= 8 && str.Length <= 20 && Regex.IsMatch(str, @"[a-zA-Z]") && Regex.IsMatch(str, @"[0-9]"))
                flag = true;
            return flag;
        }
        #endregion

        #region 验证Email的有效性

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[\w-]+$");
        }

        #endregion

        #region 验证Account的有效性

        public static bool ValidateAccount(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
                return false;

            if (account.Length < 5 || (!ValidateEmail(account) && !Regex.IsMatch(account, @"^[+]?\d+$")))
                return false;

            return true;
        }
        public static string CheckAccount(string account)
        {
            if (!ValidateAccount(account))
                return string.Empty;

            if (!account.StartsWith("86")&& !account.StartsWith("+"))
                account = "86" + account;

            if (!account.Contains("+"))
                account = "+" + account;

            return account;
        }
        #endregion
        public static bool VerifyRegisterCode(string code, string account)
        {
            var authType = Common.ValidateEmail(account) ? AuthType.Register : AuthType.MobilePhoneRegister;

            string verifyCode = AccountBusiness.GetValidVerifyCodeByAccount(new AuthenticationModel()
            {
                Account = account,
                VerifyCode = code,
                AuthType = AuthType.MobilePhoneRegister,
                AccountId = string.Empty,
                Token = string.Empty
            });

            if (!string.IsNullOrEmpty(verifyCode) && code.Equals(verifyCode))
            {
                return true;
            }
            return false;
        }

        #region 验证手机号的有效性
        /// <summary>
        /// 判断是否是手机号码
        /// </summary>
        public static bool ValidateChineseMobilePhone(string mobilePhone)
        {
            return Regex.IsMatch(mobilePhone, @"^(\((\+)?86\)|((\+)?86)?)0?1[3-8]{1}\d{9}$");
        }

        /// <summary>
        /// 手机号码验证
        /// </summary>
        public static bool ValidateChineseMobilePhoneWithCode(string mobilePhone)
        {
            if (string.IsNullOrEmpty(mobilePhone))
            {
                return false;
            }
            return Regex.IsMatch(mobilePhone, @"^(1[3-8]{1})\d{9}$");
        }

        #endregion

        #region 获取中国手机号码

        /// <summary>
        /// 带+86
        /// </summary>
        public static string GetMobileNumberWithCode(string mobileNumber)
        {
            if (ValidateChineseMobilePhone(mobileNumber))
            {
                return "+86" + mobileNumber.Substring(mobileNumber.Length - 11);
            }
            return mobileNumber;
        }

        /// <summary>
        /// 不带+86
        /// </summary>
        public static string GetMobileNumberWithoutCode(string mobileNumber)
        {
            if (ValidateChineseMobilePhone(mobileNumber))
            {
                return mobileNumber.Substring(mobileNumber.Length - 11);
            }
            return mobileNumber;
        }

        #endregion

        #region 过滤非法注入字符

        /// <summary>
        /// 去除字符串中的控制字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterControlCharacter(string str)
        {
            return str == null ? null : new string(str.Where(c => !char.IsControl(c)).ToArray());
        }

        /// <summary>
        /// 过滤非法注入字符
        /// </summary>
        public static string FilterInvalidString(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Replace(" ", string.Empty).Replace(",", string.Empty).Replace("'", string.Empty)
                    .Replace("=", string.Empty).Replace("\"", string.Empty).Replace("*", string.Empty);
            else
                return str;
        }

        /// <summary>
        /// 去掉小于号和大于号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAngleBrackets(string str)
        {
            return str?.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        #endregion

        #region 部分 html编码 替换

        public static string HtmlEncode(string str)
        {
            string returnStr = string.Empty;

            if (!string.IsNullOrEmpty(str))
            {
                returnStr = str.Replace("<", "&lt;").Replace(">", "&gt;");
            }
            return returnStr;
        }

        public static string HtmlDecode(string str, bool isApi = false)
        {
            string returnStr = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                if (isApi) //API 大于小于号要替换 PC端不可以
                {
                    returnStr =
                        str.Replace("&ndash;", "-")
                            .Replace("&#39;", "'")
                            .Replace("&lt;", "<")
                            .Replace("&gt;", ">")
                            .Replace("&acute;", "'")
                            .Replace("&quot;", "\"")
                            .Replace("&nbsp;", " ")
                            .Replace("&amp;", "&")
                            .Replace("&#183;", "·");
                }
                else
                {
                    returnStr = str;
                }
            }
            return returnStr;
        }

        #endregion

        #region 获取签名
        /// <summary>
        /// 获取签名
        /// </summary>
        public static string GetSignature(string appkey, string appsecret, string timestamp, string nonce, string content)
        {
            List<string> arr = new List<string>();
            arr.Add(appkey.ToLower());
            arr.Add(appsecret.ToLower());
            arr.Sort();
            string appinfo = string.Join(string.Empty, arr.ToArray());

            appinfo = GetMd5(appinfo).ToLower();

            arr.Clear();
            arr.Add(appinfo);
            arr.Add(timestamp.ToLower());
            arr.Add(nonce.ToLower());
            arr.Sort();
            //将content内容插到最开始部分
            arr.Insert(0, content.ToLower());

            string signature = string.Join(string.Empty, arr.ToArray());
            signature = GetSha1(signature).ToLower();

            return signature;
        }

        /// <summary>
        /// 返回MD5
        /// </summary>
        private static string GetMd5(string str)
        {
            string md5Str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            return md5Str;
        }

        /// <summary>
        /// 返回SHA1
        /// </summary>
        private static string GetSha1(string str)
        {
            string sha1Str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            return sha1Str;
        }
        #endregion

        #region 获取枚举描述 或 根据描述获取枚举

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        public static string GetEnumDesc<T>(T enumtype)
        {
            if (enumtype == null) throw new ArgumentNullException("enumtype");
            if (!enumtype.GetType().IsEnum) throw new Exception("参数类型不正确");
            return ((DescriptionAttribute)enumtype.GetType().GetField(enumtype.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 根据描述获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumName<T>(string description)
        {
            var _type = typeof(T);
            foreach (FieldInfo field in _type.GetFields())
            {
                var descriptionAttribute = field.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (descriptionAttribute != null)
                {
                    if (descriptionAttribute.Description == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        #endregion

        #region 判断是否是Guid

        public static bool IsGuid(string str)
        {
            var result = false;
            if (string.IsNullOrEmpty(str)) return false;

            try
            {
                Guid g = new Guid(str);
                result = true;
            }
            catch (Exception)
            {
                // ignored
            }
            return result;
        }

        #endregion

        #region  分离Guid和string

        public static Tuple<List<string>, List<string>> SplitGuid(List<string> dataList)
        {
            var ids = new List<string>();
            var items = new List<string>();
            foreach (var item in dataList)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (IsGuid(item))
                {
                    ids.Add(item);
                }
                else
                {
                    items.Add(item);
                }
            }
            return Tuple.Create<List<string>, List<string>>(ids, items);
        }

        #endregion

        #region 下载文件名浏览器差异处理

        public static string FileNameDownloadDecode(string fileName)
        {
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpBrowserCapabilities brObject = HttpContext.Current.Request.Browser;
            var tourversion = brObject.Type;
            if (tourversion == "IE11" || tourversion == "IE10" || tourversion == "IE9" || tourversion == "IE8" || tourversion == "IE7" || tourversion == "IE6" || tourversion == "InternetExplorer11")
            {
                fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);

            }
            return fileName;
        }

        #endregion

        #region 对象克隆

        public static T Clone<T>(T source)
        {
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion

        #region 随机数字符串

        public static string GetRandomNumberStr(int count)
        {
            List<string> codeList = new List<string>();
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                codeList.Add(rand.Next(0, 10).ToString());
            }
            return string.Join(string.Empty, codeList.ToArray());
        }

        #endregion

        #region 获取反向代理之后的真实IP地址
        /// <summary>
        /// 获取真实IP地址
        /// </summary>
        public static string GetRealIPAddress()
        {
            if (HttpContext.Current == null) return string.Empty;

            return string.IsNullOrEmpty(HttpContext.Current.Request.Headers.Get("X-Real-IP")) ? HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.Headers["X-Real-IP"];
        }
        #endregion

        #region 创建随机文件夹目录，并返回该目录名，如果存在则直接返回该目录名
        /// <summary>
        /// 创建随机文件夹目录，并返回该目录名
        /// </summary>
        public static string CreateRandomDirectoryFolder(string absolutePath)
        {
            var random = new Random();
            var folderName = RandomCode(random, 5);
            var exist = Directory.Exists(absolutePath + "\\" + folderName);
            if (!exist)
            {
                Directory.CreateDirectory(absolutePath + "\\" + folderName);
            }
            return folderName;

        }

        /// <summary>
        /// 产生随机字符串
        /// </summary>
        private static string RandomCode(Random random, int digit)
        {
            string[] strArr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string outstr = string.Empty;
            for (int i = 0; i < digit; i++)
            {
                outstr = outstr + strArr[random.Next(strArr.GetLowerBound(0), strArr.GetUpperBound(0))];
            }
            return outstr;
        }

        private static Object singletonLockStream = new Object();

        public static void WriteLog(string str)
        {
            string logPath = string.Empty;
            if ((System.Web.HttpContext.Current == null) || (System.Web.HttpContext.Current.Server == null)
                || (ConfigurationManager.AppSettings["LogPath"] == null))
            {
                logPath = "c:\\s1apilog\\";
            }
            else
            {
                logPath = ConfigurationManager.AppSettings["LogPath"];
            }


            if (string.IsNullOrEmpty(logPath))
            {
                return;
            }
            lock (singletonLockStream)
            {
                string lastFileDay = string.Empty;
                System.IO.FileStream logStream = null;
                System.IO.StreamWriter logSW = null;

                try
                {
                    //创建文件
                    if (string.IsNullOrEmpty(lastFileDay) || lastFileDay != DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        string logFilePath = logPath;
                        if (!Directory.Exists(logFilePath))
                        {
                            Directory.CreateDirectory(logFilePath);
                        }

                        lastFileDay = DateTime.Now.ToString("yyyy-MM-dd");
                        string fileName = logFilePath + "\\log_" + lastFileDay + ".txt";
                        if (logStream != null)
                        {
                            logSW.Close();
                            logSW.Dispose();
                            logStream.Close();
                            logStream.Dispose();
                        }
                        if (File.Exists(fileName))
                        {
                            logStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Write);
                        }
                        else
                        {
                            logStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                        }

                        logSW = new StreamWriter(logStream, Encoding.Default);
                        logSW.AutoFlush = true;
                    }
                    if (logStream != null)
                    {
                        if (logSW == null)
                        {
                            logSW = new StreamWriter(logStream, Encoding.Default);
                            logSW.AutoFlush = true;
                        }
                        logSW.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + str);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (logSW != null)
                    {
                        logSW.Close();
                        logSW.Dispose();
                    }
                    if (logStream != null)
                    {
                        logStream.Close();
                        logStream.Dispose();
                    }
                }
            }
        }
        #endregion
        #region 获取时间戳
        public static double GetTimestamp(DateTime d)
        {
            return (d.ToUniversalTime().Ticks - 621355968000000000) / 10000000;     //精确到毫秒
        }
        #endregion
        public static IAopClient GetAlipayClient()
        {
            //支付宝网关地址
            // -----沙箱地址-----
            //string serverUrl = "http://openapi.alipaydev.com/gateway.do";
            // -----线上地址-----
            string serverUrl = "https://openapi.alipay.com/gateway.do";
            //应用ID
            string appId = "2016110202478860";
            //商户私钥
            string privateKeyPem = "MIIEowIBAAKCAQEA0dFmScMZ9bDFyO9mzwBf4aOsWz50532VYNTKTSAj3vGqGeTdfoTo8PGsMpwXSSOyLn4fgauq6oU7FF10lUDK5l7MW1V01U9FEHHnisWFQstzisdQ/vBt0/pNg+dx80N0SwZEUQvr1j999eXfVNCKk9OeputZ6reWxym7VFelG8hRoCh38Aad6Eyx9u76uZVj7SDGKElxLHJ5NscLFgEBKkuBtk5CM3PApnRP0/0YGHFw1Q9eR48cLYNk+IYQ+NBrhLq7kpe2AU/95zCrUVAyl5/THhtXtgeXrR8DoNaQiSQeQnFkbH/AXUocQbD9xoSmpqxPpVZ5mgHi8vRFRSGI8wIDAQABAoIBAAbJh02LiiNwfHVriR/ZG0DpUoIBGzcB0Ps45vJnv8FOf/omdSPKmN5ycueQNRCgnvryCYLgUr8TYD3gaA0L84RQPLwXn5fm4I0PojXS/eSTQEGVAQG+NU9OIYPK0NzVqcVjhoI4qIBdvW1e6kKMHG97wP4VTutQ1QfMScsIqsajPTAiTTXvGG0ZQIIHa5KZQPCAV+aBVB8wcq+uXu+/t05y/bSaz/Z6jbaBu1py6vI7x4T9A/WpCQSPot0YnEUkjVHdPCLhiBUYyx7fS+Ghimx2w+HAqV+69CMlG+VJJZ2ZF2htS1OS+TE6ee9Hltf4uyzi5O2WhkvZfB0OLNTsA0ECgYEA/1iIjd5sSd0i0Ny6c5yjRw0qH9jBeSxsX9iP7rcuQDIK7meF7bWtxt9bRhEQZOZQVtS2f+xlcnRCMyGom2lu474oyybx+9zQ0ASfzJxOoGEWl4iHxBg7IaltihgrwlAQfv6/FDmMNpyq4Arez/wH3jtwyGDR+vfFCwu42lgLzC0CgYEA0lsBzgZ02dD/qaRbdXsI3tYl795bNrriIRSrM8hIuXaGmC/Oim76mgMPJzQo6KwBArlHXYzLJqH9pDnyjAzd2hSCuVekZzqcIlU7hq+Fbb6alGWxJXDWntFPDgO2DDNvwdvDP6vpTVnHbUUgp5CTlnj9e53xMBkWNNGz8cwjPZ8CgYBQfuRtdNTGZEP5e9v7XkHKwEerSnWTcYGopWiaZHzcF+qCRXhe+4sQypDHgdGdPU3OUbhGk4tXjXbhD5dLhu1CNkw93sUiFPZu3UZTBmNzA6hPlObX1putfO/fPMdutja9EinCgnHFNZZYkpSzFEyWrj5brMoFR96CIOvhSShO8QKBgQCU23P+gJEmJoEVYkqaazOOxYpZIUf5BezJrh+s6DmWjOqYPZRyYDCU1j9d5cphRH7+l3CKIf0M3KtqENu0AdLo5YTQddXQeyhMH3adTA4m6C/pfcELFru57bJ832FvZuLaE5MqtpPFehfnGnkOOwBGBl6SoOUxvT76p78oB+aUTwKBgEozzBnOBCcUYyrT0b5r5/2RU4n8672uztN3sDvDptXGWgCnRYDyExyvR+vE0D6twJKz46lIr/sAgj7TcgfHowFBCO2OHBBw6gx9JMdKrnvkByVe8ksX2co3n6xpv6YqJHuPX3wP6HR82f3jLFeOHv2FxFu5PnhoyeTPPX3XlwyC";
            string publicKeyPem = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0dFmScMZ9bDFyO9mzwBf4aOsWz50532VYNTKTSAj3vGqGeTdfoTo8PGsMpwXSSOyLn4fgauq6oU7FF10lUDK5l7MW1V01U9FEHHnisWFQstzisdQ/vBt0/pNg+dx80N0SwZEUQvr1j999eXfVNCKk9OeputZ6reWxym7VFelG8hRoCh38Aad6Eyx9u76uZVj7SDGKElxLHJ5NscLFgEBKkuBtk5CM3PApnRP0/0YGHFw1Q9eR48cLYNk+IYQ+NBrhLq7kpe2AU/95zCrUVAyl5/THhtXtgeXrR8DoNaQiSQeQnFkbH/AXUocQbD9xoSmpqxPpVZ5mgHi8vRFRSGI8wIDAQAB";

            IAopClient client = new DefaultAopClient(serverUrl, appId, privateKeyPem, "json", "1.0", "RSA2", publicKeyPem, "utf-8", false);


            return client;
        }

        public static IAopClient GetAlipayManageClient()
        {
            //支付宝网关地址
            // -----沙箱地址-----
            //string serverUrl = "http://openapi.alipaydev.com/gateway.do";
            // -----线上地址---
            string serverUrl = "https://openapi.alipay.com/gateway.do";
            //应用ID
            string appId = "2018072660803348";
            //商户私钥
            string privateKeyPem = "MIIEogIBAAKCAQEA0Lwqcl56YhrLM4xvYsVUG2sSisPM67YwjqFjlyrRfh4QYr6A" +
                            "8ftWZggw4EEyC/7QlZct26L//iQx0xJCJdeZX1Prp+AO5BP6A/LTihrHRSImnKk6" +
                            "bshNK8ftGRVV+gn5xpAcGSVXNmMTau/13YT5oImyAN2N57+Vfw5khZjrDQSdnI/v" +
                            "1ZOHMh+6M1CpooGnXFUQhfvuOUz8tnG3rhPRUeJx+OI/WokSwDvssceCw7J1tq1y" +
                            "FajsT3veBAH4Lf0CkxQ14cdOhzw1AKyB7QqRpUPVS7s/8m5Dj7BpmOhlNdaC8Qxo" +
                            "mStQ47/AMfgf0ckVPxC4y4TIFLLEE0r/P+qqWQIDAQABAoIBABhxEB9YRMhcDtM4" +
                            "bIqKRLD5NrCdAM/RA5qP91NqJiG4b37Ag+TR42oLJ3365FtXOSFrFGmVipg9sL/g" +
                            "cXJw7nqlHwEHUXcPh8USmAah4BeOSisp/befKLoprO+0+d5PcLn5gfDH2JB7xXhO" +
                            "JOINghV0Si2jw8wROPYpv0eNgmiBnZZkgOeWMSa0+7A0orAESXR/wCNyDqM+6tpU" +
                            "5VxSSC88G36Uisl+T2rygYqpUvxUJKiIw6ybPLK+AtjnZFnjSMfULs2WswN8EAcd" +
                            "WXQSV2VDYt9o89LUPkNkaZvrLvBAW0uhTPpcGY3ojcZ076J3mc6d90mKhaV/U4M6" +
                            "fLTPzMkCgYEA99xlzm7i5V+RY9hYElSpbqQqL67fdETdiXu/D1sAaU5gzoBjyCwO" +
                            "ZTVWWVYALAu4rg/yKya1He0W09P5Je9diwOepjfeAhEAdEn6mIX95yXHvEKr+KeS" +
                            "gOHPLBrpD8vjz57Qx24nqPhv9cqN+bmoqciHaPDN6UYxl9q3Cx5f1Y8CgYEA15bc" +
                            "06u1E+GyWi91qLIoPCmdrhgWvFz/oEoeSQJsudO527ESAHzWUbIQMU9F+5elahbF" +
                            "i3l/NHb8tI6RJxF3adiNBm5IBO7kTPvnM3rOGf+PCpj6WrcB4x3gznYIC5M0Kq58" +
                            "MWzlgHz0h6bGFZFhU/iDHDr+9CfxKO7rn9JlnZcCgYBhICNgtkEBIublGjTA0h4m" +
                            "tfu1/a+gbw/kvPIMgVty6Hy0zsjK+sCVVkZE8ZdVqy4uPx9lW2CjnnFQhoLeI6Mj" +
                            "Q0La3Y+IfcMQzqB9PhxVhI91LScYZAnbqSC2psDnKL1XcWNcksTFyLyOs1XZIrB9" +
                            "/49YRuxZWeE7IUCTAc8J7wKBgGOP/meassN9xdo0dMf1jvNJ3NIbudQ/tqYV1Kqu" +
                            "/ftLtlepDMFA5dFQxH2hOJaaUAAQiCaDc6WNFeNku9ApFtbA9U/0+jmdAb4zz3BM" +
                            "1IXZKfwg7e2a+roigEGY7No7nyne7uf8fHi8PEmxUWWAgExBRntxG3EySQM4Y4Sp" +
                            "q9f9AoGAMqLW2Wlu/odEOivSnwvUkTL0zfyEQz31L7gavgQv6wkgfA9O4bpJtIEe" +
                            "gQ8+dsdr9ez45yEXiuPc++xhs6RzivwejhguhsmAqclYV3w4Wr5kwjzL+5NYIWUe" +
                            "+t4DMzqBHDC2USq/wzcGwTJdE/i8e7LUNtjDB61L3Lr8FcxAOMQ=";
            string publicKeyPem = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0Lwqcl56YhrLM4xvYsVUG2sSisPM67YwjqFjlyrRfh4QYr6A8ftWZggw4EEyC/7QlZct26L//iQx0xJCJdeZX1Prp+AO5BP6A/LTihrHRSImnKk6bshNK8ftGRVV+gn5xpAcGSVXNmMTau/13YT5oImyAN2N57+Vfw5khZjrDQSdnI/v1ZOHMh+6M1CpooGnXFUQhfvuOUz8tnG3rhPRUeJx+OI/WokSwDvssceCw7J1tq1yFajsT3veBAH4Lf0CkxQ14cdOhzw1AKyB7QqRpUPVS7s/8m5Dj7BpmOhlNdaC8QxomStQ47/AMfgf0ckVPxC4y4TIFLLEE0r/P+qqWQIDAQAB";

            IAopClient client = new DefaultAopClient(serverUrl, appId, privateKeyPem, "json", "1.0", "RSA2", publicKeyPem, "utf-8", false);


            return client;
        }
        /// <summary>
        /// 创建微信的package签名
        /// </summary>
        /// <param name="key">密钥键</param>
        /// <param name="value">财付通商户密钥（自定义32位密钥）</param>
        /// <returns></returns>
        public static string CreateMd5Sign(string key, string value, Hashtable parameters, string _ContentEncoding)
        {
            var sb = new StringBuilder();
            //数组化键值对，并排序
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            //循环拼接包参数
            foreach (string k in akeys)
            {
                var v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            //最后拼接商户自定义密钥
            sb.Append(key + "=" + value);
            //加密
            string sign = MD5Util.GetMD5(sb.ToString(), _ContentEncoding).ToUpper();
            //返回密文
            return sign;
        }
    }
}
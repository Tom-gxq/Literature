using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SPManager.Common
{
    public class Common
    {
        private static Object singletonLockStream = new Object();

        public static void WriteLog(string str)
        {
            //if ((System.Web.HttpContext.Current == null) || (System.Web.HttpContext.Current.Server == null)
            //    || (ConfigurationManager.AppSettings["crm_logpath"] == null))
            //{
            //    return;
            //}

            string logPath = ConfigurationManager.AppSettings["LogPath"];


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
    }
}
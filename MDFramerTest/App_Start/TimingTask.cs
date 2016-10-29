using MD.Core.DomainModel;
using MD.Services.Message;
using MD.Services.Register;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MD.Web
{
    public class TimingTask:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                HttpGet(@"http://localhost:16717/api/UserLogin/SendMail");
                
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                // this job will refire immediately
                e2.RefireImmediately = true;
                throw e2;
            }
        }

        private string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

    }
}

using Sms.Service.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Sms.Service
{
    public class HttpSender
    {
        private const string sContentType = "application/x-www-form-urlencoded";
        private const string sUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private readonly HttpInput sendInput;

        public HttpSender(HttpInput sendInput)
        {
            this.sendInput = sendInput;
        }
        public HttpOutput Send()
        {
            return Send(Encoding.GetEncoding("UTF-8").GetBytes(this.sendInput.Data), this.sendInput.Url);
        }
        private HttpOutput Send(byte[] data, string url)
        {
            HttpOutput output = new HttpOutput();
            output.Code = 10002;
            Stream responseStream;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
            {
                throw new ApplicationException(string.Format("Invalid url string: {0}", url));
            }
            // request.UserAgent = sUserAgent;  
            request.ContentType = sContentType;
            request.Method = "POST";
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            try
            {
                responseStream = request.GetResponse().GetResponseStream();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            string str = string.Empty;
            using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
            {
                output.message = reader.ReadToEnd();
            }
            responseStream.Close();
            output.Code = 10001;
            return output;
        }
    }
}

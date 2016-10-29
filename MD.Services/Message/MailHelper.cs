using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.Message
{
   
    /// <summary>
    /// 发送邮件类
    /// </summary>
    public class MailHelper
    {
        public delegate int MethodDelegate(int x, int y);
        private readonly int smtpPort = 25;
        readonly string SmtpServer = "smtp.163.com";
        private readonly string UserName = "18624423286@163.com";
        readonly string Pwd = "ztccgrypdsntjvbi";
        private readonly string AuthorName = "HTL";
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Tos { get; set; }
        public bool EnableSsl { get; set; }
        public static MailHelper _Intance = new MailHelper();
        public static MailHelper Intance
        {
            get
            {
                return _Intance;
            }
        }
        private MailHelper()
        {

        }
        MailMessage GetClient
        {
            get
            {
                if (string.IsNullOrEmpty(Tos)) return null;
                MailMessage mailMessage = new MailMessage();
                //多个接收者                
                foreach (string _str in Tos.Split(','))
                {
                    mailMessage.To.Add(_str);
                }
                mailMessage.From = new System.Net.Mail.MailAddress(UserName, AuthorName);
                mailMessage.Subject = Subject;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.Priority = System.Net.Mail.MailPriority.High;
                return mailMessage;
            }
        }
        SmtpClient GetSmtpClient
        {
            get
            {                
                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;//将smtp的出站方式设为 Network     
                smtp.Host = SmtpServer;//指定 smtp 服务器地址 
                smtp.Port = smtpPort;//指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去   
                smtp.Credentials = new NetworkCredential(UserName, Pwd);
                //如果需要认证，则用此的方式   
                //如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了  
                smtp.UseDefaultCredentials = true;
                

                return smtp;
            }
        }
        //回调方法
        Action<string> actionSendCompletedCallback = null;
        ///// <summary>
        ///// 使用异步发送邮件
        ///// </summary>
        ///// <param name="subject">主题</param>
        ///// <param name="body">内容</param>
        ///// <param name="to">接收者,以,分隔多个接收者</param>
        //// <param name="_actinCompletedCallback">邮件发送后的回调方法</param>
        ///// <returns></returns>
        public void Send(string subject, string body, string to)
        {
            if (string.IsNullOrEmpty(to)) return;

            Tos = to;

            SmtpClient smtpClient = GetSmtpClient;
            MailMessage mailMessage = GetClient;
            if (smtpClient == null || mailMessage == null) return;
            Subject = subject;
            Body = body;
            EnableSsl = false;
            //发送邮件回调方法
            //actionSendCompletedCallback = _actinCompletedCallback;
            //smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            try
            {
                smtpClient.Send(mailMessage);//异步发送邮件,如果回调方法中参数不为"true"则表示发送失败
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                smtpClient = null;
                mailMessage = null;
            }
        }
        /// <summary>
        /// 异步操作完成后执行回调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    //同一组件下不需要回调方法,直接在此写入日志即可
        //    //写入日志
        //    //return;
        //    if (actionSendCompletedCallback == null) return;
        //    string message = string.Empty;
        //    if (e.Cancelled)
        //    {
        //        message = "异步操作取消";
        //    }
        //    else if (e.Error != null)
        //    {
        //        message = (string.Format("UserState:{0},Message:{1}", (string)e.UserState, e.Error.ToString()));
        //    }
        //    else
        //        message = (string)e.UserState;
        //    //执行回调方法
        //    actionSendCompletedCallback(message);
        //}
    }

}

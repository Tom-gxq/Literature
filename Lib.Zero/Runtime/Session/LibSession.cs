using Lib.Zero.Redis;
using LibMain.Dependency;
using LibMain.MultiTenancy;
using LibMain.Runtime.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Lib.Zero.Runtime.Session
{
    public class LibSession : ILibSession, ISingletonDependency
    {
        public static readonly LibSession Session = new LibSession();

        private readonly static Hashtable dic = new Hashtable();
        public readonly int SessionTimeOut = 24 * 7;//7天

        private static readonly DataSession session = new DataSession();

        

        /// <summary>
        /// 用户 SessionID
        /// </summary>
        public string SessionID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["dd_sign"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["dd_sign"].Value))
                    return HttpContext.Current.Request.Cookies["dd_sign"].Value;
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["Authorization"]))
                    return HttpContext.Current.Request.Headers["Authorization"].Replace("dd_sign", "");
                return HttpContext.Current.Request.QueryString["NEVER_SEND_MD_PSS_ID_LIKE_THIS_FOR_TESTING_PURPOSE_ONLY"]
                    ?? string.Empty;
            }
            set
            {
                HttpContext.Current.Response.Cookies["dd_sign"].Value = value;
                HttpContext.Current.Response.Cookies["dd_sign"].HttpOnly = true;
                HttpContext.Current.Response.Cookies["session_id"].Value =Guid.NewGuid().ToString();
                if (HttpContext.Current.Request.Url.Host.Contains(ConfigHelper.ServerDomain))
                {
                    //HttpContext.Current.Response.Cookies["dd_sign"].Domain = "."+ ConfigHelper.ServerDomain;  
                    //HttpContext.Current.Response.Cookies["session_id"].Domain = "." + ConfigHelper.ServerDomain;
                }
            }
        }

        /// <summary>
        /// 删除用户 Session
        /// </summary>
        public void Remove(string key)
        {
            lock (dic.SyncRoot)
            {
                string sessionID = SessionID;
                session.DeleteSession(sessionID, key);
                dic.Remove(sessionID + key);
            }
        }

        /// <summary>
        /// 获取用户 Session
        /// </summary>
        public object this[string key]
        {
            get
            {
                string sessionID = SessionID;
                if (string.IsNullOrEmpty(sessionID)) return null;

                object val;
                if (dic.ContainsKey(sessionID + key))
                {
                    val = dic[sessionID + key];
                    return val;
                }

                lock (dic.SyncRoot)
                {
                    dynamic obj = null;
                    if ("RegisterAccountId" == key || "hjk345k333" == key)
                    {
                        obj = session.GetStringSession(sessionID, key);
                    }
                    else
                    {
                        obj = session.GetSession(sessionID, key);
                    }

                    if (obj == null) return null;
                    
                    string htKey = sessionID + key;
                    dic[htKey] = obj;

                    return obj;
                }
            }
            set
            {
                lock (dic.SyncRoot)
                {
                    string sessionID = SessionID;
                    object val = value;
                    
                    sessionID = session.SetSession(sessionID, key, val, string.Empty, DateTime.Now.AddHours(SessionTimeOut));

                    if (HttpContext.Current.Request.Cookies["dd_sign"] != null)
                        HttpContext.Current.Request.Cookies["dd_sign"].Value = sessionID;

                    SessionID = sessionID;

                    dic[SessionID + key] = value;
                }
            }
        }

        /// <summary>
        /// 清理所有 Session
        /// </summary>
        public void Clear()
        {
            lock (dic.SyncRoot)
            {
                string sessionID = SessionID;
                List<string> keys = new List<string>();

                if (dic.Keys.Count > 0)
                {
                    IEnumerator ie = dic.Keys.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        string key = ie.Current.ToString();
                        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(sessionID))
                            key = key.Replace(sessionID, string.Empty);
                        keys.Add(key);
                    }
                }

                dic.Clear();
                session.DeleteAllSession(sessionID);
                SessionID = string.Empty;
                if (HttpContext.Current.Request.Cookies["dd_sign"] != null)
                    HttpContext.Current.Request.Cookies["dd_sign"].Value = string.Empty;
            }
        }
    }
}

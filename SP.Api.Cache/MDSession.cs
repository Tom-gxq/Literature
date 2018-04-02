using SP.Api.Model.Account;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SP.Api.Cache
{
    /// <summary>
    ///MDSession 的摘要说明
    /// </summary>
    public class MDSession
    {
        private MDSession()
        { }
        public static readonly MDSession Session = new MDSession();

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
                if (HttpContext.Current.Request.Cookies["md_pss_id"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["md_pss_id"].Value))
                    return HttpContext.Current.Request.Cookies["md_pss_id"].Value;
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["Authorization"]))
                    return HttpContext.Current.Request.Headers["Authorization"].Replace("md_pss_id ", "");
                return HttpContext.Current.Request.QueryString["NEVER_SEND_MD_PSS_ID_LIKE_THIS_FOR_TESTING_PURPOSE_ONLY"]
                    ?? string.Empty;
            }
            set
            {
                HttpContext.Current.Response.Cookies["md_pss_id"].Value = value;
                if (HttpContext.Current.Request.Url.Host.Contains("mingdao.com"))
                    HttpContext.Current.Response.Cookies["md_pss_id"].Domain = ".mingdao.com";
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
                    if ("RegisterAccountId" == key || "phoneKey" == key)
                    {
                        obj = session.GetStringSession(sessionID, key);
                    }
                    else
                    {
                        obj = session.GetSession(sessionID, key);
                    }

                    if (obj == null) return null;
                    if ("Account" == key)
                    {
                        var accountId = obj["AccountId"].ToString();

                        if (!string.IsNullOrEmpty(accountId))
                        {
                            obj = AccountInfoCache.GetAccountInfoByAccountId(accountId);
                        }
                    }
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
                    AccountModel account = null;
                    if ("Account" == key)
                    {
                        account = (AccountModel)value;
                        val = new { account.AccountId };
                        // JsonConvert.SerializeObject(new { account.AccountId });
                    }
                    sessionID = session.SetSession(sessionID, key, val, account?.AccountId, DateTime.Now.AddHours(SessionTimeOut));

                    if (HttpContext.Current.Request.Cookies["md_pss_id"] != null)
                        HttpContext.Current.Request.Cookies["md_pss_id"].Value = sessionID;

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
                if (HttpContext.Current.Request.Cookies["md_pss_id"] != null)
                    HttpContext.Current.Request.Cookies["md_pss_id"].Value = string.Empty;
            }
        }

    }
}

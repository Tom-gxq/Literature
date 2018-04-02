using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Cache
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class ConfigInfo
    {
        #region 属性

        public CryptKey CryptKey { get; set; }
        public static ConfigInfo ConfigInfoData
        {
            get
            {
                if (configInfo == null)
                {
                    lock (singletonLock)
                    {
                        if (configInfo == null)
                        {
                            configInfo = new ConfigInfo
                            {
                                CryptKey = new CryptKey()
                            };
                        }
                    }
                }

                return configInfo;
            }
        }
        #endregion

        private static object singletonLock = new object();

        private static ConfigInfo configInfo = null;

    }
    //加密key
    public class CryptKey
    {
        public string MessageKey = "jiamijiemimafano";

        public string DocViewKey = "jiamijiemimingdaodocview";
    }
}

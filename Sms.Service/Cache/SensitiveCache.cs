using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sms.Service.Cache
{
    internal class SensitiveCache
    {
        private const string CacheKey = "sensitive_sms";
        private const string SdmSensitiveChannel = "sdm_sensitive";
        //private static MingdaoSDM.MingdaoSDMClient sdmClient;
        //private static MingdaoSDM.MingdaoSDMClient SdmClient => sdmClient ?? (sdmClient = SDM.SdmClient.GetClient(ConfigHelper.GetConfigJson("SdmServiceUrl")));

        static SensitiveCache()
        {
            //RedisHelper.Subscribe(SdmSensitiveChannel, ClearCache);
        }

        private static List<string> GetSensitiveByType(SensitiveType type)
        {
            // 判断类型是否存在
            var sensitiveType = SensitiveTypeCache.SensitiveTypes.Find(item => item.Id == (int)type);
            if (sensitiveType == null) return new List<string>();

            // 缓存中已存在
            //var cacheData = MemoryCaching.MemoryCacheManager.Instance.Get<Dictionary<string, List<string>>>(CacheKey);
            //if (cacheData != null && cacheData.ContainsKey(sensitiveType.Code))
            //{
            //    return cacheData[sensitiveType.Code];
            //}

            try
            {
                //var request = new SensitivesRequest();
                //request.TypeCodes.AddRange(SensitiveTypeCache.SensitiveTypes.Select(item => item.Code));
                //var response = SdmClient.GetSensitiveByCodes(request);

                //if (response.Success)
                //{
                //    var dic = new Dictionary<string, List<string>>();
                //    var data = response.Data;
                //    foreach (var item in data)
                //    {
                //        dic.Add(item.Code, item.List.Select(w => w.ToLower()).ToList());
                //    }
                //    if (dic.Count > 0)
                //    {
                //        MemoryCaching.MemoryCacheManager.Instance.Set(CacheKey, dic);
                //    }

                //    if (dic.ContainsKey(sensitiveType.Code))
                //    {
                //        return dic[sensitiveType.Code];
                //    }
                //}
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<string>();
        }

        /// <summary>
        /// 敏感词判断
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ContainSensitive(string keywords, SensitiveType type)
        {
            if (string.IsNullOrEmpty(keywords)) return false;

            var list = GetSensitiveByType(type);
            if (list.Count > 0)
            {
                return list.Any(keywords.ToLower().Contains);
            }
            return false;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        public static void ClearCache()
        {
            //MemoryCaching.MemoryCacheManager.Instance.Remove(CacheKey);
        }
    }
}

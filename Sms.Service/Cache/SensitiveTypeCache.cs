using Sms.Service.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sms.Service.Cache
{
    internal class SensitiveTypeCache
    {
        private static readonly Lazy<SensitiveTypeCache> Lazy = new Lazy<SensitiveTypeCache>(() => new SensitiveTypeCache());

        private readonly List<SensitiveType> sensitiveTypes;

        public static List<SensitiveType> SensitiveTypes => Lazy.Value.sensitiveTypes;

        private SensitiveTypeCache()
        {
            sensitiveTypes = GetSensitiveTypes();
        }

        public List<SensitiveType> GetSensitiveTypes()
        {
            var data = new List<SensitiveType>();

            var xmlPath = Directory.GetCurrentDirectory() + "/Config/sensitiveType.xml";

            if (!File.Exists(xmlPath)) return data;

            var xmlDoc = XDocument.Load(xmlPath);

            var root = xmlDoc.Element("SensitiveTypes");
            if (root == null) return data;

            var xElements = root.Elements("SensitiveType").ToList();
            if (!xElements.Any()) return data;

            //所有供应商
            foreach (var xElement in xElements)
            {
                if (!xElement.Attributes().Any()) continue;

                var idAttr = xElement.Attribute("id");
                var sensitiveType = new SensitiveType
                {
                    Id = idAttr != null ? Convert.ToInt32(idAttr.Value) : 0,
                    Description = xElement.Attribute("desc")?.Value,
                    Code = xElement.Attribute("code")?.Value
                };

                data.Add(sensitiveType);
            }

            return data;
        }
    }
}

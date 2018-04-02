using Sms.Service.Entities;
using Sms.Service.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sms.Service.Cache
{
    internal class ProviderCache
    {
        private static readonly Lazy<ProviderCache> Lazy = new Lazy<ProviderCache>(() => new ProviderCache());

        private readonly List<Provider> providers;

        public static List<Provider> Providers => Lazy.Value.providers;

        private ProviderCache()
        {
            providers = GetProviders();
        }

        public List<Provider> GetProviders()
        {
            var data = new List<Provider>();

            var xmlPath = Directory.GetCurrentDirectory() + "/Config/providers.xml";

            if (!File.Exists(xmlPath)) return data;

            var xmlDoc = XDocument.Load(xmlPath);

            var root = xmlDoc.Element("Providers");
            if (root == null) return data;

            var xElements = root.Elements("Provider").ToList();
            if (!xElements.Any()) return data;

            //所有供应商
            foreach (var xElement in xElements)
            {
                if (!xElement.Attributes().Any()) continue;

                var provider = new Provider
                {
                    Name = xElement.Attribute("name")?.Value,
                    Weight = Convert.ToInt32(xElement.Attribute("weight")?.Value ?? "0")
                };

                //短信
                var mobileNode = xElement.Element("Mobile");
                if (mobileNode != null)
                {
                    provider.Mobile = GetSupportRange(mobileNode);
                }

                //语音
                var voiceNode = xElement.Element("Voice");
                if (voiceNode != null)
                {
                    provider.Voice = GetSupportRange(voiceNode);
                }

                data.Add(provider);
            }

            return data;
        }

        private SupportRange GetSupportRange(XElement node)
        {
            var supportRange = new SupportRange();
            var chinaNode = node.Element("China");
            if (chinaNode != null)
            {
                supportRange.China = GetSupportInfo(chinaNode);
            }
            var internationalNode = node.Element("International");
            if (internationalNode != null)
            {
                supportRange.International = GetSupportInfo(internationalNode);
            }

            return supportRange;
        }

        private SupportInfo GetSupportInfo(XElement node)
        {
            var supportInfo = new SupportInfo();
            if (node.Attributes().Any())
            {
                var textAttribute = node.Attribute("text");
                if (textAttribute != null)
                {
                    supportInfo.IsSupportText = Convert.ToBoolean(textAttribute.Value);
                }

                var templateAttribute = node.Attribute("template");
                if (templateAttribute != null)
                {
                    supportInfo.IsSupportTemplate = Convert.ToBoolean(templateAttribute.Value);
                    if (supportInfo.IsSupportTemplate)
                    {
                        supportInfo.Templates = new List<Template>();
                        var templateNodes = node.Elements("Template").ToList();
                        if (templateNodes.Any())
                        {
                            foreach (var templateNode in templateNodes)
                            {
                                if (!templateNode.Attributes().Any()) continue;

                                var template = new Template
                                {
                                    Type = (TemplateType)Convert.ToInt32(templateNode.Attribute("type")?.Value ?? "0"),
                                    Id = templateNode.Attribute("id")?.Value,
                                    Vars = new List<string>((templateNode.Attribute("vars")?.Value ?? string.Empty).Split('|'))
                                };
                                supportInfo.Templates.Add(template);
                            }
                        }
                    }
                }
            }
            return supportInfo;
        }
    }
}

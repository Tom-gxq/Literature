using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiGateway.Models.Version
{
    [DataContract]
    public class VersionModel
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string versionCode { get; set; }
        [DataMember]
        public string updateMessage { get; set; }
    }
}
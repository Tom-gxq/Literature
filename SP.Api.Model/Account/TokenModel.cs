using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    [Serializable]
    public class TokenModel
    {
        public int Id { get; set; }
        public string Access_Token { get; set; }
        public string AccountId { get; set; }
        public string Access_Token_Expires { get; set; }
        public string Refresh_Token { get; set; }
        public string Refresh_Token_Expires { get; set; }
        public bool Success { get; set; }
        public int UserType { get; set; }
    }
}

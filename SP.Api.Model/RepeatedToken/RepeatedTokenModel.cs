using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.RepeatedToken
{
    [Serializable]
    public class RepeatedTokenModel
    {
        public int Id { get; set; }
        public string Access_Token { get; set; }
        public string AccountId { get; set; }
        public int Status { get; set; }
    }
}

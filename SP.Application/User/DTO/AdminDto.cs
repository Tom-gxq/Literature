using LibMain.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User.DTO
{
    public class AdminDto : EntityDto<long>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }
    }
}

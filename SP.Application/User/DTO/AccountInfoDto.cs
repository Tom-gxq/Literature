﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User.DTO
{
    public class AccountInfoDto
    {
        public string AccountId { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public int UserType { get; set; }
    }
}

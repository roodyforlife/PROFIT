﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
    }
}

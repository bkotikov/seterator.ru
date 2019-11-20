﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rauthor.Models.Api
{
    public class ProfileRequest
    {
        public enum ProfileRequestType { concrete, list }
        public ProfileRequestType Type { get; set; }
        public Guid? Guid { get; set; }
    }

}

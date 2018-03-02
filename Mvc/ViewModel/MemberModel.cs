﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.ViewModel
{
    public class MemberModel
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public bool IsActive { get; set; }
    }
}
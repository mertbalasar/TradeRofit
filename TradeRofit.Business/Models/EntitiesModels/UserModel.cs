﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRofit.Business.Models.EntitiesModels
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool IsPremium { get; set; }
    }
}

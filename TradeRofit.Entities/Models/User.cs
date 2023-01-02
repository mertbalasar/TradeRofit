using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeRofit.Entities.Attributes;
using TradeRofit.Entities.Base;

namespace TradeRofit.Entities.Models
{
    [CollectionName("user")]
    public class User : TRCollectionBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get => FirstName + " " + LastName; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool IsPremium { get; set; } = false;
    }
}

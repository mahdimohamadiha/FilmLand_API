using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsStatus { get; set; }
        public bool UserIsDelete { get; set; }
        public Guid SubscriptionRef { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}

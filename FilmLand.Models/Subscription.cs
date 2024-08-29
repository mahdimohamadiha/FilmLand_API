using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }
        public string SubscriptionTitle { get; set; }
        public int SubscriptionDiscount { get; set; }
        public int SubscriptionPrice { get; set; }
        public bool SubscriptionIsStatus { get; set; }
        public bool SubscriptionIsDelete { get; set; }
        public int SubscriptionOrder { get; set; }
        public int SubscriptionMonthNumber { get; set; }

    }
}

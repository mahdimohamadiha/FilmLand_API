using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class UserAndSubscriptionDTO
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }

    }
}

using FilmLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetAllSubscription();
        string UpdateUser(Guid userId, Guid subscriptionId);
        (SubscriptionSummary, string) GetSubscription(Guid idUser);
        IEnumerable<User> GetSubscriptionUser(Guid idUser);
    }
}

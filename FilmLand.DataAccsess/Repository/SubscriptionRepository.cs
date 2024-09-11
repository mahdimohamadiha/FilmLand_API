using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ICustomLogger _customLogger;
        public SubscriptionRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public IEnumerable<Subscription> GetAllSubscription()
        {
            (IEnumerable<Subscription> subscriptions, string message) = DapperEntities.QueryDatabase<Subscription>(@"SELECT * FROM [Subscription] WHERE SubscriptionIsStatus = 1 AND SubscriptionIsDelete = 0  ORDER BY SubscriptionOrder", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return subscriptions;
        }

        public string UpdateUser(Guid userId, Guid subscriptionId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE [User] SET SubscriptionRef = @SubscriptionRef, SubscriptionDate = GETDATE() WHERE UserId = @UserId",
                Connection.FilmLand(), new { UserId = userId, SubscriptionRef = subscriptionId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }

        public (SubscriptionSummary, string) GetSubscription(Guid idUser)
        {
            (IEnumerable<SubscriptionSummary> subscriptionSummaries, string message) = DapperEntities.QueryDatabase<SubscriptionSummary>("SELECT [SubscriptionDate], [SubscriptionMonthNumber] FROM [User] JOIN Subscription ON SubscriptionRef = SubscriptionId WHERE UserId = @UserId", Connection.FilmLand(), new { UserId = idUser });
            if (message == "Success")
            {
                if (subscriptionSummaries.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (subscriptionSummaries.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
        public Guid GetSubscriptionUser(Guid idUser)
        {
            (IEnumerable<Guid> subscriptions, string message) = DapperEntities.QueryDatabase<Guid>(@"SELECT SubscriptionRef FROM ""User"" WHERE UserId = '71D80E15-7A9C-45AB-9B1F-BE17F5F403FE'", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return subscriptions.FirstOrDefault();
        }
    }
}

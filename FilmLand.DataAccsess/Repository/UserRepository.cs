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
    public class UserRepository : IUserRepository
    {
        private readonly ICustomLogger _customLogger;
        public UserRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public (Guid, string) Register(RegisterDTO registerDTO)
        {
            Guid userId = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase(@"
                INSERT INTO [User] (UserId, Username, UserEmail, UserPassword, UserCreateDate, UserIsStatus, UserIsDelete) VALUES (@UserId, @Username, @UserEmail, @UserPassword, GETDATE(), 1, 0);",
                Connection.FilmLand(), new { UserId = userId, Username = registerDTO.Username, UserEmail = registerDTO.UserEmail, UserPassword = registerDTO.UserPassword });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return (userId, message);
        }

        public (IEnumerable<User>, string) Login(LoginDTO loginDTO)
        {
            (IEnumerable<User> users, string message) = DapperEntities.QueryDatabase<User>(@"
                SELECT [UserId], [Username], [UserEmail], [UserPassword], [UserIsStatus], [UserIsDelete] ,[SubscriptionRef] FROM [User] WHERE Username = @Username AND UserPassword = @UserPassword;",
                Connection.FilmLand(), new { Username = loginDTO.Username, UserPassword = loginDTO.UserPassword });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
                return (users, "Success");
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }

        public (User, string) GetUser(Guid idUser)
        {
            (IEnumerable<User> users, string message) = DapperEntities.QueryDatabase<User>("SELECT * FROM [User] WHERE UserId = @UserId", Connection.FilmLand(), new { UserId = idUser });
            if (message == "Success")
            {
                if (users.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (users.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
    }
}

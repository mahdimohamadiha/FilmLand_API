using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IUserRepository
    {
        (Guid, string) Register(RegisterDTO registerDTO);
        (IEnumerable<User>, string) Login(LoginDTO loginDTO);
        (User, string) GetUser(Guid idUser);
    }
}

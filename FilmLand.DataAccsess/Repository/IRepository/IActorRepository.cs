using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IActorRepository
    {
        string AddActor(ActorAndUploadFileDTO actorAndUploadFileDTO);
        IEnumerable<ActorSummary> GetAllActorSummary();
        public (Actor, string) GetActor(Guid actorId);
    }
}

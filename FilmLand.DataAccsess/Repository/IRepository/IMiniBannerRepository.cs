using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IMiniBannerRepository
    {
        string AddMiniBanner(MiniBannerAndFileDTO miniBannerAndFileDTO);
        IEnumerable<MiniBannerAndFile> GetAllMiniBanner();
        string UpdateMiniBanner(Guid miniBannerId, MiniBannerAndFileDTO miniBannerAndFileDTO);
        (MiniBannerAndFile, string) GetMiniBanner(Guid miniBannerId);
        string RemoveMiniBanner(Guid miniBannerId);
        string ChangeStatus(Guid miniBannerId);
    }
}

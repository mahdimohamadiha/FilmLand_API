using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface ISliderRepository
    {
        string AddSlider(SliderAndFileDTO sliderAndFileNameDTO);

        IEnumerable<SlidersAndFiles> GetAllSlider();

        string UpdateSlider(Guid sliderId, SliderAndFileDTO sliderAndFileDTO);

        (SlidersAndFiles, string) GetSlider(Guid sliderId);

        string RemoveSlider(Guid sliderId);

        string ChangeStatus(Guid sliderId);
    }
}

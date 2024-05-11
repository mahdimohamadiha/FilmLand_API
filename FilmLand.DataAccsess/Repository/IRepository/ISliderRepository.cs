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
        string AddSlider(SliderAndFileNameDTO sliderAndFileNameDTO);
    }
}

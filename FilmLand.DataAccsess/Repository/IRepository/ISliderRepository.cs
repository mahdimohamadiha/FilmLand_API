﻿using FilmLand.Models;
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

        IEnumerable<SliderAndFilePath> GetAllSlider();

        string UpdateSlider(Guid sliderId, SliderAndFileDTO sliderAndFileDTO);
    }
}

﻿using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IMovieFileRepository
    {
        string AddMovieFile(MovieFileDTO movieFileDTO);
        IEnumerable<MovieFile> GetAllMovieFile(Guid movieId);
        string RemoveMovieFile(Guid movieFileId);
        (MovieFileSummary, string) GetMovieFile(Guid movieFileId);
        string UpdateMovieFile(Guid movieFileId, MovieFileDTO movieFileDTO);
    }
}

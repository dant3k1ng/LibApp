using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;

namespace LibApp.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}

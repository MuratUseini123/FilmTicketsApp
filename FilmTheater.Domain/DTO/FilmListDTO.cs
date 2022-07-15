using FilmTheater.Domain.DomainModels;
using FilmTheater.Domain.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.DTO
{
    public class FilmListDTO
    {
        public List<FilmTicket> films { get; set; }
        public FilmTheaterUser loggedInUser { get; set; }
    }
}

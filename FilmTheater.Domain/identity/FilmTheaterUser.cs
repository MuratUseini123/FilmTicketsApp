using FilmTheater.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.identity
{
    public class FilmTheaterUser:IdentityUser
    {
        public FilmTheaterUser()
        {
            this.isAdmin = false;
          /*  this.Name = "";
            this.Surname = "";
            this.Address = "";*/
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public bool isAdmin  { get; set; }

        public virtual TicketCart UserTicketCart { get; set; }
    }
}

using FilmTheater.Domain.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.DomainModels
{
    public class TicketOrder: BaseEntity
    {
        

        public string FilmTheaterUserId { get; set; }

        public FilmTheaterUser OrderedBy { get; set; }

        public List<TicketInOrder> FilmTicketsInOrder { get; set; }


    }
}

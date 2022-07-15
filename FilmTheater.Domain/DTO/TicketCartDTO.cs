using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FilmTheater.Domain.DomainModels;

namespace FilmTheater.Domain.DTO
{
    public class TicketCartDTO
    {
        public List<TicketInTicketCart> TicketsInTickets { get; set; }

        public float TotalPrice { get; set; }

    }
}

using FilmTheater.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.DTO
{
    public class AddToTicketCartDTO
    {
        public FilmTicket SelectedFilmTicket { get; set; }
        public int FilmTicketId { get; set; }

        [Display(Name = "Ticket Quantity")]
        public int TicketQuantity { get; set; }
    }
}

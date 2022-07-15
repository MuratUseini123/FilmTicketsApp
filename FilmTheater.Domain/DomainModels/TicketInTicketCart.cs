using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.DomainModels
{
    public class TicketInTicketCart: BaseEntity
    {
        public int FilmTicketId { get; set; }

        public int TicketCartId { get; set; }


        [ForeignKey("FilmTicketId")]
        public FilmTicket FilmTicket { get; set; }

        [ForeignKey("TicketCartId")]
        public TicketCart TicketCart { get; set; }

        public int TicketQuantity { get; set; }
    }
}

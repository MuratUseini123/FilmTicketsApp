using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace FilmTheater.Domain.DomainModels
{
    public class TicketInOrder: BaseEntity
    {

        [ForeignKey("FilmTicketId")]
        public int FilmTicketId { get; set; }
        public FilmTicket FilmTicket { get; set; }


        [ForeignKey("TicketOrderId")]
        public int TicketOrderId { get; set; }
        public TicketOrder TicketOrder { get; set; }

        public int Quantity { get; set; }
    }
}

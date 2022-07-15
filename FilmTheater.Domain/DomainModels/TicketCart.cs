using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.DomainModels
{
    public class TicketCart: BaseEntity
    {
      

        public string TicketOwnerId { get; set; }

        public virtual ICollection<TicketInTicketCart> TicketInTicketCarts { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmTheater.Domain.DomainModels
{
    public class FilmTicket: BaseEntity
    {

        public FilmTicket()
        {
            this.ValidUntilDate = DateTime.Now;
        }

        

        [Required]
        [Display(Name ="Film Name")]
        public string FilmName { get; set; }

        [Display(Name ="Film Poster Url")]
        public string FilmPosterImage { get; set; }

        [Display(Name = "Film Description")]
        public string FilmDescription { get; set; }

        [Display(Name ="Genre")]
        public string FilmGenre { get; set; }

        [Display(Name = "Ticket Price")]
        public float FilmTicketPrice { get; set; }

        [Display(Name = "Rating")]
        public int FilmRating { get; set; }


        [Display(Name = "Valid until date")]
        public DateTime ValidUntilDate { get; set; }

        public virtual ICollection<TicketInTicketCart> TicketInTicketCarts { get; set; }
                                                    
    }
}

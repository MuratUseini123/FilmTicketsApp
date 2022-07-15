using FilmTheater.Domain.DomainModels;
using FilmTheater.Domain.DTO;
using FilmTheater.Repository.Interface;
using FilmTheater.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmTheater.Service.Implementation
{
    public class FilmTicketService : IFilmTicketService
    {
        public readonly IRepository<FilmTicket> _filmTicketRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRepository<TicketInTicketCart> _ticketsInTicketCartRepository;

      

        public FilmTicketService(IRepository<FilmTicket> filmTicketRepository, IUserRepository userRepository, IRepository<TicketInTicketCart> ticketsInTicketCartRepository)
        {
            _filmTicketRepository = filmTicketRepository;
            _userRepository = userRepository;
            _ticketsInTicketCartRepository = ticketsInTicketCartRepository;

        }

        public bool AddToTicketCart(AddToTicketCartDTO item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserTicketCart;

            if (userShoppingCard != null)
            {
                var product = this.GetDetailsForFilmTicket(item.FilmTicketId);

                if (product != null)
                {
                    TicketInTicketCart itemToAdd = new TicketInTicketCart
                    {
                       
                        FilmTicket = product,
                        FilmTicketId = product.id,
                        TicketCart = userShoppingCard,
                        TicketCartId = userShoppingCard.id,
                        TicketQuantity = item.TicketQuantity
                    };

                    this._ticketsInTicketCartRepository.Insert(itemToAdd);
                   
                    return true;
                }
                return false;
            }
            
            return false;

        }

        public void CreateNewFilmTicket(FilmTicket newFilmTicket)
        {
            this._filmTicketRepository.Insert(newFilmTicket);
        }

        public void DeleteFilmTicket(int id)
        {
            var filmticketTobeDeleted = _filmTicketRepository.Get(id);
            this._filmTicketRepository.Delete(filmticketTobeDeleted);
        }

        public List<FilmTicket> GetAllFilmTickets()
        {
            return _filmTicketRepository.GetAll().ToList();
        }

        public FilmTicket GetDetailsForFilmTicket(int id)
        {
            return _filmTicketRepository.Get(id);
        }

        public TicketCartDTO GetTicketCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateExistingFilmTicket(FilmTicket newFilmTicket)
        {
            _filmTicketRepository.Update(newFilmTicket);
        }
    }
}

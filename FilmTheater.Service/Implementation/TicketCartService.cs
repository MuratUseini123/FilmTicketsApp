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
    public class TicketCartService : ITicketCartService
    {
        //private readonly IRepository<FilmTicket> _filmTicketRepository;

        private readonly IUserRepository _userRepository;

        private readonly IRepository<TicketInOrder> _filmTicketInOrderRepository;

        private readonly IRepository<TicketCart> _ticketCartRepository;
        private readonly IRepository<EmailMessage> _emailMessageRepository;

        

        private readonly IRepository<TicketOrder> _orderRepository;

        public TicketCartService(IUserRepository userRepository, IRepository<TicketInOrder> filmTicketInOrderRepository, IRepository<TicketCart> ticketCartRepository, IRepository<TicketOrder> orderRepository, IRepository<EmailMessage> emailMessageRepository)
        {
            _userRepository = userRepository;
            _filmTicketInOrderRepository = filmTicketInOrderRepository;
            _ticketCartRepository = ticketCartRepository;
            _orderRepository = orderRepository;
            _emailMessageRepository = emailMessageRepository;

        }

        public bool deleteTicketFromTicketCart(string userId, int ticketId)
        {
            if (!string.IsNullOrEmpty(userId) && ticketId != null)
            {
                

                var loggedInUser = this._userRepository.Get(userId);

                var userTicketCart = loggedInUser.UserTicketCart;

                var itemToDelete = userTicketCart.TicketInTicketCarts.Where(z =>z.FilmTicketId.Equals(ticketId)).FirstOrDefault();

                userTicketCart.TicketInTicketCarts.Remove(itemToDelete);

                this._ticketCartRepository.Update(userTicketCart);

                return true;
            }

            return false;

        }

        public TicketCartDTO getTicketCartInfo(string userId)
        {
           
            var user = _userRepository.Get(userId);
            var userTicketCart = user.UserTicketCart;

            var ticketList = userTicketCart.TicketInTicketCarts.Select(obj => new
            {
                Quantity = obj.TicketQuantity,
                TicketPrice = obj.FilmTicket.FilmTicketPrice
            });

            float totalPrice = 0;

            foreach (var ticket in ticketList)
            {
                totalPrice += ticket.TicketPrice * ticket.Quantity;
            }

            TicketCartDTO model = new TicketCartDTO
            {
                TotalPrice = totalPrice,
                TicketsInTickets = userTicketCart.TicketInTicketCarts.ToList()
            };


            return model;
        }

        public bool orderNow(string userId)
        {
            
            var user = _userRepository.Get(userId);
            var userTicketCart = user.UserTicketCart;



            EmailMessage mail = new EmailMessage();
            mail.MailTo = user.Email;
            mail.Subject = "Successfully created order";
            mail.Status = false;




            TicketOrder currentticketOrder = new TicketOrder
            {
                FilmTheaterUserId = user.Id,
                OrderedBy = user
            };

            _orderRepository.Insert(currentticketOrder);
         



            List<TicketInOrder> ticketInOrderList = userTicketCart.TicketInTicketCarts.Select(obj => new TicketInOrder
            {
                FilmTicket = obj.FilmTicket,
                FilmTicketId = obj.FilmTicketId,
                TicketOrder = currentticketOrder,
                TicketOrderId = currentticketOrder.id,
                Quantity = obj.TicketQuantity
            }).ToList();

            StringBuilder sb = new StringBuilder();

            float totalPrice = 0;

            sb.AppendLine("Your order is completed. The order conains: ");

            for (int i = 1; i <= ticketInOrderList.Count(); i++)
            {
                var item = ticketInOrderList[i - 1];

                totalPrice += item.Quantity * item.FilmTicket.FilmTicketPrice;

                sb.AppendLine(i.ToString() + ". " + item.FilmTicket.FilmName + " with price of: " + item.FilmTicket.FilmTicketPrice + " and quantity of: " + item.Quantity);
            }

            sb.AppendLine("Total price: " + totalPrice.ToString());


            mail.Content = sb.ToString();


            foreach (var i in ticketInOrderList)
            {
                _filmTicketInOrderRepository.Insert(i);
            }

            user.UserTicketCart.TicketInTicketCarts.Clear();
            _userRepository.Update(user);
            _emailMessageRepository.Insert(mail);

            return true;
        }
    }
}

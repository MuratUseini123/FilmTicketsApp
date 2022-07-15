using FilmTheater.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Service.Interface
{
    public interface ITicketCartService
    {
        TicketCartDTO getTicketCartInfo(string userId);

        bool deleteTicketFromTicketCart(string userId, int ticketId);

        bool orderNow(string userId);

    }
}

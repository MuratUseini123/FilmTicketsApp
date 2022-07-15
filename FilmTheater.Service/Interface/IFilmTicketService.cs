using FilmTheater.Domain.DomainModels;
using FilmTheater.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Service.Interface
{
    public interface IFilmTicketService
    {
        List<FilmTicket> GetAllFilmTickets();

        FilmTicket GetDetailsForFilmTicket(int id);

        void CreateNewFilmTicket(FilmTicket newFilmTicket);

        void UpdateExistingFilmTicket(FilmTicket newFilmTicket);

        TicketCartDTO GetTicketCartInfo(int id);

        void DeleteFilmTicket(int id);

        bool AddToTicketCart(AddToTicketCartDTO item, string userID);
    }
}

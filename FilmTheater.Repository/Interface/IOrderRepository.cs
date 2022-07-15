using FilmTheater.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Repository.Interface
{
    public interface IOrderRepository
    {
        List<TicketOrder> getAllOrders();

        TicketOrder getOrderDetails(BaseEntity model);

    }
}

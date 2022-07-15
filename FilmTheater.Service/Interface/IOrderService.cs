using FilmTheater.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Service.Interface
{
    public interface IOrderService
    {
        List<TicketOrder> getAllOrders();

        TicketOrder getOrderDetails(BaseEntity model);
    }
}

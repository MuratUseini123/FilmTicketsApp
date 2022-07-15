using FilmTheater.Domain.DomainModels;
using FilmTheater.Repository.Interface;
using FilmTheater.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Service.Implementation
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public List<TicketOrder> getAllOrders()
        {
            return _orderRepo.getAllOrders();
        }

        public TicketOrder getOrderDetails(BaseEntity model)
        {
            return _orderRepo.getOrderDetails(model);
        }
    }
}

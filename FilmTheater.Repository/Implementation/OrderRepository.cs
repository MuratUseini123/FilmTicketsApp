using FilmTheater.Domain.DomainModels;
using FilmTheater.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmTheater.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext _context;
        private DbSet<TicketOrder> entities;
        string errorMSG = string.Empty;

        public OrderRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            entities = _context.Set<TicketOrder>();
        }


        public List<TicketOrder> getAllOrders()
        {
            return entities.Include(z => z.OrderedBy).Include(z => z.FilmTicketsInOrder).Include("FilmTicketsInOrder.FilmTicket").ToList();
        }

        public TicketOrder getOrderDetails(BaseEntity model)
        {
            return entities.Include(z => z.OrderedBy).Include(z => z.FilmTicketsInOrder).Include("FilmTicketsInOrder.FilmTicket").SingleOrDefaultAsync(z=>z.id==model.id).Result;
        }
    }
}

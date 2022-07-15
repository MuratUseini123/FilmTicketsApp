using FilmTheater.Domain.DomainModels;

using FilmTheater.Domain.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Repository
{
    public class ApplicationDbContext : IdentityDbContext<FilmTheaterUser>
    {
        public virtual DbSet<FilmTicket> FilmTickets { get; set; }
        public virtual DbSet<TicketCart> TicketCarts { get; set; }
        public virtual DbSet<TicketInTicketCart> TicketInTicketCarts { get; set; }
        public virtual DbSet<FilmTheaterUser> FilmTheaterUsers { get; set; }
        public virtual DbSet<TicketOrder> TicketOrders { get; set; }
        public virtual DbSet<TicketInOrder> TicketInOrders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TicketInTicketCart>().HasKey(ticketObj => new { ticketObj.TicketCartId, ticketObj.FilmTicketId });
            builder.Entity<TicketInOrder>().HasKey(ticketObj => new { ticketObj.TicketOrderId, ticketObj.FilmTicketId });
        }
    }
}

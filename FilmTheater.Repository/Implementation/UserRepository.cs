using FilmTheater.Domain.identity;
using FilmTheater.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmTheater.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<FilmTheaterUser> entities;
        string errorMSG = string.Empty;

        public UserRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            entities = _context.Set<FilmTheaterUser>();
        }


        public void Delete(FilmTheaterUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            _context.SaveChanges();


        }

        public FilmTheaterUser Get(string id)
        {
            var user  = entities.Include("UserTicketCart.TicketInTicketCarts").
                Include("UserTicketCart.TicketInTicketCarts.FilmTicket").SingleOrDefault(x => x.Id == id);

            if(user == null)
            {
                throw new ArgumentNullException("user");
            }

            return user;
        }

        public IEnumerable<FilmTheaterUser> GetAll()
        {
            return entities.AsEnumerable();

        }

        public void Insert(FilmTheaterUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(FilmTheaterUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            _context.SaveChanges();

        }
    }
}

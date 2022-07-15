using FilmTheater.Domain.DomainModels;
using FilmTheater.Repository;
using FilmTheater.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmTheater.Service.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        string errorMSG = string.Empty;

        public Repository(ApplicationDbContext _context)
        {
            this._context = _context;
            entities = _context.Set<T>();
        }

        public void Delete(T Entity)
        {
            if (Entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(Entity);
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return entities.SingleOrDefault(obj => obj.id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T Entity)
        {
            if (Entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(Entity);
            _context.SaveChanges();
        }

        public void Update(T Entity)
        {
            if (Entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(Entity);
            _context.SaveChanges();
        }
    }
}

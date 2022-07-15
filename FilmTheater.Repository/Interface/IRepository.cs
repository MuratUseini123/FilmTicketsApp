using FilmTheater.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Insert(T Entity);

        void Update(T Entity);

        void Delete(T Entity);
    }
}

using FilmTheater.Domain.identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmTheater.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<FilmTheaterUser> GetAll();

        FilmTheaterUser Get(string id);

        void Insert(FilmTheaterUser entity);

        void Update(FilmTheaterUser entity);

        void Delete(FilmTheaterUser entity);

    }
}

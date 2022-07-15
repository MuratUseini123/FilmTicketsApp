using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmTheater.Service.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}

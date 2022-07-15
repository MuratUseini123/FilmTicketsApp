using FilmTheater.Domain.DomainModels;
using FilmTheater.Domain.identity;
using FilmTheater.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film_Theater_191546.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IOrderService orderService;
        public readonly IFilmTicketService filmTicketService;
        private UserManager<FilmTheaterUser> userManager;
        


        public AdminController(IOrderService orderService, IFilmTicketService filmTicketService, UserManager<FilmTheaterUser> _userManager)
        {
            this.orderService = orderService;
            this.filmTicketService = filmTicketService;
            this.userManager = _userManager;
        }


        [HttpGet("[action]")]
        public List<TicketOrder> GetAllActiveOrders()
        {
            return orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public TicketOrder GetOrderDetails(BaseEntity model)
        {
            return orderService.getOrderDetails(model);
        }

        [HttpGet("[action]")]
        public List<FilmTicket> GetAllFilmTickets()
        {
            return filmTicketService.GetAllFilmTickets();
        }

        [HttpPost("[action]")]
        public List<FilmTheaterUser> ImportAllUsers()
        {
            return userManager.Users.ToList();
        }

    }
}

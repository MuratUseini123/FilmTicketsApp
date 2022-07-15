
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FilmTheater.Domain.DomainModels;
using FilmTheater.Service.Interface;

namespace Film_Theater_191546.Controllers
{
    public class TicketCartController : Controller
    {
        private readonly ITicketCartService _ticketCartService;

        public TicketCartController(ITicketCartService ticketCartService)
        {
            _ticketCartService = ticketCartService;
        }

        public IActionResult Index()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = _ticketCartService.getTicketCartInfo(userID);
            return View(model);
        }

        public IActionResult DeleteFromTicketCart(int id)
        {

            _ticketCartService.deleteTicketFromTicketCart(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
            return RedirectToAction("Index");
        }

        public IActionResult OrderNow()
        {

            _ticketCartService.orderNow(User.FindFirstValue(ClaimTypes.NameIdentifier));
            

            return RedirectToAction(nameof(Index));
        }
    }
}

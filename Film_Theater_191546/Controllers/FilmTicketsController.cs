using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using FilmTheater.Domain.DomainModels;
using System.Security.Claims;
using FilmTheater.Service.Interface;
using FilmTheater.Domain.DTO;
using FilmTheater.Domain.identity;
using FilmTheater.Repository.Interface;

namespace Film_Theater_191546.Controllers
{
    public class FilmTicketsController : Controller
    {
        private readonly IFilmTicketService filmTicketService;
        private readonly IUserRepository userRepo;


        public FilmTicketsController(IFilmTicketService filmTicketService, IUserRepository userRepo)
        {
            this.filmTicketService = filmTicketService;
            this.userRepo = userRepo;

        }

        // GET: FilmTickets
        public IActionResult Index()
        {
            FilmListDTO listOfFilms = new FilmListDTO();

            listOfFilms.films = filmTicketService.GetAllFilmTickets();
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userRepo.Get(userID);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            listOfFilms.loggedInUser = user;
            return View(listOfFilms);
        }
        public async Task<IActionResult> AddToCart(int filmticketId)
        {
            var film = filmTicketService.GetDetailsForFilmTicket(filmticketId);
            var model = new AddToTicketCartDTO();
            model.SelectedFilmTicket = film;
            model.FilmTicketId = filmticketId;
            model.TicketQuantity = 0;

            return View(model);
        }


        public async Task<IActionResult> AddToTicketCart(AddToTicketCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.filmTicketService.AddToTicketCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "FilmTickets");
            }
            return RedirectToAction("Index");
        }


        // GET: FilmTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmTicket = filmTicketService.GetDetailsForFilmTicket(id??0);
            if (filmTicket == null)
            {
                return NotFound();
            }

            return View(filmTicket);
        }

        // GET: FilmTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilmTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmTicket filmTicket)
        {
            if (ModelState.IsValid)
            {
                filmTicketService.CreateNewFilmTicket(filmTicket);
                return RedirectToAction(nameof(Index));
            }
            return View(filmTicket);
        }

        // GET: FilmTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmTicket = filmTicketService.GetDetailsForFilmTicket(id??0);
            if (filmTicket == null)
            {
                return NotFound();
            }
            return View(filmTicket);
        }

        // POST: FilmTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FilmTicket filmTicket)
        {
            if (id != filmTicket.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    filmTicketService.UpdateExistingFilmTicket(filmTicket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmTicketExists(filmTicket.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(filmTicket);
        }

       

        // GET: FilmTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmTicket = filmTicketService.GetDetailsForFilmTicket(id ?? 0);
            if (filmTicket == null)
            {
                return NotFound();
            }

            return View(filmTicket);
        }

        // POST: FilmTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            filmTicketService.DeleteFilmTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FilmTicketExists(int id)
        {
            return filmTicketService.GetDetailsForFilmTicket(id) != null;
        }
    }
}

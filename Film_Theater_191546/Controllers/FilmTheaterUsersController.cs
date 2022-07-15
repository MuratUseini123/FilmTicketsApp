
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmTheater.Domain.DomainModels;
using FilmTheater.Repository.Interface;
using FilmTheater.Domain.identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Film_Theater_191546.Controllers
{
    public class FilmTheaterUsersController : Controller
    {
        private readonly IUserRepository userRepo;

        public FilmTheaterUsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public IActionResult ShowUsers()
        {
            return View(userRepo.GetAll());
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user =  userRepo.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        


        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Surname,Address,UserName,isAdmin")] FilmTheaterUser updatedUser)
        {

            var user =  userRepo.Get(id);

            user.isAdmin = updatedUser.isAdmin;
            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Address = updatedUser.Address;
            user.UserName = updatedUser.UserName;

            if (ModelState.IsValid)
            {
                try
                {
                    userRepo.Update(user);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(ShowUsers));
            }
            return View(user);
        }
    }
}

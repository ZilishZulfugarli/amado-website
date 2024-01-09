using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;

namespace MiniLayihe.Controllers
{
	public class ChatController : Controller
	{
        private readonly AppDbContext _dbContext;

        public ChatController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
		{
			var user = _dbContext.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
			var model = new ChatIndexVM()
			{
				User = user as AppUser,
                IsAdmin = User.IsInRole("Admin")
            };
			return View(model);
		}
	}
}


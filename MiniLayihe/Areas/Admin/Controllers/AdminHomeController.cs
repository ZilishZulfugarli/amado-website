
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLayihe.Areas.Admin.Models;

namespace MiniLayihe.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Policy = "Admin")]
//[Authorize]
public class AdminHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Models;

namespace MiniLayihe.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(AppDbContext dbContext, ILogger<HomeController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var product = _dbContext.Products.AsTracking().Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.Color).Include(x => x.Brand).ToList();

        var model = new ViewModel()
        {
            Products = product
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


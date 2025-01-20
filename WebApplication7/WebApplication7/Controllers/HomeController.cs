using System.Diagnostics;
using System.Net.Mime;
using WebApplication7.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using EventModel = WebApplication7.Migrations.EventModel;

namespace WebApplication7.Controllers;

public class HomeController : Controller
{
    private readonly Application _context;

    public HomeController(Application context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEvent(WebApplication7.Models.EventModel @event)
    {
        if (ModelState.IsValid)
        {
            _context.EventModel.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View("Create", @event);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;

namespace InmobiliariaGutierrez.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {   if(TempData.ContainsKey("dato"))
           ViewBag.dato=TempData["dato"];
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

     public IActionResult MiAction()
    {  TempData["dato"]="ALgo , algo";
       return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

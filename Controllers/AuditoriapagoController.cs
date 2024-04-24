using Microsoft.AspNetCore.Authorization;  // para usar la anotación  [Authorize]
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;
using InmobiliariaGutierrez.Views.ContratoView;
using InmobiliariaGutierrez.Views.ContratoView;


namespace InmobiliariaGutierrez.Controllers{

[Authorize]
public class AuditoriapController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AuditoriapController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

   
   public IActionResult Index()
    {   

		return View();
    } 
    public IActionResult Auditoria()
    {   

		return Json("carajo");
    } 

}
}

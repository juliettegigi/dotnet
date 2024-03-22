using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;

namespace InmobiliariaGutierrez.Controllers;

public class InmuebleController : Controller
{
	private readonly RepositorioPropietario rp ;
	private readonly ILogger<InmuebleController> _logger;

	public InmuebleController(ILogger<InmuebleController> logger)
	{
		_logger = logger;
        rp = new RepositorioPropietario();
	}

	public IActionResult Index()
	{
		
		return View();
	}
        public IActionResult CargarInmueble(int Id, string Direccion, string Uso, string Tipo, int Cantidad_habitacion, string latitud, string longitud, float Precio)
{  
    try
    {
       
        Inmueble inmueble = new Inmueble();
        inmueble.PropietarioId = new Propietario(); 
        inmueble.PropietarioId.Id=Id;
        inmueble.Direccion=Direccion;
        inmueble.Tipo=Uso;
        inmueble.CantidadAmbientes=Cantidad_habitacion;
        decimal latitudDecimal;
        decimal longitudDecimal;
         if (decimal.TryParse(latitud, out latitudDecimal) && decimal.TryParse(longitud, out longitudDecimal))
        {
            Coordenada coordenadas = new Coordenada(latitudDecimal, longitudDecimal);
             inmueble.Coordenadas=coordenadas;
        }
         
        inmueble.Precio=34532;
        if(Tipo=="Comercial")
        {inmueble.Uso=TipoUso.Comercial;}
        else
        { inmueble.Uso=TipoUso.Residencial;}
         
         RepositorioInmueble repo = new RepositorioInmueble(); 
        repo.AltaInmueble(inmueble);
         ViewBag.CurrentUrl = Request.Path;

         return View("~/Views/Home/Index.cshtml");
        //return RedirectToAction("Index"); 
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al cargar el inmueble: " + ex.Message);
        return RedirectToAction("Error"); // Redirigir a una acción de error
    }
}
	

    
   
	
}

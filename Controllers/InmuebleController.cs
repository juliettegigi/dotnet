using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;
using System.Globalization;

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
public IActionResult VerInmueble(Propietario propietario)
{
    RepositorioInmueble ri = new RepositorioInmueble();
    IList<Inmueble> inmuebles = ri.GetInmueblesPorIdPropietario(propietario.Id);

     return RedirectToAction(nameof(Index));
}
  	public IActionResult NuevoInmueble(Propietario propietario)
	{
		 ModalViewModel viewModel = new ModalViewModel();
         viewModel.MostrarModal = true;
				 viewModel.Apellido = propietario.Nombre+" "+propietario.Apellido;
				 viewModel.Documento = propietario.DNI;
				 viewModel.Id = propietario.Id;
				 
			      return View("~/Views/Propietario/Crear.cshtml", viewModel);
	
	}
      
   public IActionResult CargarInmueble(int Id, string Name,string Dni,string Direccion, string Uso, string Tipo, int Cantidad_habitacion, string latitud, string longitud, string Precio,string alquilar,string nextinmuebles)
   
   {  
    try
    {
       
        Inmueble inmueble = new Inmueble();
        inmueble.PropietarioId = new Propietario(); 
        inmueble.PropietarioId.Id=Id;
        inmueble.Direccion=Direccion;
        
        inmueble.InmuebleTipoId = new InmuebleTipo( );
        inmueble.InmuebleTipoId.Tipo=Tipo;
        inmueble.CantidadAmbientes=Cantidad_habitacion;
        decimal latitudDecimal;
        decimal longitudDecimal;
        inmueble.PrecioBase=decimal.Parse(Precio);
      
         
        CultureInfo cultura = CultureInfo.InvariantCulture;


if (decimal.TryParse(latitud, NumberStyles.Float, cultura, out latitudDecimal) &&
    decimal.TryParse(longitud, NumberStyles.Float, cultura, out longitudDecimal))
{
    
    Coordenada coordenadas = new Coordenada(latitudDecimal, longitudDecimal);
    inmueble.Coordenadas = coordenadas;
}
         if(alquilar=="1"){
            inmueble.Suspendido=false;
         }
         else{
           inmueble.Suspendido=true;
         }
       
        if(Uso=="1")
        {inmueble.Uso=TipoUso.Comercial;}
        else
        { inmueble.Uso=TipoUso.Residencial;}
          
         RepositorioInmueble repo = new RepositorioInmueble(); 
         Console.WriteLine(Uso,"generacion");
        repo.AltaInmueble(inmueble);
         ViewBag.CurrentUrl = Request.Path;
         
         if(nextinmuebles=="1"){
          ModalViewModel viewModel=new ModalViewModel();
          viewModel.MostrarModal = true;
				 viewModel.Apellido = Name;
				 viewModel.Documento = Dni;
				 viewModel.Id = Id;
				 
			      return View("~/Views/Propietario/Crear.cshtml", viewModel);
          

         }
         else{
            return View("~/Views/Home/Index.cshtml");

         }
      
       
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al cargar el inmueble: " + ex.Message);
        return RedirectToAction("Error"); // Redirigir a una acción de error
    }
}
	
    
   
	
}

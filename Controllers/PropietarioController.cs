using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;

namespace InmobiliariaGutierrez.Controllers;

public class PropietarioController : Controller
{
	private readonly RepositorioPropietario rp ;
	private readonly ILogger<InmuebleController> _logger;

	public PropietarioController(ILogger<InmuebleController> logger)
	{
		
        rp = new RepositorioPropietario();
	}

	public IActionResult Index()
	{
		ModalViewModel viewModel = new ModalViewModel();
        viewModel.MostrarModal = false;
		var lista = rp.GetPropietarios();
		return View(lista);
	}

	
	public IActionResult Editar(int id)
    {  
            ModalViewModel viewModel = new ModalViewModel();
            viewModel.MostrarModal = false;
		    if(id > 0){  
				viewModel.MostrarModal = false;
				var propietario = rp.GetPropietario(id);
				return View(propietario);
		    } 
			else {
				return View();
		     }
	   
	
	
	}
	public IActionResult Crear(int id)
    {  
	 ModalViewModel viewModel = new ModalViewModel();
        viewModel.MostrarModal = false;
    if (id > 0)
    {
       
        viewModel.MostrarModal = true;

        

        // Creamos un nuevo objeto que contenga tanto el propietario como el viewModel
      

        return View(viewModel);
    }
    else
    {
        return View(viewModel);
    }
}

	[HttpPost]
	public IActionResult Guardar(Propietario propietario)
	{
		RepositorioPropietario rp = new RepositorioPropietario();
		if(propietario.Id > 0)
			{
				rp.ModificaPropietario(propietario);
                return RedirectToAction(nameof(Index));
			}
		else
			{ 
				var Id=rp.AltaPropietario(propietario);
				 ModalViewModel viewModel = new ModalViewModel();
                 viewModel.MostrarModal = true;
				 viewModel.Apellido = propietario.Nombre+" "+propietario.Apellido;
				 viewModel.Documento = propietario.DNI;
				 viewModel.Id = Id;
				 
			      return View("~/Views/Propietario/Crear.cshtml", viewModel);
			}
		
	}
    
   
	public IActionResult Eliminar(int id)
	{
		RepositorioPropietario rp = new RepositorioPropietario();
		rp.EliminaPropietario(id);
		return RedirectToAction(nameof(Index));
	}


		public ActionResult GetOpciones(string input)
           {
              
           	var rp=new RepositorioPropietario();
               var opciones = rp.BuscarPorTodosLosCampos(input); 
           	
               return Json(opciones);
           }
}

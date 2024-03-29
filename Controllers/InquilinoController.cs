using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;

namespace InmobiliariaGutierrez.Controllers;

public class InquilinoController : Controller
{
	private readonly ILogger<InquilinoController> _logger;

	public InquilinoController(ILogger<InquilinoController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		RepositorioInquilino rp = new RepositorioInquilino();
		var lista = rp.GetInquilinos();
		return View(lista);
	}

	
	public IActionResult Editar(int id)
	{   
           if(id > 0){
			   RepositorioInquilino rp = new();
			   var inquilino = rp.GetInquilino(id);
			   return View(inquilino);
		    } 
			else return View();
		
	}
	[HttpPost]
	public IActionResult Guardar(Inquilino inquilino)
	{  if (ModelState.IsValid){
		RepositorioInquilino rp = new RepositorioInquilino();
		
		if(inquilino.Id > 0)
			rp.ModificaInquilino(inquilino);
		else
			rp.AltaInquilino(inquilino);
		return RedirectToAction(nameof(Index));
		}
		else return RedirectToAction(nameof(Editar));;
	}

	public IActionResult Eliminar(int id)
	{
		RepositorioInquilino rp = new RepositorioInquilino();
		rp.EliminaInquilino(id);
		return RedirectToAction(nameof(Index));
	}

	public IActionResult ContratarAlquiler(int id)
	{
		var ri=new RepositorioInmueble();
		
		return View(ri.GetInmueblesPaginado(10,1));
	}
}

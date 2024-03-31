using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Views.Inquilino;
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

	public IActionResult ContratarAlquiler(int page,int limit=5)
	{    
		int offset=(page-1)*limit;
		var ri=new RepositorioInmueble();
		var rit=new RepositorioInmuebleTipo();
		IList<InmuebleTipo>listaTipos =rit.GetInmuebleTipos();
		IList<Inmueble>lista =ri.GetInmueblesPaginado(limit,offset);

		int totalReg=ri.getCantidadRegistros();
		int cantidadPaginas=totalReg/limit;
		cantidadPaginas=totalReg%limit!=0?++cantidadPaginas:cantidadPaginas;
		
		var objetoView=new ContratarAlquilerView{Lista=lista};
		objetoView.PrimerNumero=page%5!=0?(page/5)*5+1:((page-1)/5)*5+1;
		objetoView.Page=page;
		objetoView.CantidadPaginas=cantidadPaginas;
		objetoView.ListaTipos=listaTipos;
		
		
		
		return View(objetoView);
	}
	
}


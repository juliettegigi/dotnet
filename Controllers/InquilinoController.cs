using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Views.InquilinoView;
using InmobiliariaGutierrez.Models.DAO;
namespace InmobiliariaGutierrez.Controllers{

[Authorize]
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
		
		if(inquilino.Id !=null)
			rp.ModificaInquilino(inquilino);
		else
			rp.AltaInquilino(inquilino);
		return RedirectToAction(nameof(Index));
		}
		else return RedirectToAction(nameof(Editar));
	}

[Authorize(Policy ="Administrador")]
	public IActionResult Eliminar(int id)
	{
		RepositorioInquilino rp = new RepositorioInquilino();
		rp.EliminaInquilino(id);
		return RedirectToAction(nameof(Index));
	}

	
	[HttpGet]
	public IActionResult ContratarAlquiler(int page,int filtrar,int idInquilino,ViewInquilinoFiltrarInmueble? filtro,int limit=5)
	{   
		
         int offset=(page-1)*limit;
		var ri=new RepositorioInmueble();
		var rit=new RepositorioInmuebleTipo();
		IList<InmuebleTipo>listaTipos =rit.GetInmuebleTipos();
		IList<Inmueble>lista;
		lista=ri.GetInmueblesPaginadoFiltrado(5,offset,filtro);
		
		if(filtrar==1){
			     ViewBag.Filtros=filtro ;			
			     ViewBag.Filtrar=1 ;
			}
		

		
		
		int totalReg=ri.getCantidadRegistrosFiltrado(filtro);
		int cantidadPaginas=totalReg/limit;
		cantidadPaginas=totalReg%limit!=0?++cantidadPaginas:cantidadPaginas;
		
		var objetoView=new ContratarAlquilerView{Lista=lista};
		objetoView.PrimerNumero=page%5!=0?(page/5)*5+1:((page-1)/5)*5+1;
		objetoView.Page=page;
		objetoView.CantidadPaginas=cantidadPaginas;
		objetoView.ListaTipos=listaTipos;
		objetoView.IdInquilino=idInquilino;
		ViewBag.obj=objetoView;
		
		
		return View(filtro);
	}

	public ActionResult GetOpciones(string input)
{
   
	var ri=new RepositorioInquilino();
    var opciones = ri.BuscarPorTodosLosCampos(input); 
	
    return Json(opciones);
}

}

}
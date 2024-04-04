using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.DAO;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Views.ContratoView;

namespace InmobiliariaGutierrez.Controllers;

public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;

    public ContratoController(ILogger<ContratoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int page,int limit=2)
    {   int offset=(page-1)*limit;
		var rc=new RepositorioContrato();
        IList<Contrato>lista =rc.GetContratosPaginado(limit,offset);
		int totalReg=rc.getCantidadRegistros();
		int cantidadPaginas=totalReg/limit;
		cantidadPaginas=totalReg%limit!=0?++cantidadPaginas:cantidadPaginas;
		
		var objetoView=new IndexView{ListaContratos=lista};
		objetoView.PrimerNumero=page%limit!=0?(page/limit)*limit+1:((page-1)/limit)*limit+1;
		objetoView.Page=page;
		objetoView.CantidadPaginas=cantidadPaginas;
		
		
		
		return View(objetoView);
    }


    public IActionResult Eliminar(int id)
	{   
		RepositorioContrato rp = new RepositorioContrato();
		rp.EliminarContrato(id);
		return RedirectToAction(nameof(Index),new { page = 1});
	}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


[HttpGet]
[HttpPost]
    public IActionResult FormContrato(Contrato? contrato,int idInq=0,int idInm=0){
            var rc=new RepositorioContrato();

            
            if(idInq!=0){

            ViewBag.idInq=idInq;
            ViewBag.idInm=idInm;
            }
            
          if(contrato.InquilinoId!=null){
            rc.AltaContrato(contrato);
           } 
            return View();
		
	}
}

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

  public IActionResult Index(int page,int limit=5)
    {   
        int offset=(page-1)*limit;
		var rc=new RepositorioContrato();
        IList<Contrato>lista =rc.GetContratosPaginado(limit,offset);
		int totalReg=rc.getCantidadRegistros();
		int cantidadPaginas=totalReg/limit;
		cantidadPaginas=totalReg%limit!=0?++cantidadPaginas:cantidadPaginas;
		
		var objetoView=new IndexView{ListaContratos=lista};
		objetoView.PrimerNumero=page%limit==0?page-(limit-1):((page/limit)*limit+1);
		objetoView.Page=page;
		objetoView.CantidadPaginas=cantidadPaginas;
		
		return View(objetoView);
    }


    public IActionResult Eliminar(int id)
	{   
      
        DateTime FechaFinparametro=DateTime.Now;;// tiene wue venir porparametro
		RepositorioContrato rc= new RepositorioContrato();
        RepositorioPago rp=new RepositorioPago();
        
        IList<Pago> pago ;
         pago=rp.GetPago(id);
        Contrato contrato=rc.GetContrato(id);
         DateTime fechaActual = DateTime.Now;
           /*
         int mesestotaldeuda=0;
            int diferenciaMeses = (FechaFinparametro.Year-contrato.FechaInicio.Year  ) * 12 + FechaFinparametro.Month-contrato.FechaInicio.Month ;
            int  mitad=(contrato.FechaFin.Year-contrato.FechaInicio.Year  ) * 12 + contrato.FechaFin.Month-contrato.FechaInicio.Month ;
            mitad=mitad/2 ;
            int deudas=0;
            int cantidadpagos=0;
            decimal multa;
           for(int i = 0; i<pago.Count; i++){
            if(pago[i].FechaPago>DateTime.MinValue)
            {
                cantidadpagos++;

            }else{
                deudas++;
            }
       
           }
           mesestotaldeuda=diferenciaMeses-cantidadpagos;

            if(deudas>0){

            
            if (diferenciaMeses>=mitad){
                mesestotaldeuda=mesestotaldeuda+2;
            }
            else{
                mesestotaldeuda++;
            }
            multa=mesestotaldeuda*pago[0].Importe;
           }


          Console.WriteLine(cantidadpagos);
          Console.WriteLine(deudas);
   

        return Json(pago);
        */
        
		rc.EliminarContrato(id);
		return RedirectToAction(nameof(Index),new { page = 1});
        
	}

[HttpGet]
    public IActionResult Editar(int id=0)
	{   
       
		RepositorioContrato rc = new RepositorioContrato();
        RepositorioInquilino ri=new RepositorioInquilino();
        RepositorioInmuebleTipo rit=new RepositorioInmuebleTipo();
        ViewBag.ListaTipos=rit.GetInmuebleTipos();
        if(id!=0){
         Contrato c=rc.GetContrato(id);
         return View(c);
        }

		//ri.BuscarPorTodosLosCampos();
		return View();
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
    

[HttpPost]
	public IActionResult Guardar(Contrato contrato)
	{  
        Console.WriteLine("*****************************************************contrato");
        Console.WriteLine(contrato);
       
		RepositorioContrato rc = new RepositorioContrato();
		RepositorioPago rp = new RepositorioPago();
		
		if(contrato.Id > 0)
			rc.ModificaContrato(contrato);
		else{
			rc.AltaContrato(contrato);
            var pago=new Pago{
                NumeroPago=1,
                ContratoId=contrato.Id,
                Fecha=contrato.FechaInicio,
                FechaPago=contrato.FechaInicio
            };
            rp.InsertPago(pago);
        }
		return RedirectToAction(nameof(Index),new{page=1});
	
	}


    public IActionResult GetListaInmueblePag(int page,int limit=5,ViewInquilinoFiltrarInmueble filtro=null)
    {   

        
        int offset=(page-1)*limit;
		var ri=new RepositorioInmueble();
        IList<Inmueble>lista =ri.GetInmueblesPaginadoFiltrado(limit,offset,filtro);
		int totalReg=ri.getCantidadRegistrosFiltrado(filtro);
		int cantidadPaginas=totalReg/limit;
		cantidadPaginas=totalReg%limit!=0?++cantidadPaginas:cantidadPaginas;
		
		var objetoView=new IndexView{ListaInmuebles=lista};
		objetoView.PrimerNumero=page%limit==0?page-4:((page/limit)*limit+1);
		objetoView.Page=page;
		objetoView.CantidadPaginas=cantidadPaginas;

		return Json(objetoView);
        
    }
   
   public IActionResult Finalizar(int Id){
    RepositorioContrato rc=new RepositorioContrato();
    
    Contrato contrato=rc.GetContrato(Id);
    return View(contrato);
   }
 public IActionResult Calcular([FromBody] List<Arreglo> arreglo)
    {
            RepositorioContrato rc=new RepositorioContrato();
            RepositorioPago rp=new RepositorioPago();
            IList<Pago> pagos;
            Contrato contrato = new Contrato();
            
            string[] partesId = arreglo[0].Id.Split('/');
            int Id = Convert.ToInt32(partesId[0]);
            DateTime FechaFinparametro = DateTime.Parse(partesId[1]);
            contrato=rc.GetContrato(Id);
            pagos=rp.GetPago(Id);
            
          

            
         int mesestotaldeuda=0;
         /*entre fechaanticipada y contrato*/   int diferenciaMeses = (FechaFinparametro.Year-contrato.FechaInicio.Year  ) * 12 + FechaFinparametro.Month-contrato.FechaInicio.Month ;
     Console.WriteLine( "diferenciaMeses");
      Console.WriteLine( diferenciaMeses);
      Console.WriteLine( "diferenciaMeses");
             int  mitad=(contrato.FechaFin.Year-contrato.    FechaInicio.Year  ) * 12 + contrato.FechaFin.Month-contrato.FechaInicio.Month ;
            
            /*mitadl total*/ mitad=mitad/2 ;

       /*aca sacar calculo de multa*/     int fechamulta = (contrato.FechaFin.Year-FechaFinparametro.Year  ) * 12 + contrato.FechaFin.Month-FechaFinparametro.Month ;
           
            int deudas=0;
            int cantidadpagos=0;
            decimal multa=0;
           for(int i = 0; i<pagos.Count; i++){
            if(pagos[i].FechaPago>DateTime.MinValue)
            {
                cantidadpagos++;

            }else{
                deudas++;
            }
       
           }
               Console.WriteLine( "cantidadpagos");
      Console.WriteLine(cantidadpagos);
      Console.WriteLine( "cantidadpagos");
           /*tengo el total de la fecha hacia la anticipads me falta la multa*/
            mesestotaldeuda=diferenciaMeses-cantidadpagos;
                Console.WriteLine( "mesestotaldeuda");
      Console.WriteLine(mesestotaldeuda);
      Console.WriteLine( "mesestotaldeuda");
       Console.WriteLine( "fechamulta");
      Console.WriteLine(fechamulta);
      Console.WriteLine( "fechamulta");
      int mesesadeudado=mesestotaldeuda;
      int mesesmulta=0;
           

            
            if (fechamulta>mitad){
                mesestotaldeuda=mesestotaldeuda+2;
                mesesmulta=mesesmulta+2;
            }
            else{
                mesestotaldeuda++;
                mesesmulta=mesesmulta+1;
            }
            multa=mesestotaldeuda*pagos[0].Importe;
           


       var resultado = new
    {
        Mensaje = "Tiene un total de meses adeudado: " + mesesadeudado + ", y una multa de " + mesesmulta+" meses de alquiler", 
        total = multa
    };

   
        return Json(resultado);
    }
}

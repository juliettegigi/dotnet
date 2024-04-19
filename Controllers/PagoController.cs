using Microsoft.AspNetCore.Authorization;  // para usar la anotación  [Authorize]
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;
using InmobiliariaGutierrez.Views.ContratoView;
using InmobiliariaGutierrez.Views.ContratoView;


namespace InmobiliariaGutierrez.Controllers;

public class PagoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public PagoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

   
   public IActionResult Index()
    {   
		RepositorioContrato rc=new RepositorioContrato();
    DateTime da=DateTime.MinValue;
        IList<Contrato> contratos=rc.GetContratosTodos();
		return View(contratos);
    } 
      public IActionResult Pagar( string nombre, string apellido,int ContratoId,int NumeroPago, DateTime fecha )
    {   
         DateTime nuevaFecha = new DateTime(fecha.Year, fecha.Day + 1 , fecha.Month );
         Console.WriteLine( "ContratoId");
        Console.WriteLine( ContratoId);
        
        RepositorioContrato rc=new RepositorioContrato();
        RepositorioPago rp=new RepositorioPago();
        IList <Pago> pagos=new List<Pago>();
        pagos=rp.GetPago(ContratoId);
        // aca me falta contar cuantas pagos hay para agregarq
        Contrato con=new Contrato();
        con=rc.GetContrato(ContratoId);
         DateTime fechaActual = DateTime.Now;
         Pago pago = new Pago();
         pago.ContratoId = ContratoId;
        // pago.NumeroPago=NumeroPago;
         pago.Importe=con.PrecioXmes;
         decimal numeroDecimal;
        
        
         pago.FechaPago= DateTime.Now;
         Console.WriteLine(nuevaFecha);
           Console.WriteLine( con.FechaFin);
           
    
         

         if (nuevaFecha>con.FechaFin)
        {
             TempData["dato"]="Tiene que renovar Contrato O tiene que desalojarlo";
             decimal importe=rp.UpdatePago(pago);
       return RedirectToAction(nameof(Index));
   
       }
       else{
        
        decimal importe=rp.UpdatePago(pago);
        Pago pagose=new Pago();
          pagose.ContratoId = ContratoId;
         pagose.NumeroPago=NumeroPago+1;
          fecha=fecha.AddMonths(1);
         pagose.Fecha=nuevaFecha;
         pagose.FechaPago= null;
         pagose.Importe=importe;   
        
        rp.InsertPago(pago);
         
         

  
           return RedirectToAction(nameof(Index));  
       
       }
       

 }





   
    public IActionResult Privacy()
    {
        return View();
    }



    public IActionResult Prepago(int Id, int idcontrato, string nombre, string apellido, string dni)
    {    
 RepositorioPago rp = new RepositorioPago();
    IList<Pago> pagos = rp.GetPago(idcontrato);
      RepositorioContrato rc=new RepositorioContrato();
        Contrato con=new Contrato();

        con=rc.GetContrato(idcontrato);
         Console.WriteLine("pagos");
        Console.WriteLine(pagos);
        Console.WriteLine("pagos");
      
       
        if(pagos.Count()<1){
          try{

             Pago pago= new Pago();
           pago.ContratoId=idcontrato;
           pago.NumeroPago=1;
           pago.Fecha=con.FechaInicio;
           pago.FechaPago=null;
           pago.Importe=con.PrecioXmes;
        
           rp.InsertPago(pago); 
           }
           catch (Exception e){
              return Json("ca");
           }

        }
        pagos = rp.GetPago(idcontrato);

    ModelAuxiliar modelo = new ModelAuxiliar();
    modelo.FinContrato=con.FechaFin;
    modelo.Nombre = nombre;
    modelo.Apellido = apellido;
    modelo.Dni = dni;
     modelo.Pagos =new List<Pago>(); 
     modelo.Pagos=pagos;
       DateTime nuevaFecha = DateTime.Now;
    
   //return Json(pagos);
   if (nuevaFecha>con.FechaFin)
        {
             TempData["dato"]="Tiene que renovar Contrato O tiene que desalojarlo";
             
       
   
       }
    return View("Pagar", modelo);
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

    public ActionResult Restringido()
		{
			return View();
		}
}

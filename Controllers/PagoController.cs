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

    [Authorize(Policy ="Administrador")]
     public IActionResult Eliminar(int ContratoId,int NumeroPago)
    {   
		  RepositorioPago rp=new RepositorioPago();
      Pago pago=rp.GetPago(ContratoId,NumeroPago);
      pago.FechaPago=null;
      rp.UpdatePago(pago);
        string idUsuario = HttpContext.User.FindFirst("id")?.Value;
            
            if (string.IsNullOrEmpty(idUsuario))
            {
                
                return RedirectToAction("Error");
            }
              else{

                string idUsuarioStr=idUsuario ;
                int idsuario;
                if (int.TryParse(idUsuarioStr, out idsuario))
                {
                    RepositorioAuditoriapago rap=new RepositorioAuditoriapago();
                    Usuario usuario=new Usuario();
                    Auditoriapago auditoriap=new Auditoriapago();
                    auditoriap.UsuarioId=new Usuario();
                    auditoriap.UsuarioId.Id=idsuario;
                    auditoriap.FechaPago=null;
                    auditoriap.FechaCancelacion=DateTime.Now;
                     auditoriap.ContratoId=new Contrato();
                    auditoriap.ContratoId.Id=ContratoId;
                    auditoriap.NumeroPago=NumeroPago;
                    rap.InsertAuditoriapago(auditoriap);

                    
                }

               }

     return RedirectToAction(nameof(Index));
    } 
      public IActionResult Pagar( string nombre, string apellido,int ContratoId,int NumeroPago, DateTime fecha )
    {   
         DateTime nuevaFecha = new DateTime(fecha.Year, fecha.Day + 1 , fecha.Month );
        
        RepositorioContrato rc=new RepositorioContrato();
        RepositorioPago rp=new RepositorioPago();
        IList <Pago> pagos=new List<Pago>();
        pagos=rp.GetPago(ContratoId);
        Pago pago1=rp.GetPago(ContratoId,NumeroPago);
        pago1.FechaPago=DateTime.Now;
          
        Contrato con=new Contrato();
        con=rc.GetContrato(ContratoId);
        
        pago1.FechaPago=DateTime.Now;
        DateTime fechacondicion=DateTime.Now;
        rp.UpdatePago(pago1);
        //---------------------------------------------------------------------------------------
             string idUsuario = HttpContext.User.FindFirst("id")?.Value;
            
            if (string.IsNullOrEmpty(idUsuario))
            {
                
                return RedirectToAction("Error");
            }
              else{

                string idUsuarioStr=idUsuario ;
                int idsuario;
                if (int.TryParse(idUsuarioStr, out idsuario))
                {
                    RepositorioAuditoriapago rap=new RepositorioAuditoriapago();
                    Usuario usuario=new Usuario();
                    Auditoriapago auditoriap=new Auditoriapago();
                    auditoriap.UsuarioId=new Usuario();
                    auditoriap.UsuarioId.Id=idsuario;
                    auditoriap.FechaPago=DateTime.Now;
                    auditoriap.FechaCancelacion=null;
                     auditoriap.ContratoId=new Contrato();
                    auditoriap.ContratoId.Id=ContratoId;
                    auditoriap.NumeroPago=NumeroPago;
                    rap.InsertAuditoriapago(auditoriap);

                    
                }

               }

        //-----------------------------------------------------------------------------------------------------------

        if(pagos.Count()==pago1.NumeroPago)
        { try{

          if(pago1.Fecha.AddMonths(1)<=con.FechaFin){
          Pago pago=pago1;
          pago.Fecha=pago.Fecha.AddMonths(1);
          pago.FechaPago=null;  
          pago.NumeroPago=pago.NumeroPago+1;
         
          rp.InsertPago(pago);
          return RedirectToAction(nameof(Index));
          }else{
             Pago pago=pago1;
         
          pago.FechaPago=DateTime.Now;
         
         
          rp.UpdatePago(pago);

              TempData["dato"]="Tiene que renovar Contrato O tiene que desalojarlo";
              return RedirectToAction(nameof(Index));

          }
        } catch (Exception e)
        {
            // Manejar la excepción adecuadamente
            // Aquí podrías registrar la excepción, mostrar un mensaje al usuario, etc.
            return Json(new { error = e.Message });
        }
        }

         return RedirectToAction(nameof(Index));
        
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
     
       
        if(pagos.Count()==0){
        

             Pago pago= new Pago();
           pago.ContratoId=idcontrato;
           pago.NumeroPago=1;
           pago.Fecha=con.FechaInicio;
           pago.FechaPago=null;
           pago.Importe=con.PrecioXmes;
        
           rp.InsertPago(pago); 
           
           
      
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
}
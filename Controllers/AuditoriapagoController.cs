using Microsoft.AspNetCore.Authorization;  // para usar la anotación  [Authorize]
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;



namespace InmobiliariaGutierrez.Controllers{

[Authorize]
public class AuditoriapController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AuditoriapController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

   
   public IActionResult Index()
    {   

		return View();
    } 

    /*
    [Authorize(Policy ="Administrador")]
    public IActionResult Auditoriaa(int ContratoId,int NumeroPago)
    {   
      RepositorioContrato con=new RepositorioContrato();
      Contrato contrato;
       contrato=con.GetContrato(ContratoId);
       AuditoriaViewModelPago Avm=new AuditoriaViewModelPago();
       Avm.Contrato = contrato;
        string idUsuario = HttpContext.User.FindFirst("id")?.Value;
        RepositorioAuditoriapago rap=new RepositorioAuditoriapago();
       
        IList<Auditoriapago>auditoria=new List<Auditoriapago>();
        auditoria=rap.GetPorPago(NumeroPago);
        Avm.Auditoriapago=auditoria;
            
            if (string.IsNullOrEmpty(idUsuario))
            {
                
                return RedirectToAction("Error");
            }
              else{

                string idUsuarioStr=idUsuario ;
                int idsuario;
                if (int.TryParse(idUsuarioStr, out idsuario))
                {
                    
                 Usuario usuario=new Usuario();
                 RepositorioUsuario ru=new RepositorioUsuario();
                 usuario=ru.ObtenerPorId(idsuario);
                 Avm.Usuario=usuario;
                    
                }

               }




		     return View(Avm);
    } 
    */
    [Authorize(Policy ="Administrador")]
    public IActionResult Auditoria(int ContratoId,int NumeroPago)
    {   
      RepositorioContrato con=new RepositorioContrato();
      Contrato contrato;
       contrato=con.GetContrato(ContratoId);
       AuditoriaViewModelPago Avm=new AuditoriaViewModelPago();
       Avm.Contrato = contrato;
        string idUsuario = HttpContext.User.FindFirst("id")?.Value;
        RepositorioAuditoriapago rap=new RepositorioAuditoriapago();
       
        IList<Auditoriapago>auditoria=new List<Auditoriapago>();
        auditoria=rap.GetPorPago(NumeroPago);
         Avm.AuditoriaDatapago=new List<AuditoriaDataPago>();
         var i=0;
       foreach(var auditor in auditoria)
{
    Usuario usuario = new Usuario();
    RepositorioUsuario ru = new RepositorioUsuario();

    // Crear un nuevo objeto AuditoriaDataPago y llenarlo con los datos
    AuditoriaDataPago nuevaAuditoriaData = new AuditoriaDataPago
    {
        Usuario = ru.ObtenerPorId(auditor.UsuarioId.Id),
        Fecha = auditor.FechaPago,
        FechaPago = auditor.FechaCancelacion
    };

    // Agregar el nuevo objeto a la lista
    Avm.AuditoriaDatapago.Add(nuevaAuditoriaData);
}
          

           




		     return View(Avm);
    } 

}
}

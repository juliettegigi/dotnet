using Microsoft.AspNetCore.Authorization;  // para usar la anotación  [Authorize]
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using InmobiliariaGutierrez.Models.DAO;



namespace InmobiliariaGutierrez.Controllers{

[Authorize]
public class AuditoriacController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AuditoriacController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

   [Authorize(Policy ="Administrador")]
public IActionResult Detalle(int ContratoId, bool estado=true)
{   
    RepositorioContrato rc = new RepositorioContrato();
    Contrato contrato = rc.GetContrato(ContratoId,estado);
    
    RepositorioUsuario ru = new RepositorioUsuario();
    RepositorioAuditoriac rac = new RepositorioAuditoriac();
    IList<Auditoriacontrato> auditoria;
    
    auditoria = rac.GetAuditoriasPorContratoId(ContratoId);
    
    AuditoriaViewModelContrato Avm = new AuditoriaViewModelContrato();
    Avm.Contrato = contrato;
    Avm.AuditoriaData = new List<AuditoriaData>();

    foreach (var aud in auditoria)
    {
        var usuario = ru.ObtenerPorId(aud.UsuarioId.Id);
        var auditoriaData = new AuditoriaData
        {
            Usuario = usuario,
            FechasInicio = aud.FechaInicio,
            FechasCancelacion = aud.FechaCancelacion
        };

        Avm.AuditoriaData.Add(auditoriaData);
    }

    return View(Avm);
}

[Authorize(Policy ="Administrador")]
public IActionResult CotratosEstado0()
{   
    RepositorioContrato rc = new RepositorioContrato();
    IList<Contrato> contratos=rc.GetContratosEstado0();
    return View("~/Views/Contrato/ContratosEstado0.cshtml",contratos);
}

}
}
using System.Data;
using InmobiliariaGutierrez.Models.VO;
namespace InmobiliariaGutierrez.Views.InquilinoView;
public class ContratarAlquilerView{
	public int PrimerNumero {get;set;}
	public int Page {get;set;}
	public IList<Inmueble> Lista {get;set;}
	public IList<InmuebleTipo> ListaTipos {get;set;}
	public int CantidadPaginas {get;set;}
	public int IdInquilino {get;set;}
	
}
using System.Data;
using InmobiliariaGutierrez.Models.VO;
namespace InmobiliariaGutierrez.Views.ContratoView;
public class IndexView{
	public int PrimerNumero {get;set;}
	public int Page {get;set;}
	public IList<Contrato> ListaContratos {get;set;}
	public IList<Inquilino> ListaInquilinos {get;set;}
	public IList<Inmueble> ListaInmuebles {get;set;}
	public int CantidadPaginas {get;set;}

	
	public override string ToString()
     {
    	
    	return $"{ListaInmuebles[0].Direccion}";
	}
}

	
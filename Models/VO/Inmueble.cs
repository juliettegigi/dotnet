

using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaGutierrez.Models.VO;

public class Inmueble
{
	public int Id { get; set; }
	public Propietario? PropietarioId { get; set; }
	[ForeignKey(nameof(PropietarioId))]
	
	

	public string? Direccion { get; set; }
	public string? Tipo { get; set; }

	public int CantidadAmbientes {get;set;}
	public TipoUso Uso { get; set; }
	public Coordenada? Coordenadas {get;set;}
	public decimal Precio{get;set;}
}


public enum TipoUso
{
	Comercial=0,
	Residencial=1
}
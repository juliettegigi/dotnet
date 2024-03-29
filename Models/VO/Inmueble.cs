

using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaGutierrez.Models.VO;

public class Inmueble
{
	public int Id { get; set; }
	public Propietario? PropietarioId { get; set; }
	[ForeignKey(nameof(PropietarioId))]
	
	

	public string? Direccion { get; set; }
	public InmuebleTipo? InmuebleTipoId { get; set; }
  
	public int CantidadAmbientes {get;set;}
	public bool Suspendido {get;set;}
	public decimal precioBase {get;set;}
	public TipoUso Uso { get; set; }
	public Coordenada? Coordenadas {get;set;}
	
	 public override string ToString()
        {
            return $" PropietarioId: {PropietarioId}, Dirección: {Direccion}, Tipo: {InmuebleTipoId}, Cantidad de Ambientes: {CantidadAmbientes}, Uso: {Uso}, Coordenadas: {Coordenadas}, Precio: {precioBase}";
        }
}


public enum TipoUso
{
	Comercial=0,
	Residencial=1
}
/*Para los contratos, se deben registrar la fecha de inicio y fecha de
finalización del mismo (se deben controlar las fechas), el monto de
alquiler mensual en pesos y un vínculo entre la propiedad inmueble
y el inquilino. Se debe volver a verificar que el inmueble no esté
ocupado en esas fechas por otro contrato*/

using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaGutierrez.Models.VO;

public class Contrato
{
    public int Id { get; set; }
    [ForeignKey(nameof(InmuebleId))]
    public Inmueble InmuebleId { get; set; }
    [ForeignKey(nameof(InquilinoId))]
    public Inquilino InquilinoId { get; set; }
    
	public DateTime FechaInicio { get; set; }
	public DateTime FechaFin { get; set; }
	public DateTime FechaFinAnticipada { get; set; }
	public decimal PrecioXmes { get; set; }
    public bool Estado{ get; set; }
  
    public override string ToString()
        {  string inquilino=InquilinoId!=null?InquilinoId.ToString():" ";
           string inm=InmuebleId!=null?InmuebleId.ToString():" ";
            return $"Id: {Id},\n InquilinoId: {inquilino},\n  InmuebleId: {inm},\n  FechaInicio: {FechaInicio},\n  FechaFin: {FechaFin},\n  FechaFinAnticipada: {FechaFinAnticipada},\n PrecioXmes: {PrecioXmes},\n  Estado: {Estado},";
        }
      
}


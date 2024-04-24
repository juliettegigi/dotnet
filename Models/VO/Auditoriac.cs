/*Para los contratos, se deben registrar la fecha de inicio y fecha de
finalización del mismo (se deben controlar las fechas), el monto de
alquiler mensual en pesos y un vínculo entre la propiedad inmueble
y el inquilino. Se debe volver a verificar que el inmueble no esté
ocupado en esas fechas por otro contrato*/

using System.ComponentModel.DataAnnotations.Schema;
using InmobiliariaGutierrez.Models.Validacioness;

namespace InmobiliariaGutierrez.Models.VO;

public class Auditoriac
{
    public int Id { get; set; }
    
 
    [ForeignKey(nameof(UsuarioId))]
    public Usuario UsuarioId { get; set; }

  
	public DateTime? FechaInicio { get; set; }

    public DateTime? FechaCancelacion { get; set; }
	


      
}


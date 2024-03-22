/*Para los contratos, se deben registrar la fecha de inicio y fecha de
finalización del mismo (se deben controlar las fechas), el monto de
alquiler mensual en pesos y un vínculo entre la propiedad inmueble
y el inquilino. Se debe volver a verificar que el inmueble no esté
ocupado en esas fechas por otro contrato*/

using System.ComponentModel.DataAnnotations;

namespace InmobiliariaGutierrez.Models.VO;



public class ModalViewModel
{   
    public bool MostrarModal { get; set; }
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Documento { get; set; }
    public string? Apellido { get; set; }
}



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaGutierrez.Models.VO;

public class Pago
{
    public int Id { get; set; }
    [ForeignKey(nameof(ContratoId))]
    public int ContratoId { get; set; }
    public  int NumeroPago { get; set; }
    public  DateTime? FechaPago { get; set; }
	public DateTime Fecha { get; set; }
	[Required(ErrorMessage="Ingrese importe."), DataType(DataType.Currency)]
    public decimal Importe { get; set; }

    
}


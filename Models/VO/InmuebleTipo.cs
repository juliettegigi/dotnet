using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaGutierrez.Models.VO;

public class InmuebleTipo
{
	public int Id { get; set; }

	public string? Tipo { get; set; }
	
	 public override string ToString()
        {
            return $"Id: {Id},  Tipo: {Tipo}";
        }
}



namespace InmobiliariaGutierrez.Models.VO;

public class Coordenada
{
	public decimal CLatitud { get; set; }
    public decimal CLongitud { get; set; }

    public Coordenada(decimal x,decimal y ){
        CLatitud=x;
        CLongitud=y;
    }
    public override string ToString()
        {
            return $"({CLatitud}, {CLongitud})";
        }
}


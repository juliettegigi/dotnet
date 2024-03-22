namespace InmobiliariaGutierrez.Models.VO;

public class Coordenada
{
	public decimal Cx { get; set; }
    public decimal Cy { get; set; }

    public Coordenada(decimal x,decimal y ){
        Cx=x;
        Cy=y;
    }
    public override string ToString()
        {
            return $"({Cx}, {Cy})";
        }
}


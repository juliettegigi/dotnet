namespace InmobiliariaGutierrez.Models;
[Serializable]
public class ViewInquilinoFiltrarInmueble
{
    public bool CbComercial { get; set; }
    public bool CbResidencial { get; set; }
    public string? Tipo { get; set; }
    public int CantidadAmbientes { get; set; }
    public decimal PrecioMin { get; set; }
    public decimal PrecioMax { get; set; }

     public override string ToString()
        {  
            return $@"cbComercial: {CbComercial},
             cbResidencial: {CbResidencial},
             tipo: {Tipo},
             cantidad de ambientes: {CantidadAmbientes},
             precio mínimo: {PrecioMin},
             precio mmáximo: {PrecioMax}";
        }
}
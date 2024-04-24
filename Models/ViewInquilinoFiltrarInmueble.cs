using InmobiliariaGutierrez.Models.Validacioness; 
using System.ComponentModel.DataAnnotations;


namespace InmobiliariaGutierrez.Models;


public class ViewInquilinoFiltrarInmueble
{
    public bool CbComercial { get; set; }
    public bool CbResidencial { get; set; }
    public string? Tipo { get; set; }
    public int CantidadAmbientes { get; set; }
    public decimal PrecioMin { get; set; }
    public decimal PrecioMax { get; set; }
    
     [FechaNoMenorQueLaActual(ErrorMessage = "La fecha no puede ser menor que la actual.")]
       [DataType(DataType.Date)]
    [Required(ErrorMessage = "Campo fecha obligatorio.")]
   
   
    public DateTime ApartirDe { get; set; }
	public DateTime Hasta { get; set; }

    public ViewInquilinoFiltrarInmueble(){
        CantidadAmbientes=0;
                           CbComercial=false;
                           CbResidencial=false;
                           PrecioMin=0;
                           PrecioMax=0;
                           ApartirDe=DateTime.Now.AddYears(199);
                           Hasta=DateTime.Now.AddYears(200);
                           Tipo="";
    }

     public override string ToString()
        {  
            return $@"cbComercial: {CbComercial},
             cbResidencial: {CbResidencial},
             tipo: {Tipo},
             cantidad de ambientes: {CantidadAmbientes},
             precio mínimo: {PrecioMin},
             precio mmáximo: {PrecioMax},
             A partir de: {ApartirDe},
             Hasta: {Hasta}
             ";
        }
}



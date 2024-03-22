using System.ComponentModel.DataAnnotations;
using InmobiliariaGutierrez.Models.DAO;
namespace InmobiliariaGutierrez.Models.VO;
/*Cuando el inquilino viene a alquilar un local se lo entrevista
solicitando sus datos personales. ABM inquilino. DNI, nombre
completo y datos de contacto.*/
public class Inquilino
{
	public int Id { get; set; }
    public String? DNI {get;set;}
	public String? Nombre { get; set; }
    public String? Apellido { get; set; }
    
    [Display(Name = "Teléfono")]
    public String? Telefono { get; set; }
    public String? Email { get; set; }
    public String? Domicilio { get; set; }
    public List<Contrato> ListaContratos{get;set;}

    public Inquilino(){
        ListaContratos = new List<Contrato>();
    }
    
      public override string ToString()
        {  string? listaContratosStr = ListaContratos != null ? ListaContratos.ToString() : "Ningún inmueble asignado.";
            return $"Id: {Id}, DNI: {DNI}, Nombre: {Nombre}, Apellido: {Apellido}, Telefono: {Telefono}, Email: {Email}, Domicilio: {Domicilio}, ListaContratos: {ListaContratos?.Count}";
        }

    /*El inquilino puede terminar antes el contrato si lo desea, pero
pagando una multa. En caso de terminar el alquiler, se debe
actualizar la fecha de fin del contrato y calcular la multa. Si se
cumplió menos de la mitad del tiempo original de alquiler, deberá
pagar 2 (dos) meses extra de alquiler. Caso contrario, sólo uno.
Además, se debe revisar que no deba meses de alquiler. El sistema
no registra el pago de la multa, pero sí debe informar ese valor.*/

public decimal TerminarContrato(int contratoId){
    decimal multa=0;
    Contrato contrato=ListaContratos.Find(c => c.Id == contratoId);
    // busco su contrato,
    // actualizar fecha de fin de contrato
    
    TimeSpan tiempoContratoAntesDeTerminar = contrato.FechaFin - contrato.FechaInicio;
    contrato.FechaFin= DateTime.Now;
    var rc=new RepositorioContrato();
    rc.ModificaContrato(contrato); 
    TimeSpan tiempoContratoDespuesDeTerminar = contrato.FechaFin - contrato.FechaInicio;
    //calcular multa

         // si ((fechaFin-fechaInicio)/2)>(fechaActual-fechaInicio)
                  // return 2* contrato.precioXmes
         // else multa=contrato.precioXmes
    if((tiempoContratoAntesDeTerminar/2)>tiempoContratoDespuesDeTerminar)
       return 2*contrato.PrecioXmes;      
    
    return contrato.PrecioXmes;

}

/*El sistema debe permitir fácilmente renovar un contrato de alquiler.
Esto generará un nuevo alquiler, con un nuevo monto y fechas, pero
con el mismo inquilino e inmueble.
● Cuando el inquilino viene a pagar el alquiler, quedará registrado el
número de pago, la fecha en la que se realizó el pago y el importe.
*/
public void RenovarCotrato(){

}

public void PagarAlquiler(){

}

}


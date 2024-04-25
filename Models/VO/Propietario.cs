	/* Si el propietario no estaba ingresado desde antes, hay que
agregarlo al sistema solicitando su DNI, apellido, nombre y sus
datos de contacto.
*/

using System.ComponentModel.DataAnnotations;
 using InmobiliariaGutierrez.Models.Validacioness;

namespace InmobiliariaGutierrez.Models.VO;

public class Propietario
{   [Key]
	public int Id { get; set; }
	public string? Nombre { get; set; }
	public string? Apellido { get; set; }
    [Required(ErrorMessage="Campo obligatorio.")]
	public String? DNI {get;set;}

	//[Display(Name = "Teléfono")]
	public String? Telefono  { get; set; }
	 [Required, EmailAddress]
    public String? Email { get; set; }
	public String? Domicilio { get; set; }
	public List<Inmueble>? ListaInmuebles {get;set;}
	
	
	public override string ToString()
     {
    	string? listaInmueblesStr = ListaInmuebles != null ? ListaInmuebles.ToString() : "Ningún inmueble asignado.";
    	return $"Id: {Id}, DNI: {DNI}, Nombre: {Nombre}, Apellido: {Apellido}, Telefono: {Telefono}, Email: {Email}, ListaInmuebles: {listaInmueblesStr}";
	}
}


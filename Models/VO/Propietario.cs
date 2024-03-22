/* Si el propietario no estaba ingresado desde antes, hay que
agregarlo al sistema solicitando su DNI, apellido, nombre y sus
datos de contacto.
*/

using System.ComponentModel.DataAnnotations;

namespace InmobiliariaGutierrez.Models.VO;

public class Propietario
{   [Key]
	public int Id { get; set; }
	public string? Nombre { get; set; }
	public string? Apellido { get; set; }
    [Required(ErrorMessage="Campo obligatorio.")]
	public String? DNI {get;set;}

	[Display(Name = "Teléfono")]
	public String? Telefono  { get; set; }
    public String? Email  { get; set; }
	public Inmueble? ListaInmuebles {get;set;}
	
	
	public override string ToString()
     {
    	string? listaInmueblesStr = ListaInmuebles != null ? ListaInmuebles.ToString() : "Ningún inmueble asignado.";
    	return $"Id: {Id}, DNI: {DNI}, Nombre: {Nombre}, Apellido: {Apellido}, Telefono: {Telefono}, Email: {Email}, ListaInmuebles: {listaInmueblesStr}";
	}
}


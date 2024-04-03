using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public abstract class RepositorioBase
	{
		protected readonly IConfiguration configuration;
		protected readonly string? ConnectionString ;

		protected RepositorioBase()
		{  ConnectionString="Server=localhost;Database=inmobiliariadotnet;User=root;Password=15581016gpt;";
		}


		public String getCamposContrato(String nombreTabla, String id,String como){
         return @$"{nombreTabla}.{id } as {como}, 
	               {nombreTabla}.inquilinoId,
				   {nombreTabla}.inmuebleId,
				   {nombreTabla}.fechaInicio,
				   {nombreTabla}.fechaFin,
				   {nombreTabla}.fechaFinAnticipada,
				   {nombreTabla}.precioXmes,
				   {nombreTabla}.estado";
	}
    public String getCamposInquilino(String nombreTabla, String id,String como){
             return @$"{nombreTabla}.{id} as {como},
	                   {nombreTabla}.dni as dniInq,
					   {nombreTabla}.nombre as nombreInq,
					   {nombreTabla}.apellido as apellidoInq,
					   {nombreTabla}.telefono as telefonoInq,
					   {nombreTabla}.email as emailInq,
					   {nombreTabla}.domicilio as domicilioInq";
	}
	public String getCamposInmueble(String nombreTabla, String id,String como){
          return  @$"
	                  {nombreTabla}.{id } as {como},
	                  {nombreTabla}.propietarioId,
	                  {nombreTabla}.inmuebleTipoId,
	                  {nombreTabla}.direccion,
	                  {nombreTabla}.cantidadAmbientes,
	                  {nombreTabla}.uso,
	                  {nombreTabla}.precioBase,
	                  {nombreTabla}.cLatitud,
	                  {nombreTabla}.cLongitud,
					  {nombreTabla}.suspendido, 
					  {nombreTabla}.disponible
                     ";
	               }

	public String getCamposInmuebleTipo(String nombreTabla, String id,String como){
		return @$"{nombreTabla}.{id} as {como},
                  {nombreTabla}.tipo";
	}			

	public String getCamposPropietario(String nombreTabla, String id,String como){
		return @$"{nombreTabla}.{id } as {como},
                  {nombreTabla}.dni,
                  {nombreTabla}.nombre,
                  {nombreTabla}.apellido,
                  {nombreTabla}.telefono,
                  {nombreTabla}.email,
                  {nombreTabla}.domicilio";
	}	


	public Propietario crearPropietario(MySqlDataReader reader){
		return new Propietario{
							Id=reader.GetInt32("idPropietario"),
							DNI=reader.GetString("dni"),
							Nombre=reader.GetString("nombre"),
							Apellido=reader.GetString("apellido"),
							Telefono=reader.GetString("telefono"),
							Email=reader.GetString("email"),
							Domicilio = reader.GetString("domicilio")
						}; 
	}

	public InmuebleTipo crearInmuebleTipo(MySqlDataReader reader){
		return new InmuebleTipo{
							Id=reader.GetInt32("idInmuebleTipo"),
							Tipo=reader.GetString("tipo")
						};
	}


public TipoUso crearUso(MySqlDataReader reader){
	                    string valorUso = reader.GetString("uso");
						TipoUso uso;
						Enum.TryParse(valorUso, out uso);
						return uso;
}


public Inquilino crearInquilino(MySqlDataReader reader){
	return new Inquilino
						{
							Id = reader.GetInt32("idInquilino"),
							DNI = reader.GetString($"{nameof(Inquilino.DNI)}Inq"),
                            Nombre = reader.GetString($"{nameof(Inquilino.Nombre)}Inq"),
                            Apellido = reader.GetString($"{nameof(Inquilino.Apellido)}Inq"),
							Email = reader.GetString($"{nameof(Inquilino.Email)}Inq"),
							Telefono = reader.GetString($"{nameof(Inquilino.Telefono)}Inq"),
							Domicilio = reader.GetString($"{nameof(Inquilino.Domicilio)}Inq"),
						};
					}





	public Inmueble crearInmueble(MySqlDataReader reader,Propietario propietario,InmuebleTipo tipo ,Coordenada coordenada,TipoUso uso){
	return  new Inmueble
                    {
                        Id = reader.GetInt32("idInmueble"),
                        PropietarioId = propietario,
                        Direccion = reader.GetString("direccion"),
                        InmuebleTipoId = tipo,
                        CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                        Uso = uso,
                        Coordenadas = coordenada,
                    };			
}


public Coordenada crearCoordenadas(MySqlDataReader reader){
	return new Coordenada(
                        reader.GetDecimal("cLatitud"),
                        reader.GetDecimal("cLongitud")
                    );
}

	
	}


	










	
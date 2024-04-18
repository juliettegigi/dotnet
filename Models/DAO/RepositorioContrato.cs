using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioContrato:RepositorioBase
{
	

	public RepositorioContrato():base()
	{
		
	}

	public Contrato? GetContrato(int id)
	{
		Contrato? Contrato = null;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Contrato.Id)}, 
			                    {nameof(Contrato.InquilinoId)},
								{nameof(Contrato.InmuebleId)},
								{nameof(Contrato.FechaInicio)},
								{nameof(Contrato.FechaFin)},
								{nameof(Contrato.FechaFinAnticipada)},
								{nameof(Contrato.PrecioXmes)},
								{nameof(Contrato.Estado)}
			             FROM Contratos
			             WHERE {nameof(Contrato.Id)} = @{nameof(Contrato.Id)} 
						       and 
							   {nameof(Contrato.Estado)} = true ;
                         ";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Contrato.Id)}", id);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					if(reader.Read())
					{   var rinq=new RepositorioInquilino();
                        var rinm=new RepositorioInmueble();
						Contrato = new Contrato
						{
							Id = reader.GetInt32(nameof(Contrato.Id)),
							InquilinoId = rinq.GetInquilino(reader.GetInt32(nameof(Contrato.InquilinoId))),   
                            InmuebleId = rinm.GetInmueble(reader.GetInt32(nameof(Contrato.InmuebleId))),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
							FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
							FechaFinAnticipada = reader.GetDateTime(nameof(Contrato.FechaFinAnticipada)),
							PrecioXmes = reader.GetDecimal(nameof(Contrato.PrecioXmes)),
							Estado = true
						};
					}
				}
			}
            connection.Close();
		}
		return Contrato;
	}


	public IList<Contrato> GetContratos()
	{
		var Contratos = new List<Contrato>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Contrato.Id)},
								{nameof(Contrato.InquilinoId)},
								{nameof(Contrato.InmuebleId)},
								{nameof(Contrato.FechaInicio)},
								{nameof(Contrato.FechaFin)},
								{nameof(Contrato.FechaFinAnticipada)},
								{nameof(Contrato.PrecioXmes)}
			             FROM Contratos
                         WHERE  {nameof(Contrato.Estado)} = true ;/
                         ";
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{   var rinq=new RepositorioInquilino();
				    var rinm=new RepositorioInmueble();
					while(reader.Read())
					{    
						Contratos.Add(new Contrato
						{   Id = reader.GetInt32(nameof(Contrato.Id)),
							InquilinoId = rinq.GetInquilino(reader.GetInt32(nameof(Contrato.InquilinoId))),
                            InmuebleId = rinm.GetInmueble(reader.GetInt32(nameof(Contrato.InmuebleId))),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
							FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
							FechaFinAnticipada = reader.GetDateTime(nameof(Contrato.FechaFinAnticipada)),
							Estado = true

						});
					}
				}
                
			}
             connection.Close();
		}
		return Contratos;
	}

	public int AltaContrato(Contrato contrato){

		int id = 0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"INSERT INTO Contratos({nameof(Contrato.InquilinoId)},
											  {nameof(Contrato.InmuebleId)},
											  {nameof(Contrato.FechaInicio)},
											  {nameof(Contrato.FechaFin)},
											  {nameof(Contrato.FechaFinAnticipada)},
											  {nameof(Contrato.PrecioXmes)},
											  {nameof(Contrato.Estado)})
				VALUES ( @{nameof(Contrato.InquilinoId)},
				         @{nameof(Contrato.InmuebleId)},
				         @{nameof(Contrato.FechaInicio)},
				         @{nameof(Contrato.FechaFin)},
				         @{nameof(Contrato.FechaFinAnticipada)},
				         @{nameof(Contrato.PrecioXmes)},
				         true
						 );
				SELECT LAST_INSERT_ID();";

				
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Contrato.InquilinoId)}", contrato.InquilinoId.Id);
                command.Parameters.AddWithValue($"@{nameof(Contrato.InmuebleId)}", contrato.InmuebleId.Id);
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaInicio)}", contrato.FechaInicio.ToString("yyyy-MM-dd"));
				command.Parameters.AddWithValue($"@{nameof(Contrato.FechaFin)}", contrato.FechaFin.ToString("yyyy-MM-dd"));
				command.Parameters.AddWithValue($"@{nameof(Contrato.FechaFinAnticipada)}", contrato.FechaFinAnticipada.ToString("yyyy-MM-dd"));
				command.Parameters.AddWithValue($"@{nameof(Contrato.PrecioXmes)}", contrato.PrecioXmes);
				

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				contrato.Id = id;
				connection.Close();
			}
		}
		return id;
	}


	public int ModificaContrato(Contrato contrato)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE Contratos
                         SET {nameof(Contrato.InquilinoId)} = @{nameof(Contrato.InquilinoId)},
                             {nameof(Contrato.InmuebleId)} = @{nameof(Contrato.InmuebleId)},
				             {nameof(Contrato.FechaInicio)} = @{nameof(Contrato.FechaInicio)},
				             {nameof(Contrato.FechaFin)} = @{nameof(Contrato.FechaFin)},
				             {nameof(Contrato.FechaFinAnticipada)} = @{nameof(Contrato.FechaFinAnticipada)},
				             {nameof(Contrato.PrecioXmes)} = @{nameof(Contrato.PrecioXmes)},
				             {nameof(Contrato.Estado)} = 1
				          WHERE {nameof(Contrato.Id)} = @{nameof(Contrato.Id)};";

		     Console.WriteLine("*****************************************************sql");
        Console.WriteLine(sql);
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Contrato.InquilinoId)}", contrato.InquilinoId.Id);
                command.Parameters.AddWithValue($"@{nameof(Contrato.InmuebleId)}", contrato.InmuebleId.Id);
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaInicio)}", contrato.FechaInicio);
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaFin)}", contrato.FechaFin);
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaFinAnticipada)}", contrato.FechaFinAnticipada);
                command.Parameters.AddWithValue($"@{nameof(Contrato.PrecioXmes)}", contrato.PrecioXmes);
                command.Parameters.AddWithValue($"@{nameof(Contrato.Estado)}", contrato.Estado);
				command.Parameters.AddWithValue($"@{nameof(Contrato.Id)}", contrato.Id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}


	public int EliminarContrato(int id)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE Contratos
                         SET {nameof(Contrato.Estado)} = 0
				        WHERE {nameof(Contrato.Id)} = @{nameof(Contrato.Id)};";

			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Contrato.Id)}", id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}



public IList<Contrato> GetContratosXinquilino(int idInquilino)
	{
		var Contratos = new List<Contrato>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Contrato.Id)}, 
			                    {nameof(Contrato.InquilinoId)},
								{nameof(Contrato.InmuebleId)},
								{nameof(Contrato.FechaInicio)}, 
								{nameof(Contrato.FechaFin)},
								{nameof(Contrato.FechaFinAnticipada)},
								{nameof(Contrato.PrecioXmes)}
			             FROM Contratos,Inquilinos
                         WHERE Contratos.inquilinoId=@{nameof(Contrato.InquilinoId)} 
                         ;
                         ";		 
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{   
				command.Parameters.AddWithValue($"@{nameof(Contrato.InquilinoId)}", idInquilino);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{   var rinm= new RepositorioInmueble();
                    var ri=new RepositorioInquilino();
                    var inquilino= ri.GetInquilino(idInquilino);
					while(reader.Read())
					{
						Contratos.Add(new Contrato
						{   
                            InmuebleId=rinm.GetInmueble(reader.GetInt32(nameof(Contrato.InmuebleId))),                              
							Id = reader.GetInt32(nameof(Contrato.Id)),
							InquilinoId=inquilino,
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                            FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                            PrecioXmes = reader.GetDecimal(nameof(Contrato.PrecioXmes)),
						});
					}
				}
                
			}
             connection.Close();
		}
		return Contratos;
	}


/* Listar todos los contratos que terminen en 30, 60 o 90 días (permitir elegir o especificar plazo). 
*/
public IList<Contrato> GetContratosTerminan(int dias)
	{
		var Contratos = new List<Contrato>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$" select {getCamposContrato("contratos","id","id")},
			                     {getCamposInquilino("inquilinos","id","idInquilino")},
								 {getCamposInmueble("inmuebleCompleto","idInmueble","idInmueble")}, 
						         {getCamposInmuebleTipo("inmuebleCompleto","idInmuebleTipo","idInmuebleTipo")},
								 {getCamposPropietario("inmuebleCompleto","idPropietario","idPropietario")}
						  from contratos 
	                      inner join inquilinos on contratos.inquilinoId=inquilinos.id
                          inner join (   select {getCamposInmueble("inmuebles","id","idInmueble")}, 
                                                {getCamposInmuebleTipo("inmuebleTipos","id","idInmuebleTipo")},
									   		    {getCamposPropietario("propietarios","id","idPropietario")}
                                         from inmuebles 
                                         inner join Inmuebletipos on inmuebles.inmuebletipoId=inmuebletipos.id
                                         inner join propietarios on inmuebles.propietarioId=propietarios.id
									  ) AS inmuebleCompleto 
                          on contratos.inmuebleId=inmuebleCompleto.idInmueble
                        where datediff(fechaFin,curdate())=30;
                         
                         ";		 
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{   
				command.Parameters.AddWithValue("dias", dias);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{  
					while(reader.Read())
					{
						var inquilino=new Inquilino{
							Id=reader.GetInt32("idInquilino"),
							DNI=reader.GetString("dniInq"),
							Nombre=reader.GetString("nombreInq"),
							Apellido=reader.GetString("apellidoInq"),
							Telefono=reader.GetString("telefonoInq"),
							Email=reader.GetString("emailInq"),
							Domicilio=reader.GetString("domicilioInq"),
						};
						var propietario=new Propietario{
							Id=reader.GetInt32("idPropietario"),
							DNI=reader.GetString("dni"),
							Nombre=reader.GetString("nombre"),
							Apellido=reader.GetString("apellido"),
							Telefono=reader.GetString("telefono"),
							Email=reader.GetString("email"),
							Domicilio=reader.GetString("domicilio"),
						};

						var inmuebleTipo=new InmuebleTipo{
							Id=reader.GetInt32("idInmuebleTipo"),
							Tipo=reader.GetString("tipo")
						};

                        
						string valorUso = reader.GetString("uso");
						TipoUso uso;
						Enum.TryParse(valorUso, out uso);
						
						var inmueble=new Inmueble{
							Id=reader.GetInt32("idInmueble"),
							PropietarioId=propietario,
							InmuebleTipoId=inmuebleTipo,
							Direccion=reader.GetString("direccion"),
							CantidadAmbientes=reader.GetInt32("cantidadAmbientes"),
							Uso=uso,
							PrecioBase=reader.GetDecimal("precioBase"),
							Coordenadas=new Coordenada(reader.GetDecimal("cLatitud"),reader.GetDecimal("cLongitud")),
							Suspendido=reader.GetBoolean("suspendido"),
							Disponible=reader.GetBoolean("disponible")
						};
						Contratos.Add(new Contrato
						{   
                            InmuebleId=inmueble,                              
							Id = reader.GetInt32(nameof(Contrato.Id)),
							InquilinoId=inquilino,
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                            FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                            PrecioXmes = reader.GetDecimal(nameof(Contrato.PrecioXmes)),
                            Estado = reader.GetBoolean(nameof(Contrato.Estado)),
						});
					}
				}
                
			}
             connection.Close();
		}
		return Contratos;
	}


	public IList<Contrato> GetContratosPaginado(int limite, int offset)
	{
		var contratos = new List<Contrato>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {getCamposContrato("contratos","id","id")}, 
			                    {getCamposInquilino("inquilinos","id","idInquilino")},
								{getCamposInmueble("inmueblesCompleto","idInmueble","idInmueble")},
								{getCamposPropietario("inmueblesCompleto","idPropietario","idPropietario")},
								{getCamposInmuebleTipo("inmueblesCompleto","idInmuebleTipo","idInmuebleTipo")}
						FROM contratos
                        
						INNER JOIN inquilinos 
						ON contratos.inquilinoId=Inquilinos.id
                        
						INNER JOIN (   SELECT {getCamposInmueble("inmuebles","id","idInmueble")},
						                      {getCamposPropietario("propietarios","id","idPropietario")},
								              {getCamposInmuebleTipo("inmuebleTipos","id","idInmuebleTipo")} 
		                               FROM inmuebles
                                       
									   INNER JOIN propietarios 
									   on inmuebles.propietarioId=propietarios.id
                                       
									   INNER JOIN inmuebleTipos 
									   on inmuebles.inmuebleTipoId=inmuebleTipos.id
									) as inmueblesCompleto
	                    ON contratos.inmuebleId=idInmueble
						where contratos.estado=true
						order by id
						
                        limit @limite offset @offset;
 

            "; 
			

			using(var command = new MySqlCommand(sql, connection))
			{   command.Parameters.AddWithValue("limite", limite);
			    command.Parameters.AddWithValue("offset", offset);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{    
                         var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.CLatitud)), reader.GetDecimal(nameof(Inmueble.Coordenadas.CLongitud)) );
					    
					
						contratos.Add(new Contrato
						{   Id = reader.GetInt32("id"),
							InquilinoId= crearInquilino(reader),
							InmuebleId= crearInmueble(reader,crearPropietario(reader),crearInmuebleTipo(reader),crearCoordenadas(reader),crearUso(reader)),
                            FechaInicio= reader.GetDateTime(nameof(Contrato.FechaInicio)),
							FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
							FechaFinAnticipada = reader.GetDateTime(nameof(Contrato.FechaFinAnticipada)),
                            PrecioXmes = reader.GetDecimal(nameof(Contrato.PrecioXmes)),
                            Estado = reader.GetBoolean(nameof(Contrato.Estado)),
							
							
						});
					}
				}
                
			}
             connection.Close();
		}
		return contratos;
	}

	public int getCantidadRegistros()
	{   int cantidad=0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"  SELECT COUNT(*) AS cantidadRegistros
                           FROM contratos
						   where contratos.estado=true;";
			using(var command = new MySqlCommand(sql, connection))
			{    
				connection.Open();
				using(var reader = command.ExecuteReader())
                {
                       if (reader.Read())
                          cantidad=reader.GetInt32("cantidadRegistros");
                }
				
				
			}
            connection.Close();
		}
        
		return cantidad;
	}

public IList<Contrato> GetContratosTodos()
{
    var contratos = new List<Contrato>();
    using(var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @$"SELECT {getCamposContrato("contratos","id","id")}, 
                            {getCamposInquilino("inquilinos","id","idInquilino")},
                            {getCamposInmueble("inmueblesCompleto","idInmueble","idInmueble")},
                            {getCamposPropietario("inmueblesCompleto","idPropietario","idPropietario")},
                            {getCamposInmuebleTipo("inmueblesCompleto","idInmuebleTipo","idInmuebleTipo")}
                    FROM contratos
                        
                    INNER JOIN inquilinos 
                    ON contratos.inquilinoId=Inquilinos.id
                        
                    INNER JOIN (   SELECT {getCamposInmueble("inmuebles","id","idInmueble")},
                                      {getCamposPropietario("propietarios","id","idPropietario")},
                                      {getCamposInmuebleTipo("inmuebleTipos","id","idInmuebleTipo")} 
                                   FROM inmuebles
                                       
                                   INNER JOIN propietarios 
                                   ON inmuebles.propietarioId=propietarios.id
                                       
                                   INNER JOIN inmuebleTipos 
                                   ON inmuebles.inmuebleTipoId=inmuebleTipos.id
                                ) as inmueblesCompleto
                    ON contratos.inmuebleId=idInmueble
				    where contratos.estado=true	
                    ORDER BY id;
 

            "; 

        using(var command = new MySqlCommand(sql, connection))
        {   
            connection.Open();
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {    
                     var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.CLatitud)), reader.GetDecimal(nameof(Inmueble.Coordenadas.CLongitud)) );
                        
                    
                    contratos.Add(new Contrato
                    {   Id = reader.GetInt32("id"),
                        InquilinoId= crearInquilino(reader),
                        InmuebleId= crearInmueble(reader,crearPropietario(reader),crearInmuebleTipo(reader),crearCoordenadas(reader),crearUso(reader)),
                        FechaInicio= reader.GetDateTime(nameof(Contrato.FechaInicio)),
                        FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                        FechaFinAnticipada = reader.GetDateTime(nameof(Contrato.FechaFinAnticipada)),
                        PrecioXmes = reader.GetDecimal(nameof(Contrato.PrecioXmes)),
                        Estado = reader.GetBoolean(nameof(Contrato.Estado)),
                    });
                }
            }
                
        }
        connection.Close();
    }
    return contratos;
}

    
}









 
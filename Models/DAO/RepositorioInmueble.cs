using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioInmueble:RepositorioBase
{
	//readonly string ConnectionString = "Server=localhost;Database=InmobiliariaGutierrez;User=root;Password=;";

	public RepositorioInmueble():base()
	{
		
	}

public Inmueble? GetInmueble(int id)
{
    Inmueble? inmueble = null;
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"
            SELECT inmuebles.id AS idInmueble,
                   inmuebles.propietarioId,
                   inmuebles.inmuebleTipoId,
                   inmuebles.direccion,
                   inmuebles.cantidadAmbientes,
                   inmuebles.uso,
                   inmuebles.precioBase,
                   inmuebles.cLatitud,
                   inmuebles.cLongitud,
                   inmuebles.suspendido,
                   propietarios.id AS idPropietario,
                   propietarios.dni,
                   propietarios.nombre,
                   propietarios.apellido,
                   propietarios.telefono,
                   propietarios.email,
                   propietarios.domicilio,
                   inmuebleTipos.id AS idInmuebleTipo,
                   inmuebleTipos.tipo
            FROM inmuebles
            INNER JOIN propietarios ON inmuebles.propietarioId = propietarios.id
            INNER JOIN inmuebleTipos ON inmuebles.inmuebleTipoId = inmuebleTipos.id
            WHERE inmuebles.id = @id";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Propietario propietario;
                    InmuebleTipo tipo;
                    TipoUso uso;

                    propietario = new Propietario
                    {
                        Id = reader.GetInt32("idPropietario"),
                        DNI = reader.GetString("dni"),
                        Nombre = reader.GetString("nombre"),
                        Apellido = reader.GetString("apellido"),
                        Email = reader.GetString("email"),
                        Telefono = reader.GetString("telefono"),
                        Domicilio = reader.IsDBNull(reader.GetOrdinal("domicilio")) ? "" : reader.GetString("domicilio"),
                    };

                    tipo = new InmuebleTipo
                    {
                        Id = reader.GetInt32("idInmuebleTipo"),
                        Tipo = reader.GetString("tipo")
                    };

                    var coordenada = new Coordenada(
                        reader.GetDecimal("cLatitud"),
                        reader.GetDecimal("cLongitud")
                    );

                    // Lectura del uso y conversión a TipoUso
                    var usoString = reader.GetString("uso");
                    Enum.TryParse<TipoUso>(usoString, out uso);

                    inmueble = new Inmueble
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
            }
        }
    }
    return inmueble;
}


	public IList<Inmueble> GetInmuebles()
	{
		var inmuebles = new List<Inmueble>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Inmueble.Id)}, 
			                    {nameof(Inmueble.PropietarioId)},
								{nameof(Inmueble.Direccion)},
								{nameof(Inmueble.InmuebleTipoId)},
								{nameof(Inmueble.CantidadAmbientes)},
								{nameof(Inmueble.Uso)},
								{nameof(Inmueble.Coordenadas.CLatitud)},
								{nameof(Inmueble.Coordenadas.CLongitud)}
			             
						  FROM inmuebles
            INNER JOIN propietarios ON inmuebles.propietarioId = propietarios.id
            INNER JOIN inmuebleTipos ON inmuebles.inmuebleTipoId = inmuebleTipos.id
            WHERE inmuebles.id = @id"; // Añadido filtro por id
                         
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{    var rp=new RepositorioPropietario();
					     var propietario=rp.GetPropietario(reader.GetInt32(nameof(Propietario.Id))); 
                         var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.CLatitud)), reader.GetDecimal(nameof(Inmueble.Coordenadas.CLongitud)) );
					   var tipo = new InmuebleTipo
                    {
                        Id = reader.GetInt32("idInmuebleTipo"),
                        Tipo = reader.GetString("tipo")
                    };

					
						inmuebles.Add(new Inmueble
						{   Id = reader.GetInt32(nameof(Inmueble.Id)),
							PropietarioId = propietario,
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            InmuebleTipoId = tipo,
							CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
							Uso = (TipoUso)reader.GetInt32(nameof(Inmueble.Uso)),
                            Coordenadas=coordenada
							
							//Tipo = (TipoInquilino)reader.GetInt32(nameof(Inquilino.Tipo))
						});
					}
				}
                
			}
             connection.Close();
		}
		return inmuebles;
	}



    public IList<Inmueble> GetInmueblesPaginado(int limite, int offset)
	{
		var inmuebles = new List<Inmueble>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {getCamposInmueble("inmuebles")}, 
			                    {getCamposPropietario("propietarios")},
								{getCamposInmuebleTipo("inmuebleTipos")}
			             
						  FROM inmuebles
            INNER JOIN propietarios ON inmuebles.propietarioId = propietarios.id
            INNER JOIN inmuebleTipos ON inmuebles.inmuebleTipoId = inmuebleTipos.id
            order by idInmueble
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
					    
					
						inmuebles.Add(new Inmueble
						{   Id = reader.GetInt32("idInmueble"),
							PropietarioId = crearPropietario(reader),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            InmuebleTipoId = crearInmuebleTipo(reader),
							CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
                            PrecioBase=reader.GetDecimal(nameof(Inmueble.PrecioBase)),
							Uso = crearUso(reader),
                            Coordenadas=coordenada
							
							
						});
					}
				}
                
			}
             connection.Close();
		}
		return inmuebles;
	}

public int AltaInmueble(Inmueble inmueble)
{   
    Console.WriteLine("-----------------------------------------------------prrr");
    Console.WriteLine(inmueble);
    Console.WriteLine("..............................................papaappa");
    
    int id = 0;
    
    using(var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @$"INSERT INTO Inmuebles (
                            {nameof(Inmueble.PropietarioId)},
                            {nameof(Inmueble.Direccion)},
                            {nameof(Inmueble.InmuebleTipoId)},
                            {nameof(Inmueble.CantidadAmbientes)},
                            {nameof(Inmueble.Uso)},
                            {nameof(Inmueble.PrecioBase)},
                            {nameof(Inmueble.Coordenadas.CLatitud)},
                            {nameof(Inmueble.Coordenadas.CLongitud)},
                            {nameof(Inmueble.Suspendido)}
                   )
                   VALUES (
                            @{nameof(Inmueble.PropietarioId)}, 
                            @{nameof(Inmueble.Direccion)}, 
                            @{nameof(Inmueble.InmuebleTipoId)}, 
                            @{nameof(Inmueble.CantidadAmbientes)},
                            @{nameof(Inmueble.Uso)}, 
                            @{nameof(Inmueble.PrecioBase)}, 
                            @{nameof(Inmueble.Coordenadas.CLatitud)}, 
                            @{nameof(Inmueble.Coordenadas.CLongitud)}, 
                            @{nameof(Inmueble.Suspendido)}
                   );
                   SELECT LAST_INSERT_ID();";
        
        using(var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue($"@{nameof(Inmueble.PropietarioId)}", inmueble.PropietarioId.Id);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.Direccion)}", inmueble.Direccion);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.InmuebleTipoId)}", inmueble.InmuebleTipoId.Tipo);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.CantidadAmbientes)}", inmueble.CantidadAmbientes);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.PrecioBase)}", inmueble.PrecioBase);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.CLatitud)}", inmueble.Coordenadas.CLatitud);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.CLongitud)}", inmueble.Coordenadas.CLongitud);
            command.Parameters.AddWithValue($"@{nameof(Inmueble.Suspendido)}", inmueble.Suspendido);

            connection.Open();
            id = Convert.ToInt32(command.ExecuteScalar());
            inmueble.Id = id;
            connection.Close();
        }
    }
    
    return id;
}

	public int ModificaInmueble(Inmueble inmueble)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE Inquilinos
				SET {nameof(Inmueble.PropietarioId)} = @{nameof(Inmueble.PropietarioId)},
				{nameof(Inmueble.Direccion)} = @{nameof(Inmueble.Direccion)},
				{nameof(Inmueble.InmuebleTipoId)} = @{nameof(Inmueble.InmuebleTipoId)},
				{nameof(Inmueble.CantidadAmbientes)} = @{nameof(Inmueble.CantidadAmbientes)},
				{nameof(Inmueble.Uso)} = @{nameof(Inmueble.Uso)},
				{nameof(Inmueble.Coordenadas.CLatitud)} = @{nameof(Inmueble.Coordenadas.CLatitud)},
				{nameof(Inmueble.Coordenadas.CLongitud)} = @{nameof(Inmueble.Coordenadas.CLongitud)}
				WHERE {nameof(Inmueble.Id)} = @{nameof(Inmueble.Id)};";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inmueble.PropietarioId)}", inmueble.PropietarioId.Id);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Direccion)}", inmueble.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.InmuebleTipoId)}", inmueble.InmuebleTipoId);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.CantidadAmbientes)}", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.CLatitud)}", inmueble.Coordenadas.CLatitud);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.CLongitud)}", inmueble.Coordenadas.CLongitud);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", inmueble.Id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
				
			}
		}
		return 0;
	}


	public int EliminaInmueble(int id)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"DELETE FROM Inquilinos
				WHERE {nameof(Inmueble.Id)} = @{nameof(Inmueble.Id)}";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}


public IList<Inmueble> GetInmueblesXpropietario(int PropietarioId)
	{
		var Inmuebles = new List<Inmueble>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Inmueble.Id)}, {nameof(Inmueble.PropietarioId)},{nameof(Inmueble.Direccion)},{nameof(Inmueble.InmuebleTipoId)}, {nameof(Inmueble.CantidadAmbientes)},{nameof(Inmueble.Uso)},{nameof(Inmueble.Coordenadas.CLatitud)},{nameof(Inmueble.Coordenadas.CLongitud)}
			             FROM inmuebles, propietarios
                         WHERE inmuebles.propietarioId=@{nameof(Inmueble.PropietarioId)} 
                         ;
                         ";
			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{   var rp=new RepositorioPropietario(); 
                    var propietario=rp.GetPropietario(reader.GetInt32(nameof(Propietario.Id)));
					var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.CLatitud)),reader.GetDecimal(nameof(Inmueble.Coordenadas.CLongitud)));
					while(reader.Read())
					{
                         	   var tipo = new InmuebleTipo
                    {
                        Id = reader.GetInt32("idInmuebleTipo"),
                        Tipo = reader.GetString("tipo")
                    };


						Inmuebles.Add(new Inmueble
						{   
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
							PropietarioId = propietario,
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                           
							InmuebleTipoId = tipo,
							CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
							Uso = (TipoUso)reader.GetInt32(nameof(Inmueble.Uso)),
							Coordenadas=coordenada,
						});
					}
				}
                
			}
             connection.Close();
		}
		return Inmuebles;
	}

public int getCantidadRegistros()
	{   int cantidad=0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"  SELECT COUNT(*) AS cantidadRegistros
                           FROM Inmuebles;";
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
	}public IList<Inmueble> GetInmueblesPorId(int id)
{
    var inmuebles = new List<Inmueble>();
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"
            SELECT i.id AS idInmueble,
                   i.propietarioId,
                   i.inmuebleTipoId,
                   i.direccion,
                   i.cantidadAmbientes,
                   i.uso,
                   i.precioBase,
                   i.cLatitud,
                   i.cLongitud,
                   i.suspendido,
                   p.id AS idPropietario,
                   p.dni,
                   p.nombre,
                   p.apellido,
                   p.telefono,
                   p.email,
                   p.domicilio,
                   it.id AS idInmuebleTipo,
                   it.tipo
            FROM inmuebles i
            INNER JOIN propietarios p ON i.propietarioId = p.id
            INNER JOIN inmuebleTipos it ON i.inmuebleTipoId = it.id
            WHERE i.id = @id
            ORDER BY i.id";
                     
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var coordenada = new Coordenada(reader.GetDecimal("cLatitud"), reader.GetDecimal("cLongitud"));

                    inmuebles.Add(new Inmueble
                    {
                        Id = reader.GetInt32("idInmueble"),
                        PropietarioId = new Propietario
                        {
                            Id = reader.GetInt32("idPropietario"),
                            DNI = reader.GetString("dni"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Email = reader.GetString("email"),
                            Telefono = reader.GetString("telefono"),
                            Domicilio = reader.IsDBNull(reader.GetOrdinal("domicilio")) ? "" : reader.GetString("domicilio"),
                        },
                        Direccion = reader.GetString("direccion"),
                        InmuebleTipoId = new InmuebleTipo
                        {
                            Id = reader.GetInt32("idInmuebleTipo"),
                            Tipo = reader.GetString("tipo")
                        },
                        CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                        PrecioBase = reader.GetDecimal("precioBase"),
                        Uso = (TipoUso)reader.GetInt32("uso"),
                        Coordenadas = coordenada
                    });
                }
            }
        }
    }
    return inmuebles;
}

	
}



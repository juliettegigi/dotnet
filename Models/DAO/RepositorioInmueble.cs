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
		Inmueble inmueble = null;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Inmueble.Id)}, {nameof(Inmueble.PropietarioId)},{nameof(Inmueble.Direccion)},{nameof(Inmueble.Tipo)}, {nameof(Inmueble.CantidadAmbientes)},{nameof(Inmueble.Uso)},Cx,Cy
			             FROM Inmuebles
			             WHERE {nameof(Inmueble.Id)} = @{nameof(Inmueble.Id)};
                         ";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", id);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					if(reader.Read())
					{   
                        var rp=new RepositorioPropietario(); 
                        var propietario=rp.GetPropietario(reader.GetInt32(nameof(Propietario.Id)));
						var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.Cx)),reader.GetDecimal(nameof(Inmueble.Coordenadas.Cy)));
                        inmueble = new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
							PropietarioId = propietario,
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Tipo = reader.GetString(nameof(Inmueble.Tipo)),
							CantidadAmbientes = reader.GetInt32(nameof(Inmueble.CantidadAmbientes)),
							Uso = (TipoUso)reader.GetInt32(nameof(Inmueble.Uso)),
							Coordenadas=coordenada,
						};
					}
				}
			}
            connection.Close();
		}
		return inmueble;
	}


	public IList<Inmueble> GetInmuebles()
	{
		var inmuebles = new List<Inmueble>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Inmueble.Id)}, {nameof(Inmueble.PropietarioId)},{nameof(Inmueble.Direccion)},{nameof(Inmueble.Tipo)}, {nameof(Inmueble.CantidadAmbientes)},{nameof(Inmueble.Uso)},{nameof(Inmueble.Coordenadas.Cx)},{nameof(Inmueble.Coordenadas.Cy)}
			             FROM Inquilinos;
                         ";
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{    var rp=new RepositorioPropietario();
					     var propietario=rp.GetPropietario(reader.GetInt32(nameof(Propietario.Id))); 
                         var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.Cx)), reader.GetDecimal(nameof(Inmueble.Coordenadas.Cy)) );
						inmuebles.Add(new Inmueble
						{   Id = reader.GetInt32(nameof(Inmueble.Id)),
							PropietarioId = propietario,
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Tipo = reader.GetString(nameof(Inmueble.Tipo)),
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

	public int AltaInmueble(Inmueble inmueble)
	{   Console.WriteLine("-----------------------------------------------------prrr");
        Console.WriteLine(inmueble);
		int id = 0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"INSERT INTO Inmuebles ({nameof(Inmueble.PropietarioId)}, {nameof(Inmueble.Direccion)}, {nameof(Inmueble.Tipo)}, {nameof(Inmueble.CantidadAmbientes)}, {nameof(Inmueble.Uso)}, {nameof(Inmueble.Coordenadas.Cx)},{nameof(Inmueble.Coordenadas.Cy)})
				VALUES (@{nameof(Inmueble.PropietarioId)}, @{nameof(Inmueble.Direccion)}, @{nameof(Inmueble.Tipo)}, @{nameof(Inmueble.CantidadAmbientes)}, @{nameof(Inmueble.Uso)}, @{nameof(Inmueble.Coordenadas.Cx)}, @{nameof(Inmueble.Coordenadas.Cy)});
				SELECT LAST_INSERT_ID();";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inmueble.PropietarioId)}", inmueble.PropietarioId.Id);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Direccion)}", inmueble.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Tipo)}", inmueble.Tipo);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.CantidadAmbientes)}", inmueble.CantidadAmbientes);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.Cx)}", inmueble.Coordenadas.Cx);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.Cy)}", inmueble.Coordenadas.Cy);
				

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				inmueble.Id = id;
				connection.Close();
			}
		}
		return id;
	}


	public int ModificaInquilino(Inmueble inmueble)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE Inquilinos
				SET {nameof(Inmueble.PropietarioId)} = @{nameof(Inmueble.PropietarioId)},
				{nameof(Inmueble.Direccion)} = @{nameof(Inmueble.Direccion)},
				{nameof(Inmueble.Tipo)} = @{nameof(Inmueble.Tipo)},
				{nameof(Inmueble.CantidadAmbientes)} = @{nameof(Inmueble.CantidadAmbientes)},
				{nameof(Inmueble.Uso)} = @{nameof(Inmueble.Uso)},
				{nameof(Inmueble.Coordenadas.Cx)} = @{nameof(Inmueble.Coordenadas.Cx)},
				{nameof(Inmueble.Coordenadas.Cy)} = @{nameof(Inmueble.Coordenadas.Cy)}
				WHERE {nameof(Inmueble.Id)} = @{nameof(Inmueble.Id)};";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inmueble.PropietarioId)}", inmueble.PropietarioId.Id);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Direccion)}", inmueble.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Tipo)}", inmueble.Tipo);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.CantidadAmbientes)}", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.Cx)}", inmueble.Coordenadas.Cx);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Coordenadas.Cy)}", inmueble.Coordenadas.Cy);
				command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", inmueble.Id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}


	public int EliminaInquilino(int id)
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
			var sql = @$"SELECT {nameof(Inmueble.Id)}, {nameof(Inmueble.PropietarioId)},{nameof(Inmueble.Direccion)},{nameof(Inmueble.Tipo)}, {nameof(Inmueble.CantidadAmbientes)},{nameof(Inmueble.Uso)},{nameof(Inmueble.Coordenadas.Cx)},{nameof(Inmueble.Coordenadas.Cy)}
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
					var coordenada=new Coordenada(reader.GetDecimal(nameof(Inmueble.Coordenadas.Cx)),reader.GetDecimal(nameof(Inmueble.Coordenadas.Cy)));
					while(reader.Read())
					{
						Inmuebles.Add(new Inmueble
						{   
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
							PropietarioId = propietario,
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Tipo = reader.GetString(nameof(Inmueble.Tipo)),
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
	
}

using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioPropietario:RepositorioBase
{
	//readonly string ConnectionString = "Server=localhost;Database=InmobiliariaGutierrez;User=root;Password=;";

	public RepositorioPropietario():base()
	{
		
	}

	public Propietario? GetPropietario(int id)
	{
		Propietario? Propietario = null;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.DNI)},{nameof(Propietario.Nombre)},{nameof(Propietario.Apellido)}, {nameof(Propietario.Email)},{nameof(Propietario.Telefono)}
			             FROM Propietarios
			             WHERE {nameof(Propietario.Id)} = @{nameof(Propietario.Id)};
                         ";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Propietario.Id)}", id);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					if(reader.Read())
					{
						Propietario = new Propietario
						{
							Id = reader.GetInt32(nameof(Propietario.Id)),
							DNI = reader.GetString(nameof(Propietario.DNI)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
							Email = reader.GetString(nameof(Propietario.Email)),
							Telefono = reader.GetString(nameof(Propietario.Telefono)),
						};
					}
				}
			}
            connection.Close();
		}
		return Propietario;
	}


	public IList<Propietario> GetPropietarios()
	{
		var Propietarios = new List<Propietario>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.DNI)},{nameof(Propietario.Nombre)},{nameof(Propietario.Apellido)}, {nameof(Propietario.Email)},{nameof(Propietario.Telefono)}
			             FROM Propietarios;
                         ";
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{
						Propietarios.Add(new Propietario
						{
							Id = reader.GetInt32(nameof(Propietario.Id)),
							DNI = reader.GetString(nameof(Propietario.DNI)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
							Email = reader.GetString(nameof(Propietario.Email)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),

							
							//Tipo = (TipoInquilino)reader.GetInt32(nameof(Inquilino.Tipo))
						});
						
		                Console.WriteLine("----------------------------------------------------------¡Hola, mundo!");
		                Console.WriteLine(Propietarios[^1]);
					}
				}
                
			}
             connection.Close();
		}
		
		return Propietarios;
	}

	public int AltaPropietario(Propietario propietario){
		int id = 0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"INSERT INTO Propietarios ({nameof(Propietario.DNI)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Email)}, {nameof(Propietario.Telefono)})
				VALUES (@{nameof(Propietario.DNI)}, @{nameof(Propietario.Nombre)}, @{nameof(Propietario.Apellido)}, @{nameof(Propietario.Email)}, @{nameof(Propietario.Telefono)});
				SELECT LAST_INSERT_ID();";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Propietario.DNI)}", propietario.DNI);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
				command.Parameters.AddWithValue($"@{nameof(Propietario.Email)}", propietario.Email);
				command.Parameters.AddWithValue($"@{nameof(Propietario.Telefono)}", propietario.Telefono);
				

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				propietario.Id = id;
				connection.Close();
			}
		}
		return id;
	}


	public int ModificaPropietario(Propietario propietario)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE Propietarios
				SET {nameof(Propietario.DNI)} = @{nameof(Propietario.DNI)},
				{nameof(Propietario.Nombre)} = @{nameof(Propietario.Nombre)},
				{nameof(Propietario.Apellido)} = @{nameof(Propietario.Apellido)},
				{nameof(Propietario.Email)} = @{nameof(Propietario.Email)},
				{nameof(Propietario.Telefono)} = @{nameof(Propietario.Telefono)}
				WHERE {nameof(Propietario.Id)} = @{nameof(Propietario.Id)};";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Propietario.DNI)}", propietario.DNI);
				command.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Email)}", propietario.Email);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Telefono)}", propietario.Telefono);
				command.Parameters.AddWithValue($"@{nameof(Propietario.Id)}", propietario.Id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}


	public int EliminaPropietario(int id)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"DELETE FROM Propietarios
				WHERE {nameof(Propietario.Id)} = @{nameof(Propietario.Id)}";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Propietario.Id)}", id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}
}

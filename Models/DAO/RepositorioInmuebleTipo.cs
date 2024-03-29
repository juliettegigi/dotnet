using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioInmuebleTipo:RepositorioBase
{
	//readonly string ConnectionString = "Server=localhost;Database=InmobiliariaGutierrez;User=root;Password=;";

	public RepositorioInmuebleTipo():base()
	{
		
	}

	public InmuebleTipo? GetInmuebleTipo(int id)
	{
		InmuebleTipo inmuebleTipo = null;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(InmuebleTipo.Id)}, 
                                {nameof(InmuebleTipo.Tipo)}
			             FROM InmuebleTipos
			             WHERE {nameof(InmuebleTipo.Id)} = @{nameof(InmuebleTipo.Id)};
                         ";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(InmuebleTipo.Id)}", id);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					if(reader.Read())
					{
						inmuebleTipo = new InmuebleTipo
						{
							Id = reader.GetInt32(nameof(InmuebleTipo.Id)),
							Tipo = reader.GetString(nameof(InmuebleTipo.Tipo))
						};
					}
				}
			}
            connection.Close();
		}
		return inmuebleTipo;
	}


	public IList<InmuebleTipo> GetInmuebleTipos()
	{
		var inmuebleTipos = new List<InmuebleTipo>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(InmuebleTipo.Id)}, 
                                {nameof(InmuebleTipo.Tipo)}
			             FROM InmuebleTipos;
                         ";

			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{
						inmuebleTipos.Add(new InmuebleTipo
						{
							Id = reader.GetInt32(nameof(InmuebleTipo.Id)),
							Tipo = reader.GetString(nameof(InmuebleTipo.Tipo))
						});
					}
				}
                
			}
             connection.Close();
		}
		return inmuebleTipos;
	}

	public int AltaInmuebleTipo(InmuebleTipo inmuebleTipo)
	{   
        Console.WriteLine(inmuebleTipo);
		int id = 0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"INSERT INTO InmuebleTipos ({nameof(InmuebleTipo.Tipo)})
				         VALUES (@{nameof(InmuebleTipo.Tipo)});";

			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(InmuebleTipo.Tipo)}", inmuebleTipo.Tipo);
				

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				inmuebleTipo.Id = id;
				connection.Close();
			}
		}
		return id;
	}


	public int ModificaInmuebleTipo(InmuebleTipo inmuebleTipo)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE InmuebleTipos
				SET {nameof(InmuebleTipo.Tipo)} = @{nameof(InmuebleTipo.Tipo)}
				WHERE {nameof(InmuebleTipo.Id)} = @{nameof(InmuebleTipo.Id)};";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(InmuebleTipo.Tipo)}", inmuebleTipo.Tipo);
				command.Parameters.AddWithValue($"@{nameof(InmuebleTipo.Id)}", inmuebleTipo.Id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}


	public int EliminaInmuebleTipo(int id)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"DELETE FROM InmuebleTipos
				WHERE {nameof(InmuebleTipo.Id)} = @{nameof(InmuebleTipo.Id)}";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(InmuebleTipo.Id)}", id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}



	
}

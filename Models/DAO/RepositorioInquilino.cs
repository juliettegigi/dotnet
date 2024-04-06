using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioInquilino:RepositorioBase
{
	//readonly string ConnectionString = "Server=localhost;Database=InmobiliariaGutierrez;User=root;Password=;";

	public RepositorioInquilino():base()
	{
		
	}

	public Inquilino? GetInquilino(int id)
	{
		Inquilino inquilino = null;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Inquilino.Id)}, {nameof(Inquilino.DNI)},{nameof(Inquilino.Nombre)},{nameof(Inquilino.Apellido)}, {nameof(Inquilino.Email)},{nameof(Inquilino.Telefono)},{nameof(Inquilino.Domicilio)}
			             FROM Inquilinos
			             WHERE {nameof(Inquilino.Id)} = @{nameof(Inquilino.Id)};
                         ";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Id)}", id);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					if(reader.Read())
					{
						inquilino = new Inquilino
						{
							Id = reader.GetInt32(nameof(Inquilino.Id)),
							DNI = reader.GetString(nameof(Inquilino.DNI)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
							Email = reader.GetString(nameof(Inquilino.Email)),
							Telefono = reader.GetString(nameof(Inquilino.Telefono)),
							Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),
						};
					}
				}
			}
            connection.Close();
		}
		return inquilino;
	}


	public IList<Inquilino> GetInquilinos()
	{
		var inquilinos = new List<Inquilino>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Inquilino.Id)}, {nameof(Inquilino.DNI)},{nameof(Inquilino.Nombre)},{nameof(Inquilino.Apellido)}, {nameof(Inquilino.Email)},{nameof(Inquilino.Telefono)},{nameof(Inquilino.Domicilio)}
			             FROM Inquilinos;
                         ";
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{   
				connection.Open();
				using(var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{
						inquilinos.Add(new Inquilino
						{
							Id = reader.GetInt32(nameof(Inquilino.Id)),
							DNI = reader.GetString(nameof(Inquilino.DNI)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
							Email = reader.GetString(nameof(Inquilino.Email)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),

							
							//Tipo = (TipoInquilino)reader.GetInt32(nameof(Inquilino.Tipo))
						});
					}
				}
                
			}
             connection.Close();
		}
		return inquilinos;
	}



// ****************************************************************************************************
public IList<Inquilino>  BuscarPorTodosLosCampos(string input){
    List<Inquilino> inquilinos =new List<Inquilino>();
	using(var connection=new MySqlConnection(ConnectionString))
	{
		var sql=@$"SELECT id,dni,nombre,apellido,telefono,domicilio,email
                   FROM inquilinos
                  WHERE id LIKE @input OR
                          dni LIKE CONCAT('%', @input, '%') OR
                          nombre LIKE CONCAT('%', @input, '%') OR
                          apellido LIKE CONCAT('%', @input, '%') OR
                          telefono LIKE CONCAT('%', @input, '%') OR
                          domicilio LIKE CONCAT('%', @input, '%') OR
                          email LIKE CONCAT('%', @input, '%');
				";
	    	using(var command = new MySqlCommand(sql, connection))
			{   command.Parameters.AddWithValue($"@input", input);
				connection.Open();
				using(var reader = command.ExecuteReader())
				{  
					  while(reader.Read())
					  {
					  	inquilinos.Add(new Inquilino
					  	{
					  		Id = reader.GetInt32(nameof(Inquilino.Id)),
					  		DNI = reader.GetString(nameof(Inquilino.DNI)),
                              Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                              Apellido = reader.GetString(nameof(Inquilino.Apellido)),
					  		Email = reader.GetString(nameof(Inquilino.Email)),
                              Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                              Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),
  
					  		
					  		//Tipo = (TipoInquilino)reader.GetInt32(nameof(Inquilino.Tipo))
					  	});
					  
					}
				}
                
			}
             connection.Close();			
	}
	return inquilinos;
}




	public int AltaInquilino(Inquilino inquilino)
	{  
		int id = 0;
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"INSERT INTO Inquilinos ({nameof(Inquilino.DNI)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Email)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.Domicilio)})
				VALUES (@{nameof(Inquilino.DNI)}, @{nameof(Inquilino.Nombre)}, @{nameof(Inquilino.Apellido)}, @{nameof(Inquilino.Email)}, @{nameof(Inquilino.Telefono)}, @{nameof(Inquilino.Domicilio)});
				SELECT LAST_INSERT_ID();";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inquilino.DNI)}", inquilino.DNI);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}", inquilino.Apellido);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Email)}", inquilino.Email);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Telefono)}", inquilino.Telefono);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Domicilio)}", inquilino.Domicilio);
				

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				inquilino.Id = id;
				var repContrato=new RepositorioContrato();
				inquilino.ListaContratos=repContrato.GetContratosXinquilino(id).ToList();
				connection.Close();
			}
		}
		return id;
	}


	public int ModificaInquilino(Inquilino inquilino)
	{
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"UPDATE Inquilinos
				SET {nameof(Inquilino.DNI)} = @{nameof(Inquilino.DNI)},
				{nameof(Inquilino.Nombre)} = @{nameof(Inquilino.Nombre)},
				{nameof(Inquilino.Apellido)} = @{nameof(Inquilino.Apellido)},
				{nameof(Inquilino.Email)} = @{nameof(Inquilino.Email)},
				{nameof(Inquilino.Telefono)} = @{nameof(Inquilino.Telefono)},
				{nameof(Inquilino.Domicilio)} = @{nameof(Inquilino.Domicilio)}
				WHERE {nameof(Inquilino.Id)} = @{nameof(Inquilino.Id)};";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inquilino.DNI)}", inquilino.DNI);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}", inquilino.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Email)}", inquilino.Email);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Telefono)}", inquilino.Telefono);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Domicilio)}", inquilino.Domicilio);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Id)}", inquilino.Id);
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
				WHERE {nameof(Inquilino.Id)} = @{nameof(Inquilino.Id)}";
			using(var command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Id)}", id);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return 0;
	}






	
}

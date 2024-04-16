using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;
using System.Data;

namespace InmobiliariaGutierrez.Models.DAO;

	//readonly string ConnectionString = "Server=localhost;Database=InmobiliariaGutierrez;User=root;Password=;";

public class RepositorioUsuario:RepositorioBase
{


	public RepositorioUsuario():base()
	{
		
	}

	public int AltaUsuario(Usuario e)
		{   Console.WriteLine($"zaaaco {e.Rol} ");
			int res = -1;
			using (var connection = new MySqlConnection(ConnectionString))
			{
				string sql = @"INSERT INTO Usuarios 
					(Nombre, Apellido, Avatar, Email, Pass, Rol) 
					VALUES (@nombre, @apellido, @avatar, @email, @Pass, @rol);
					SELECT LAST_INSERT_ID();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					if (String.IsNullOrEmpty(e.Avatar))
						command.Parameters.AddWithValue("@avatar", DBNull.Value);
					else
						command.Parameters.AddWithValue("@avatar", e.Avatar);
					command.Parameters.AddWithValue("@email", e.Email);
					command.Parameters.AddWithValue("@Pass", e.Pass);
					command.Parameters.AddWithValue("@rol", e.Rol);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					e.Id = res;
					connection.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (var connection = new MySqlConnection(ConnectionString))
			{
				string sql = "DELETE FROM Usuarios WHERE Id = @id";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Modificacion(Usuario e)
		{
			int res = -1;
			using (var connection = new MySqlConnection(ConnectionString))
			{
				string sql = @"UPDATE Usuarios 
					SET Nombre=@nombre, Apellido=@apellido, Avatar=@avatar, Email=@email, Pass=@Pass, Rol=@rol
					WHERE Id = @id";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					command.Parameters.AddWithValue("@avatar", e.Avatar);
					command.Parameters.AddWithValue("@email", e.Email);
					command.Parameters.AddWithValue("@Pass", e.Pass);
					command.Parameters.AddWithValue("@rol", e.Rol);
					command.Parameters.AddWithValue("@id", e.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}


public Usuario ObtenerPorId(int id)
{
    Usuario usuario = null;

    using (var connection = new MySqlConnection(ConnectionString))
    {
        string sql = @"SELECT Id, Nombre, Apellido, Avatar, Email, Pass, Rol FROM Usuarios WHERE Id = @id";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = reader.GetInt32("Id");
                    usuario.Nombre = reader.GetString("Nombre");
                    usuario.Apellido = reader.GetString("Apellido");
                    if (!reader.IsDBNull("Avatar"))
                        usuario.Avatar = reader.GetString("Avatar");
                    usuario.Email = reader.GetString("Email");
                    usuario.Pass = reader.GetString("Pass");
                    usuario.Rol = reader.GetInt32("Rol");
                }
            }
            connection.Close();
        }
    }

    return usuario;
}

		//---------------------------------------------------------------------------------------------------
      public List<Usuario> ObtenerTodos()
{
    List<Usuario> usuarios = new List<Usuario>();

    using (var connection = new MySqlConnection(ConnectionString))
    {
        string sql = @"SELECT Id, Nombre, Apellido, Avatar, Email, Pass, Rol FROM Usuarios";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.CommandType = CommandType.Text;
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = reader.GetInt32("Id");
                    usuario.Nombre = reader.GetString("Nombre");
                    usuario.Apellido = reader.GetString("Apellido");
                    if (!reader.IsDBNull("Avatar"))
                        usuario.Avatar = reader.GetString("Avatar");
                    usuario.Email = reader.GetString("Email");
                    usuario.Pass = reader.GetString("Pass");
                    usuario.Rol = reader.GetInt32("Rol");
                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }
    }

    return usuarios;
}






		//-------------------------------------------------------------------------------------------------

		public Usuario ObtenerPorEmail(string email)
		{
			Usuario? e = null;
			using (var connection = new MySqlConnection(ConnectionString))
			{
				string sql = @"SELECT
					Id, Nombre, Apellido, Avatar, Email, Pass, Rol FROM Usuarios
					WHERE Email=@email";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@email",  email);
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32("Id"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Avatar = reader.GetString("Avatar"),
							Email = reader.GetString("Email"),
							Pass = reader.GetString("Pass"),
							Rol = reader.GetInt32("Rol"),
						};
					}
					connection.Close();
				}
			}
			return e;
		}


		public void ActualizarUsuario(Usuario usuario)
{
    using (var connection = new MySqlConnection(ConnectionString))
    {
        string sql = @"UPDATE Usuarios 
                       SET Nombre = @nombre, 
                           Apellido = @apellido, 
                           Avatar = @avatar, 
                           Email = @email,
                           Pass = @pass,
                           Rol = @rol
                       WHERE Id = @id";
                       
        using (var command = new MySqlCommand(sql, connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@apellido", usuario.Apellido);
            command.Parameters.AddWithValue("@avatar", (object)usuario.Avatar ?? DBNull.Value);
            command.Parameters.AddWithValue("@email", usuario.Email);
            command.Parameters.AddWithValue("@pass", (object)usuario.Pass ?? DBNull.Value);
            command.Parameters.AddWithValue("@rol", usuario.Rol);
            command.Parameters.AddWithValue("@id", usuario.Id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

		
	}


	


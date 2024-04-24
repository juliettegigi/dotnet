using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;


namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioAuditoriap:RepositorioBase
{
	

	public RepositorioAuditoriap():base()
	{
		
	}



 // Asegúrate de importar el espacio de nombres adecuado

public void InsertAucitoriac(Auditoriac auditoriac)
{
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"INSERT INTO auditoriacontrato (id_usuario, fechaInicio, fechaCancelacion)
                    VALUES (@IdUsuario, @FechaInicio, @FechaCancelacion)";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@IdUsuario", auditoriac.UsuarioId.Id); // Reemplaza con el nombre correcto de la columna para el ID del usuario
            command.Parameters.AddWithValue("@FechaInicio", auditoriac.FechaInicio);
            command.Parameters.AddWithValue("@FechaCancelacion", auditoriac.FechaCancelacion);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
}
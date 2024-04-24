using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;


namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioAuditoriac:RepositorioBase
{
	

	public RepositorioAuditoriac():base()
	{
		
	}


    public void InsertAucitoriac(Auditoriac auditoriac)
{
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"INSERT INTO auditoriacontrato (id_usuario, fechaInicio, fechaCancelacion,id_contrato)
                    VALUES (@IdUsuario, @FechaInicio, @FechaCancelacion, @IdContrato)";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@IdUsuario", auditoriac.UsuarioId.Id); // Reemplaza con el nombre correcto de la columna para el ID del usuario
            command.Parameters.AddWithValue("@FechaInicio", auditoriac.FechaInicio);
            command.Parameters.AddWithValue("@FechaCancelacion", auditoriac.FechaCancelacion);
            command.Parameters.AddWithValue("@IdContrato", auditoriac.ContratoId.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
}
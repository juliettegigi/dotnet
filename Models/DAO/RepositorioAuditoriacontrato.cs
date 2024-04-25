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


    public void InsertAucitoriac(Auditoriacontrato auditoriac)
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
public List<Auditoriacontrato> GetAuditoriasPorContratoId(int contratoId)
{
    List<Auditoriacontrato> auditorias = new List<Auditoriacontrato>();

    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"SELECT * FROM auditoriacontrato WHERE id_contrato = @ContratoId";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ContratoId", contratoId);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var auditoria = new Auditoriacontrato
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UsuarioId = new Usuario { Id = Convert.ToInt32(reader["id_usuario"]) },
                        FechaInicio = Convert.IsDBNull(reader["FechaInicio"]) ? null : (DateTime?)Convert.ToDateTime(reader["FechaInicio"]),
                        FechaCancelacion = Convert.IsDBNull(reader["fechaCancelacion"]) ? null : (DateTime?)Convert.ToDateTime(reader["fechaCancelacion"]),
                        ContratoId = new Contrato { Id = Convert.ToInt32(reader["id_contrato"]) }
                    };
                    auditorias.Add(auditoria);
                }
            }
        }
    }

    return auditorias;
}

}
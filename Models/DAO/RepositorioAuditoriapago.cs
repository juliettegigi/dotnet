using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;


namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioAuditoriapago:RepositorioBase
{
	

	public RepositorioAuditoriapago():base()
	{
		
	}



 // Asegúrate de importar el espacio de nombres adecuado

   public void InsertAuditoriapago(Auditoriapago auditoriap)
{   
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"INSERT INTO auditoriapagos (id_usuario, fechaPago, fechaCancelacion,numero_pago,id_contrato)
                    VALUES (@IdUsuario, @FechaInicio, @FechaCancelacion, @NumeroPago, @ContratoId)";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@IdUsuario", auditoriap.UsuarioId.Id); // Reemplaza con el nombre correcto de la columna para el ID del usuario
            command.Parameters.AddWithValue("@FechaInicio", auditoriap.FechaPago);
            command.Parameters.AddWithValue("@FechaCancelacion", auditoriap.FechaCancelacion);
             command.Parameters.AddWithValue("@NumeroPago", auditoriap.NumeroPago);
                 command.Parameters.AddWithValue("@ContratoId", auditoriap.ContratoId.Id);
             

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
public List<Auditoriapago> GetPorPago(int numeroPago)
{
    List<Auditoriapago> pagos = new List<Auditoriapago>();

    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"SELECT * FROM auditoriapagos WHERE numero_pago = @NumeroPago";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@NumeroPago", numeroPago);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var pago = new Auditoriapago
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UsuarioId = new Usuario { Id = Convert.ToInt32(reader["id_usuario"]) },
                        FechaPago = reader.IsDBNull(reader.GetOrdinal("fechaPago")) ? null : (DateTime?)reader["fechaPago"],
                        FechaCancelacion = reader.IsDBNull(reader.GetOrdinal("fechaCancelacion")) ? null : (DateTime?)reader["fechaCancelacion"],
                        NumeroPago = Convert.ToInt32(reader["numero_pago"]),
                        ContratoId = new Contrato { Id = Convert.ToInt32(reader["id_contrato"]) }
                    };
                    pagos.Add(pago);
                }
            }
        }
    }
    return pagos;
}


}
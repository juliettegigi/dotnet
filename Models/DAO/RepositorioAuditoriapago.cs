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
}
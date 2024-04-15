using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;


namespace InmobiliariaGutierrez.Models.DAO;

public class RepositorioPago:RepositorioBase
{
	

	public RepositorioPago():base()
	{
		
	}

public IList<Pago>  GetPago(int id)
{
    Pago pago = null;
	IList<Pago> pagos = new List<Pago>();
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"SELECT NumeroPago,
                            ContratoId,
                            Fecha,
                            FechaPago,
                            Importe
                     FROM Pagos
                     WHERE ContratoId = @Id";
                     
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
            while (reader.Read())
                {
                    pago = new Pago
                    {
                       NumeroPago = reader.GetInt32(reader.GetOrdinal(nameof(Pago.NumeroPago))),
                        ContratoId = reader.GetInt32(reader.GetOrdinal(nameof(Pago.ContratoId))),
                        Fecha = reader.GetDateTime(reader.GetOrdinal(nameof(Pago.Fecha))),
						FechaPago = !reader.IsDBNull(reader.GetOrdinal(nameof(Pago.FechaPago))) ? reader.GetDateTime(reader.GetOrdinal(nameof(Pago.FechaPago))) : DateTime.MinValue,
                        Importe = reader.GetDecimal(reader.GetOrdinal(nameof(Pago.Importe)))
                    };
					pagos.Add(pago);
                }
            }
        }
        connection.Close();
    }
    return pagos;
}

public void InsertPago(Pago pago)
{
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"INSERT INTO Pagos (NumeroPago, ContratoId, Fecha, FechaPago, Importe)
                    VALUES (@NumeroPago, @ContratoId, @Fecha, @FechaPago, @Importe)";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@NumeroPago", pago.NumeroPago);
            command.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
            command.Parameters.AddWithValue("@Fecha", pago.Fecha);
            command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
            command.Parameters.AddWithValue("@Importe", pago.Importe);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
public decimal UpdatePago(Pago pago)
{
    decimal importeBaseDatos = 0;

    // Verificar si el importe es cero
    if (pago.Importe == 0)
    {
        // Consulta SQL para obtener el importe de la base de datos
        var sqlImporte = @"SELECT Importe FROM Pagos WHERE NumeroPago = @NumeroPago";

        using (var connection = new MySqlConnection(ConnectionString))
        {
            using (var commandImporte = new MySqlCommand(sqlImporte, connection))
            {
                commandImporte.Parameters.AddWithValue("@NumeroPago", pago.NumeroPago);

                connection.Open();
                var importe = commandImporte.ExecuteScalar();
                if (importe != null && importe != DBNull.Value)
                {
                    // Asignar el importe obtenido de la base de datos a la variable importeBaseDatos
                    importeBaseDatos = Convert.ToDecimal(importe);
                }
            }
        }
    }

    // Actualizar el pago en la base de datos
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"UPDATE Pagos 
                    SET ContratoId = @ContratoId,
                        FechaPago = @FechaPago";

        // Agregar el parámetro Importe solo si es diferente de cero
        if (pago.Importe != 0)
            sql += ", Importe = @Importe";

        sql += " WHERE NumeroPago = @NumeroPago";

        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@NumeroPago", pago.NumeroPago);
            command.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
            command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);

            // Agregar el parámetro Importe solo si es diferente de cero
            if (pago.Importe != 0)
                command.Parameters.AddWithValue("@Importe", pago.Importe);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Retornar el importe obtenido de la base de datos
    return importeBaseDatos;
}


	public IList<Contrato> GetContratos()
	{
		var Contratos = new List<Contrato>();
		using(var connection = new MySqlConnection(ConnectionString))
		{
			var sql = @$"SELECT {nameof(Contrato.Id)},
								{nameof(Contrato.InquilinoId)},
								{nameof(Contrato.InmuebleId)},
								{nameof(Contrato.FechaInicio)},
								{nameof(Contrato.FechaFin)},
								{nameof(Contrato.FechaFinAnticipada)},
								{nameof(Contrato.PrecioXmes)}
			             FROM Contratos
                         WHERE  {nameof(Contrato.Estado)} = true ;/
                         ";
			 //var sql=""
			using(var command = new MySqlCommand(sql, connection))
			{
				connection.Open();
				using(var reader = command.ExecuteReader())
				{   var rinq=new RepositorioInquilino();
				    var rinm=new RepositorioInmueble();
					while(reader.Read())
					{    
						Contratos.Add(new Contrato
						{   Id = reader.GetInt32(nameof(Contrato.Id)),
							InquilinoId = rinq.GetInquilino(reader.GetInt32(nameof(Contrato.InquilinoId))),
                            InmuebleId = rinm.GetInmueble(reader.GetInt32(nameof(Contrato.InmuebleId))),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
							FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
							FechaFinAnticipada = reader.GetDateTime(nameof(Contrato.FechaFinAnticipada)),
							Estado = true

						});
					}
				}
                
			}
             connection.Close();
		}
		return Contratos;
	}

	

public IList<(Pago pago, Contrato contrato, Inquilino inquilino)> GetDetallesPagosPorContrato(int contratoId)
{
    var detallesPagos = new List<(Pago pago, Contrato contrato, Inquilino inquilino)>();
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @$"SELECT {GetCamposPago("pagos", "contratoId", "ContratoIdPago")},
                            {getCamposContrato("contratos", "id", "IdContrato")},
                            {getCamposInquilino("inquilinos", "Id", "IdInquilino")}
                     FROM pagos
                     INNER JOIN contratos ON pagos.contratoId = contratos.id
                     INNER JOIN inquilinos ON contratos.inquilinoId = inquilinos.Id
                     WHERE pagos.contratoId = @ContratoId";

        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ContratoId", contratoId);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var pago = new Pago
                    {
                        NumeroPago = reader.GetInt32("NumeroPago"),
                        ContratoId = reader.GetInt32("ContratoId"),
                        Fecha = reader.GetDateTime("Fecha"),
                        FechaPago = reader.GetDateTime("FechaPago"),
                        Importe = reader.GetDecimal("Importe")
                    };

                    var contrato = new Contrato
                    {
                             Id = reader.GetInt32(nameof(Contrato.Id)),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                            FechaFin = reader.GetDateTime(nameof(Contrato.FechaFin)),
                            PrecioXmes = reader.GetDecimal(nameof(Contrato.PrecioXmes)),
                            Estado = reader.GetBoolean(nameof(Contrato.Estado)),
                    };

                    var inquilino = new Inquilino
                    {
                        Id = reader.GetInt32("IdInquilino"),
                        Nombre = reader.GetString("Nombre"),
                        Apellido = reader.GetString("Apellido"),
                        // Completar mapeo de otros campos del inquilino
                    };

                    detallesPagos.Add((pago, contrato, inquilino));
                }
            }
        }
    }
    return detallesPagos;
}

    
}










 
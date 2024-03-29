using System.Configuration;
using System.Data;
using InmobiliariaGutierrez.Models.VO;
using MySql.Data.MySqlClient;

namespace InmobiliariaGutierrez.Models.DAO;

public abstract class RepositorioBase
	{
		protected readonly IConfiguration configuration;
		protected readonly string? ConnectionString ;

		protected RepositorioBase()
		{  ConnectionString="Server=localhost;Database=inmobiliariadotnet;User=root;Password=;";
		}
	}
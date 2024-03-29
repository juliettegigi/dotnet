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


		public String getCamposContrato(String nombreTabla){
         return @$"{nombreTabla}.id as idContrato, 
	               {nombreTabla}.inquilinoId,
				   {nombreTabla}.inmuebleId,
				   {nombreTabla}.fechaInicio,
				   {nombreTabla}.fechaFin,
				   {nombreTabla}.fechaFinAnticipada,
				   {nombreTabla}.precioXmes,
				   {nombreTabla}.estado";
	}
    public String getCamposInquilino(String nombreTabla){
             return @$"{nombreTabla}.id as idInquilino,
	                   {nombreTabla}.dni as dniInq,
					   {nombreTabla}.nombre as nombreInq,
					   {nombreTabla}.apellido as apellidoInq,
					   {nombreTabla}.telefono as telefonoInq,
					   {nombreTabla}.email as emailInq,
					   {nombreTabla}.domicilio as domicilioInq";
	}
	public String getCamposInmueble(String nombreTabla){
          return  @$"
	                  {nombreTabla}.id as idInmueble,
	                  {nombreTabla}.propietarioId,
	                  {nombreTabla}.inmuebleTipoId,
	                  {nombreTabla}.direccion,
	                  {nombreTabla}.cantidadAmbientes,
	                  {nombreTabla}.uso,
	                  {nombreTabla}.precioBase,
	                  {nombreTabla}.cLatitud,
	                  {nombreTabla}.cLongitud,
					  {nombreTabla}.suspendido, 
					  {nombreTabla}.disponible
                     ";
	               }

	public String getCamposInmuebleTipo(String nombreTabla){
		return @$"{nombreTabla}.id as idInmuebleTipo,
                  {nombreTabla}.tipo";
	}			

	public String getCamposPropietario(String nombreTabla){
		return @$"{nombreTabla}.id as idPropietario,
                  {nombreTabla}.dni,
                  {nombreTabla}.nombre,
                  {nombreTabla}.apellido,
                  {nombreTabla}.telefono,
                  {nombreTabla}.email,
                  {nombreTabla}.domicilio";
	}	


	}
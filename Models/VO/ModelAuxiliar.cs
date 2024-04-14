using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaGutierrez.Models.VO
{
    public class ModelAuxiliar
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public IList<Pago> Pagos { get; set; }
        
        // Campos para los contratos
  

        public override string ToString() 
        {
            return $"Nombre: {Nombre}, Apellido: {Apellido}, Dni: {Dni}, Pagos: {Pagos}";
        }
    }
}

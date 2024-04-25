using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGutierrez.Models.VO
{
    public class AuditoriaViewModelPago
    {
        public Contrato? Contrato { get; set; }

        public List<AuditoriaDataPago?> AuditoriaDatapago { get; set; }
    }
}

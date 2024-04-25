using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGutierrez.Models.VO
{
    public class AuditoriaViewModelContrato
    {
        public Contrato Contrato { get; set; }

        public List<AuditoriaData> AuditoriaData { get; set; }
    }
}
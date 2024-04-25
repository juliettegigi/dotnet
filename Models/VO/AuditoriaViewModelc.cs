using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InmobiliariaGutierrez.Models.VO;

    public class AuditoriaViewModelContrato
    {
    
        public Usuario Usuario { get; set; }
        public Contrato Contrato { get; set; }

        public IList<AuditoriaData> AuditoriaData { get; set;}
    
        
    }

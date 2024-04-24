using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InmobiliariaGutierrez.Models.VO;

    public class AuditoriaViewModel
    {
    
        public Usuario Usuario { get; set; }
        public Contrato Contrato { get; set; }

        public IList<Auditoriapago> Auditoriapago { get; set;}
    
        
    }

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_NOTICIA_CURTIDA
    {       
        [Key]
        public int ID { get; set; }
        public int ID_NOTICIA { get; set; }
        public int ID_APOIADOR { get; set; }
    }
}

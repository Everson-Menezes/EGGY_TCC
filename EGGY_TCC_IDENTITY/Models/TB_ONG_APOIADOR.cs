using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_ONG_APOIADOR
    {
        [Key]
        public int ID_ONG_APOIADOR { get; set; }
        [ForeignKey("TB_ONG")]
        public int ID_ONG { get; set; }
        [ForeignKey("TB_APOIADOR")]
        public int ID_APOIADOR { get; set; }

    }
}

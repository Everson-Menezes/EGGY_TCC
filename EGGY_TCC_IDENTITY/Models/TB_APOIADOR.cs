using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_APOIADOR
    {
        [Key]
        public int ID_APOIADOR { get; set; }
        public string DE_NOME { get; set; }
        public string DE_EMAIL { get; set; }
        public bool BL_RECEBE_NOVIDADE { get; set; }
        [ForeignKey("TB_USUARIO")]
        public int ID_USUARIO { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime DT_ALTERACAO { get; set; }
        public DateTime? DT_INATIVACAO { get; set; }
    }
}

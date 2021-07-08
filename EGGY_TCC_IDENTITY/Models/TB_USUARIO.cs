using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_USUARIO
    {
        [Key]
        public int ID_USUARIO { get; set; }        
        public string DE_SENHA { get; set; }
        public string DE_LOGIN { get; set; }
        public string DE_NOME { get; set; }
        public string DE_EMAIL { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime DT_ALTERACAO { get; set; }
        public DateTime? DT_INATIVACAO { get; set; }
        [ForeignKey("TB_APOIADOR")]
        public int ID_APOIADOR { get; set; }
        [ForeignKey("TB_STATUS_USUARIO")]
        public int ID_STATUS { get; set; }
        [ForeignKey("TB_NIVEL_ACESSO")]
        public int ID_NIVEL_ACESSO { get; set; }
    }
}

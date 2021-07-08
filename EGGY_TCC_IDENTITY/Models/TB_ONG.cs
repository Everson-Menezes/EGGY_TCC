using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_ONG
    {        
        [Key]
        public int ID_ONG { get; set; }
        [ForeignKey("TB_USUARIO")]
        public int ID_USUARIO_ADM { get; set; }
        public string DE_LOGIN_USUARIO_ADM { get; set; }
        public string DE_REPRESENTANTE { get; set; }
        public string DE_EMAIL { get; set; }
        public string DE_TELEFONE { get; set; }
        public string DE_CNPJ { get; set; }
        public string DE_RAZAO_SOCIAL { get; set; }
        public string DE_NOME_FANTASIA { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime DT_ALTERACAO { get; set; }
        public DateTime? DT_INATIVACAO { get; set; }
        [ForeignKey("TB_STATUS_ONG")]
        public int ID_STATUS { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_NOTICIA
    {
        [Key]
        public int ID_NOTICIA { get; set; }
        [ForeignKey("TB_IMAGEM")]
        public int ID_IMAGEM { get; set; }
        public string DE_NOME_FANTASIA { get; set; }
        public string DE_TITULO { get; set; }
        public string DE_CONTEUDO { get; set; }
        public DateTime DT_POSTAGEM{ get; set; }
        public int NU_CURTIDAS { get; set; }

    }
}

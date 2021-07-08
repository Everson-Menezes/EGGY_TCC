using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_IMAGEM
    {
        [Key]
        public int ID_IMAGEM { get; set; }
        public string DE_TITULO { get; set; }
        public byte [] ARQUIVO { get; set; }
        [ForeignKey ("TB_ONG")]
        public int ID_ONG { get; set; }
    }
}
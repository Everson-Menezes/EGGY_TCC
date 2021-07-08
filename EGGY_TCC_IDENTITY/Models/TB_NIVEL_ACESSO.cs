using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_NIVEL_ACESSO
    {
        [Key]
        public int ID_NIVEL { get; set; }
        public string DE_NIVEL { get; set; }
    }
}

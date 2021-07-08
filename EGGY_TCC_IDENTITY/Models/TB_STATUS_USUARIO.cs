using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Models
{
    public class TB_STATUS_USUARIO
    {
        [Key]
        public int ID_STATUS { get; set; }
        public string DE_STATUS { get; set; }
    }
}

using System;
using System.ComponentModel;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class ApoidorViewModel 
    {
        public int ID_APOIADOR { get; set; }
        [DisplayName("Nome")]
        public string DE_NOME { get; set; }
        [DisplayName("E-mail")]
        public string DE_EMAIL { get; set; }
        [DisplayName("Gostaria de receber novidades?")]
        public bool BL_RECEBE_NOVIDADE { get; set; }
        public int ID_USUARIO { get; set; }
        public UsuarioViewModel usuarioViewModel { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime DT_ALTERACAO { get; set; }
        public DateTime? DT_INATIVACAO { get; set; }
    }
}
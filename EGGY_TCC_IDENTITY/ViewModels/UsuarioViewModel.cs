using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EGGY_TCC_IDENTITY.Models;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class UsuarioViewModel 
    {
        public int ID_USUARIO { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string DE_SENHA { get; set; }
        [DataType(DataType.Password)]
        [Compare("DE_SENHA", ErrorMessage = "As senhas não conferem")]
        [DisplayName("Confirmar Senha")]
        public string DE_CONFIRMA_SENHA { get; set; }
        [DisplayName("Login")]
        public string DE_LOGIN { get; set; }
        [DisplayName("Nome")]
        public string DE_NOME { get; set; }
        [DisplayName("E-Mail")]
        public string DE_EMAIL { get; set; }
        [DisplayName("Data de Cadastro")]
        public DateTime DT_CADASTRO { get; set; }
        [DisplayName("Data de Alteração")]
        public DateTime DT_ALTERACAO { get; set; }
        [DisplayName("Data de Inativação")]
        public DateTime? DT_INATIVACAO { get; set; }
        [DisplayName("Gostaria de receber novidades?")]
        public bool BL_RECEBE_NOVIDADE { get; set; }
        public int ID_APOIADOR { get; set; }
        public ApoidorViewModel APOIADOR { get; set; }
        public int ID_STATUS { get; set; }        
        public int ID_NIVEL_ACESSO { get; set; }

        public UsuarioViewModel()
        {
            APOIADOR = new ApoidorViewModel();
        }
               
    }
}

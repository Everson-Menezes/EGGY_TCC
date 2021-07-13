using EGGY_TCC_IDENTITY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class OngViewModel 
    {
        public int ID_Ong { get; set; }
        public string Representante { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [DisplayName("Usuário Admin")]
        public int ID_Usuario { get; set; }
        public UsuarioViewModel UsuarioAdm { get; set; }
        [DisplayName("Cadastrado em")]
        public DateTime DT_CADASTRO { get; set; }
        [DisplayName("Alterado em")]
        public DateTime DT_ALTERACAO { get; set; }
        [DisplayName("Gostaria de Receber Novidades?")]
        public bool BL_RECEBE_NOVIDADE { get; set; }
        public int ID_Status { get; set; }
        public string Mensagem { get; set; }

        public OngViewModel()
        {
            UsuarioAdm = new UsuarioViewModel();
        }
    }
}

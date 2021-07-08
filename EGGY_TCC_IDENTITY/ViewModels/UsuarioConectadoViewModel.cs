using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class UsuarioConectadoViewModel
    {
        public int ID_CONEXAO { get; set; }
        public int ID_USUARIO { get; set; }
        public UsuarioViewModel UsuarioViewModel { get; set; }

        public UsuarioConectadoViewModel()
        {
            UsuarioViewModel = new UsuarioViewModel();
        }
    }
}

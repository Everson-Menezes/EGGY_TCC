using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class RoleViewModels
    {        
        public string Id { get; set; }
        public string Perfil { get; set; }
        public List<string> UsuariosPerfil { get; set; }

        public RoleViewModels()
        {
            UsuariosPerfil = new List<string>();
        }
    }
}

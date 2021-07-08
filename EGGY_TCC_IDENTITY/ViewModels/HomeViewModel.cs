using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class HomeViewModel 
    {
        public int ID_Noticia { get; set; }
        [DisplayName("Título da Notícia")]
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        [DisplayName("Data da Postagem")]
        public DateTime DataPostagem { get; set; }
        public int ID_Ong { get; set; }
        public OngViewModel Ong { get; set; }
        [DisplayName("Número de Curtidas")]
        public int Num_Curtidas { get; set; }
        public int ID_Imagem { get; set; }
        public ImagemViewModel Imagem { get; set; }
    }
}

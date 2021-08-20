using EGGY_TCC_IDENTITY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class NoticiaViewModel 
    {
        public int ID_Noticia { get; set; }
        [DisplayName("Título da Notícia")]
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        [DisplayName("Data da Postagem")]
        public DateTime DataPostagem{ get; set; }
        [DisplayName("Nome Fantasia")]
        public int ID_Ong { get; set; }
        public TB_ONG Ong { get; set; }
        [DisplayName("Número de Curtidas")]
        public int Num_Curtidas { get; set; }
        [DisplayName("Imagem")]
        public int ID_Imagem { get; set; }
        public ImagemViewModel Imagem { get; set; }
        public List<OngViewModel> ongViewModels { get; set; }
        public bool UsuarioJaCurtiu { get; set; }
    }
}

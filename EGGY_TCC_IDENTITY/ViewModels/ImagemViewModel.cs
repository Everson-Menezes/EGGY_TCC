using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace EGGY_TCC_IDENTITY.ViewModels
{
    public class ImagemViewModel 
    {
        public int ID_Imagem { get; set; }
        [DisplayName("Título da Imagem")]
        public string Titulo { get; set; }
        public IFormFile Arquivo { get; set; }
        public string? TipoArquivo { get; set; }
        public string? CaminhoArquivo { get; set; }
        [DisplayName("Imagem")]
        public string imagemString { get; set; }
    }
}
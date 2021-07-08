using EGGY_TCC_IDENTITY.Data;
using EGGY_TCC_IDENTITY.Models;
using EGGY_TCC_IDENTITY.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {           
            IList<TB_NOTICIA> noticias = _context.TB_NOTICIA.OrderBy(x => x.NU_CURTIDAS).ToList();
            IList<HomeViewModel> destaques = new List<HomeViewModel>();

            foreach (var registro in noticias)
            {

                HomeViewModel obj = new HomeViewModel();
                obj.Conteudo = registro.DE_CONTEUDO;
                obj.DataPostagem = registro.DT_POSTAGEM;
                obj.Num_Curtidas = registro.NU_CURTIDAS;
                obj.Titulo = registro.DE_TITULO;
                obj.Ong = new OngViewModel();
                obj.Ong.NomeFantasia = registro.DE_NOME_FANTASIA;
                obj.ID_Imagem = registro.ID_IMAGEM;
                obj.Imagem = new ImagemViewModel();
                byte[] imagem = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == registro.ID_IMAGEM).Select(x => x.ARQUIVO).FirstOrDefault();
                obj.Imagem.imagemString = ConverterArquivoByteArrayEmBase64(imagem);
                destaques.Add(obj);
            }

            return View(destaques);
        }
        protected string ConverterArquivoByteArrayEmBase64(byte[] arq)
        {
            try
            {
                return "data:application/pdf;base64," + Convert.ToBase64String(arq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Sistema()
        {
            return View();
        }
        public IActionResult OngHome()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Erro", "Home");
        }
    }
}

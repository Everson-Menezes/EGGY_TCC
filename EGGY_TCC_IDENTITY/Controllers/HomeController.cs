using EGGY_TCC_IDENTITY.Data;
using EGGY_TCC_IDENTITY.Models;
using EGGY_TCC_IDENTITY.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            IList<HomeViewModel> homeViewModels = new List<HomeViewModel>();
            
            foreach (var registro in noticias)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.OngNoticia = new OngViewModel();
                obj.ImagemNoticia = new ImagemViewModel();
                obj.Conteudo = registro.DE_CONTEUDO;
                obj.DataPostagem = registro.DT_POSTAGEM;
                obj.Num_Curtidas = registro.NU_CURTIDAS;
                obj.Titulo = registro.DE_TITULO;
                
                obj.OngNoticia.NomeFantasia = registro.DE_NOME_FANTASIA;
                obj.ID_Imagem = registro.ID_IMAGEM;
                
                byte[] imagem = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == registro.ID_IMAGEM).Select(x => x.ARQUIVO).FirstOrDefault();
                obj.ImagemNoticia.imagemString = ConverterArquivoByteArrayEmBase64(imagem);
                homeViewModels.Add(obj);
            }

            ViewData["Mensagem"] = (ViewData["Mensagem"] is null) ? "Bem vido!" : ViewData["Mensagem"];
            return View(homeViewModels);
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
        [Authorize(Roles = "Master, Avançado, Básico")]
        public IActionResult OngHome()
        {
            var ongs = _context.TB_ONG.ToList().OrderBy(x => x.DE_RAZAO_SOCIAL);
            var ongViewModels = new List<OngViewModel>();
            
            foreach (var registro in ongs)
            {
                OngViewModel obj = new OngViewModel();
                obj.RazaoSocial = registro.DE_RAZAO_SOCIAL;
                obj.ID_Ong = registro.ID_ONG;
                ongViewModels.Add(obj);
            }
            return View(ongViewModels);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Erro");
        }
    }
}

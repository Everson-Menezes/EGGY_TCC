using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EGGY_TCC_IDENTITY.Data;
using EGGY_TCC_IDENTITY.Models;
using EGGY_TCC_IDENTITY.ViewModels;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace EGGY_TCC_IDENTITY.Controllers
{
    [Authorize(Roles = "Master, Avançado")]
    public class ImagemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImagemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Index()
        {
            IList<ImagemViewModel> imagemViewModel = new List<ImagemViewModel>();
            var imagem = await _context.TB_IMAGEM.ToListAsync();
            foreach (var registro in imagem)
            {
                var obj = new ImagemViewModel();
                obj.Titulo = registro.DE_TITULO;
                obj.imagemString = ConverterArquivoByteArrayEmBase64(registro.ARQUIVO);
                obj.ID_Imagem = registro.ID_IMAGEM;
                imagemViewModel.Add(obj);
            }
            return View(imagemViewModel);
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

        // GET: Imagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagem = await _context.TB_IMAGEM.FirstOrDefaultAsync(m => m.ID_IMAGEM == id);
            if (imagem == null)
            {
                return NotFound();
            }
            ImagemViewModel imagemViewModel = new ImagemViewModel();
            imagemViewModel.ID_Imagem = imagem.ID_IMAGEM;
            imagemViewModel.Titulo = imagem.DE_TITULO;
            imagemViewModel.imagemString = ConverterArquivoByteArrayEmBase64(imagem.ARQUIVO);

            return View(imagemViewModel);
        }

        // GET: Imagems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Imagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImagemViewModel imagemViewModel)
        {

            if (ModelState.IsValid)
            {
                var usuarioLogado = User.Identity.Name;
                int idUsuario = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(usuarioLogado)).Select(x => x.ID_USUARIO).FirstOrDefault();
                int idOng = _context.TB_ONG.Where(x => x.ID_USUARIO_ADM == idUsuario).Select(x => x.ID_ONG).FirstOrDefault();
                if (idOng != 0)
                {

                    TB_IMAGEM imagem = new TB_IMAGEM();

                    imagem.DE_TITULO = imagemViewModel.Titulo;
                    imagem.ID_ONG = idOng;

                    if (imagemViewModel.Arquivo.Length > 0)
                    {
                        //Leitura dos binarios
                        using (BinaryReader br = new BinaryReader(imagemViewModel.Arquivo.OpenReadStream()))
                        {
                            // Converte o arquivo em bytes
                            imagem.ARQUIVO = br.ReadBytes((int)imagemViewModel.Arquivo.OpenReadStream().Length);
                        }
                    }

                    _context.Add(imagem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "Noticias");
                }
            }
            return View(imagemViewModel);
        }

        // GET: Imagems/Edit/5
        /* public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var imagem = await _context.TB_IMAGEM.FindAsync(id);

             if (imagem == null)
             {
                 return NotFound();
             }
             ImagemViewModel imagemViewModel = new ImagemViewModel();
             imagemViewModel.ID_Imagem = imagem.ID_IMAGEM;
             imagemViewModel.Titulo = imagem.DE_TITULO;
             imagemViewModel.Arquivo = imagem.ARQUIVO;
             return View(imagemViewModel);
         }

         // POST: Imagems/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, ImagemViewModel imagemViewModel)
         {
             if (id != imagemViewModel.ID_Imagem)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 TB_IMAGEM imagem = new TB_IMAGEM();
                 imagem.DE_TITULO = imagemViewModel.Titulo;

                 if (imagemViewModel.Arquivo.Length > 0)
                 {
                     //Leitura dos binarios
                     using (BinaryReader br = new BinaryReader(imagemViewModel.Arquivo.OpenReadStream()))
                     {
                         // Converte o arquivo em bytes
                         imagem.ARQUIVO = br.ReadBytes((int)imagemViewModel.Arquivo.OpenReadStream().Length);
                     }
                 }
                 try
                 {
                     _context.Update(imagem);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!ImagemExists(imagem.ID_IMAGEM))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(imagemViewModel);
         }
        */
        // GET: Imagems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var imagem = await _context.TB_IMAGEM.FindAsync(id);
            if (imagem == null)
            {
                return NotFound();
            }
            var imagemViewModel = new ImagemViewModel();
            imagemViewModel.ID_Imagem = imagem.ID_IMAGEM;
            imagemViewModel.imagemString = ConverterArquivoByteArrayEmBase64(imagem.ARQUIVO);
            imagemViewModel.Titulo = imagem.DE_TITULO;
            imagemViewModel.Mensagem = "";
            return View(imagemViewModel);
        }

        // POST: Imagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagem = await _context.TB_IMAGEM.FindAsync(id);
            if (!ImagemVinculadaNoticia(imagem.ID_IMAGEM))
            {
                _context.TB_IMAGEM.Remove(imagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var imagemViewModel = new ImagemViewModel();
            imagemViewModel.ID_Imagem = imagem.ID_IMAGEM;
            imagemViewModel.imagemString = ConverterArquivoByteArrayEmBase64(imagem.ARQUIVO);
            imagemViewModel.Titulo = imagem.DE_TITULO;
            imagemViewModel.Mensagem = "Não é possível excluir a imagem, pois ela está vinculada à uma notícia.";
            return View(imagemViewModel);
        }

        private bool ImagemExists(int id)
        {
            return _context.TB_IMAGEM.Any(e => e.ID_IMAGEM == id);
        }
        private bool ImagemVinculadaNoticia(int id)
        {

            return _context.TB_NOTICIA.Any(e => e.ID_IMAGEM == id);
        }
    }
}

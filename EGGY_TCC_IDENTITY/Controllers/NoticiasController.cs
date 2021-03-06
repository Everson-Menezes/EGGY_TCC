using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EGGY_TCC_IDENTITY.Data;
using EGGY_TCC_IDENTITY.Models;
using EGGY_TCC_IDENTITY.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace EGGY_TCC_IDENTITY.Controllers
{
    [Authorize(Roles = "Master,Básico, Avançado")]
    public class NoticiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ICollection<NoticiaViewModel> noticiaViewModels = new List<NoticiaViewModel>();

            var noticias = _context.TB_NOTICIA.ToList();
            foreach (var registro in noticias)
            {
                NoticiaViewModel obj = new NoticiaViewModel();
                obj.ID_Noticia = registro.ID_NOTICIA;
                obj.Titulo = registro.DE_TITULO;
                obj.Conteudo = registro.DE_CONTEUDO;
                obj.DataPostagem = registro.DT_POSTAGEM;
                obj.Ong = new TB_ONG();
                obj.Ong.DE_NOME_FANTASIA = registro.DE_NOME_FANTASIA;
                obj.Num_Curtidas = registro.NU_CURTIDAS;
                obj.Imagem = new ImagemViewModel();
                obj.ID_Imagem = registro.ID_IMAGEM;
                byte[] imagem = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == registro.ID_IMAGEM).Select(x => x.ARQUIVO).FirstOrDefault();
                obj.Imagem.imagemString = ConverterArquivoByteArrayEmBase64(imagem);
                noticiaViewModels.Add(obj);
            }

            return View(noticiaViewModels);
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
        public async Task<IActionResult> Curtir(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var noticia = await _context.TB_NOTICIA.FirstOrDefaultAsync(m => m.ID_NOTICIA == id);
            if (noticia == null)
            {
                return NotFound();
            }
            var idApoiador = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(User.Identity.Name)).Select(x => x.ID_APOIADOR).FirstOrDefault();
            noticia.NU_CURTIDAS++;
            _context.Update(noticia);
            _context.SaveChanges();
            TB_NOTICIA_CURTIDA TB_NOTICIA_CURTIDA = new TB_NOTICIA_CURTIDA
            {
                ID_APOIADOR = idApoiador,
                ID_NOTICIA = noticia.ID_NOTICIA
            };
            _context.Add(TB_NOTICIA_CURTIDA);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.TB_NOTICIA.FirstOrDefaultAsync(m => m.ID_NOTICIA == id);
            var noticiarCurtida = _context.TB_NOTICIA_CURTIDA.FirstOrDefault(c => c.ID_NOTICIA == noticia.ID_NOTICIA);
            if (noticia == null)
            {
                return NotFound();
            }

            NoticiaViewModel noticiaViewModel = new NoticiaViewModel();
            noticiaViewModel.ID_Noticia = noticia.ID_NOTICIA;
            noticiaViewModel.Titulo = noticia.DE_TITULO;
            noticiaViewModel.Conteudo = noticia.DE_CONTEUDO;
            noticiaViewModel.DataPostagem = noticia.DT_POSTAGEM;
            noticiaViewModel.Ong = new TB_ONG();
            noticiaViewModel.Ong.DE_NOME_FANTASIA = noticia.DE_NOME_FANTASIA;
            noticiaViewModel.Imagem = new ImagemViewModel();
            noticiaViewModel.ID_Imagem = noticia.ID_IMAGEM;
            noticiaViewModel.Imagem.Titulo = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.DE_TITULO).FirstOrDefault();
            noticiaViewModel.Num_Curtidas = noticia.NU_CURTIDAS;
            int? idApoiador = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(User.Identity.Name)).Select(x => x.ID_APOIADOR).FirstOrDefault();

            if (noticiarCurtida != null && noticiarCurtida.ID_APOIADOR == idApoiador)
                noticiaViewModel.UsuarioJaCurtiu = true;

            return View(noticiaViewModel);
        }


        public IActionResult Create()
        {
            var usuarioLogado = User.Identity.Name;
            int idUsuario = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(usuarioLogado)).Select(x => x.ID_USUARIO).FirstOrDefault();
            List<int> listaOngs = _context.TB_ONG.Where(x => x.ID_USUARIO_ADM == idUsuario).Select(x => x.ID_ONG).Distinct().ToList();

            foreach (var item in listaOngs)
            {
                ViewData["ID_Imagem"] = new SelectList(_context.TB_IMAGEM.Where(x => x.ID_ONG == item), "ID_IMAGEM", "DE_TITULO");
                ViewData["ID_Ong"] = new SelectList(_context.TB_ONG.Where(x => x.ID_ONG == item), "ID_ONG", "DE_NOME_FANTASIA");
            }

            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoticiaViewModel noticiaViewModel)
        {
            TB_NOTICIA noticia = new TB_NOTICIA();
            if (ModelState.IsValid)
            {

                noticia.ID_IMAGEM = noticiaViewModel.ID_Imagem;
                noticia.DE_CONTEUDO = noticiaViewModel.Conteudo;
                noticia.DT_POSTAGEM = noticiaViewModel.DataPostagem;
                noticia.DE_NOME_FANTASIA = _context.TB_ONG.Where(x => x.ID_ONG == noticiaViewModel.ID_Ong).Select(x => x.DE_NOME_FANTASIA).FirstOrDefault();
                noticia.DE_TITULO = noticiaViewModel.Titulo;
                noticia.NU_CURTIDAS = noticiaViewModel.Num_Curtidas;
                _context.Add(noticia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Imagem"] = new SelectList(_context.TB_IMAGEM, "ID_IMAGEM", "DE_TITULO", noticiaViewModel.ID_Imagem);
            ViewData["ID_Ong"] = new SelectList(_context.TB_ONG, "ID_ONG", "DE_NOME_FANTASIA", noticiaViewModel.ID_Ong);
            return View(noticiaViewModel);
        }

        // GET: Noticias/Edit/5

        [Authorize(Roles = "Master, Avançado")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = _context.TB_NOTICIA.Find(id);
            if (noticia == null)
            {
                return NotFound();
            }
            var usuarioLogado = User.Identity.Name;
            int idUsuario = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(usuarioLogado)).Select(x => x.ID_USUARIO).FirstOrDefault();
            List<int> listaOngs = _context.TB_ONG.Where(x => x.ID_USUARIO_ADM == idUsuario).Select(x => x.ID_ONG).Distinct().ToList();
            NoticiaViewModel noticiaViewModel = new NoticiaViewModel();
            if (User.IsInRole("Master"))
            {
                noticiaViewModel.ID_Noticia = noticia.ID_NOTICIA;
                noticiaViewModel.ID_Imagem = noticia.ID_IMAGEM;
                noticiaViewModel.Ong = new TB_ONG();
                noticiaViewModel.Ong.DE_NOME_FANTASIA = noticia.DE_NOME_FANTASIA;
                noticiaViewModel.Titulo = noticia.DE_TITULO;
                noticiaViewModel.Conteudo = noticia.DE_CONTEUDO;
                noticiaViewModel.DataPostagem = noticia.DT_POSTAGEM;
                noticiaViewModel.Imagem = new ImagemViewModel();
                noticiaViewModel.Imagem.Titulo = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.DE_TITULO).FirstOrDefault();
                noticiaViewModel.Num_Curtidas = noticia.NU_CURTIDAS;
                return View(noticiaViewModel);
            }

            foreach (var ongImagem in listaOngs)
            {
                var imagens = _context.TB_IMAGEM.Where(i => i.ID_ONG == ongImagem).Select(i => i.ID_IMAGEM).ToList();

                if (imagens.Contains(noticia.ID_IMAGEM))
                {

                    noticiaViewModel.ID_Noticia = noticia.ID_NOTICIA;
                    noticiaViewModel.ID_Imagem = noticia.ID_IMAGEM;
                    noticiaViewModel.Ong = new TB_ONG();
                    noticiaViewModel.Ong.DE_NOME_FANTASIA = noticia.DE_NOME_FANTASIA;
                    noticiaViewModel.ID_Ong = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.ID_ONG).FirstOrDefault();
                    noticiaViewModel.Titulo = noticia.DE_TITULO;
                    noticiaViewModel.Conteudo = noticia.DE_CONTEUDO;
                    noticiaViewModel.DataPostagem = noticia.DT_POSTAGEM;
                    noticiaViewModel.Imagem = new ImagemViewModel();
                    noticiaViewModel.Imagem.Titulo = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.DE_TITULO).FirstOrDefault();
                    noticiaViewModel.Num_Curtidas = noticia.NU_CURTIDAS;
                    return View(noticiaViewModel);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [Authorize(Roles = "Master, Avançado")]
        public async Task<IActionResult> Edit(int id, NoticiaViewModel noticiaViewModel)
        {
            if (id != noticiaViewModel.ID_Noticia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TB_NOTICIA noticia = new TB_NOTICIA();
                noticia.ID_NOTICIA = noticiaViewModel.ID_Noticia;
                noticia.DE_TITULO = noticiaViewModel.Titulo;
                noticia.DE_CONTEUDO = noticiaViewModel.Conteudo;
                noticia.DT_POSTAGEM = noticiaViewModel.DataPostagem;
                noticia.DE_NOME_FANTASIA = _context.TB_ONG.Where(x => x.ID_ONG == noticiaViewModel.ID_Ong).Select(x => x.DE_NOME_FANTASIA).FirstOrDefault();
                noticia.NU_CURTIDAS = noticiaViewModel.Num_Curtidas;
                noticia.ID_IMAGEM = noticiaViewModel.ID_Imagem;

                try
                {
                    _context.Update(noticia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiaExists(noticia.ID_NOTICIA))
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
            return View(noticiaViewModel);
        }


        [Authorize(Roles = "Master")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = _context.TB_NOTICIA.Find(id);
            if (noticia == null)
            {
                return NotFound();
            }
            var usuarioLogado = User.Identity.Name;
            int idUsuario = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(usuarioLogado)).Select(x => x.ID_USUARIO).FirstOrDefault();
            List<int> listaOngs = _context.TB_ONG.Where(x => x.ID_USUARIO_ADM == idUsuario).Select(x => x.ID_ONG).Distinct().ToList();
            NoticiaViewModel noticiaViewModel = new NoticiaViewModel();
            if (User.IsInRole("Master"))
            {
                noticiaViewModel.ID_Noticia = noticia.ID_NOTICIA;
                noticiaViewModel.ID_Imagem = noticia.ID_IMAGEM;
                noticiaViewModel.Ong = new TB_ONG();
                noticiaViewModel.Ong.DE_NOME_FANTASIA = noticia.DE_NOME_FANTASIA;
                noticiaViewModel.Titulo = noticia.DE_TITULO;
                noticiaViewModel.Conteudo = noticia.DE_CONTEUDO;
                noticiaViewModel.DataPostagem = noticia.DT_POSTAGEM;
                noticiaViewModel.Imagem = new ImagemViewModel();
                noticiaViewModel.Imagem.Titulo = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.DE_TITULO).FirstOrDefault();
                noticiaViewModel.Num_Curtidas = noticia.NU_CURTIDAS;
                return View(noticiaViewModel);
            }

            foreach (var ongImagem in listaOngs)
            {
                var imagens = _context.TB_IMAGEM.Where(i => i.ID_ONG == ongImagem).Select(i => i.ID_IMAGEM).ToList();

                if (imagens.Contains(noticia.ID_IMAGEM))
                {

                    noticiaViewModel.ID_Noticia = noticia.ID_NOTICIA;
                    noticiaViewModel.ID_Imagem = noticia.ID_IMAGEM;
                    noticiaViewModel.Ong = new TB_ONG();
                    noticiaViewModel.Ong.DE_NOME_FANTASIA = noticia.DE_NOME_FANTASIA;
                    noticiaViewModel.ID_Ong = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.ID_ONG).FirstOrDefault();
                    noticiaViewModel.Titulo = noticia.DE_TITULO;
                    noticiaViewModel.Conteudo = noticia.DE_CONTEUDO;
                    noticiaViewModel.DataPostagem = noticia.DT_POSTAGEM;
                    noticiaViewModel.Imagem = new ImagemViewModel();
                    noticiaViewModel.Imagem.Titulo = _context.TB_IMAGEM.Where(x => x.ID_IMAGEM == noticia.ID_IMAGEM).Select(x => x.DE_TITULO).FirstOrDefault();
                    noticiaViewModel.Num_Curtidas = noticia.NU_CURTIDAS;
                    return View(noticiaViewModel);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]

        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticia = await _context.TB_NOTICIA.FindAsync(id);
            _context.TB_NOTICIA.Remove(noticia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiaExists(int id)
        {
            return _context.TB_NOTICIA.Any(e => e.ID_NOTICIA == id);
        }
    }
}

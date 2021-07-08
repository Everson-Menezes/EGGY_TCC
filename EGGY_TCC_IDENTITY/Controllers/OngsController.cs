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

namespace EGGY_TCC_IDENTITY.Controllers
{
    public class OngsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OngsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ongs
        public IActionResult Index()
        {
            var ongs = _context.TB_ONG;
            ICollection<OngViewModel> ongViewModel = new List<OngViewModel>();

            foreach (var registro in ongs)
            {
                OngViewModel obj = new OngViewModel();
                obj.ID_Usuario = registro.ID_USUARIO_ADM;
                obj.ID_Ong = registro.ID_ONG;
                obj.CNPJ = registro.DE_CNPJ;
                obj.Email = registro.DE_EMAIL;
                obj.NomeFantasia = registro.DE_NOME_FANTASIA;
                obj.RazaoSocial = registro.DE_RAZAO_SOCIAL;
                obj.Representante = registro.DE_REPRESENTANTE;
                obj.Telefone = registro.DE_TELEFONE;
                ongViewModel.Add(obj);
            }
            return View(ongViewModel);
        }

        // GET: Ongs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ong = _context.TB_ONG.FirstOrDefault(m => m.ID_ONG == id);
            ong.DE_LOGIN_USUARIO_ADM = _context.TB_ONG.Where(x => x.ID_ONG == id).Select(x => x.DE_LOGIN_USUARIO_ADM).FirstOrDefault();
            if (ong == null)
            {
                return NotFound();
            }

            OngViewModel ongViewModel = new OngViewModel();
            ongViewModel.ID_Ong = ong.ID_ONG;
            ongViewModel.ID_Usuario = ong.ID_USUARIO_ADM;
            ongViewModel.NomeFantasia = ong.DE_NOME_FANTASIA;
            ongViewModel.RazaoSocial = ong.DE_RAZAO_SOCIAL;
            ongViewModel.Representante = ong.DE_REPRESENTANTE;
            ongViewModel.Telefone = ong.DE_TELEFONE;
            ongViewModel.Email = ong.DE_EMAIL;
            ongViewModel.CNPJ = ong.DE_CNPJ;
            ongViewModel.UsuarioAdm.DE_LOGIN = ong.DE_LOGIN_USUARIO_ADM;

            return View(ongViewModel);
        }

        // GET: Ongs/Create
        public IActionResult Create()
        {
            ViewData["ID_Usuario"] = new SelectList(_context.TB_USUARIO, "ID_Usuario", "UserName");
            return View();
        }

        // POST: Ongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OngViewModel ongViewModel)
        {

            if (ModelState.IsValid)
            {
                TB_ONG ong = new TB_ONG();
                TB_USUARIO usuario = new TB_USUARIO();
                TB_APOIADOR apoiador = new TB_APOIADOR();

                ong.DE_REPRESENTANTE = ongViewModel.Representante;
                ong.DE_EMAIL = ongViewModel.Email;
                ong.DE_TELEFONE = ongViewModel.Telefone;
                ong.DE_CNPJ = ongViewModel.CNPJ;
                ong.DE_RAZAO_SOCIAL = ongViewModel.RazaoSocial;
                ong.DE_NOME_FANTASIA = ongViewModel.NomeFantasia;
                ong.DT_CADASTRO = DateTime.Now;
                ong.DT_ALTERACAO = DateTime.Now;
                ong.DT_INATIVACAO = null;
                ong.ID_STATUS = 1;
                usuario.DE_SENHA = ongViewModel.UsuarioAdm.DE_SENHA;
                ong.DE_LOGIN_USUARIO_ADM = usuario.DE_LOGIN = ongViewModel.UsuarioAdm.DE_LOGIN;
                apoiador.DE_NOME = usuario.DE_NOME = ongViewModel.UsuarioAdm.DE_NOME;
                apoiador.DE_EMAIL = usuario.DE_EMAIL = ongViewModel.UsuarioAdm.DE_EMAIL;
                apoiador.DT_CADASTRO = usuario.DT_CADASTRO = DateTime.Now;
                apoiador.DT_ALTERACAO = usuario.DT_ALTERACAO = DateTime.Now;
                apoiador.DT_INATIVACAO = usuario.DT_INATIVACAO = null;
                usuario.ID_STATUS = 1;
                apoiador.BL_RECEBE_NOVIDADE = ongViewModel.BL_RECEBE_NOVIDADE;
                _context.Add(usuario);
                _context.Add(apoiador);
                await _context.SaveChangesAsync();
                ong.ID_USUARIO_ADM = apoiador.ID_USUARIO = usuario.ID_USUARIO;
                usuario.ID_APOIADOR = apoiador.ID_APOIADOR;
                _context.Add(ong);
                _context.Update(usuario);
                _context.Update(apoiador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ongViewModel);
        }

        // GET: Ongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ong = await _context.TB_ONG.FindAsync(id);

            if (ong == null)
            {
                return NotFound();
            }

            OngViewModel ongViewModel = new OngViewModel();
            ongViewModel.ID_Ong = ong.ID_ONG;
            ongViewModel.Representante = ong.DE_REPRESENTANTE;
            ongViewModel.UsuarioAdm.DE_LOGIN = ong.DE_LOGIN_USUARIO_ADM;
            ongViewModel.Email = ong.DE_EMAIL;
            ongViewModel.Telefone = ong.DE_TELEFONE;
            ongViewModel.CNPJ = ong.DE_CNPJ;
            ongViewModel.RazaoSocial = ong.DE_RAZAO_SOCIAL;
            ongViewModel.NomeFantasia = ong.DE_NOME_FANTASIA;
            ongViewModel.ID_Usuario = ong.ID_USUARIO_ADM;
            ongViewModel.ID_Status = ong.ID_STATUS;
            ongViewModel.DT_CADASTRO = ong.DT_CADASTRO;
            ongViewModel.DT_ALTERACAO = ong.DT_ALTERACAO;

            return View(ongViewModel);
        }

        // POST: Ongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OngViewModel ongViewModel)
        {
            if (id != ongViewModel.ID_Ong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TB_ONG ong = new TB_ONG();
                ong.ID_ONG = ongViewModel.ID_Ong;
                ong.DE_REPRESENTANTE = ongViewModel.Representante;
                ong.DE_EMAIL = ongViewModel.Email;
                ong.DE_TELEFONE = ongViewModel.Telefone;
                ong.DE_CNPJ = ongViewModel.CNPJ;
                ong.DE_RAZAO_SOCIAL = ongViewModel.RazaoSocial;
                ong.DE_NOME_FANTASIA = ongViewModel.NomeFantasia;
                ong.ID_STATUS = ongViewModel.ID_Status;
                ong.ID_USUARIO_ADM = ongViewModel.ID_Usuario;
                ong.DE_LOGIN_USUARIO_ADM = ongViewModel.UsuarioAdm.DE_LOGIN;
                ong.DT_CADASTRO = ongViewModel.DT_CADASTRO;
                ong.DT_ALTERACAO = DateTime.Now;

                try
                {
                    _context.Update(ong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OngExists(ong.ID_ONG))
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
            return View(ongViewModel);
        }

        // GET: Ongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ong = await _context.TB_ONG.FirstOrDefaultAsync(m => m.ID_ONG == id);
            if (ong == null)
            {
                return NotFound();
            }
            OngViewModel ongViewModel = new OngViewModel();
            ongViewModel.ID_Ong = ong.ID_ONG;
            ongViewModel.Representante = ong.DE_REPRESENTANTE;
            ongViewModel.UsuarioAdm.DE_LOGIN = ong.DE_LOGIN_USUARIO_ADM;
            ongViewModel.Email = ong.DE_EMAIL;
            ongViewModel.Telefone = ong.DE_TELEFONE;
            ongViewModel.CNPJ = ong.DE_CNPJ;
            ongViewModel.RazaoSocial = ong.DE_RAZAO_SOCIAL;
            ongViewModel.NomeFantasia = ong.DE_NOME_FANTASIA;
            ongViewModel.ID_Usuario = ong.ID_USUARIO_ADM;
            ongViewModel.ID_Status = ong.ID_STATUS;
            ong.DT_CADASTRO = ong.DT_CADASTRO;
            ong.DT_ALTERACAO = ong.DT_ALTERACAO;
            return View(ongViewModel);
        }

        // POST: Ongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ong = await _context.TB_ONG.FindAsync(id);
            _context.TB_ONG.Remove(ong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OngExists(int id)
        {
            return _context.TB_ONG.Any(e => e.ID_ONG == id);
        }
    }
}

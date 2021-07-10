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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EGGY_TCC_IDENTITY.Controllers
{
    [Authorize(Roles = "Master")]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            var usuarios = _context.TB_USUARIO.ToList();
            ICollection<UsuarioViewModel> usuarioViewModels = new List<UsuarioViewModel>();

            foreach (var registro in usuarios)
            {
                UsuarioViewModel obj = new UsuarioViewModel();
                obj.ID_USUARIO = registro.ID_USUARIO;
                obj.DE_EMAIL = registro.DE_EMAIL;
                obj.DE_NOME = registro.DE_NOME;
                obj.DE_SENHA = registro.DE_SENHA;
                obj.DE_LOGIN = registro.DE_LOGIN;
                obj.DT_CADASTRO = registro.DT_CADASTRO;
                obj.DT_ALTERACAO = registro.DT_ALTERACAO;
                usuarioViewModels.Add(obj);
            }
            return View(usuarioViewModels);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.TB_USUARIO.FirstOrDefaultAsync(m => m.ID_USUARIO == id);

            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
            usuarioViewModel.ID_USUARIO = usuario.ID_USUARIO;
            usuarioViewModel.DE_NOME = usuario.DE_NOME;
            usuarioViewModel.DE_SENHA = usuario.DE_SENHA;
            usuarioViewModel.DE_LOGIN = usuario.DE_LOGIN;
            usuarioViewModel.DE_EMAIL = usuario.DE_EMAIL;
            usuarioViewModel.DT_CADASTRO = usuario.DT_CADASTRO;
            usuarioViewModel.DT_ALTERACAO = usuario.DT_ALTERACAO;
            return View(usuarioViewModel);
        }
        [AllowAnonymous]
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {

                // Copia alguns dados do UsuarioViewModel para o IdentityUser
                var user = new IdentityUser();

                user.UserName = usuarioViewModel.DE_LOGIN;
                user.Email = usuarioViewModel.DE_EMAIL;
                user.PasswordHash = usuarioViewModel.DE_SENHA;
                // Armazena os dados do usuário na tabela AspNetUsers
                var resultado = await _userManager.CreateAsync(user, user.PasswordHash);
                // Se o usuário foi criado com sucesso, faz o login do usuário
                // usando o serviço SignInManager e redireciona para o Método Action Index
                if (resultado.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync("858d6f98-9229-4833-9009-a0dd29fb11cc");
                    await _userManager.AddToRoleAsync(user, role.Name);
                    TB_USUARIO usuario = new TB_USUARIO();
                    TB_APOIADOR apoiador = new TB_APOIADOR();

                    apoiador.DE_NOME = usuario.DE_NOME = usuarioViewModel.DE_NOME;
                    usuario.DE_SENHA = usuarioViewModel.DE_SENHA;
                    usuario.DE_LOGIN = usuarioViewModel.DE_LOGIN;
                    apoiador.DE_EMAIL = usuario.DE_EMAIL = usuarioViewModel.DE_EMAIL;
                    apoiador.DT_CADASTRO = usuario.DT_CADASTRO = DateTime.Now;
                    apoiador.DT_ALTERACAO = usuario.DT_ALTERACAO = DateTime.Now;
                    apoiador.DT_INATIVACAO = usuario.DT_INATIVACAO = null;
                    apoiador.BL_RECEBE_NOVIDADE = usuarioViewModel.BL_RECEBE_NOVIDADE;
                    _context.Add(apoiador);
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    apoiador.ID_USUARIO = usuario.ID_USUARIO;
                    usuario.ID_APOIADOR = apoiador.ID_APOIADOR;
                    _context.Update(apoiador);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                // Se houver erros então inclui no ModelState
                // que será exibido pela tag helper summary na validação
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(usuarioViewModel);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*    [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(UsuarioViewModel usuarioViewModel)
            {
                if (ModelState.IsValid)
                {
                    TB_USUARIO usuario = new TB_USUARIO();
                    TB_APOIADOR apoiador = new TB_APOIADOR();

                    apoiador.DE_NOME = usuario.DE_NOME = usuarioViewModel.DE_NOME;
                    usuario.DE_SENHA = usuarioViewModel.DE_SENHA;
                    usuario.DE_LOGIN = usuarioViewModel.DE_LOGIN;
                    apoiador.DE_EMAIL = usuario.DE_EMAIL = usuarioViewModel.DE_EMAIL;
                    apoiador.DT_CADASTRO = usuario.DT_CADASTRO = DateTime.Now;
                    apoiador.DT_ALTERACAO = usuario.DT_ALTERACAO = DateTime.Now;
                    apoiador.DT_INATIVACAO = usuario.DT_INATIVACAO = null;
                    apoiador.BL_RECEBE_NOVIDADE = usuarioViewModel.BL_RECEBE_NOVIDADE;
                    _context.Add(apoiador);
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    apoiador.ID_USUARIO = usuario.ID_USUARIO;
                    usuario.ID_APOIADOR = apoiador.ID_APOIADOR;
                    _context.Update(apoiador);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(usuarioViewModel);
            }*/

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.TB_USUARIO.FindAsync(id);
            var apoiador = _context.TB_APOIADOR.Where(x => x.ID_USUARIO == id).FirstOrDefault();
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
            usuarioViewModel.APOIADOR.ID_USUARIO = usuarioViewModel.ID_USUARIO = usuario.ID_USUARIO;
            usuarioViewModel.APOIADOR.DE_NOME = usuarioViewModel.DE_NOME = usuario.DE_NOME;
            usuarioViewModel.DE_SENHA = usuario.DE_SENHA;
            usuarioViewModel.DE_LOGIN = usuario.DE_LOGIN;
            usuarioViewModel.DE_EMAIL = usuario.DE_EMAIL;
            usuarioViewModel.DT_CADASTRO = usuario.DT_CADASTRO;
            usuarioViewModel.DT_INATIVACAO = usuario.DT_INATIVACAO;
            usuarioViewModel.DT_ALTERACAO = usuario.DT_ALTERACAO;
            usuarioViewModel.ID_APOIADOR = usuarioViewModel.APOIADOR.ID_APOIADOR = apoiador.ID_APOIADOR;
            usuarioViewModel.BL_RECEBE_NOVIDADE = apoiador.BL_RECEBE_NOVIDADE;
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["Ong"] = new SelectList(_context.TB_ONG, "ID_Ong", "NomeFantasia");
            return View(usuarioViewModel);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel usuarioViewModel)
        {
            if (id != usuarioViewModel.ID_USUARIO)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TB_USUARIO usuario = new TB_USUARIO();
                TB_APOIADOR apoiador = new TB_APOIADOR();

                try
                {
                    apoiador.ID_USUARIO = usuario.ID_USUARIO = usuarioViewModel.ID_USUARIO;
                    apoiador.ID_APOIADOR = usuarioViewModel.ID_APOIADOR;
                    apoiador.DE_NOME = usuario.DE_NOME = usuarioViewModel.DE_NOME;
                    usuario.DE_SENHA = usuarioViewModel.DE_SENHA;
                    usuario.DE_LOGIN = usuarioViewModel.DE_LOGIN;
                    apoiador.DE_EMAIL = usuario.DE_EMAIL = usuarioViewModel.DE_EMAIL;
                    apoiador.DT_CADASTRO = usuario.DT_CADASTRO = usuarioViewModel.DT_CADASTRO;
                    apoiador.DT_ALTERACAO = usuario.DT_ALTERACAO = DateTime.Now;
                    apoiador.DT_INATIVACAO = usuario.DT_INATIVACAO = null;
                    apoiador.BL_RECEBE_NOVIDADE = usuarioViewModel.BL_RECEBE_NOVIDADE;
                    _context.Update(usuario);
                    _context.Update(apoiador);
                    if (UsuarioAdmExists(usuarioViewModel.ID_USUARIO))
                    {
                        var ong = _context.TB_ONG.Where(x => x.ID_USUARIO_ADM == usuarioViewModel.ID_USUARIO).FirstOrDefault();
                        ong.DE_LOGIN_USUARIO_ADM = usuarioViewModel.DE_LOGIN;


                        _context.Update(ong);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID_USUARIO))
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
            return View(usuarioViewModel);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.TB_USUARIO
                .FirstOrDefaultAsync(m => m.ID_USUARIO == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.TB_USUARIO.FindAsync(id);
            _context.TB_USUARIO.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.TB_USUARIO.Any(e => e.ID_USUARIO == id);
        }
        private bool UsuarioAdmExists(int id)
        {
            return _context.TB_ONG.Any(e => e.ID_USUARIO_ADM == id);
        }
    }
}

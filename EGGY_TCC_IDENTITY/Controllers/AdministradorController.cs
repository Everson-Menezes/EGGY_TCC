using EGGY_TCC_IDENTITY.Data;
using EGGY_TCC_IDENTITY.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<IdentityUser> _userManager;
        public AdministradorController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModels roleViewModels)
        {
            if (ModelState.IsValid)
            {
                // precisamos apenas especificar o nome único da role
                IdentityRole identityRole = new IdentityRole();

                identityRole.Name = roleViewModels.Perfil;
                
                // Salva a role na tabela AspNetRole
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Administrador");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(roleViewModels);
        }
        public async Task<IActionResult> Edit(string id)
        {
            // Localiza a role pelo ID
            var perfil = await _roleManager.FindByIdAsync(id);
            if (perfil == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {id} não foi localizada";
                return View("NotFound");
            }
            var roleViewModels = new RoleViewModels();

            roleViewModels.Id = perfil.Id;
            roleViewModels.Perfil = perfil.Name;

            var listaUsuarios = _userManager.Users.ToList();
            // Retorna todos os usuários
            foreach (var obj in listaUsuarios)
            {
                // Se o usuário existir na role, inclui o nome do usuário
                // para a propriedade Users de EditRoleViewModel
                // Este objeto model é então passado para ser exibido
                if (await _userManager.IsInRoleAsync(obj, perfil.Name))
                {
                    roleViewModels.UsuariosPerfil.Add(obj.UserName);
                }
            }
            return View(roleViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModels roleViewModels)
        {
            var role = await _roleManager.FindByIdAsync(roleViewModels.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleViewModels.Id} não foi encontrada";
                return View("NotFound");
            }
            else
            {
               
                role.Name = roleViewModels.Perfil;
                // Atualiza a role usando UpdateAsync
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Administrador");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(roleViewModels);
            }
        }
    
    public async Task<IActionResult> EditUsersInRole(string Id)
        {
            
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {Id} não foi encontrada";
                return View("NotFound");
            }
            var model = new List<UsuarioPerfilViewModel>();
            var listaUsuarios = _userManager.Users.ToList();
            foreach (var user in listaUsuarios)
            {
                var userRoleViewModel = new UsuarioPerfilViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            ViewBag.roleId = Id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UsuarioPerfilViewModel> model)
        {

            var roleId = model[0].roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleId} não foi encontrada";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Edit", new { Id = roleId });
                }
            }
            return RedirectToAction("Edit", new { Id = roleId });
        }
    }
}


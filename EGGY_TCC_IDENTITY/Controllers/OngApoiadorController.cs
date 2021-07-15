using EGGY_TCC_IDENTITY.Data;
using EGGY_TCC_IDENTITY.Models;
using EGGY_TCC_IDENTITY.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGGY_TCC_IDENTITY.Controllers
{
    public class OngApoiadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<IdentityUser> _userManager;

        public OngApoiadorController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var idApoiador = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(User.Identity.Name)).Select(x => x.ID_APOIADOR).FirstOrDefault();
            var listaOngApoiador = _context.TB_ONG_APOIADOR.Where(x => x.ID_APOIADOR.Equals(idApoiador)).ToList();
            IList<OngApoiadorViewModel> ongApoiadorViewModels = new List<OngApoiadorViewModel>();

            foreach(var registro in listaOngApoiador)
            {
                var obj = new OngApoiadorViewModel();
                obj.ID_APOIADOR = registro.ID_APOIADOR;
                obj.ID_ONG = registro.ID_ONG;
                obj.Ong = new OngViewModel();
                obj.Ong.NomeFantasia = _context.TB_ONG.Where(x => x.ID_ONG == obj.ID_ONG).Select(x => x.DE_NOME_FANTASIA).FirstOrDefault();
                ongApoiadorViewModels.Add(obj);
            }
            
            return View(ongApoiadorViewModels);
        }
        public IActionResult Apoiar(int idOng)
        {
            if (idOng > 0)
            {
                var registroOng = _context.TB_ONG.Where(o => o.ID_ONG == idOng).FirstOrDefault();
                OngApoiadorViewModel ongApoiadorViewModel = new OngApoiadorViewModel();
                ongApoiadorViewModel.Ong = new OngViewModel();
                ongApoiadorViewModel.ID_ONG = registroOng.ID_ONG;
                ongApoiadorViewModel.Ong.DT_ALTERACAO = registroOng.DT_ALTERACAO;
                ongApoiadorViewModel.Ong.DT_CADASTRO = registroOng.DT_CADASTRO;
                ongApoiadorViewModel.Ong.NomeFantasia = registroOng.DE_NOME_FANTASIA;
                ongApoiadorViewModel.Ong.RazaoSocial = registroOng.DE_RAZAO_SOCIAL;
                ongApoiadorViewModel.Ong.Representante = registroOng.DE_REPRESENTANTE;
                ongApoiadorViewModel.Ong.Telefone = registroOng.DE_TELEFONE;
                ongApoiadorViewModel.Ong.Email = registroOng.DE_EMAIL;
                ongApoiadorViewModel.Ong.CNPJ = registroOng.DE_CNPJ;

                int idUsuario = _context.TB_USUARIO.Where(x => x.DE_LOGIN.Equals(User.Identity.Name)).Select(x => x.ID_USUARIO).FirstOrDefault();
                var regstroApoiador = _context.TB_APOIADOR.Where(a => a.ID_USUARIO == idUsuario).FirstOrDefault();
                ongApoiadorViewModel.Apoidor = new ApoidorViewModel();
                ongApoiadorViewModel.ID_APOIADOR = regstroApoiador.ID_APOIADOR;
                ongApoiadorViewModel.Apoidor.DE_NOME = regstroApoiador.DE_NOME;
                ongApoiadorViewModel.Apoidor.DE_EMAIL = regstroApoiador.DE_EMAIL;

                var listaOngApoiador = _context.TB_ONG_APOIADOR.ToList();
                var idsOngApoiador = _context.TB_ONG_APOIADOR.Where(a => a.ID_APOIADOR == ongApoiadorViewModel.ID_APOIADOR).Select(x => x.ID_ONG_APOIADOR).ToList();
                var intersecao = idsOngApoiador.Intersect(listaOngApoiador.Select(x => x.ID_ONG_APOIADOR).ToList());

                List<TB_ONG_APOIADOR> TB_ONG_APOIADORES = new List<TB_ONG_APOIADOR>();
                foreach(var item in intersecao)
                {
                    TB_ONG_APOIADOR obj = _context.TB_ONG_APOIADOR.Where(x => x.ID_ONG_APOIADOR == item).FirstOrDefault();
                    TB_ONG_APOIADORES.Add(obj);
                }
                if (TB_ONG_APOIADORES.Count > 0)
                {                                        

                    foreach (var item in TB_ONG_APOIADORES)
                    {
                        if (item.ID_APOIADOR == ongApoiadorViewModel.ID_APOIADOR && item.ID_ONG == ongApoiadorViewModel.ID_ONG)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TB_ONG_APOIADOR TB_ONG_APOIADOR = new TB_ONG_APOIADOR();
                            TB_ONG_APOIADOR.ID_ONG = ongApoiadorViewModel.ID_ONG;
                            TB_ONG_APOIADOR.ID_APOIADOR = ongApoiadorViewModel.ID_APOIADOR;
                            _context.TB_ONG_APOIADOR.Add(TB_ONG_APOIADOR);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    TB_ONG_APOIADOR TB_ONG_APOIADOR = new TB_ONG_APOIADOR();
                    TB_ONG_APOIADOR.ID_ONG = ongApoiadorViewModel.ID_ONG;
                    TB_ONG_APOIADOR.ID_APOIADOR = ongApoiadorViewModel.ID_APOIADOR;
                    _context.TB_ONG_APOIADOR.Add(TB_ONG_APOIADOR);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index", "Home"); ;
        }
    }
}

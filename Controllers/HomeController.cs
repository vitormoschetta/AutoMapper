using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFSemMigrations.Models;
using EFSemMigrations.Data;
using AutoMapper;
using EFSemMigrations.ViewModels;

namespace EFSemMigrations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HomeController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index(){
            var listaModel = _context.Usuario.ToList();
            var listaviewModel = _mapper.Map<IEnumerable<UsuarioViewModel>>(listaModel);
            foreach (var item in listaviewModel){
                item.Mensagem = "Atributo de outra entidade";
            }
            return View(listaviewModel);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(UsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Erro no preenchimento das informações");
                return View(viewModel);
            }

            if (UsuarioExist(viewModel.Nome))
            {
                ModelState.AddModelError(String.Empty, "Esse nome já existe");
                return View(viewModel);
            }

            var model = _mapper.Map<Usuario>(viewModel);
            _context.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int Id){            
            var model = _context.Usuario.SingleOrDefault(x => x.Id == Id);
            var viewModel = _mapper.Map<UsuarioViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int Id, Usuario model)
        {
            if (Id != model.Id)
            {
                ModelState.AddModelError(String.Empty, "Usuário Invalido");
                return View(model);
            }

            _context.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id) => View(_context.Usuario.SingleOrDefault(x => x.Id == Id));

        [HttpPost]
        public IActionResult Delete(int Id, Usuario model)
        {
            if (Id != model.Id)
            {
                ModelState.AddModelError(String.Empty, "Usuário Invalido");
                return View(model);
            }

            _context.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public bool UsuarioExist(string Nome)
        {
            var model = _context.Usuario.SingleOrDefault(x => x.Nome == Nome);
            if (model != null) return true;
            return false;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

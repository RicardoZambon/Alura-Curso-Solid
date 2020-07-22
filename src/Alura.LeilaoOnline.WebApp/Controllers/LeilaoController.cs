using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class LeilaoController : Controller
    {
        private readonly ILeiloesDao _leiloesDAO;
        private readonly ICategoriasDao _categoriasDAO;

        public LeilaoController(ILeiloesDao leiloesDAO, ICategoriasDao categoriasDAO)
        {
            _leiloesDAO = leiloesDAO;
            _categoriasDAO = categoriasDAO;
        }


        public IActionResult Index()
        {
            return View(_leiloesDAO.List());
        }


        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categorias"] = _categoriasDAO.List();
            ViewData["Operacao"] = "Inclusão";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _leiloesDAO.Insert(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _categoriasDAO.List();
            ViewData["Operacao"] = "Inclusão";
            return View("Form", model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _categoriasDAO.List();
            ViewData["Operacao"] = "Edição";

            var leilao = _leiloesDAO.FindByID(id);
            if (leilao == null)
            {
                return NotFound();
            }

            return View("Form", leilao);
        }

        [HttpPost]
        public IActionResult Edit(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _leiloesDAO.Update(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _categoriasDAO.List();
            ViewData["Operacao"] = "Edição";

            return View("Form", model);
        }


        [HttpPost]
        public IActionResult Inicia(int id)
        {
            var leilao = _leiloesDAO.FindByID(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao != SituacaoLeilao.Rascunho)
            {
                return StatusCode(405);
            }

            leilao.Situacao = SituacaoLeilao.Pregao;
            leilao.Inicio = DateTime.Now;

            _leiloesDAO.Update(leilao);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var leilao = _leiloesDAO.FindByID(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao != SituacaoLeilao.Pregao)
            {
                return StatusCode(405);
            }

            leilao.Situacao = SituacaoLeilao.Finalizado;
            leilao.Termino = DateTime.Now;

            _leiloesDAO.Update(leilao);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Remove(int id)
        {
            var leilao = _leiloesDAO.FindByID(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao == SituacaoLeilao.Pregao)
            {
                return StatusCode(405);
            }

            _leiloesDAO.Delete(leilao);
            return NoContent();
        }


        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;
            return View("Index", _leiloesDAO.FindByTerm(termo));
        }
    }
}

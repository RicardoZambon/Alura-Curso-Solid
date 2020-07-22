using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class LeilaoController : Controller
    {
        private readonly LeiloesDAO _leiloesDAO;
        private readonly CategoriasDAO _categoriasDAO;

        public LeilaoController(LeiloesDAO leiloesDAO, CategoriasDAO categoriasDAO)
        {
            _leiloesDAO = leiloesDAO;
            _categoriasDAO = categoriasDAO;
        }

        public IActionResult Index()
        {
            return View(_leiloesDAO.Listar());
        }


        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categorias"] = _categoriasDAO.Listar();
            ViewData["Operacao"] = "Inclusão";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _leiloesDAO.Inserir(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _categoriasDAO.Listar();
            ViewData["Operacao"] = "Inclusão";
            return View("Form", model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _categoriasDAO.Listar();
            ViewData["Operacao"] = "Edição";

            var leilao = _leiloesDAO.ProcurarPorId(id);
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
                _leiloesDAO.Alterar(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _categoriasDAO.Listar();
            ViewData["Operacao"] = "Edição";

            return View("Form", model);
        }


        [HttpPost]
        public IActionResult Inicia(int id)
        {
            var leilao = _leiloesDAO.ProcurarPorId(id);
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

            _leiloesDAO.Alterar(leilao);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var leilao = _leiloesDAO.ProcurarPorId(id);
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

            _leiloesDAO.Alterar(leilao);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Remove(int id)
        {
            var leilao = _leiloesDAO.ProcurarPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao == SituacaoLeilao.Pregao)
            {
                return StatusCode(405);
            }

            _leiloesDAO.Remover(leilao);
            return NoContent();
        }


        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;
            return View("Index", _leiloesDAO.ProcurarPorTermo(termo));
        }
    }
}

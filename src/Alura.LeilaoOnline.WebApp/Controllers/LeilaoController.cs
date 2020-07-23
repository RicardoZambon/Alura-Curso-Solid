using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class LeilaoController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IBuscaService _buscaService;

        public LeilaoController(IAdminService adminService, IBuscaService buscaService)
        {
            _adminService = adminService;
            this._buscaService = buscaService;
        }


        public IActionResult Index()
        {
            return View(_adminService.ConsultaLeiloes());
        }


        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categorias"] = _adminService.ConsultaCategorias();
            ViewData["Operacao"] = "Inclusão";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _adminService.CadastraLeilao(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _adminService.ConsultaCategorias();
            ViewData["Operacao"] = "Inclusão";
            return View("Form", model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _adminService.ConsultaCategorias();
            ViewData["Operacao"] = "Edição";

            var leilao = _adminService.ConsultaLeilaoPorId(id);
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
                _adminService.ModificaLeilao(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _adminService.ConsultaCategorias();
            ViewData["Operacao"] = "Edição";

            return View("Form", model);
        }


        [HttpPost]
        public IActionResult Inicia(int id)
        {
            var leilao = _adminService.ConsultaLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao != SituacaoLeilao.Rascunho)
            {
                return StatusCode(405);
            }

            _adminService.IniciaPregaoDoLeilaoComId(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var leilao = _adminService.ConsultaLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao != SituacaoLeilao.Pregao)
            {
                return StatusCode(405);
            }

            _adminService.FinalizaPregaoDoLeilaoComId(id);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Remove(int id)
        {
            var leilao = _adminService.ConsultaLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }

            if (leilao.Situacao == SituacaoLeilao.Pregao)
            {
                return StatusCode(405);
            }

            _adminService.RemoveLeilao(leilao);

            return NoContent();
        }


        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;
            return View("Index", _buscaService.PesquisaLeiloesEmPregaoPorTermo(termo));
        }
    }
}

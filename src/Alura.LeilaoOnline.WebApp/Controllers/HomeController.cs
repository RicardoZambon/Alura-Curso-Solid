using Alura.LeilaoOnline.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoService _produtoService;

        public HomeController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        public IActionResult Index()
        {
            return View(_produtoService.ConsultaCategoriasComTotalDeLeiloesEmPregao());
        }


        [Route("[controller]/StatusCode/{statusCode}")]
        public IActionResult StatusCodeError(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("404");
            }
            return View(statusCode);
        }


        [Route("[controller]/Categoria/{categoria}")]
        public IActionResult Categoria(int categoria)
        {
            return View(_produtoService.ConsultaCategoriaPorIdComLeiloesEmPregao(categoria));
        }

        [HttpPost]
        [Route("[controller]/Busca")]
        public IActionResult Busca(string termo)
        {
            ViewData["termo"] = termo;
            var leiloes = _produtoService.PesquisaLeiloesEmPregaoPorTermo(termo);
            return View(leiloes);
        }
    }
}
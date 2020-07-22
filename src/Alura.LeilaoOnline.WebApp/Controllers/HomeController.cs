using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILeiloesDao _leiloesDAO;
        private readonly ICategoriasDao _categoriasDAO;

        public HomeController(ILeiloesDao leiloesDAO, ICategoriasDao categoriasDAO)
        {
            _leiloesDAO = leiloesDAO;
            _categoriasDAO = categoriasDAO;
        }


        public IActionResult Index()
        {
            return View(_categoriasDAO.ListWithAuction());
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
            return View(_categoriasDAO.FindByID(categoria));
        }

        [HttpPost]
        [Route("[controller]/Busca")]
        public IActionResult Busca(string termo)
        {
            ViewData["termo"] = termo;
            var termoNormalized = termo.ToUpper();
            var leiloes = _leiloesDAO.FindByTerm(termoNormalized);
            return View(leiloes);
        }
    }
}
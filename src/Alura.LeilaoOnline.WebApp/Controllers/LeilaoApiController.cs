using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        private readonly ILeiloesDao _leiloesDAO;

        public LeilaoApiController(ILeiloesDao leiloesDAO)
        {
            _leiloesDAO = leiloesDAO;
        }


        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            return Ok(_leiloesDAO.List());
        }


        [HttpGet("{id}")]
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var leilao = _leiloesDAO.FindByID(id);
            if (leilao == null)
            {
                return NotFound();
            }
            return Ok(leilao);
        }


        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao leilao)
        {
            _leiloesDAO.Insert(leilao);
            return Ok(leilao);
        }

        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao leilao)
        {
            _leiloesDAO.Update(leilao);
            return Ok(leilao);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var leilao = _leiloesDAO.FindByID(id);
            if (leilao == null)
            {
                return NotFound();
            }
            _leiloesDAO.Delete(leilao);
            return NoContent();
        }
    }
}
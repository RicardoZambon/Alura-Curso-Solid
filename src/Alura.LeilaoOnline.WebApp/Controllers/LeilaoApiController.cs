using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public LeilaoApiController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            return Ok(_adminService.ConsultaLeiloes());
        }


        [HttpGet("{id}")]
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var leilao = _adminService.ConsultaLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }
            return Ok(leilao);
        }


        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao leilao)
        {
            _adminService.CadastraLeilao(leilao);
            return Ok(leilao);
        }

        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao leilao)
        {
            _adminService.ModificaLeilao(leilao);
            return Ok(leilao);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var leilao = _adminService.ConsultaLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }
            _adminService.RemoveLeilao(leilao);
            return NoContent();
        }
    }
}
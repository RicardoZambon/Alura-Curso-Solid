using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class ArquivamentoAdminService : IAdminService
    {
        private readonly IAdminService _defaultService;

        public ArquivamentoAdminService(ILeilaoDao leilaoDao, ICategoriaDao categoriaDao)
        {
            _defaultService = new DefaultAdminService(leilaoDao, categoriaDao);
        }


        public void CadastraLeilao(Leilao leilao)
            => _defaultService.CadastraLeilao(leilao);



        public IEnumerable<Categoria> ConsultaCategorias()
            => _defaultService.ConsultaCategorias();

        public Leilao ConsultaLeilaoPorId(int id)
            => _defaultService.ConsultaLeilaoPorId(id);

        public IEnumerable<Leilao> ConsultaLeiloes()
            => _defaultService
            .ConsultaLeiloes()
            .Where(x => x.Situacao != SituacaoLeilao.Arquivado);


        public void FinalizaPregaoDoLeilaoComId(int id)
            => _defaultService.FinalizaPregaoDoLeilaoComId(id);

        public void IniciaPregaoDoLeilaoComId(int id)
            => _defaultService.IniciaPregaoDoLeilaoComId(id);


        public void ModificaLeilao(Leilao leilao)
            => _defaultService.ModificaLeilao(leilao);


        public void RemoveLeilao(Leilao leilao)
        {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Arquivado;
                _defaultService.ModificaLeilao(leilao);
            }
        }
    }
}

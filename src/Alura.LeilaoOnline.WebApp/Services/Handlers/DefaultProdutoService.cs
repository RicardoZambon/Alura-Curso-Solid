using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class DefaultProdutoService : IProdutoService
    {
        private readonly ICategoriaDao _categoriaDao;

        public DefaultProdutoService(ICategoriaDao categoriaDao)
        {
            _categoriaDao = categoriaDao;
        }


        public IEnumerable<CategoriaComInfoLeilao> ConsultaCategoriasComTotalDeLeiloesEmPregao()
            => _categoriaDao.List()
                .Select(c => new CategoriaComInfoLeilao
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Imagem = c.Imagem,
                    EmRascunho = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Rascunho).Count(),
                    EmPregao = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Pregao).Count(),
                    Finalizados = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Finalizado).Count(),
                });

        public Categoria ConsultaCategoriaPorIdComLeiloesEmPregao(int id)
            => _categoriaDao.FindByID(id);
    }
}
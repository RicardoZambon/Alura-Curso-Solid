using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class DefaultProdutoService : IProdutoService
    {
        private readonly ILeilaoDao _leilaoDao;
        private readonly ICategoriasDao _categoriaDao;

        public DefaultProdutoService(ILeilaoDao leilaoDao, ICategoriasDao categoriaDao)
        {
            this._leilaoDao = leilaoDao;
            this._categoriaDao = categoriaDao;
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

        public IEnumerable<Leilao> PesquisaLeiloesEmPregaoPorTermo(string termo)
            => _leilaoDao.FindByTerm(termo.ToUpper());
    }
}
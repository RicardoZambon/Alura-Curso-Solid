using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class DefaultBuscaService : IBuscaService
    {
        private readonly ILeilaoDao _leilaoDao;

        public DefaultBuscaService(ILeilaoDao leilaoDao)
        {
            _leilaoDao = leilaoDao;
        }


        public IEnumerable<Leilao> PesquisaLeiloesEmPregaoPorTermo(string termo)
            => _leilaoDao.FindByTerm(termo.ToUpper());
    }
}
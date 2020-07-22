using Alura.LeilaoOnline.WebApp.Models;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO
{
    public interface ILeilaoDao : ICommand<Leilao>, IQuery<Leilao>
    {
        IQueryable<Leilao> FindByTerm(string term);
    }
}
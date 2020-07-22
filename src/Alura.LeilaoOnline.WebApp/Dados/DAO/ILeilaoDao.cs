using Alura.LeilaoOnline.WebApp.Models;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO
{
    public interface ILeilaoDao : IDao<Leilao>
    {
        IQueryable<Leilao> FindByTerm(string term);
    }
}
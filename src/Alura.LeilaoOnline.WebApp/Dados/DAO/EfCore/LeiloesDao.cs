using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore
{
    public class LeiloesDao : BaseDao<Leilao>, ILeiloesDao
    {
        public LeiloesDao(AppDbContext context) : base(context)
        {

        }


        public override IQueryable<Leilao> List()
            => base.List().Include(l => l.Categoria);

        public IQueryable<Leilao> FindByTerm(string term)
            => List()
                .Where(l => string.IsNullOrWhiteSpace(term) ||
                    l.Titulo.ToUpper().Contains(term.ToUpper()) ||
                    l.Descricao.ToUpper().Contains(term.ToUpper()) ||
                    l.Categoria.Descricao.ToUpper().Contains(term.ToUpper())
                );
    }
}
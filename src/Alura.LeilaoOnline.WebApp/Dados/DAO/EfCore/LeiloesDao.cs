using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore
{
    public class LeiloesDao : ILeilaoDao
    {
        private readonly AppDbContext _context;

        public LeiloesDao(AppDbContext context)
        {
            _context = context;
        }


        public virtual IQueryable<Leilao> List()
            => _context.Set<Leilao>()
                .Include(l => l.Categoria);

        public virtual Leilao FindByID(int id)
            => _context.Set<Leilao>().Find(id);


        public virtual void Insert(Leilao entity)
        {
            _context.Set<Leilao>().Add(entity);
            _context.SaveChanges();
        }


        public virtual void Update(Leilao entity)
        {
            _context.Set<Leilao>().Update(entity);
            _context.SaveChanges();
        }


        public virtual void Delete(Leilao entity)
        {
            _context.Set<Leilao>().Remove(entity);
            _context.SaveChanges();
        }


        public IQueryable<Leilao> FindByTerm(string term)
            => List()
                .Where(l => string.IsNullOrWhiteSpace(term) ||
                    l.Titulo.ToUpper().Contains(term.ToUpper()) ||
                    l.Descricao.ToUpper().Contains(term.ToUpper()) ||
                    l.Categoria.Descricao.ToUpper().Contains(term.ToUpper())
                );
    }
}
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore
{
    public class CategoriasDao : ICategoriaDao
    {
        private readonly AppDbContext _context;

        public CategoriasDao(AppDbContext context)
        {
            _context = context;
        }


        public IQueryable<Categoria> List()
            => _context.Set<Categoria>()
                .Include(c => c.Leiloes);


        public Categoria FindByID(int id)
            => _context.Set<Categoria>()
                .Include(x => x.Leiloes)
                .First(x => x.Id == id);
    }
}
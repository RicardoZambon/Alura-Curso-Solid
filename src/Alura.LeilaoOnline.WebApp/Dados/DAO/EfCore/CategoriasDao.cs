using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore
{
    public class CategoriasDao : BaseDao<Categoria>, ICategoriasDao
    {
        public CategoriasDao(AppDbContext context) : base(context)
        {
        }


        public override IQueryable<Categoria> List()
            => base.List()
                .Include(c => c.Leiloes);


        public override Categoria FindByID(int id)
            => _context.Set<Categoria>().Include(x => x.Leiloes).First(x => x.Id == id);
    }
}
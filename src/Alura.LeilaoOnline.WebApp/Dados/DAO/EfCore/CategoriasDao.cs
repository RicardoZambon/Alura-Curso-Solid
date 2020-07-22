using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore
{
    public class CategoriasDao : BaseDao<Categoria>, ICategoriasDao
    {
        public CategoriasDao(AppDbContext context) : base(context)
        {
        }
    }
}
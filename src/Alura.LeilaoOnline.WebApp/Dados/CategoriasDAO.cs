using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public class CategoriasDAO : BaseDAO<Categoria>
    {
        public CategoriasDAO(AppDbContext context) : base(context)
        {
        }
    }
}
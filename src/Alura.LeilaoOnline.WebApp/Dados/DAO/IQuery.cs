using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO
{
    public interface IQuery<T> where T : class
    {
        IQueryable<T> List();

        T FindByID(int id);
    }
}
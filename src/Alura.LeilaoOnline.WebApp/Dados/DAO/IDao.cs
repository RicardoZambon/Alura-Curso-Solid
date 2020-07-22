using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO
{
    public interface IDao<T> where T : class
    {
        IQueryable<T> List();

        T FindByID(int id);


        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
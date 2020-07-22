using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO
{
    public interface ICommand<T> where T : class
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
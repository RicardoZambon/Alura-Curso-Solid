using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.DAO.EfCore
{
    public abstract class BaseDao<T> : IDao<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseDao(AppDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> List()
            => _context.Set<T>();

        public virtual T FindByID(int id)
            => _context.Set<T>().Find(id);


        public virtual void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public abstract class BaseDAO<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseDAO(AppDbContext context)
        {
            _context = context;
        }


        public virtual IQueryable<T> Listar()
            => _context.Set<T>();

        public virtual T ProcurarPorId(int id)
            => _context.Set<T>().Find(id);


        public virtual void Inserir(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Alterar(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void Remover(T leilao)
        {
            _context.Set<T>().Remove(leilao);
            _context.SaveChanges();
        }
    }
}
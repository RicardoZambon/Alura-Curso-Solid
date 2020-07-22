using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class LeiloesDAO : BaseDAO<Leilao>
{
    public LeiloesDAO(AppDbContext context) : base(context)
    {

    }


    public override IQueryable<Leilao> Listar()
        => base.Listar().Include(l => l.Categoria);

    public IQueryable<Leilao> ProcurarPorTermo(string termo)
        => Listar()
            .Where(l => string.IsNullOrWhiteSpace(termo) ||
                l.Titulo.ToUpper().Contains(termo.ToUpper()) ||
                l.Descricao.ToUpper().Contains(termo.ToUpper()) ||
                l.Categoria.Descricao.ToUpper().Contains(termo.ToUpper())
            );
}
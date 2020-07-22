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

        public IQueryable<CategoriaComInfoLeilao> ListWithAuction()
            => List()
                .Select(c => new CategoriaComInfoLeilao
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Imagem = c.Imagem,
                    EmRascunho = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Rascunho).Count(),
                    EmPregao = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Pregao).Count(),
                    Finalizados = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Finalizado).Count(),
                });


        public override Categoria FindByID(int id)
            => _context.Set<Categoria>().Include(x => x.Leiloes).First(x => x.Id == id);
    }
}
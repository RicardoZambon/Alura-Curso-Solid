using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Services
{
    public interface IProdutoService
    {
        Categoria ConsultaCategoriaPorIdComLeiloesEmPregao(int id);

        IEnumerable<CategoriaComInfoLeilao> ConsultaCategoriasComTotalDeLeiloesEmPregao();
    }
}
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Services
{
    public interface IBuscaService
    {
        IEnumerable<Leilao> PesquisaLeiloesEmPregaoPorTermo(string termo);
    }
}
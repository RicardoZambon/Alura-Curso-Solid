using Alura.LeilaoOnline.WebApp.Dados.DAO;
using Alura.LeilaoOnline.WebApp.Models;
using System;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class DefaultAdminService : IAdminService
    {
        private readonly ILeilaoDao _leilaoDao;
        private readonly ICategoriaDao _categoriaDao;

        public DefaultAdminService(ILeilaoDao leilaoDao, ICategoriaDao categoriaDao)
        {
            this._leilaoDao = leilaoDao;
            this._categoriaDao = categoriaDao;
        }


        public void CadastraLeilao(Leilao leilao)
            => _leilaoDao.Insert(leilao);


        public IEnumerable<Categoria> ConsultaCategorias()
            => _categoriaDao.List();

        public Leilao ConsultaLeilaoPorId(int id)
            => _leilaoDao.FindByID(id);

        public IEnumerable<Leilao> ConsultaLeiloes()
            => _leilaoDao.List();


        public void FinalizaPregaoDoLeilaoComId(int id)
        {
            var leilao = _leilaoDao.FindByID(id);
            if (leilao != null && leilao.Situacao == SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Finalizado;
                leilao.Termino = DateTime.Now;
                _leilaoDao.Update(leilao);
            }
        }

        public void IniciaPregaoDoLeilaoComId(int id)
        {
            var leilao = _leilaoDao.FindByID(id);
            if (leilao != null && leilao.Situacao == SituacaoLeilao.Rascunho)
            {
                leilao.Situacao = SituacaoLeilao.Pregao;
                leilao.Termino = DateTime.Now;
                _leilaoDao.Update(leilao);
            }
        }


        public void ModificaLeilao(Leilao leilao)
        {
            _leilaoDao.Update(leilao);
        }


        public void RemoveLeilao(Leilao leilao)
        {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao)
            {
                _leilaoDao.Delete(leilao);
            }
        }
    }
}
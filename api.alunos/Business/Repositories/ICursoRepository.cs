using api.alunos.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.alunos.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();

        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.alunos.Models.Usuarios
{
    public class ValidaCampoViewModelOutput
    {
        public IEnumerable<string> Erros { get; set; }

        public ValidaCampoViewModelOutput(IEnumerable<string> erros) 
        {
            Erros = erros;
        }
    }
}

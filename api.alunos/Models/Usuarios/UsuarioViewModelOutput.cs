using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.alunos.Controllers
{
    public class UsuarioViewModelOutput
    {
        public int Codigo { get; internal set; }
        public string Login { get; internal set; }
        public string Email { get; internal set; }
    }
}

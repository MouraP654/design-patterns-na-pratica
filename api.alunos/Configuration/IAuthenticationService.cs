using api.alunos.Controllers;

namespace api.alunos.Configuration
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}

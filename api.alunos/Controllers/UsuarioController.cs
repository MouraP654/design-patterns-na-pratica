using api.alunos.Business.Entities;
using api.alunos.Business.Repositories;
using api.alunos.Configuration;
using api.alunos.Filters;
using api.alunos.Infrastructure.Data;
using api.alunos.Infrastructure.Data.Repositories;
using api.alunos.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace api.alunos.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]

    
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;
        public UsuarioController(
            IUsuarioRepository usuarioRepository, IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado
        /// </summary>
        /// <param name = "loginViewModelInput"> View model do login </param>
        /// <returns> Retorna status ok, dados do usuários e o token</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("login")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViwModelInput)
        {

            var usuario = _usuarioRepository.ObterUsuario(loginViwModelInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar");
            }
            //if (usuario.Senha != loginViwModel.Senha.GerarSenhaCriptografada()) 
            //{
            //    return BadRequest("Houve um erro ao tentar acessar");
            //}

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViwModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new { 
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite Cadastrar um novo usuário
        /// </summary>
        /// <param name = "loginViewModelInput"> View model do login </param>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar Usuário")]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput loginViewModelInput)
        {
            var usuario = new Usuario();
            usuario.Login = loginViewModelInput.Login;
            usuario.Senha = loginViewModelInput.Senha;
            usuario.Email = loginViewModelInput.Email;
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();


            return Created("", loginViewModelInput);
        }

    }
}

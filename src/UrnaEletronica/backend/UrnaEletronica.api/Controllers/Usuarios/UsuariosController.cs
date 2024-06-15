

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Dtos.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;

namespace UrnaEletronica.api.Controllers.Usuarios
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly ITokenServico _tokenServico;

        public UsuariosController(IUsuarioServico usuarioServico, ITokenServico tokenServico)
        {
            _usuarioServico = usuarioServico;
            _tokenServico = tokenServico;
        }

        [HttpPost("CreateUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                if (await _usuarioServico.VerificarUsuarioExisteAsync(usuarioDto.UserName)) return BadRequest("Conta já  criada!");

                var usuario = await _usuarioServico.CreateUsuario(usuarioDto);

                if (usuario != null)
                {
                    return Ok(
                        new
                        {
                            userName = usuario.UserName,
                            nome = usuario.Nome,
                            id = usuario.Id,
                            token = _tokenServico.CreateToken(usuario).Result
                        }
                        );
                }
                return BadRequest("Conta não cadastrada!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar conta. Erro: {ex.Message}");
            }
        }
        [HttpPut("UpdateUsuario")]
        public async Task<IActionResult> UpdateUsuario(UsuarioUpdateDto usuarioUpdateDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                var usuarioUpdated = await _usuarioServico.UpdateUsuario(usuarioUpdateDto);

                if (usuarioUpdated == null) return NoContent();

                
                return Ok(
                    new
                    {
                        userName = usuario.UserName,
                        nome = usuario.Nome,
                        id = usuario.Id,
                        token = _tokenServico.CreateToken(usuarioUpdated).Result
                    }
                    );
                                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar conta. Erro: {ex.Message}");
            }
        }
    
        [HttpGet("GetUsuarios/{nome}/nome")]
        public async Task<IActionResult> GetUsuariosByNome(string nome)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var usuarios = await _usuarioServico.GetAllUsuariosByNomeAsync(nome);
                if (usuarios == null) return NoContent();
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar conta. Erro: {ex.Message}");
            }
        }
        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios() 
        {
            try 
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var usuarios = await _usuarioServico.GetAllUsuariosAsync();
                if (usuarios == null) return NoContent();
                return Ok();
            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar conta. Erro: {ex.Message}");
            }
        }
        [HttpGet("GetUsuario/{id}")]
        public async Task<IActionResult> GetUsuarioById(int usuarioId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var usuario = await _usuarioServico.GetUsuarioByIdAsync(usuarioId);
                if (usuario == null) return NoContent();
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar conta por Id. Erro: {ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto) {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(usuarioLoginDto.UserName);
                if (usuario == null) return Unauthorized();

                var validacaoUsuario = await _usuarioServico.CompararSenhaUsuarioAsync(usuario, usuarioLoginDto.Password);

                if (!validacaoUsuario.Succeeded) return Unauthorized("Conta ou senha inválida.");


                return Ok(
                    new
                    {
                        userName = usuario.UserName,
                        nome = usuario.Nome,
                        id = usuario.Id,
                        token = _tokenServico.CreateToken(usuario).Result
                    }
                    );

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar conta. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetUserName")]
        public async Task<IActionResult> GetUsuarioByUserName()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();

                if (claimUserName == null) return Unauthorized();

                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(claimUserName);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar conta. Erro: {ex.Message}");
            }
        }
    }
}

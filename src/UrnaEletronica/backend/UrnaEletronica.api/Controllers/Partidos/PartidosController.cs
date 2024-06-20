using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Dtos.Partidos;
using UrnaEletronica.Servico.Servicos.Contratos.Partidos;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;

namespace UrnaEletronica.api.Controllers.Partidos
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PartidosController : ControllerBase
    {
        private readonly IPartidoServico _partidoServico;
        private readonly IUsuarioServico _usuarioServico;


        public PartidosController(IPartidoServico partidoServico, IUsuarioServico usuarioServico)
        {
            _partidoServico = partidoServico;
            _usuarioServico = usuarioServico;

        }

        /// <summary>
        /// Obt�m os dados de todos os partidos cadastrados 
        /// </summary>
        /// <response code="200">Dados dos partidos cadastrados</response>
        /// <response code="400">Par�metros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet()]
        public async Task<IActionResult> GetPartidos()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var partidos = await _partidoServico.GetAllPartidosAsync();
                if (partidos == null) return NotFound("N�o existem partidos cadastrados.");
                return Ok(partidos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar partidos. Erro: {ex.Message}");
            }
        }
        /// <summary>
        /// Obt�m os dados de um partido espec�fico
        /// </summary>
        /// <param name="partidoId">Identificador do partido</param>
        /// <response code="200">Dados do partido consultado</response>
        /// <response code="400">Par�metros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet("{partidoId}")]
        public async Task<IActionResult> GetPartidoByIdAsync(int partidoId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var partido = await _partidoServico.GetPartidoByIdAsync(partidoId);
                if (partido == null) return NotFound("N�o existe partido cadastrado para o ID informado.");
                return Ok(partido);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar partido por Id. Erro: {ex.Message}");
            }
        }


        /// <summary>
        /// Realiza inclus�o de um partido 
        /// </summary>
        /// <response code="200">Dados dos partidos cadastrados</response>
        /// <response code="400">Par�metros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpPost]
        public async Task<IActionResult> CreatePartido(PartidoDto partidoDto)

        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var partido = await _partidoServico.GetPartidoByIdAsync(partidoDto.Id);
                if (partido != null) return BadRequest("J� existe um partido cadastrado.");

                var createdPartido = await _partidoServico.CreatePartido(partidoDto);
                if (createdPartido != null) return Ok(partido);
                return BadRequest("Ocorreu um erro ao incluir partido");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar partido. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a atualiza��o dos dados de um partido
        /// </summary>
        /// <param name="partidoId">Identificador do partido</param>
        /// <param name="partidoDto">partido cadastrado</param>
        /// <response code="200">partido atualizado com sucesso</response>
        /// <response code="400">Par�metros incorretos</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("partidoId")]
        public async Task<IActionResult> UpdatePartido(int partidoId, PartidoDto partidoDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                var partidoUpdated = await _partidoServico.UpdatePartido(partidoId, partidoDto);

                if (partidoUpdated == null) return NotFound("N�o existe partido cadastrado para o Id informado.");


                return Ok(partidoUpdated);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar partido. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obt�m os dados de um partido espec�fico
        /// </summary>
        /// <param name="partidoId">Identificador do partido</param>
        /// <response code="200">Dados do partido consultado</response>
        /// <response code="400">Par�metros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 
        [HttpDelete("{partidoId}")]
        public async Task<IActionResult> DeletePartido(int partidoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                if (await _partidoServico.DeletePartido(partidoId)) return Ok(new
                {
                    message = "Ok"
                });


                return BadRequest("Ocorreu um erro ao deletar partido");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar partido. Erro: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Servicos.Contratos.Coligacoes;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Dtos.Coligacoes;

namespace UrnaEletronica.api.Controllers.Coligacoes
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ColigacoesController : ControllerBase
    {
        private readonly IColigacaoServico _coligacaoServico;
        private readonly IUsuarioServico _usuarioServico;

        public ColigacoesController(IColigacaoServico coligacaoServico, IUsuarioServico usuarioServico)
        {
            _coligacaoServico = coligacaoServico;
            _usuarioServico = usuarioServico;
        }

        /// <summary>
        /// Obtém os dados de todos as coligacoes cadastradas 
        /// </summary>
        /// <response code="200">Dados das coligacoes cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet()]
        public async Task<IActionResult> GetColigacoes()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var coligacoes = await _coligacaoServico.GetAllColigacoesAsync();
                if (coligacoes == null) return NotFound("Não existem coligacoes cadastradas.");
                return Ok(coligacoes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar coligacoes. Erro: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtém os dados de uma coligacao específica
        /// </summary>
        /// <param name="coligacaoId">Identificador da coligacao</param>
        /// <response code="200">Dados da coligacao consultada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet("{coligacaoId}")]
        public async Task<IActionResult> GetColigacaoByIdAsync(int coligacaoId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var coligacao = await _coligacaoServico.GetColigacaoByIdAsync(coligacaoId);
                if (coligacao == null) return NotFound("Não existe coligacao cadastrada para o ID informado.");
                return Ok(coligacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar coligacao por Id. Erro: {ex.Message}");
            }
        }


        /// <summary>
        /// Realiza inclusão de uma coligacao 
        /// </summary>
        /// <response code="200">Dados das coligacoes cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpPost]
        public async Task<IActionResult> CreateColigacao(ColigacaoDto coligacaoDto)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var coligacao = await _coligacaoServico.GetColigacaoByIdAsync(coligacaoDto.Id);
                if (coligacao != null) return BadRequest("Já existe uma coligacao cadastrada.");

                var createdColigacao = await _coligacaoServico.CreateColigacao(coligacaoDto);
                if (createdColigacao != null) return Ok(createdColigacao);
                return BadRequest("Ocorreu um erro ao incluir coligacao");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar coligacao. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a atualização dos dados de uma coligacao
        /// </summary>
        /// <param name="coligacaoId">Identificador da coligacao</param>
        /// <param name="coligacaoDto">Coligacao cadastrada</param>
        /// <response code="200">Coligacao atualizada com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("coligacaoId")]
        public async Task<IActionResult> UpdateColigacao(int coligacaoId, ColigacaoDto coligacaoDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                var coligacaoUpdated = await _coligacaoServico.UpdateColigacao(coligacaoId, coligacaoDto);

                if (coligacaoUpdated == null) return NotFound("Não existe coligacao cadastrada para o Id informado.");


                return Ok(coligacaoUpdated);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar coligacao. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém os dados de uma coligacao específica
        /// </summary>
        /// <param name="coligacaoId">Identificador da coligacao</param>
        /// <response code="200">Dados da coligacao consultada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 
        [HttpDelete("{coligacaoId}")]
        public async Task<IActionResult> DeleteColigacao(int coligacaoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                if (await _coligacaoServico.DeleteColigacao(coligacaoId)) return Ok(new
                {
                    message = "Ok"
                });


                return BadRequest("Ocorreu um erro ao deletar coligacao");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar coligacao. Erro: {ex.Message}");
            }
        }
    }
}

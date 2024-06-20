using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Resultados;
using UrnaEletronica.Servico.Dtos.Resultado;

namespace UrnaEletronica.api.Controllers.Resultados
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResultadosController : ControllerBase
    {
        private readonly IResultadoServico _resultadoServico;
        private readonly IUsuarioServico _usuarioServico;

        public ResultadosController(IResultadoServico resultadoServico, IUsuarioServico usuarioServico)
        {
            _resultadoServico = resultadoServico;
            _usuarioServico = usuarioServico;
        }

        /// <summary>
        /// Obtém os dados de todos resultados cadastrados
        /// </summary>
        /// <response code="200">Dados das resultados cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet()]
        public async Task<IActionResult> GetColigacoes()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var resultados = await _resultadoServico.GetAllResultadosAsync();
                if (resultados == null) return NotFound("Não existem resultados cadastradas.");
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar resultados. Erro: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtém os dados de uma resultado específico
        /// </summary>
        /// <param name="resultadoId">Identificador do resultado</param>
        /// <response code="200">Dados do resultado consultado</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet("{resultadoId}")]
        public async Task<IActionResult> GetResultadoByIdAsync(int resultadoId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var resultado = await _resultadoServico.GetResultadoByIdAsync(resultadoId);
                if (resultado == null) return NotFound("Não existe resultado cadastrado para o ID informado.");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar resultado por Id. Erro: {ex.Message}");
            }
        }


        /// <summary>
        /// Realiza inclusão de uma resultado 
        /// </summary>
        /// <response code="200">Dados das resultados cadastrados</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpPost]
        public async Task<IActionResult> CreateResultado(ResultadoDto resultadoDto)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var resultado = await _resultadoServico.GetResultadoByIdAsync(resultadoDto.Id);
                if (resultado != null) return BadRequest("Já existe uma resultado cadastrado.");

                var createdResultado = await _resultadoServico.CreateResultado(resultadoDto);
                if (createdResultado != null) return Ok(resultado);
                return BadRequest("Ocorreu um erro ao incluir resultado");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar resultado. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a atualização dos dados de um resultado
        /// </summary>
        /// <param name="resultadoId">Identificador do resultado</param>
        /// <param name="resultadoDto">Resultado cadastrad</param>
        /// <response code="200">Resultado atualizada com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("resultadoId")]
        public async Task<IActionResult> UpdateResultado(int resultadoId, ResultadoDto resultadoDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                var resultadoUpdated = await _resultadoServico.UpdateResultado(resultadoId, resultadoDto);

                if (resultadoUpdated == null) return NotFound("Não existe resultado cadastrado para o Id informado.");


                return Ok(resultadoUpdated);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar resultado. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém os dados de um resultado específica
        /// </summary>
        /// <param name="resultadoId">Identificador dp resultado</param>
        /// <response code="200">Dados do resultado consultado</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 
        [HttpDelete("{resultadoId}")]
        public async Task<IActionResult> DeleteResultado(int resultadoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                if (await _resultadoServico.DeleteResultado(resultadoId)) return Ok(new
                {
                    message = "Ok"
                });


                return BadRequest("Ocorreu um erro ao deletar resultado");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar resultado. Erro: {ex.Message}");
            }
        }
    }
}

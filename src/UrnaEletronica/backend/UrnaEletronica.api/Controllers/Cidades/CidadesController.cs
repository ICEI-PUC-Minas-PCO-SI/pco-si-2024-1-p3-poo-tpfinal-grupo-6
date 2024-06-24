using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;

namespace UrnaEletronica.api.Controllers.Cidades
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CidadesController : ControllerBase 
    {
        private readonly ICidadeServico _cidadeServico;
        private readonly IUsuarioServico _usuarioServico;
        

        public CidadesController(ICidadeServico cidadeServico, IUsuarioServico usuarioServico )
        {
            _cidadeServico = cidadeServico;
            _usuarioServico = usuarioServico;
            
        }

        /// <summary>
        /// Obtém os dados de todos as cidades cadastradas 
        /// </summary>
        /// <response code="200">Dados das cidades cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        
        [HttpGet()]
        public async Task<IActionResult> GetCidades()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var cidades = await _cidadeServico.GetAllCidadesAsync();
                if (cidades == null) return NotFound("Não existem cidades cadastradas.");
                return Ok(cidades);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar cidades. Erro: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtém os dados de uma cidade específica
        /// </summary>
        /// <param name="cidadeId">Identificador da cidade</param>
        /// <response code="200">Dados da cidade consultada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet("{cidadeId}")]
        public async Task<IActionResult> GetCidadeByIdAsync(int cidadeId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var cidade = await _cidadeServico.GetCidadeByIdAsync(cidadeId);
                if (cidade == null) return NotFound("Não existe cidade cadastrada para o ID informado.");
                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar cidade por Id. Erro: {ex.Message}");
            }
        }


        /// <summary>
        /// Realiza inclusão de uma cidade 
        /// </summary>
        /// <response code="200">Dados das cidades cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpPost]
        public async Task<IActionResult> CreateCidade(CidadeDto cidadeDto)
        
            {
                try
                {
                    var claimUserName = User.GetUserNameClaim();
                    if (claimUserName == null) return Unauthorized();

                    var cidade = await _cidadeServico.GetCidadeByIdAsync(cidadeDto.Id);
                    if (cidade != null) return BadRequest("Já existe uma cidade cadastrada.");

                    var createdCidade = await _cidadeServico.CreateCidade(cidadeDto);
                    if (createdCidade != null) return Ok(createdCidade);
                    return BadRequest("Ocorreu um erro ao incluir cidade");

                }
                catch (Exception ex)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar cidade. Erro: {ex.Message}");
                }
            }

        /// <summary>
        /// Realiza a atualização dos dados de uma cidade
        /// </summary>
        /// <param name="cidadeId">Identificador da cidade</param>
        /// <param name="cidadeDto">Cidade cadastrada</param>
        /// <response code="200">Cidade atualizada com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("cidadeId")]
        public async Task<IActionResult> UpdateCidade(int cidadeId, CidadeDto cidadeDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                var cidadeUpdated = await _cidadeServico.UpdateCidade(cidadeId, cidadeDto);

                if (cidadeUpdated == null) return NotFound("Não existe cidade cadastrada para o Id informado.");


                return Ok(cidadeUpdated);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar cidade. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém os dados de uma cidade específica
        /// </summary>
        /// <param name="cidadeId">Identificador da cidade</param>
        /// <response code="200">Dados da cidade consultada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 
        [HttpDelete("{cidadeId}")]
        public async Task<IActionResult> DeleteCidade(int cidadeId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                if (await _cidadeServico.DeleteCidade(cidadeId)) return Ok(new
                {
                    message = "Ok"
                });


                return BadRequest("Ocorreu um erro ao deletar cidade");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar cidade. Erro: {ex.Message}");
            }
        }
    }
}

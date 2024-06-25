using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Dtos.ParametrosEleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.ParametrosEleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Implementacoes.Cidades;

namespace UrnaEletronica.api.Controllers.ParametrosEleicoes
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ParametrosEleicoesController : Controller 
    {
        private readonly IParametroEleicaoServico _parametroEleicaoServico;
        private readonly IUsuarioServico _usuarioServico;
        

        public ParametrosEleicoesController(IParametroEleicaoServico parametroEleicaoServico, IUsuarioServico usuarioServico )
        {
            _parametroEleicaoServico = parametroEleicaoServico;
            _usuarioServico = usuarioServico;
            
        }

        /// <summary>
        /// Obtém os dados de dos parâmetros de eleições
        /// </summary>
        /// <response code="200">Dados dos parâmetros cadastrados</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        
        [HttpGet()]
        public async Task<IActionResult> GetParametros()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var parametros = await _parametroEleicaoServico.GetParametrosAsync();
                if (parametros == null) return NotFound("Não existe parâmetro cadastrado.");
                return Ok(parametros);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar parâmetro de eleição. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém os dados de um parâmetro
        /// </summary>
        /// <param name="parametroId">Identificador da cidade</param>
        /// <response code="200">Dados da cidade consultada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpGet("{parametroId}")]
        public async Task<IActionResult> GetParamtroByIdAsync(int parametroId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();
                if (claimUserName == null) return Unauthorized();

                var cidade = await _parametroEleicaoServico.GetParametroByIdAsync(parametroId);
                if (cidade == null) return NotFound("Não existe cidade cadastrada para o ID informado.");
                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar cidade por Id. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza inclusão de parâmetros
        /// </summary>
        /// <response code="200">Dados do parâmetro cadastrado</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [HttpPost]
        public async Task<IActionResult> CreateParametroEleicao(ParametroEleicaoDto parametroEleicaoDto)
        
            {
                try
                {
                    var claimUserName = User.GetUserNameClaim();
                    if (claimUserName == null) return Unauthorized();

                    var parametro = await _parametroEleicaoServico.GetParametroByIdAsync(parametroEleicaoDto.Id);
                    if (parametro != null) return BadRequest("Já existe um parâmetro cadastrado.");

                    var createdParametro = await _parametroEleicaoServico.CreateParametroEleicao(parametroEleicaoDto);
                    if (createdParametro != null) return Ok(createdParametro);
                    return BadRequest("Ocorreu um erro ao incluir parâmetro");

                }
                catch (Exception ex)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar parâmetro. Erro: {ex.Message}");
                }
            }

        /// <summary>
        /// Realiza a atualização dos dados de um parâmetro
        /// </summary>
        /// <param name="parametroId">Identificador do parâmetro</param>
        /// <param name="parametroEleicaoDto">parâmetro cadastrada</param>
        /// <response code="200">parâmetro atualizada com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("parametroId")]
        public async Task<IActionResult> UpdateParametroEleicao(int parametroId, ParametroEleicaoDto parametroEleicaoDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());
                if (usuario == null) return Unauthorized();

                var parametroUpdated = await _parametroEleicaoServico.UpdateParametroEleicao(parametroId, parametroEleicaoDto);

                if (parametroUpdated == null) return NotFound("Não existe parâmetro cadastrado para o Id informado.");


                return Ok(parametroUpdated);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar parâmetro. Erro: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Eleicoes;
using UrnaEletronica.Servico.Servicos.Implementacoes.Eleicoes;
using UrnaEletronica.Dominio.Modelos.Eleicoes;

namespace UrnaEletronica.api.Controllers.Eleicoes
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EleicoesController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IEleicaoServico _eleicaoServico;

        public EleicoesController(IUsuarioServico usuarioServico, IEleicaoServico eleicaoServico)
        {
            _usuarioServico = usuarioServico;
            _eleicaoServico = eleicaoServico;
        }


        /// <summary>
        /// Calcular vencedor processo de eleição
        /// </summary>
        /// <param name="eleicaoId">Identificador da eleição</param>
        /// <response code="200">Dados do resultado das Eleições</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [AllowAnonymous]
        [HttpPost("{eleicaoId}/calcularVencedor")]
        public async Task<IActionResult> CalcularVencedor(int eleicaoId)
        {
            try
            {
                Console.WriteLine("1===============================================================");
                var eleicao = await _eleicaoServico.GetEleicaoByIdAsync(eleicaoId);

                if (eleicao == null) return BadRequest("Não existe processo de Eleição em andamento.");

                if (!eleicao.IniciarVotacao) return BadRequest("Não existe processo de Eleição em andamento, vencedor não disponível.");

                Console.WriteLine("2===============================================================");
                var resultadoEleicao =  _eleicaoServico.CalcularResultado(eleicaoId);
                Console.WriteLine("3===============================================================");
                if (resultadoEleicao == null) return BadRequest("Não foi possível apurar vencedor.");

                return Ok(resultadoEleicao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar vencedor. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Inicia processo de eleição
        /// </summary>
        /// <response code="200">Retorna true (verdadeiro) se inicialização OK.</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [Authorize]
        [HttpPost("iniciarEleicao")]
        public async Task<IActionResult> IniciarEleicao()
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();

                if (!usuario.IsAdmin) return Unauthorized();

                if (await _eleicaoServico.IniciarEleicaoAsync()) return Ok(true);

                return BadRequest("Falha ao iniciar eleições.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar coligacoes. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Encerrar processo de eleição
        /// </summary>
        /// <param name="eleicaoId">Identificador da eleição</param>
        /// <response code="200">Dretorn true (verdadeiro) se a eleição for encerrada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [Authorize]
        [HttpPost("{eleicaoId}/encerrarEleicao")]
        public async Task<IActionResult> EncerrarEleicao(int eleicaoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();

                if (!usuario.IsAdmin) return Unauthorized();

                var eleicao = await _eleicaoServico.GetEleicaoByIdAsync(eleicaoId);

                if (eleicao == null) return BadRequest("Não existe processo de Eleição em andamento.");

                if (!eleicao.IniciarVotacao) return BadRequest("Não existe processo de Eleição em andamento.");

                if (eleicao.EncerrarVotacao) return BadRequest("Eleição já encerrada.");

                if (await _eleicaoServico.EncerrarEleicaoAsync(eleicaoId)) return Ok(true);

                return BadRequest("Falha ao encerrar eleição.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao encerrar eleicao. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera um processo de Eleição por eleicaoId
        /// </summary>
        /// <param name="eleicaoId">Identificador da eleição</param>
        /// <response code="200">Dados das eleição cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        [Authorize]
        [HttpGet("{eleicaoId}")]
        public async Task<IActionResult> GetEleicaoById(int eleicaoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();

                var eleicao = await _eleicaoServico.GetEleicaoByIdAsync(eleicaoId);

                if (eleicao == null) return BadRequest("Não existe processo de Eleição em andamento.");

                return Ok(eleicao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar eleição por Id. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Encerrar todos os processos de eleição
        /// </summary>
        /// <response code="200">Dados das eleições cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllEleicoes()
        {
            try
            {
                var eleicao = await _eleicaoServico.GetAllEleicoesAsync();

                if (eleicao == null) return BadRequest("Não existem processos de Eleições em andamento.");

                return Ok(eleicao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar eleições. Erro: {ex.Message}");
            }
        }

    }
}

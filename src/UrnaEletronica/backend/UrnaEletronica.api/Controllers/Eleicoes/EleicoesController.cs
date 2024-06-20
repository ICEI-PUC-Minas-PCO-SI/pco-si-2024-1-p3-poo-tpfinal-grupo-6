using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Eleicoes;
using UrnaEletronica.Servico.Servicos.Implementacoes.Eleicoes;

namespace UrnaEletronica.api.Controllers.Eleicoes
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EleicoesController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IEleicaoExecutivaServico _eleicaoExecutivaServico;
        private readonly IEleicaoLegislativaServico _eleicaoLegislativaServico;

        public EleicoesController(IUsuarioServico usuarioServico, IEleicaoExecutivaServico eleicaoExecutivaServico, IEleicaoLegislativaServico eleicaoLegislativaServico)
        {
            _usuarioServico = usuarioServico;
            _eleicaoExecutivaServico = eleicaoExecutivaServico;
            _eleicaoLegislativaServico = eleicaoLegislativaServico;
        }


        /// <summary>
        /// Calcular vencedor processo de eleição Executiva
        /// </summary>
        /// <response code="200">Dados das coligacoes cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [Authorize]
        [HttpGet("calcularVencedorExecutivo/executivo")]
        public async Task<IActionResult> CalcularVencedorExecutivo()
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();

                if (!usuario.IsAdmin) return Unauthorized();

                if (!_eleicaoExecutivaServico.EleicaoEmAndamento()) return BadRequest("Não existe processo de Eleição em andamento, vencedor não disponível.");

                var resultadoEleicao =  _eleicaoExecutivaServico.CalcularResultado();

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
        /// <response code="200">Dados das coligacoes cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [Authorize]
        [HttpGet("iniciarEleicao")]
        public async Task<IActionResult> IniciarEleicao()
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();

                if (!usuario.IsAdmin) return Unauthorized();

                if (_eleicaoExecutivaServico.EleicaoEmAndamento()) return BadRequest("Eleição em andamento, não pode ser iniciada.");

                if (_eleicaoExecutivaServico.EleicaoEncerrada()) return BadRequest("Eleição finalizada, não pode ser iniciada.");

                if (_eleicaoExecutivaServico.IniciarEleicao()) return Ok("Eleição iniciada.");

                return BadRequest("Falha ao iniciar eleição.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar coligacoes. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Encerra processo de eleição
        /// </summary>
        /// <response code="200">Dados das coligacoes cadastradas</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>

        [Authorize]
        [HttpGet("encerrarEleicao")]
        public async Task<IActionResult> EncerrarEleicao()
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();

                if (!usuario.IsAdmin) return Unauthorized();

                if (!_eleicaoExecutivaServico.EleicaoEmAndamento()) return BadRequest("Não existe processo de Eleição em andamento, não pode ser encerrada.");

                if (_eleicaoExecutivaServico.EleicaoEncerrada()) return BadRequest("Eleição já encerrada.");

                if (_eleicaoExecutivaServico.EncerrarEleicao()) return Ok("Eleição encerrada.");

                return BadRequest("Falha ao encerrar eleição.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar vencedor. Erro: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("calcularVencedorLegislativo/legislativo")]
        public async Task<IActionResult> CalcularVencedorLegislativo()
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (usuario == null) return Unauthorized();
                if (!usuario.IsAdmin) return Unauthorized();

                if (!_eleicaoLegislativaServico.EleicaoEmAndamento()) return BadRequest("Não existe processo de Eleição em andamento, vencedor não disponível.");

                var resultadoEleicao = _eleicaoLegislativaServico.CalcularResultado();

                if (resultadoEleicao == null) return BadRequest("Não foi possível apurar vencedor.");

                return Ok(resultadoEleicao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar vencedor. Erro: {ex.Message}");
            }
        }
    }
}

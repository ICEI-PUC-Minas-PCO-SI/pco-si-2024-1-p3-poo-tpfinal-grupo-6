using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Implementacoes.Usuarios;

namespace UrnaEletronica.api.Controllers.Candidatos
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatoController : Controller
    {
        private readonly ICandidatoServico _candidatoServico;
        private readonly IUsuarioServico _usuarioServico;

        public CandidatoController(ICandidatoServico candidatoServico, IUsuarioServico usuarioServico)
        {
            _candidatoServico = candidatoServico;
            _usuarioServico = usuarioServico;
        }

        /// <summary>
        /// Obtém os dados de todos os candidatos cadastrados 
        /// </summary>
        /// <response code="200">Dados dos candidatos</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpGet]
        public async Task<IActionResult> GetCandidatos()
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();

                if (claimUserName == null) return Unauthorized();

                var candidatos = await _candidatoServico.GetAllCandidatosAsync();

                if (candidatos == null) return NotFound("Não existem candidatos cadastrados.");

                return Ok(candidatos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar candidatos. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém os dados de um candidato específico
        /// </summary>
        /// <param name="candidatoId">Identificador do candidato</param>
        /// <response code="200">Dados do candidato consultada</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpGet("{candidatoId}")]
        public async Task<IActionResult> GetCandidatoByIdAsync(int candidatoId)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();

                if (claimUserName == null) return Unauthorized();

                var candidato = await _candidatoServico.GetCandidatoByIdAsync(candidatoId);

                if (candidato == null) return NotFound("Não existe candidato cadastrado para o ID informado.");

                return Ok(candidato);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar candidato por Id. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza inclusão de um candidato 
        /// </summary>
        /// <param name="candidatoDto">Candidato a ser cadastrado</param>
        /// <response code="200">Dados do candidato cadastrado</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpPost]
        public async Task<IActionResult> CreateCandidato(CandidatoDto candidatoDto)
        {
            try
            {
                var claimUserName = User.GetUserNameClaim();

                if (claimUserName == null) return Unauthorized();

                var candidato = await _candidatoServico.GetCandidatoByIdAsync(candidatoDto.Id);
                
                if (candidato != null) return BadRequest("Já existe um candidato cadastrado.");

                var createdCandidato = await _candidatoServico.CreateCandidato(candidatoDto);

                if (createdCandidato != null) return Ok(candidato);

                return BadRequest("Ocorreu um erro ao incluir candidato.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar candidado. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a atualização dos dados de um candidato
        /// </summary>
        /// <param name="candidatoId">Identificador do candidato</param>
        /// <param name="candidatoDto">Candidato a ser alterado</param>
        /// <response code="200">Candidato atualizado com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpPut("candidatoId")]
        public async Task<IActionResult> UpdateCandidato(int candidatoId, CandidatoDto candidatoDto)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());

                if (usuario == null) return Unauthorized();

                var candidatoUpdated = await _candidatoServico.UpdateCandidato(candidatoId, candidatoDto);

                if (candidatoUpdated == null) return NotFound("Não existe candiato cadastrado para o Id informado.");

                return Ok(candidatoUpdated);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar candidato. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui os dados de um candidato específico
        /// </summary>
        /// <param name="candidatoId">Identificador do candidato</param>
        /// <response code="200">Dados do candidato excluído</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpDelete("{candidatoId}")]
        public async Task<IActionResult> DeleteCandidato(int candidatoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());

                if (usuario == null) return Unauthorized();

                if (await _candidatoServico.DeleteCandidato(candidatoId)) return Ok(new { message = "Ok" });

                return BadRequest("Ocorreu um erro ao deletar candidato");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar candidato. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a votação online para um candidato
        /// </summary>
        /// <param name="candidatoId">Identificador do candidato</param>
        /// <response code="200">Voto registrado com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpPatch("{candidatoId}/VotoOnline")]
        public async Task<IActionResult> RegistrarVoto(int candidatoId)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());

                if (usuario == null) return Unauthorized();

                var registrarVoto = await _candidatoServico.RegistrarVoto(candidatoId);

                if (registrarVoto == null) return NotFound("Não existe canditado cadastrado para votação");

                return Ok(registrarVoto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao registrar voto online para o candidato. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a votação batch para um candidato
        /// </summary>
        /// <param name="candidatoId">Identificador do candidato</param>
        /// <param name="qtdVotos">Quantidade de votos do candidato</param>
        /// <response code="200">Voto registrado com sucesso</response>
        /// <response code="400">Parâmetros incorretos</response>
        /// <response code="500">Erro interno</response>
        /// 

        [HttpPatch("{emprestimoId}/{qtdVotos}/VotoBatch")]
        public async Task<IActionResult> RegistrarVoto(int candidatoId, int qtdVotos)
        {
            try
            {
                var usuario = await _usuarioServico.GetUsuarioByIdAsync(User.GetUserIdClaim());

                if (usuario == null) return Unauthorized();

                var registrarVoto = await _candidatoServico.RegistrarVoto(candidatoId, qtdVotos);

                if (registrarVoto == null) return NotFound("Não existe canditado cadastrado para votação");

                return Ok(registrarVoto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao registrar voto batch para o candidato. Erro: {ex.Message}");
            }
        }
    }
}

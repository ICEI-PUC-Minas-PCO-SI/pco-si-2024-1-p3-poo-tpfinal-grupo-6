using BibCorpPrevenir2.api.Util.Services.Interfaces.Contracts.Uploads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UrnaEletronica.api.Util.Extensions.Security;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;

namespace UrnaEletronica.api.Controllers.UpLoads
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UpLoadsController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private readonly IUsuarioServico _usuarioServico;
        private readonly ICandidatoServico _candidatoServico;
        private readonly string _destinoFoto = "Fotos";
        private readonly string _destinoFotoCandidatos = "Candidatos";

        public UpLoadsController(IUploadService uploadService, IUsuarioServico usuarioServico, ICandidatoServico candidatoServico)
        {
            _uploadService = uploadService;
            _usuarioServico = usuarioServico;
            _candidatoServico = candidatoServico;
        }
        [HttpPost("upload-user-photo")]
        public async Task<IActionResult> UploadFotoUser()
        {
            try
            {
                Console.WriteLine("---------------------- " + User.GetUserNameClaim());
                var user = await _usuarioServico.GetUsuarioByUserNameAsync(User.GetUserNameClaim());

                if (user == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    _uploadService.DeleteImageUpload(user.Id, user.FotoURL, _destinoFoto);
                    user.FotoURL = await _uploadService.SaveImageUpload(user.Id, file, _destinoFoto);
                }

                return Ok(await _usuarioServico.UpdateUsuario(user));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar upload de fotos. Erro: {e.Message}");
            }
        }

        [HttpPost("upload-candidato-photo/{candidatoId}")]
        public async Task<IActionResult> UploadFotoCAndidato(int candidatoId)
        {
            try
            {
                Console.WriteLine("---------------------- " + User.GetUserNameClaim());
                var candidato = await _candidatoServico.GetCandidatoByIdAsync(candidatoId);

                if (candidato == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    _uploadService.DeleteImageUpload(candidato.Id, candidato.FotoURL, _destinoFotoCandidatos);
                    candidato.FotoURL = await _uploadService.SaveImageUpload(candidato.Id, file, _destinoFoto);
                }

                   return Ok(await _candidatoServico.UpdateCandidato(candidato.Id, candidato));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar upload de fotos. Erro: {e.Message}");
            }
        }
    }
}

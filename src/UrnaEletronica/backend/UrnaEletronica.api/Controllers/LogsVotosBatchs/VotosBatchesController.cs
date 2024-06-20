using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrnaEletronica.Servico.Servicos.Contratos.Log;

namespace UrnaEletronica.api.Controllers.LogsVotosBatchs
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VotosBatchesController : ControllerBase
    {
        private readonly IProcessarVotosBatchServico _processarVotosBatchServico;

        public VotosBatchesController(IProcessarVotosBatchServico processarVotosBatchServico)
        {
            _processarVotosBatchServico = processarVotosBatchServico;
        }
        [HttpPost("processar-arquivos")]
        public async Task<IActionResult> ProcessarArquivos()
        {
            try
            {
                await _processarVotosBatchServico.ProcessarArquivos();
                return Ok("Arquivos processados com suacesso!");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar arquivos: {ex.Message}");
            }
        }
    }
}

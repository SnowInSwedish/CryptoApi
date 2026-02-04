using Microsoft.AspNetCore.Mvc;
using CryptoApi.Models;
using CryptoApi.Services;

namespace CryptoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly ILogger<CryptoController> _logger;

        public CryptoController(ICryptoService cryptoService, ILogger<CryptoController> logger)
        {
            _cryptoService = cryptoService;
            _logger = logger;
        }

        [HttpPost("encrypt")]
        public ActionResult<EncryptResponse> Encrypt([FromBody] EncryptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Text cannot be empty");

            _logger.LogInformation("Encrypting text with shift {Shift}", request.Shift);

            var encryptedText = _cryptoService.Encrypt(request.Text, request.Shift);

            return Ok(new EncryptResponse
            {
                OriginalText = request.Text,
                EncryptedText = encryptedText,
                Shift = request.Shift
            });
        }

        [HttpPost("decrypt")]
        public ActionResult<DecryptResponse> Decrypt([FromBody] DecryptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Text cannot be empty");

            _logger.LogInformation("Decrypting text with shift {Shift}", request.Shift);

            var decryptedText = _cryptoService.Decrypt(request.Text, request.Shift);

            return Ok(new DecryptResponse
            {
                EncryptedText = request.Text,
                DecryptedText = decryptedText,
                Shift = request.Shift
            });
        }

        [HttpGet("health")]
        public ActionResult<object> Health()
        {
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }
    }
}

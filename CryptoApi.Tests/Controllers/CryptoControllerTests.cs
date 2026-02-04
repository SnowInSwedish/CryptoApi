using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoApi.Controllers;
using CryptoApi.Models;
using CryptoApi.Services;

namespace CryptoApi.Tests.Controllers
{
    public class CryptoControllerTests
    {
        private readonly Mock<ICryptoService> _mockService;
        private readonly Mock<ILogger<CryptoController>> _mockLogger;
        private readonly CryptoController _controller;

        public CryptoControllerTests()
        {
            _mockService = new Mock<ICryptoService>();
            _mockLogger = new Mock<ILogger<CryptoController>>();
            _controller = new CryptoController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Encrypt_WithValidRequest_ReturnsOkResult()
        {
            var request = new EncryptRequest { Text = "Hello", Shift = 3 };
            _mockService.Setup(s => s.Encrypt(request.Text, request.Shift))
                .Returns("Khoor");

            var result = _controller.Encrypt(request);

            var okResult = Assert.IsType<ActionResult<EncryptResponse>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
            var response = Assert.IsType<EncryptResponse>(okObjectResult.Value);

            Assert.Equal("Hello", response.OriginalText);
            Assert.Equal("Khoor", response.EncryptedText);
        }

        [Fact]
        public void Health_ReturnsOkWithStatus()
        {
            var result = _controller.Health();
            var okResult = Assert.IsType<ActionResult<object>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.NotNull(okObjectResult.Value);
        }
    }
}

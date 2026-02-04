using Xunit;
using CryptoApi.Services;

namespace CryptoApi.Tests.Services
{
    public class CaesarCipherServiceTests
    {
        private readonly CaesarCipherService _service;

        public CaesarCipherServiceTests()
        {
            _service = new CaesarCipherService();
        }

        [Fact]
        public void Encrypt_WithSimpleText_ReturnsEncryptedText()
        {
            var result = _service.Encrypt("ABC", 3);
            Assert.Equal("DEF", result);
        }

        [Fact]
        public void Encrypt_WithMixedCase_PreservesCasing()
        {
            var result = _service.Encrypt("Hello World", 3);
            Assert.Equal("Khoor Zruog", result);
        }

        [Fact]
        public void Decrypt_WithEncryptedText_ReturnsOriginalText()
        {
            var originalText = "Hello World";
            var encrypted = _service.Encrypt(originalText, 3);
            var result = _service.Decrypt(encrypted, 3);
            Assert.Equal(originalText, result);
        }

        [Theory]
        [InlineData("ABC", 1, "BCD")]
        [InlineData("XYZ", 3, "ABC")]
        public void Encrypt_WithVariousInputs_ReturnsExpectedResults(
            string input, int shift, string expected)
        {
            var result = _service.Encrypt(input, shift);
            Assert.Equal(expected, result);
        }
    }
}

namespace CryptoApi.Models
{
    public class EncryptRequest
    {
        public string Text { get; set; } = string.Empty;
        public int Shift { get; set; } = 3;
    }

    public class EncryptResponse
    {
        public string OriginalText { get; set; } = string.Empty;
        public string EncryptedText { get; set; } = string.Empty;
        public int Shift { get; set; }
    }

    public class DecryptRequest
    {
        public string Text { get; set; } = string.Empty;
        public int Shift { get; set; } = 3;
    }

    public class DecryptResponse
    {
        public string EncryptedText { get; set; } = string.Empty;
        public string DecryptedText { get; set; } = string.Empty;
        public int Shift { get; set; }
    }
}
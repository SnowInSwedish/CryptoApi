namespace CryptoApi.Services
{
    public class CaesarCipherService : ICryptoService
    {
        public string Encrypt(string text, int shift)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            shift = shift % 26;
            var result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    result[i] = (char)((c - baseChar + shift + 26) % 26 + baseChar);
                }
                else
                {
                    result[i] = c;
                }
            }

            return new string(result);
        }

        public string Decrypt(string text, int shift)
        {
            return Encrypt(text, -shift);
        }
    }
}
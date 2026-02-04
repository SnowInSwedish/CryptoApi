namespace CryptoApi.Services
{
    public interface ICryptoService
    {
        string Encrypt(string text, int shift);
        string Decrypt(string text, int shift);
    }
}
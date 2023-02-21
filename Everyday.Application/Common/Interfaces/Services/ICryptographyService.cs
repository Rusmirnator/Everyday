namespace Everyday.Application.Common.Interfaces.Services
{
    public interface ICryptographyService
    {
        public string AESKey { get; }

        public string Encrypt(string rawText);
        public string Decrypt(string encodedText);
    }
}

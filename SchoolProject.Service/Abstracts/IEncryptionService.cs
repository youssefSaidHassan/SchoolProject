namespace SchoolProject.Service.Abstracts
{
    public interface IEncryptionService
    {
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);
    }
}

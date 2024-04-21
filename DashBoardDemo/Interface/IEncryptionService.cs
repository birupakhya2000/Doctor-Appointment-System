namespace DashBoardDemo.Interface
{
    public interface IEncryptionService
    {
        string Encrypt(int parameter);
        int Decrypt(string encryptedValue);
    }
}

using DashBoardDemo.Interface;
using System.Security.Cryptography;
using System.Text;

namespace DashBoardDemo.Services
{
    public class EncryptionService : IEncryptionService
    {

        private readonly byte[] encryptionKey = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10 };
        public string Encrypt(int parameter)
        {
            string parameterString = parameter.ToString();

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;

                byte[] encryptedBytes;

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create a memory stream to hold the encrypted data
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Create a crypto stream that transforms the data as it is written to and read from
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        // Convert the parameter string to a byte array
                        byte[] parameterBytes = Encoding.UTF8.GetBytes(parameterString);

                        // Write the parameter bytes to the crypto stream
                        csEncrypt.Write(parameterBytes, 0, parameterBytes.Length);
                    }

                    // Get the encrypted bytes from the memory stream
                    encryptedBytes = msEncrypt.ToArray();
                }

                // Combine the IV and encrypted bytes into a single byte array
                byte[] combinedBytes = new byte[aes.IV.Length + encryptedBytes.Length];
                Array.Copy(aes.IV, 0, combinedBytes, 0, aes.IV.Length);
                Array.Copy(encryptedBytes, 0, combinedBytes, aes.IV.Length, encryptedBytes.Length);

                // Convert the combined bytes to a base64-encoded string
                string encryptedValue = Convert.ToBase64String(combinedBytes);

                return encryptedValue;
            }
        }
        public int Decrypt(string encryptedValue)
        {
            try
            {
                byte[] combinedBytes = Convert.FromBase64String(encryptedValue);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = encryptionKey;

                    // Extract the IV from the combined bytes
                    byte[] iv = new byte[aes.IV.Length];
                    Array.Copy(combinedBytes, 0, iv, 0, aes.IV.Length);

                    // Extract the encrypted bytes from the combined bytes
                    byte[] encryptedBytes = new byte[combinedBytes.Length - aes.IV.Length];
                    Array.Copy(combinedBytes, aes.IV.Length, encryptedBytes, 0, encryptedBytes.Length);

                    int decryptedValue;

                    // Create a decryptor to perform the stream transform
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, iv);

                    // Create a memory stream to hold the decrypted data
                    using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                    {
                        // Create a crypto stream that transforms the data as it is written to and read from
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            // Create a byte array to hold the decrypted data
                            byte[] decryptedBytes = new byte[encryptedBytes.Length];

                            // Read the decrypted data from the crypto stream
                            int bytesRead = csDecrypt.Read(decryptedBytes, 0, decryptedBytes.Length);

                            // Convert the decrypted bytes to a string
                            string decryptedString = Encoding.UTF8.GetString(decryptedBytes, 0, bytesRead);

                            // Parse the decrypted string to an integer
                            if (int.TryParse(decryptedString, out decryptedValue))
                            {
                                return decryptedValue;
                            }
                            else
                            {
                                // Invalid decrypted value
                                throw new InvalidOperationException("Invalid decrypted value");
                            }
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                // Invalid Base64 format
                throw new ArgumentException("Invalid encrypted value", ex);
            }
            catch (CryptographicException ex)
            {
                // Error occurred during decryption
                throw new InvalidOperationException("Decryption failed", ex);
            }
        }


    }
}

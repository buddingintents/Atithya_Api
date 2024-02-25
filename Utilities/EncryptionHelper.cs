using System.Security.Cryptography;
using System.Text;

namespace Atithya_Api.Utilities
{
    public class EncryptionHelper(string key, string iv)
    {
        string key = key;
        string iv = iv;
        byte[] keyData = Encoding.UTF8.GetBytes(key);
        byte[] ivData = Encoding.UTF8.GetBytes(iv);

        //byte[] encPassPhrase = Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["passPhrase"]);
        //byte[] enciv = Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["enciv"]);
        internal string EncryptData(string? clearTextData)
        {
            try
            {
                if (string.IsNullOrEmpty(clearTextData))
                    return string.Empty;

                byte[] hexData = Encoding.UTF8.GetBytes(clearTextData);
                using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
                {
                    csp.KeySize = 256;
                    csp.BlockSize = 128;
                    csp.Key = keyData;
                    csp.IV = ivData;
                    csp.Padding = PaddingMode.PKCS7;
                    csp.Mode = CipherMode.CBC;
                    ICryptoTransform encrypter = csp.CreateEncryptor();
                    return Convert.ToBase64String(encrypter.TransformFinalBlock(hexData, 0, hexData.Length));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DecryptData(string? cipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    return string.Empty;

                byte[] hexData = Convert.FromBase64String(cipherText);
                using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
                {
                    csp.KeySize = 256;
                    csp.BlockSize = 128;
                    csp.Key = keyData;
                    csp.IV = ivData;
                    csp.Padding = PaddingMode.PKCS7;
                    csp.Mode = CipherMode.CBC;
                    ICryptoTransform decrypter = csp.CreateDecryptor();
                    return Encoding.UTF8.GetString(decrypter.TransformFinalBlock(hexData, 0, hexData.Length));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

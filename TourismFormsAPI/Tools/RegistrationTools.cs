using System.Security.Cryptography;
using System.Text;

namespace TourismFormsAPI.Tools
{
    public class RegistrationTools
    {
        private static RSAParameters privateKey;
        private static readonly RSACryptoServiceProvider provideRSA = new RSACryptoServiceProvider(2048);
        public static string GetPublicRsaKey()
        {
            privateKey = provideRSA.ExportParameters(true);
            return provideRSA.ToXmlString(false);
        }
        public static (string, string) GetPasswordWithSalt(byte[] encrypt)
        {
            provideRSA.ImportParameters(privateKey);
            byte[] decr = provideRSA.Decrypt(encrypt, false);
            if (decr.Length == 0) throw new NullReferenceException();
            string salt = GetRandomKey(decr.Length * 10);
            return (GetMD5(Encoding.Unicode.GetString(decr) + salt), salt);
        }

        public static string GetPasswordMD5(byte[] encrypt, string salt)
        {
            provideRSA.ImportParameters(privateKey);
            byte[] decr = provideRSA.Decrypt(encrypt, false);
            if (decr.Length == 0) throw new NullReferenceException();
            return GetMD5(Encoding.Unicode.GetString(decr) + salt);
        }
        public static string GetPasswordMD5(string pas, string salt)
        {
            if (pas.Length == 0) throw new NullReferenceException();
            return GetMD5(pas + salt);
        }
        private static string GetMD5(string value)
        {
            MD5CryptoServiceProvider my_pass = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
            data = my_pass.ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public static string GetRandomKey(int length)
        {
            Random rand = new Random();
            string s = "1234567890@#$%^&*?/abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(s[rand.Next(0, s.Length)]);
            }
            return stringBuilder.ToString();
        }

        public static string GetPasswordSha256(string pas)
        {
            if ((!String.IsNullOrEmpty(pas)))
            {
                return Sha256Hash(pas);
            }
            else throw new NullReferenceException();
        }

        private static string Sha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace Project_63135901.Extensions
{
    public class HashMD5
    {
        public static string ToMD5(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder hashSb = new StringBuilder();
            foreach (byte b in hash)
            {
                hashSb.Append(b.ToString("X2"));
            }
            return hashSb.ToString();
        }
    }
}

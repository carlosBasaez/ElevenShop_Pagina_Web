using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace NegocioElevenShop
{
    public class Utilities
    {
        public static string GetSHA256(string frase, int maxlenght=25)
        {
            byte[] bytes_frase = Encoding.ASCII.GetBytes(frase);

            SHA256 sha256 = SHA256.Create();

            byte[] bytes_hash = sha256.ComputeHash(bytes_frase);
            string hash = Encoding.ASCII.GetString(bytes_hash);

            if (hash.Length > maxlenght) hash = hash.Substring(0, maxlenght);

            return hash;
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            if (path.Length > 6)
                path = path.Substring(0, 6);
            return path;
        }
    }
}

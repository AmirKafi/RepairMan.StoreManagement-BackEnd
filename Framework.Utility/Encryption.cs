using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    public static class Encryption
    {
        public static string ToMd5(this string input)
        {
            using var md5 = MD5.Create();
            var byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hash = BitConverter.ToString(byteHash).Replace("-", "");
            return hash;
        }

        public static string Encrypt(this string input)
        {
            return input.ToMd5();
        }
    }
}

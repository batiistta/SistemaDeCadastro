﻿using System.Text;
using System.Security.Cryptography;

namespace SistemaDeCadastro.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string value)
        {
            var hash = SHA256.Create();
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            var hexValue = new StringBuilder();

            foreach (var b in bytes)
            {
                hexValue.Append(b.ToString("x2"));
            }

            return hexValue.ToString();
        }
    }
}

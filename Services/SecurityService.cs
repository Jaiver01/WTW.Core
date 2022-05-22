using System;
using System.Security.Cryptography;

namespace WTW.Core.Services
{
    public class SecurityService
    {
        public string GenerateHash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            String hash = s.ToString();
            return hash;
        }

        public bool Equals(string hash, string input)
        {
            return hash == this.GenerateHash(input);
        }
    }
}

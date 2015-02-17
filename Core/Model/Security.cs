using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Core.Model
{
    public static class Security
    {
        private readonly static MD5 crypt = new System.Security.Cryptography.MD5CryptoServiceProvider();

        public static string GetHash(string Password)
        {
           return Encoding.ASCII.GetString(crypt.ComputeHash(Encoding.ASCII.GetBytes(Password)));
        }
    }
}

using System;
using System.Security.Cryptography;

namespace JwtGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);

            Console.WriteLine(key);
        }
    }
}

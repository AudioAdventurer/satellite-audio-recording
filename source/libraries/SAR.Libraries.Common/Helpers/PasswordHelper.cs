using System;
using System.Security.Cryptography;

namespace SAR.Libraries.Common.Helpers
{
    public static class PasswordHelper
    {
        private const int SALT_BYTES = 20;
        private const int HASH_BYTES = 20;
        private const int ITERATIONS = 10000;

        public static string Hash(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            if (saltBytes.Length != SALT_BYTES)
            {
                throw new ArgumentOutOfRangeException(nameof(salt));
            }

            byte[] hashBytes;

            using (var hasher = new Rfc2898DeriveBytes(
                password: password,
                salt: saltBytes,
                iterations: ITERATIONS))
            {
                hashBytes = hasher.GetBytes(HASH_BYTES);
            }

            return Convert.ToBase64String(hashBytes);
        }

        public static string GenerateSalt()
        {
            var salt = new byte[SALT_BYTES];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}

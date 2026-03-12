using System;
using System.Security.Cryptography;

namespace GYMProject.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int DefaultIterations = 100000;

        public static (string hash, string salt, int iterations) HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(password));
            }

            var saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            var hashBytes = DeriveKey(password, saltBytes, DefaultIterations);
            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes), DefaultIterations);
        }

        public static bool VerifyPassword(string password, string expectedHash, string salt, int iterations)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(expectedHash) || string.IsNullOrWhiteSpace(salt))
            {
                return false;
            }

            if (iterations <= 0)
            {
                return false;
            }

            byte[] saltBytes;
            byte[] expectedHashBytes;

            try
            {
                saltBytes = Convert.FromBase64String(salt);
                expectedHashBytes = Convert.FromBase64String(expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }

            var actualHashBytes = DeriveKey(password, saltBytes, iterations);
            return CryptographicOperations.FixedTimeEquals(actualHashBytes, expectedHashBytes);
        }

        private static byte[] DeriveKey(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KeySize);
            }
        }
    }
}

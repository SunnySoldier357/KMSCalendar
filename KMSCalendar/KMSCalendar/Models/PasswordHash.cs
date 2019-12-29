using System;
using System.Security.Cryptography;

namespace KMSCalendar.Models
{
	public static class PasswordHasher
	{
		// TODO: KENNETH Add documentation

		//* Constants

		// To match the size of the PBKDF2-HMAC-SHA-1 hash
		public const int HASH_BYTE_SIZE = 20;
		public const int ITERATION_INDEX = 0;
		public const int PBKDF2_INDEX = 2;
		public const int PBKDF2_ITERATIONS = 1000;
		public const int SALT_BYTE_SIZE = 24;
		public const int SALT_INDEX = 1;
		public const int TOKEN_BYTE_SIZE = 24;

		//* Public Methods
		public static string GetRandomToken()
		{
			var cryptoProvider = new RNGCryptoServiceProvider();
			byte[] token = new byte[TOKEN_BYTE_SIZE];
			cryptoProvider.GetBytes(token);

			return Convert.ToBase64String(token);
		}

		public static string HashPassword(string password)
		{
			var cryptoProvider = new RNGCryptoServiceProvider();
			byte[] salt = new byte[SALT_BYTE_SIZE];
			cryptoProvider.GetBytes(salt);

			byte[] hash = getPbkdf2Bytes(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
			return string.Format("{0}:{1}:{2}", PBKDF2_ITERATIONS,
				Convert.ToBase64String(salt),
				Convert.ToBase64String(hash));
		}

		public static bool ValidatePassword(string password, string correctHash)
		{
			char[] delimiter = { ':' };
			string[] split = correctHash.Split(delimiter);
			int iterations = int.Parse(split[ITERATION_INDEX]);
			byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
			byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

			byte[] testHash = getPbkdf2Bytes(password, salt, iterations, hash.Length);
			return slowEquals(hash, testHash);
		}

		//* Private Methods
		private static byte[] getPbkdf2Bytes(string password, byte[] salt,
			int iterations, int outputBytes)
		{
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
			{
				IterationCount = iterations
			};
			return pbkdf2.GetBytes(outputBytes);
		}

		private static bool slowEquals(byte[] a, byte[] b)
		{
			uint diff = (uint) a.Length ^ (uint) b.Length;
			for (int i = 0; i < a.Length && i < b.Length; i++)
				diff |= (uint) (a[i] ^ b[i]);
			return diff == 0;
		}
	}
}
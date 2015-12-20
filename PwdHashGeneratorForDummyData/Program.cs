using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PwdHashGeneratorForDummyData {

	internal class Program {

		private static void Main(string[] args) {
			string salt = "H4g3nb3Rg";
			string userName = "stefan.roesch1985@gmail.com";
			string password = "ufo";
			SHA1 sha = new SHA1CryptoServiceProvider();
			var shaPwd = sha.ComputeHash(Encoding.ASCII.GetBytes(salt + password + userName));
			var sb = new StringBuilder();

			foreach (byte b in shaPwd) {
				sb.AppendFormat("{0:x2}", b);
			}

			password = sb.ToString();
			Console.WriteLine(password);
			Console.ReadLine();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;

namespace DALTestClient {

    internal class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Start DALTestClient");
            Console.WriteLine("Create Database ...");
            IDatabase db = DALFactory.GetInstance().CreateDatabase();
            Console.WriteLine("Create Command ...");
            var cmd = db.CreateCommand("Select * from user");
            Console.WriteLine("Execute Command ...");
            var reader = db.ExecuteReader(cmd);
            while (reader.Read()) {
                Console.WriteLine((int)reader["Id"] + " " + (string)reader["email"]);
            }
        }
    }
}
using System;
using System.Text;
using Grpc.Core;
using System.Collections.Generic;

namespace GrpcClient
{
    class Program
    {
        private const string SQLITE_DB_PATH = "D:/DataGrip 2020.2.3/bin/countries.sqlite";
        private const string SERVER_URI = "127.0.0.1:5001";

        public static Queue<Country> Countries;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Countries = new Queue<Country>();
            // Чтение данных из ненормализованной БД
            var dbReader = new DbReader(SQLITE_DB_PATH);
            dbReader.GetAllData();
            if (Countries == null)
                return;

            Console.WriteLine("---------------------------");

            Grpc.SendMessage(SERVER_URI);
            Console.ReadKey();
        }
    }
}

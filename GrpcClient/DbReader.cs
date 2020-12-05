using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace GrpcClient
{
	class DbReader
	{
		private string dbPath;
		private SQLiteConnection dbConnection;
		private SQLiteDataReader reader;

		public DbReader(string path)
		{
			dbPath = path;
		}

		private bool OpenConnection()
		{
			if (!File.Exists(dbPath))
			{
				Console.WriteLine($"Ошибка: база данных \"{dbPath}\" не существует");
				return false;
			}

			try
			{
				var connString = "Data Source=" + dbPath + ";Version=3";
				dbConnection = new SQLiteConnection(connString);
				dbConnection.Open();
				Console.WriteLine("Подключение к ненормализованной базе данных прошло успешно");
				return true;
			}
			catch (SQLiteException ex)
			{
				Console.WriteLine($"Не удалось установить подключение к ненормализованной" +
					$"базе данных. Ошибка: {ex.Message}");
				return false;
			}
		}

		private SQLiteDataReader ReadAllData()
		{
			try
			{
				var dbCommand = "SELECT * FROM Countries";
				var command = new SQLiteCommand(dbCommand, dbConnection);
				reader = command.ExecuteReader();
				command.Dispose();

				if (reader.HasRows)
				{
					Console.WriteLine("Данные успешно считаны");
					return reader;
				}
				else
				{
					Console.WriteLine("База данных пуста");
					Close();
					return null;
				}
			}
			catch (SQLiteException ex)
			{
				Console.WriteLine($"Данные считать не удалось. Ошибка: {ex.Message}");
				Close();
				return null;
			}
		}

		public void GetAllData()
		{
			if (OpenConnection())
			{
				var data = ReadAllData();
				if (data != null)
				{
					while (data.Read())
					{
						Program.Countries.Enqueue(new Country(data));
					}
					Close();
				}
			}
		}

		private void Close()
		{
			if (reader != null)
				reader.Close();
			dbConnection.Close();
			Console.WriteLine("Подключение к ненормализованной базе данных закрыто");
		}
	}
}

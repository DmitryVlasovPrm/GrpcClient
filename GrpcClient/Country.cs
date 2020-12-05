using System;
using System.Data.SQLite;

namespace GrpcClient
{
	public class Country
	{
		public int Id { get; }
		public string CountryName { get; }
		public int CountryPopulation { get; }
		public int CountrySquare { get; }
		public string CapitalName { get; }
		public int CapitalFoundation { get; }
		public string TotalGDP { get; }
		public int HumanGDP { get; }
		public int GDPYear { get; }
		public string LanguageName { get; }
		public int LanguagePrevalencePlace { get; }
		public string RegionName { get; }
		public string RegionCenter { get; }

		public Country(SQLiteDataReader dataRow)
		{
			Id = Convert.ToInt32(dataRow["id"]);
			CountryName = Convert.ToString(dataRow["country_name"]);
			CountryPopulation = dataRow["country_population"] != DBNull.Value ? Convert.ToInt32(dataRow["country_population"]) : 0;
			CountrySquare = dataRow["country_square"] != DBNull.Value ? Convert.ToInt32(dataRow["country_square"]) : 0;
			CapitalName = dataRow["capital_name"] != DBNull.Value ? Convert.ToString(dataRow["capital_name"]) : String.Empty;
			CapitalFoundation = dataRow["capital_foundation"] != DBNull.Value ? Convert.ToInt32(dataRow["capital_foundation"]) : 0;
			TotalGDP = dataRow["total_gdp"] != DBNull.Value ? Convert.ToString(dataRow["total_gdp"]) : String.Empty;
			HumanGDP = dataRow["human_gdp"] != DBNull.Value ? Convert.ToInt32(dataRow["human_gdp"]) : 0;
			GDPYear = dataRow["gdp_year"] != DBNull.Value ? Convert.ToInt32(dataRow["gdp_year"]) : 0;
			LanguageName = dataRow["language_name"] != DBNull.Value ? Convert.ToString(dataRow["language_name"]) : String.Empty;
			LanguagePrevalencePlace = dataRow["language_prevalence_place"] != DBNull.Value ? Convert.ToInt32(dataRow["language_prevalence_place"]) : 0;
			RegionName = dataRow["region_name"] != DBNull.Value ? Convert.ToString(dataRow["region_name"]) : String.Empty;
			RegionCenter = dataRow["region_center"] != DBNull.Value ? Convert.ToString(dataRow["region_center"]) : String.Empty;
		}
	}
}

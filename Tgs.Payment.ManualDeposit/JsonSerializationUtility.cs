using System.Text.Json;

namespace TGS.Payment.ManualDeposit
{
	public static class JsonSerializationUtility
	{
		public static JsonSerializerOptions SerializerOption => new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = false
		};
	}
}

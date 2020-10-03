using System.Collections.Generic;

namespace Eap.Equipment.Disco.Dfg8540
{
	public class LineProcessorPattern2 : LineProcessorBase
	{
		public override bool IsMatch(string lineString)
		{
			return lineString.Contains("= \"");
		}

		public override Dictionary<string, string> Parse(string lineString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			char[] separator = new char[1]
			{
				'='
			};
			string key = lineString.Split(separator)[0].Trim();
			separator = new char[1]
			{
				'='
			};
			string value = lineString.Split(separator)[1].Trim().TrimStart("\"".ToCharArray()).TrimEnd("\"".ToCharArray());
			dictionary.Add(key, value);
			return dictionary;
		}
	}
}

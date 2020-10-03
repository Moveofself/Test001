using System.Collections.Generic;

namespace Eap.Equipment.Disco.Dfg8540
{
	public class LineProcessorPattern1 : LineProcessorBase
	{
		public override bool IsMatch(string lineString)
		{
			return lineString.Contains("= {");
		}

		public override Dictionary<string, string> Parse(string lineString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			char[] separator = new char[1]
			{
				'='
			};
			string text = lineString.Split(separator)[0].Trim();
			separator = new char[1]
			{
				'['
			};
			string str = text.Split(separator)[0];
			separator = new char[1]
			{
				'='
			};
			string text2 = lineString.Split(separator)[1].Trim().TrimStart("{".ToCharArray()).TrimEnd("}".ToCharArray());
			separator = new char[1]
			{
				','
			};
			string[] array = text2.Split(separator);
			for (int i = 0; i < array.Length; i++)
			{
				dictionary.Add(str + "_" + $"{i + 1:00}", array[i].Trim().TrimStart("\"".ToCharArray()).TrimEnd("\"".ToCharArray()));
			}
			return dictionary;
		}
	}
}

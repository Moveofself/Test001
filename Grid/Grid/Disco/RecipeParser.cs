using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Eap.Equipment.Disco.Dfg8540
{
	public class RecipeParser
	{
		public static string LINE_POSTFIX;

		public static LineProcessorBase CURRENT_PROCESSOR;

		static RecipeParser()
		{
			LINE_POSTFIX = "\\$Now";
			LineProcessorBase lineProcessorBase = new LineProcessorPattern1();
			LineProcessorBase lineProcessorBase2 = new LineProcessorPattern2();
			LineProcessorBase nextLineProcessor = new LineProcessorPattern3();
			lineProcessorBase.NextLineProcessor = lineProcessorBase2;
			lineProcessorBase2.NextLineProcessor = nextLineProcessor;
			CURRENT_PROCESSOR = lineProcessorBase;
		}

		public static Dictionary<string, string> ParseFromFile(string fullFileName)
		{
			string empty = string.Empty;
			empty = File.ReadAllText(fullFileName);
			if (string.IsNullOrEmpty(empty))
			{
				throw new Exception("Recipe body format error. Recipe string is empty.");
			}
			return ParseFromString(empty);
		}

		public static Dictionary<string, string> ParseFromString(string recipeString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			recipeString = recipeString.Replace("\r\n", "").Replace("\n", "");
			string[] array = Regex.Split(recipeString, LINE_POSTFIX);
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					foreach (KeyValuePair<string, string> item in CURRENT_PROCESSOR.Process(text))
					{
						dictionary.Add(item.Key, item.Value);
					}
				}
			}
			return dictionary;
		}
	}
}

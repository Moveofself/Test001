using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Eap.Equipment.Towa
{
	public class RecipeParser
	{
		private const string _SetFormat_120T_set = "Format_120T";

		private const string _SetFormat_60T_set = "Format_60T";

		private const string _SetFormat_60T_HS_set = "Format_60T_HS";

		private const string _SetFormat_E3120_set = "Format_E3120";

		private string _settingDirectory = string.Empty;

		private Dictionary<string, PPBodyType> PPBodyDict = new Dictionary<string, PPBodyType>();

		public RecipeParser()
		{
			this._settingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Recipe");
		}

		public string GetRecipeSettingFile(string fileName)
		{
			FileInfo info = new FileInfo(fileName);
			string result;
			if (info.Length == 3072L)
			{
				result = "Format_120T";
			}
			else if (info.Length == 586L)
			{
				result = "Format_60T";
			}
			else if (info.Length == 590L)
			{
				result = "Format_60T_HS";
			}
			else if (info.Length == 15272L)
			{
				result = "Format_E3120";
			}
            else if (info.Length == 14272L)
            {
                result = "Format_PMC1040D";
            }
            else if (info.Length == 19872L)
            {
                result = "Format_YPM1180";
            }
			else
			{
				result = "Format_120T";
			}
			return result;
		}

		public string GetRecipeSettingFile(MemoryStream stream)
		{
			long length = stream.Length;
			string result;
			if (length == 3072L)
			{
				result = "Format_120T";
			}
			else if (length == 586L)
			{
				result = "Format_60T";
			}
			else if (length == 590L)
			{
				result = "Format_60T_HS";
			}
			else if (length == 15272L)
			{
				result = "Format_E3120";
			}
            else if (length == 14272L)
            {
                result = "Format_PMC1040D";
            }
            else if (length == 19872L)
            {
                result = "Format_YPM1180";
            }
			else
			{
				result = "Format_120T";
			}
			return result;
		}

		public string[] GetValues(string[] parameterList, string fileName1)
		{
			FileStream reader = new FileStream(fileName1, FileMode.Open);
			int i = -1;
			string[] value = new string[parameterList.Length];
			for (int j = 0; j < parameterList.Length; j++)
			{
				string parameterName = parameterList[j];
				i++;
                //if (this.PPBodyDict.Keys.Contains(parameterName))
                if (this.PPBodyDict.ContainsKey(parameterName))
                {
					PPBodyType pbd = this.PPBodyDict[parameterName];
					reader.Seek((long)(pbd.BytePosition - 2), SeekOrigin.Begin);
					switch (pbd.VariableType)
					{
					case 0:
					{
						byte[] readdata = new byte[pbd.Count];
						reader.Read(readdata, 0, pbd.Count);
						StringBuilder sb = new StringBuilder();
						byte[] array = readdata;
						for (int k = 0; k < array.Length; k++)
						{
							byte b = array[k];
							sb.Append((char)b);
						}
						value[i] = sb.ToString();
						break;
					}
					case 1:
					{
						byte[] readdata = new byte[pbd.Count * 2];
						reader.Read(readdata, 0, pbd.Count * 2);
						value[i] = string.Concat(BitConverter.ToInt16(readdata, 0));
						break;
					}
					case 2:
					{
						byte[] readdata = new byte[pbd.Count * 2];
						reader.Read(readdata, 0, pbd.Count * 2);
						value[i] = string.Concat(BitConverter.ToInt32(readdata, 0));
						break;
					}
					case 3:
					{
						byte[] readdata = new byte[pbd.Count];
						reader.Read(readdata, 0, pbd.Count);
						value[i] = string.Concat(BitConverter.ToSingle(readdata, 0));
						break;
					}
					case 5:
					{
						byte[] readdata = new byte[8];
						reader.Read(readdata, 0, 8);
						value[i] = this.TDateTimeConvertToCSharpString(readdata);
						break;
					}
					}
				}
				else
				{
					value[i] = "__Nothing__";
				}
			}
			reader.Close();
			return value;
		}

		public string[] GetValues(string[] parameterList, Stream reader1)
		{
			int i = -1;
			string[] value = new string[parameterList.Length];
			for (int j = 0; j < parameterList.Length; j++)
			{
				string parameterName = parameterList[j];
				i++;
				if (this.PPBodyDict.ContainsKey(parameterName))
				{
					PPBodyType pbd = this.PPBodyDict[parameterName];
					reader1.Seek((long)(pbd.BytePosition - 2), SeekOrigin.Begin);
					switch (pbd.VariableType)
					{
					case 0:
					{
						byte[] readdata = new byte[pbd.Count];
						reader1.Read(readdata, 0, pbd.Count);
						StringBuilder sb = new StringBuilder();
						byte[] array = readdata;
						for (int k = 0; k < array.Length; k++)
						{
							byte b = array[k];
							sb.Append((char)b);
						}
						value[i] = sb.ToString();
						break;
					}
					case 1:
					{
						byte[] readdata = new byte[pbd.Count * 2];
						reader1.Read(readdata, 0, pbd.Count * 2);
						value[i] = string.Concat(BitConverter.ToInt16(readdata, 0));
						break;
					}
					case 2:
					{
						byte[] readdata = new byte[pbd.Count * 2];
						reader1.Read(readdata, 0, pbd.Count * 2);
						value[i] = string.Concat(BitConverter.ToInt32(readdata, 0));
						break;
					}
					case 3:
					{
						byte[] readdata = new byte[pbd.Count];
						reader1.Read(readdata, 0, pbd.Count);
						value[i] = string.Concat(BitConverter.ToSingle(readdata, 0));
						break;
					}
					case 5:
					{
						byte[] readdata = new byte[8];
						reader1.Read(readdata, 0, 8);
						value[i] = this.TDateTimeConvertToCSharpString(readdata);
						break;
					}
					}
				}
				else
				{
					value[i] = "__Nothing__";
				}
			}
			reader1.Close();
			return value;
		}

		public string TDateTimeConvertToCSharpString(byte[] longDateTime)
		{
			long longNum = (long)((ulong)longDateTime[7] << 56 | (ulong)((ulong)((long)(longDateTime[6] & 255)) << 48) | (ulong)((ulong)((long)(longDateTime[5] & 255)) << 40) | (ulong)((ulong)((long)(longDateTime[4] & 255)) << 32) | (ulong)((ulong)((long)(longDateTime[3] & 255)) << 24) | (ulong)((ulong)((long)(longDateTime[2] & 255)) << 16) | (ulong)((ulong)((long)(longDateTime[1] & 255)) << 8) | (ulong)((long)(longDateTime[0] & 255)));
			double doubleTime = BitConverter.Int64BitsToDouble(longNum);
			DateTime date = new DateTime((long)((doubleTime + 693593.0) * 864000000000.0));
			return date.ToString();
		}

		public string[] ReadParamFromFile(string[] paramList, string directoryPath, string recipeFileName)
		{
			string[] result = new string[paramList.Length];
			string recipeSetName = this.GetRecipeSettingFile(recipeFileName);
			this.init(this._settingDirectory + "\\" + recipeSetName);
			return this.GetValues(paramList, recipeFileName);
		}

		public Dictionary<string, string> ReadParameterAndValueFromFile(string recipeFullFileName)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			string recipeSetName = this.GetRecipeSettingFile(recipeFullFileName);
			this.init(this._settingDirectory + "\\" + recipeSetName);
			string[] values = new string[this.PPBodyDict.Keys.Count];
			List<string> keys = new List<string>();
			foreach (KeyValuePair<string, PPBodyType> kv in this.PPBodyDict)
			{
				keys.Add(kv.Key);
			}
			values = this.GetValues(keys.ToArray(), recipeFullFileName);
			for (int index = 0; index < this.PPBodyDict.Keys.Count; index++)
			{
				if (!result.ContainsKey(keys[index]))
				{
					result.Add(keys[index], values[index]);
				}
			}
			return result;
		}

		public string[] ReadParamFromFile(string[] paramList, MemoryStream stream)
		{
			string[] result = new string[paramList.Length];
			string recipeSetName = this.GetRecipeSettingFile(stream);
			this.init(this._settingDirectory + "\\" + recipeSetName);
			return this.GetValues(paramList, stream);
		}

		public Dictionary<string, string> ReadParameterAndValueFromFile(MemoryStream stream)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			string recipeSetName = this.GetRecipeSettingFile(stream);
			this.init(this._settingDirectory + "\\" + recipeSetName);
			string[] values = new string[this.PPBodyDict.Keys.Count];
			List<string> keys = new List<string>();
			foreach (KeyValuePair<string, PPBodyType> kv in this.PPBodyDict)
			{
				keys.Add(kv.Key);
			}
			values = this.GetValues(keys.ToArray(), stream);
			for (int index = 0; index < this.PPBodyDict.Keys.Count; index++)
			{
				if (!result.ContainsKey(keys[index]))
				{
					result.Add(keys[index], values[index]);
				}
			}
			return result;
		}

		public void init(string recipeSetName)
		{
			StreamReader initFile = new StreamReader(recipeSetName + ".set");
			StreamReader initFile2 = new StreamReader(recipeSetName + "_Name.txt");
			string line = initFile.ReadLine();
			string line2 = initFile2.ReadLine();
			while (line != null)
			{
				string[] alist = line.Split(new char[]
				{
					',',
					'='
				});
				string[] alist2 = line2.Split(new char[]
				{
					'_'
				});
				if (alist2.Length > 1)
				{
					PPBodyType pbd = new PPBodyType();
					pbd.BytePosition = int.Parse(alist2[0]) * 2;
					pbd.Name = alist2[1].Trim();
					if (alist[1].ToUpper().StartsWith("B"))
					{
						pbd.VariableType = 0;
						pbd.Count = int.Parse(alist[1].Substring(1)) / 2;
					}
					else if (alist[1].ToUpper().StartsWith("L"))
					{
						pbd.VariableType = 2;
						pbd.Count = int.Parse(alist[1].Substring(1)) * 2;
					}
					else if (alist[1].ToUpper().StartsWith("D"))
					{
						pbd.VariableType = 3;
						pbd.Count = int.Parse(alist[1].Substring(1));
					}
					else if (alist[1].ToUpper().StartsWith("T"))
					{
						pbd.VariableType = 5;
						pbd.Count = int.Parse(alist[1].Substring(1)) * 8;
					}
					else if (alist[1].ToUpper().StartsWith("I"))
					{
						pbd.VariableType = 1;
						pbd.Count = int.Parse(alist[1].Substring(1));
					}
					if (this.PPBodyDict.ContainsKey(pbd.Name))                    
                    {
						this.PPBodyDict.Add(pbd.Name + alist[0], pbd);
					}
					else
					{
						this.PPBodyDict.Add(pbd.Name, pbd);
					}
				}
				line = initFile.ReadLine();
				line2 = initFile2.ReadLine();
			}
			initFile.Close();
			initFile2.Close();
		}

    }
}

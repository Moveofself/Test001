using System.Collections.Generic;

namespace Eap.Equipment.Disco.Dfg8540
{
	public interface ILineProcessor
	{
		Dictionary<string, string> Process(string lineString);
	}
}

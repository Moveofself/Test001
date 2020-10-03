using System;
using System.Collections.Generic;

namespace Eap.Equipment.Disco.Dfg8540
{
	public abstract class LineProcessorBase : ILineProcessor
	{
		public ILineProcessor NextLineProcessor;

		public virtual bool IsMatch(string lineString)
		{
			throw new NotImplementedException();
		}

		public virtual Dictionary<string, string> Parse(string lineString)
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, string> Process(string lineString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!IsMatch(lineString))
			{
				return (NextLineProcessor == null) ? dictionary : NextLineProcessor.Process(lineString);
			}
			return Parse(lineString);
		}
	}
}

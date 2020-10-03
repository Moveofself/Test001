using System;

namespace Eap.Equipment.Towa
{
	internal class PPBodyType
	{
		public const int _TYPE_BYTE = 0;

		public const int _TYPE_INTEGER = 1;

		public const int _TYPE_LONG = 2;

		public const int _TYPE_DOUBLE = 3;

		public const int _TYPE_TIME = 5;

		public const int _TYPE_NULL = 6;

		internal int BytePosition
		{
			get;
			set;
		}

		internal int Count
		{
			get;
			set;
		}

		internal int VariableType
		{
			get;
			set;
		}

		internal string Name
		{
			get;
			set;
		}
	}
}

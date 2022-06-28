using System;

namespace KTB.DNet.Lib
{
	public class SysLogServerNotAvailableException : Exception
	{
		public SysLogServerNotAvailableException() : base() {}
		public SysLogServerNotAvailableException(string message): base(message) {}
		public SysLogServerNotAvailableException(string message, Exception inner) 
			: base(message, inner) {}

	}
}

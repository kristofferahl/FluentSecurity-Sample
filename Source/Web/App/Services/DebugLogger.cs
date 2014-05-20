using System;
using System.Diagnostics;
using Web.App.ServiceLocation;

namespace Web.App.Services
{
	public class DebugLogger : ILogger
	{
		public void Log(string message, params object[] values)
		{
			if (values != null && values.Length > 0)
				message = String.Format(message, values);

			Debug.WriteLine(message);
		}
	}
}
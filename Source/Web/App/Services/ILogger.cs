namespace Web.App.Services
{
	public interface ILogger
	{
		void Log(string message, params object[] values);
	}
}
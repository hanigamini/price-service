using System;

namespace Domain.Extension
{
	public static class ExceptionExtension
	{
		public static string FullTextError(this Exception? ex)
		{
			var error = "";
			if (ex != null)
			{
				while (ex != null)
				{
					error += ex.GetType().FullName;
					error += "Message : " + ex.Message + "\n";
					if (ex.InnerException != null)
						error += "InnerException : " + ex.InnerException + "\n";
					error += "StackTrace : " + ex.StackTrace + "\n";

					ex = ex.InnerException!;
				}

			}
			return error.Replace("'", "").Replace("`", "").Replace("{", "").Replace("}", "");
		}
	}
}

using System;

namespace HashCode
{
	/// <summary>
	/// A class responsible for logging things into console
	/// </summary>
    internal class ConsoleLogger
    {
		/// <summary>
		/// Print the given text
		/// </summary>
		/// <param name="results">Results.</param>
		public void Write(string text)
		{
			Console.Write(text);
		}
    }
}
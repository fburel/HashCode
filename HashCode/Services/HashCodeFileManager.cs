using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace HashCode
{

	public class HashCodeFile
	{
		public string Name { get ; private set ;}

		private readonly string _ressourceName;

		public HashCodeFile(string name, string prefix)
		{
			Name = name;
			_ressourceName = prefix + name;
		}

		public StreamReader OpenStream(){

			var assembly = Assembly.GetExecutingAssembly();

			Stream stream = assembly.GetManifestResourceStream(_ressourceName);

			return  new StreamReader(stream);
		}
	}

	/// <summary>
	/// A class responsible for handling file on the app.
	/// </summary>
    internal class HashCodeFileManager
    {
		/// <summary>
		/// Gets the list of loaded files.
		/// </summary>
		/// <value>The files.</value>
		public List<HashCodeFile> Files { get; private set; }

		public HashCodeFileManager()
		{
			Files = new List<HashCodeFile>();
		}

		/// <summary>
		/// Load the file with the specified fileName for the "materials" folder.
		/// </summary>
		/// <param name="fileName">File name.</param>
		public void Load(string fileName)
		{
			var file = new HashCodeFile(fileName, "HashCode.Materials.");

			this.Files.Add(file);
		
		}

		/// <summary>
		/// Writes to file.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="text">Text.</param>
		public void WriteToFile(string fileName, string text)
		{
			throw new System.NotImplementedException();
		}
    }
}
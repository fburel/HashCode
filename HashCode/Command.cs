using System;
using System.Collections.Generic;

namespace HashCode
{
	public class Command
	{
	}


	public class LoadCommand : Command {

		public LoadCommand()
		{
			
		}

		public int NumberOfItem { get ; set ; }

		public ProductType Type { get ; set ;}

		public Cell LoadCell { get ; set ;}

	}

}


using System;

namespace HashCode
{
	public class Map {
		public int Row {
			get;
			set;
		}
		public int Column {
			get;
			set;
		}

		public Map() {
			Row = 0;
			Column = 0;
		}

		public Map (string r, string c) : this() {
			Row = int.Parse(r);
			Column = int.Parse(c);
		}
	}
}


using System;

namespace HashCode
{
	public class Cell
	{
		public int Row { get ; private set ; }

		public int Column { get ; private set ;}

		public Cell(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public double Distance (Cell c) {
			return Math.Sqrt (Math.Pow (this.Column - c.Column, 2) + Math.Pow (this.Row - c.Row, 2));
		}

		public override bool Equals (object obj)
		{
			return Row == ((Cell)obj).Row && Column == ((Cell)obj).Column;
		}
	}
}


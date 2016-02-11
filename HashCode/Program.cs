using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashCode
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Problem pb = new Problem ();
			string line = "";
			int counter = 0;
			StreamReader file = new StreamReader("/Users/JulienCroain/Downloads/busy_day.in");

			while((line = file.ReadLine()) != null)
			{
				Console.WriteLine (line);
				if (counter == 0) {
					pb = new Problem (line);
				}
				counter++;
			}

			file.Close();

			// Suspend the screen.
			Console.ReadLine();
			Console.WriteLine ("Hello World!");

		}
	}

	public class Problem {
		public List<WareHouse> WareHouses;
		public Map Map;
		public int Turn;
		public List<ProductType> Products;
		public List<Order> Orders;
		public List<Drone> Drones;

		public Problem() {
			Turn = 0;
			WareHouses = new List<WareHouse> ();
			Map = new Map ();
			Products = new List<ProductType> ();
			Orders = new List<Order> ();
			Drones = new List<Drone> ();
		}

		public Problem(string datas) : this() {
			List<String> data = datas.Split (' ').ToList();
			Map = new Map (data [0], data [1]);

			Turn = int.Parse(data[3]);
			int maxPayLoad = int.Parse(data [4]);

			for (int i = 0; i < int.Parse(data[2]); i++) {
				Drones.Add (new Drone (maxPayLoad));
			}
		}
	}

	public class WareHouse {
		public Dictionary<ProductType,int> Products {
			get;
			set;
		}

		public Cell Position { get ; set ; }
		public int Idx {
			get;
			set;
		}

		public WareHouse() {
			Products = new Dictionary<ProductType, int> ();
			Position = new Cell ();
			Idx = 0;
		}

		public WareHouse (int idx, List<string> datas, Problem pb) {
			Position = new Cell (datas [0]);
			Idx = idx;

			List<string> data = datas[1].Split(' ').ToList();
			for (int i = 0; i < data.Count; i++) {
				Products.Add (pb.Products [i], int.Parse(data [i]));
			}
		}
	}

	public class ProductType {
		public int Idx {
			get;
			set;
		}
		public int Weight {
			get;
			set;
		}
	}

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

	public class Cell{
		int Row { get ; set ; }
		int Column { get ; set ;}

		public Cell() {
			Row = 0;
			Column = 0;
		}

		public Cell(string data) {
			string[] d = data.Split (' ');
			Row = int.Parse(d [0]);
			Column = int.Parse(d [1]);
		}

		public double Distance (Cell c) {
			return Math.Sqrt (Math.Pow (this.Column - c.Column, 2) + Math.Pow (this.Row - c.Row, 2));
		}

		public override bool Equals (object obj)
		{
			return Row == ((Cell)obj).Row && Column == ((Cell)obj).Column;
		}
	}

	public class Order{
		public Dictionary<ProductType,int> Products {
			get;
			set;
		}
		public Cell Destination { get ; set ; }

	}

	public class Drone {
		public int MaxWeight { get; set; }
		public Cell Position { get; set; }
		public List<ProductType> Products;

		public Drone(int max) {
			Position = new Cell ();
			MaxWeight = max;
		}

		public void LoadProduct(ProductType pt, int quantity) {
		}

		public void MoveTo(Cell c) {
		}


	}
}

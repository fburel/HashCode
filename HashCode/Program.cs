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
			List<string> files = new List<string> ();
			files.Add ("/Users/JulienCroain/Downloads/busy_day.in");
			files.Add ("/Users/JulienCroain/Downloads/mother_of_all_warehouses.in");
			files.Add ("/Users/JulienCroain/Downloads/redundancy.in");

			foreach (var file in files) {
				Problem.ResolveProblem (file);
			}
		}
	}

	public class Problem {
		public List<WareHouse> WareHouses;
		public Map Map;
		public int Turn;
		public List<ProductType> Products;
		public List<Order> Orders;
		public List<Drone> Drones;

		public static void ResolveProblem(string filepath) {

			Problem pb = new Problem ();
			string line = "";
			int counter = 0;
			int nbWarehouse = 0;
			int currentWarehouse = 0;
			int nbOrder = 0;
			int currentOrder = 0;
			StreamReader file = new StreamReader(filepath);

			while((line = file.ReadLine()) != null)
			{
				//Console.WriteLine (line);
				if (counter == 0) {
					pb = new Problem (line);
				} else if (counter == 1) {
				} else if (counter == 2) {
					List<string> datas = line.Split (' ').ToList ();
					for (int i = 0; i < datas.Count; i++) {
						pb.Products.Add (new ProductType (i, int.Parse (datas [i])));
					}
				} else if (counter == 3) {
					nbWarehouse = int.Parse (line);
				} else if (counter <= 3 + (nbWarehouse * 2)) {
					List<string> datas = new List<string> ();
					datas.Add (line);
					datas.Add (file.ReadLine());
					counter++;
					pb.WareHouses.Add (new WareHouse (currentWarehouse, datas, pb));
				} else if (counter == 3 + (nbWarehouse * 2) + 1) {
					nbOrder = int.Parse (line);
				} else {
					List<string> datas = new List<string> ();
					datas.Add (line);
					datas.Add (file.ReadLine());
					counter++;
					datas.Add (file.ReadLine());
					counter++;
					pb.Orders.Add(new Order(datas, pb));
				}
				counter++;
			}

			file.Close();

			Console.WriteLine (pb.ToString ());

			// Suspend the screen.
			Console.ReadLine();
			Console.WriteLine ("Hello World!");

			CommandDispatch cd = new CommandDispatch (pb);
			cd.Resolve ();
			
		}

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

		public override string ToString ()
		{
			return string.Format ("[Problem] [WareHouses:{0}] [Map:{1}:{2}] [Turn:{3}] [Products:{4}] [Orders:{5}] [Drones:{6}]",
				WareHouses.Count, Map.Row, Map.Column, Turn, Products.Count, Orders.Count, Drones.Count);
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

		public WareHouse (int idx, List<string> datas, Problem pb) : this() {
			Position = new Cell (datas [0]);
			Idx = idx;

			List<string> data = datas[1].Split(' ').ToList();
			for (int i = 0; i < data.Count; i++) {
				Products.Add (pb.Products [i], int.Parse(data [i]));
			}
		}

		public int UnloadProduct(ProductType p, int qty) {
			if (!Products.ContainsKey (p))
				return 0;

			int maxQty = Math.Min (Products [p], qty);
			Products [p] -= maxQty;
			if (Products [p] == 0)
				Products.Remove (p);

			return maxQty;
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

		public ProductType(int idx, int w) {
			Idx = idx;
			Weight = w;
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
		public Dictionary<ProductType,int> Products { get; set; }
		public Cell Destination { get ; set ; }

		public Order() {
			Products = new Dictionary<ProductType, int> ();
		}

		public Order(List<string> datas, Problem pb) : this() {
			Destination = new Cell (datas [0]);
			string[] products = datas [2].Split (' ');
			for (int i = 0; i < products.Length; i++) {
				int qty = int.Parse (products [i]);
				if (qty > 0) {
					Products.Add (pb.Products [i], qty);
				}
			}
		}
	}

	public class Drone {
		public int MaxWeight { get; set; }
		public Cell Position { get; set; }
		public Dictionary<ProductType,int> Products { get; set; }
		public int TotalTurn;

		public Drone() {
			Products = new Dictionary<ProductType, int> ();
			Position = new Cell ();
		}

		public Drone(int max) : this() {
			Position = new Cell ();
			MaxWeight = max;
		}

		public int Weight {
			get {
				int w = 0;
				foreach (var item in Products) {
					w += item.Key.Weight * item.Value;
				}
				return w;
			}
		}

		public int CanLoadProduct(ProductType pt, int quantity) {
			return Math.Min(MaxWeight, Weight + (pt.Weight * quantity));
		}

		public int LoadProduct(ProductType pt, int quantity) {
			int q = CanLoadProduct(pt, quantity);
			
			if (Products.ContainsKey (pt)) {
				Products [pt] += q;
			} else {
				Products.Add (pt, q);
			}
			return q;
		}

		public bool UnloadProduct(ProductType pt, int quantity) {
			if (Products.ContainsKey (pt) && Products[pt] >= quantity) {
				Products [pt] -= quantity;
				return true;
			} else {
				throw new Exception ("NO PRODUCT IN DRONE");
			}
			return false;
		}

		public void MoveTo(Cell c) {
			TotalTurn += (int)Math.Ceiling (Position.Distance (c));
			Position = c;
		}
	}
}

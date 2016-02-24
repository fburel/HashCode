using System;
using System.Collections.Generic;

namespace HashCode
{
	public class Drone 
	{
		public int MaxWeight { get; private set; }
		public Cell Position { get; private set; }
		public Dictionary<ProductType,int> Products { get; private set; }
		public int TotalTurn { get; private set; }


		public Drone(int max) {
			Position = new Cell (0, 0);
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
		}

		public void MoveTo(Cell c) {
			TotalTurn += (int)Math.Ceiling (Position.Distance (c));
			Position = c;
		}
	}
}


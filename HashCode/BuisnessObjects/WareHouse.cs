using System;
using System.Collections.Generic;
using System.Linq;

namespace HashCode
{
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

		public WareHouse (Dictionary<ProductType, int> productInventory, Cell position, int index)
		{
			Products = productInventory;
			Position = position;
			Idx = index;
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

}


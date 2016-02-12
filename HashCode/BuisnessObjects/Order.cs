using System;
using System.Collections.Generic;

namespace HashCode
{
	public class Order{


		public Dictionary<ProductType,int> Products { get; private set; }
		public Cell Destination { get ; private set ; }

		public Order( Dictionary<ProductType,int> products, Cell destination) {
			Products = products;
			Destination = destination;
		}
	}
}


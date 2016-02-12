using System;

namespace HashCode
{
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

}


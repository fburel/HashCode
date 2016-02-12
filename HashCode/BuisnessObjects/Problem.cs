using System;
using System.Collections.Generic;
using System.Linq;

namespace HashCode
{
	public class Problem {

		/// <summary>
		/// the set of warehouse
		/// </summary>
		/// <value>The ware houses.</value>
		public List<WareHouse> WareHouses { get ; private set; }

		public Map Map { get ; private set; }
		public int Turn { get ; private set; }
		public List<ProductType> Products { get ; private set; }
		public List<Order> Orders { get ; private set; }
		public List<Drone> Drones { get ; private set; }

		public Problem(
			List<WareHouse> wareHouses,
			List<Order> orders,
			Map map,
			int numberOfTurns,
			List<ProductType> products, 
			List<Drone> drones
		)
		{
			WareHouses = wareHouses;
			Map = map;
			Orders = orders;
			Turn = numberOfTurns;
			Products = products;
			Drones = drones;
		}


		public override string ToString ()
		{
			return string.Format ("[Problem] [WareHouses:{0}] [Map:{1}:{2}] [Turn:{3}] [Products:{4}] [Orders:{5}] [Drones:{6}]",
				WareHouses.Count, Map.Row, Map.Column, Turn, Products.Count, Orders.Count, Drones.Count);
		}
			
		public string Name {
			get;
			set;
		}

		public void Run()
		{
			// Dispatch the orders to the nearby warehouses.
			CommandDispatch cd = new CommandDispatch (this.Orders, this.WareHouses);
			var dispatchResult = cd.DispatchOrders ();


			// TODO: Once the dispatch is done, it's time to put the drone to action...


			this.Results = dispatchResult.ToString();

		}

		public string Results {
			get;
			private set;
		}
	}
}


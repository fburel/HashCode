using System;
using System.Collections.Generic;

namespace HashCode
{
	public class CommandDispatch
	{
		Problem _problem;

		public CommandDispatch(Problem pb)
		{
			_problem = pb;
		}

		public void DispatchOrders()
		{
			foreach (var order in _problem.Orders)
			{

				var warehouse = RetrieveBestWareHouse(_problem.WareHouses, order);

				Affect(order, warehouse);

			}
		}


		WareHouse RetrieveBestWareHouse(List<WareHouse> warehouses, Order order)
		{
			return null;
		}

		int CalculateWeight(WareHouse h, Order order)
		{
			// int
			int distance = (int) Math.Ceiling(h.Position.Distance(order.Destination));


			int nbProdManque = 0;
			int nbTotalProduit = 0;

			foreach (var orderItem in order.Products)
			{
				var numberOfAvailableItem = h.Products [orderItem.Key];

				var remainingItems = numberOfAvailableItem - orderItem.Value;

				nbTotalProduit += orderItem.Value;

				if(remainingItems < 0)
				{
					nbProdManque += -1 * remainingItems;
				}

			}
				

			return distance + nbProdManque;
		}
			
		int Affect(Order o, WareHouse h)
		{
			return 0;
		}

	}
}


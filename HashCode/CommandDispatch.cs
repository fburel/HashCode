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

		List<Order> quickdeliveries =new List<Order>();

		List<Order> bigDeliery = new List<Order>();


		List<Command> Result;



		void splitDelivery()
		{
			
			foreach (var order in _problem.Orders)
			{
				if(order.Products.Count == 1)
				{
					quickdeliveries.Add(order);
				}
				else if(order.Products.Count > 1)
				{
					bigDeliery.Add(order);
				}
			}
		}


		public void Resolve()
		{
			

		}


		int CalculateWeight(WareHouse h, Order order)
		{
			// int
			int distance = (int) Math.Ceiling(h.Position.Distance(order.Destination));


			int nbProdManque = 0;

			foreach (var orderItem in order.Products)
			{
				var numberOfAvailableItem = h.Products [orderItem.Key];

				var remainingItems = numberOfAvailableItem - orderItem.Value;

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


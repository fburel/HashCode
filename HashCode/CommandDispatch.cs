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


		void Resolve()
		{

			// 1st step :
			splitDelivery();


			// 2nd step



			foreach (var drone in _problem.Drones)
			{

			}

		}





	}
}


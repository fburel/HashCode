using System;
using System.Collections.Generic;
using System.Linq;

namespace HashCode
{
	/// <summary>
	/// A class that helps dispatching a set of orders through different warehouses
	/// </summary>
	public class CommandDispatch
	{

		Dictionary<Cell, Dictionary<ProductType, int>> Deliveries = new Dictionary<Cell, Dictionary<ProductType, int>>();

		Dictionary<WareHouse, Dictionary<ProductType, int>> Inventories = new Dictionary<WareHouse, Dictionary<ProductType, int>>();

		Dictionary<WareHouse, List<Order>> DispatchResults = new Dictionary<WareHouse, List<Order>>();


		public CommandDispatch(IEnumerable<Order> orders, IEnumerable<WareHouse> warehouses)
		{
			// Reorganize the order by cell in deliveries
			foreach (var order in orders)
			{
				AppendOrder(order);
			}

			foreach (var warehouse in warehouses)
			{
				Dictionary<ProductType, int> inventory = new Dictionary<ProductType, int>();

				foreach (var item in warehouse.Products)
				{
					inventory.Add(item.Key, item.Value);
				}

				Inventories.Add(warehouse, inventory);
			}

		}

		void AppendOrder(Order order)
		{
			if (!Deliveries.ContainsKey(order.Destination))
			{
				Deliveries.Add(order.Destination, new Dictionary<ProductType, int>());
			}

			var destinationDeliveries = Deliveries [order.Destination];

			foreach (var orderItem in order.Products)
			{
				if(!destinationDeliveries.ContainsKey(orderItem.Key))
				{
					destinationDeliveries.Add(orderItem.Key, orderItem.Value);
				}
				else
				{
					var value = destinationDeliveries [orderItem.Key] + orderItem.Value;
					destinationDeliveries [orderItem.Key] = value;
				}
			}
		}
			
		/// <summary>
		/// Distribute the orders to the proper warehouses.
		/// </summary>
		public Dictionary<WareHouse, List<Order>> DispatchOrders()
		{
			DispatchResults.Clear();

			foreach (var delivery in Deliveries)
			{
				// Reorder the warehouse List 
				List<WareHouse> warehouses = OrderWareHousesByDistance(delivery.Key);

				var destination = delivery.Key;


				foreach (var item in delivery.Value)
				{

					var productType = item.Key;
					int quantityToDeliver = item.Value;
					int wareHouseIdx = 0;

					while(quantityToDeliver > 0)
					{
						var observedWareHouse = warehouses [wareHouseIdx];

						var remainingInWH = RemainingProducts(observedWareHouse, productType);

						if(remainingInWH == 0)
						{
							// Do nothing
						}
						else if(remainingInWH > quantityToDeliver)
						{
							// full order
							PlaceOrder(observedWareHouse, destination, productType, quantityToDeliver);
							quantityToDeliver = 0;
						}
						else
						{
							// partial order
							PlaceOrder(observedWareHouse, destination, productType, remainingInWH);
							quantityToDeliver -= remainingInWH;
						}
						wareHouseIdx++;
					}
				}
					
			}

			return DispatchResults;
		}

		/// <summary>
		/// Orders the ware houses by distance from the given cell
		/// </summary>
		/// <returns>The ware houses by distance.</returns>
		/// <param name="cell">Cell.</param>
		List<WareHouse> OrderWareHousesByDistance(Cell cell)
		{
			return Inventories.Keys.ToList().OrderBy(wh => cell.Distance(wh.Position)).ToList();
		}

		/// <summary>
		/// Base on the previsionnal inventory, retrieve the stock for a given product type
		/// </summary>
		/// <returns>The products.</returns>
		/// <param name="warehouse">Warehouse.</param>
		/// <param name="type">Type.</param>
		int RemainingProducts(WareHouse warehouse, ProductType type)
		{
			// Get the wh prev inventory
			var inv = Inventories [warehouse];

			// How many product of this type left?
			var remainingProduct = 0;
			inv.TryGetValue(type, out remainingProduct);

			return remainingProduct;

		}

		/// <summary>
		/// Create a monoProduct order and update the inventory
		/// </summary>
		/// <param name="warehouse">Warehouse.</param>
		/// <param name="cell">Cell.</param>
		/// <param name="type">Type.</param>
		/// <param name="qtt">Qtt.</param>
		void PlaceOrder(WareHouse warehouse, Cell cell, ProductType type, int qtt)
		{

			var detail = new Dictionary<ProductType, int>();
			detail.Add(type, qtt);

			Order order = new Order(detail, cell);

			Affect(order, warehouse);

			var inv = Inventories [warehouse];

			var value = inv [type];

			value -= qtt;

			if(value == 0)
			{
				inv.Remove(type);
			}
			else
			{
				inv [type] = value;
			}
		}
			
		/// <summary>
		/// Affect the specified order to the given warehouse
		/// </summary>
		/// <param name="o">O.</param>
		/// <param name="h">The height.</param>
		void Affect(Order o, WareHouse h)
		{
			if(!DispatchResults.ContainsKey(h))
			{
				DispatchResults.Add(h, new List<Order>());
			}

			var orders = DispatchResults [h];

			orders.Add(o);
		}

	}
}


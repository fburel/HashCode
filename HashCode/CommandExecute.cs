using System;
using System.Linq;

namespace HashCode
{
	public class CommandExecute
	{
		public Problem Problem;

		public CommandExecute (Problem pb)
		{
		}

		public Drone FindOptimistDrone(WareHouse wh) {
			int min = int.MaxValue;
			Drone result = null;

			foreach (var d in Problem.Drones) {
				if (min > wh.Position.Distance (d.Position) + d.TotalTurn) {
					result = d;
					min = (int)wh.Position.Distance (d.Position) + d.TotalTurn;
				}
			}

			return result;
		}

		public void DispacthOrder(WareHouse wh, Order o) {
			foreach (var p in o.Products) {
				int q = wh.UnloadProduct(p.Key, p.Value);
				o.Products[p.Key] -= q;

				int load = 0;
				while (q > 0) {
					Drone d = FindOptimistDrone (wh);
					d.MoveTo (wh.Position);
					q -= d.LoadProduct (p.Key, q);
					d.MoveTo (o.Destination);
				}
			}

		}
	}
}
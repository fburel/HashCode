using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace HashCode
{

	/// <summary>
	/// A factory class use to build problem object from multiple sources.
	/// </summary>
    internal class ProblemFactory
    {
		/// <summary>
		/// Creates from file.
		/// </summary>
		/// <returns>The from file.</returns>
		/// <param name="file">File.</param>
		public Problem CreateFromFile(HashCodeFile file)
		{

			// Problem data
			List<WareHouse> wareHouses = new List<WareHouse>();
			List<Order> orders = new List<Order>();
			Map map = new Map();
			int numberOfTurns= 0;
			List<ProductType> productTypes = new List<ProductType>();
			List<Drone> drones = new List<Drone>();




			int lineCount = 0;
			int nbWarehouse = 0;
			int currentWarehouse = 0;
			int nbOrder = 0;
			int currentOrder = 0;





			// Parse the file
			using (StreamReader sr = file.OpenStream())
			{
				string line = "";
				while((line = sr.ReadLine()) != null)
				{


					if (lineCount == 0) {
						
						List<string> data = line.Split (' ').ToList();

						var numberOfRow = data [0];
						var numberOfColumn = data [1];
						var numberOfDrones = data[2];
						int.TryParse(data[3], out numberOfTurns);
						int maxWeightPerDrone = int.Parse(data [4]);

						map = new Map (numberOfRow , numberOfColumn);

						for (int i = 0; i < int.Parse(numberOfDrones); i++) {
							drones.Add (new Drone (maxWeightPerDrone));
						}
					} 

					else if (lineCount == 1) 
					{
						// number of product type.
					} 

					else if (lineCount == 2) 
					{
						List<string> datas = line.Split (' ').ToList ();
						for (int i = 0; i < datas.Count; i++) {
							productTypes.Add (new ProductType (i, int.Parse (datas [i])));
						}
					} 


					else if (lineCount == 3) {
						
						nbWarehouse = int.Parse (line);
					} 

					else if (lineCount <= 3 + (nbWarehouse * 2)) {
						List<string> datas = new List<string> ();
						datas.Add (line);
						datas.Add (sr.ReadLine());
						lineCount++;


						var rowColumnData = datas [0];

						string[] d = rowColumnData.Split (' ');
						int row = int.Parse(d [0]);
						int column = int.Parse(d [1]);

						Cell position = new Cell(row, column);

						var productInventory = new Dictionary<ProductType, int>();

						List<string> quantitities = datas[1].Split(' ').ToList();


						for (int pdtCode = 0 ; pdtCode < quantitities.Count; pdtCode++) {
							
							var qtt = int.Parse(quantitities[pdtCode]);
							var productType = productTypes [pdtCode];

							productInventory.Add(productType, qtt);

							pdtCode++;

						}

						wareHouses.Add (new WareHouse(productInventory, position, currentWarehouse));
					} 


					else if (lineCount == 3 + (nbWarehouse * 2) + 1) {
						nbOrder = int.Parse (line);
					} 


					else {
						List<string> datas = new List<string> ();
						datas.Add (line);
						datas.Add (sr.ReadLine());
						lineCount++;
						datas.Add (sr.ReadLine());
						lineCount++;

						var rowColumnData = datas [0];

						string[] d = rowColumnData.Split (' ');
						int row = int.Parse(d [0]);
						int column = int.Parse(d [1]);

						Cell destination = new Cell(row, column);

						Dictionary<ProductType, int> products = new Dictionary<ProductType, int>();

						string[] productsData = datas [2].Split (' ');

						for (int i = 0; i < productsData.Length; i++) 
						{
							int qty = int.Parse (productsData [i]);
							if (qty > 0) 
							{
								products.Add (productTypes [i], qty);
							}
						}

						orders.Add(new Order(products, destination));


					}
					lineCount++;
				}

				sr.Close();
				sr.Dispose();
			}


			var problem = new Problem(
				wareHouses,
				orders,
				map,
				numberOfTurns,
				productTypes,
				drones
			);

			problem.Name = file.Name;

			return problem;
		}
    }
}
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashCode
{

	class MainClass
	{

        // Services
        readonly static HashCodeFileManager _fileManager        = new HashCodeFileManager();
        readonly static ProblemFactory _problemFactory          = new ProblemFactory();
        readonly static ConsoleLogger _logger                   = new ConsoleLogger();

        public static void Main (string[] args)
		{

//            _fileManager.Load("busy_day.in");
//            _fileManager.Load("mother_of_all_warehouses.in");
//            _fileManager.Load("redundancy.in");
			_fileManager.Load("quiet_day_at_the_office.in");

            foreach (var file in _fileManager.Files)
            {
                var simulation = _problemFactory.CreateFromFile(file);

                simulation.Run();

				_logger.Write(simulation.Results);

				//TODO : Implement the WriteToFile method
//				_fileManager.WriteToFile(simulation.Name + ".out", simulation.Results);

            }
		}
	}

}

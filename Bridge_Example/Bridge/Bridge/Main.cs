using System;

namespace Bridge
{
	class BridgePattern
	{
		// Keeps the impementations separate by abstracting it to a simple operation (in this case, write a console line).
		class Abstraction {
			Bridge bridge;
			public Abstraction (Bridge implementation) {
				bridge = implementation;
			}
			public string Operation () {
				return "Abstraction" + " <<<BRIDGE>>> " + bridge.OperationImp ();
			}
		}

		interface Bridge {
			string OperationImp();
		}

		//Implements the interface
		class ImpA : Bridge {
			public string OperationImp() {
				return "Implementation A";
			}
		}
		class ImpB : Bridge {
			public string OperationImp() {
				return "Implementation B";
			}
		}

		static void Main () {
			Console.WriteLine ("Bridge Pattern");
			Console.WriteLine (new Abstraction (new ImpA ()).Operation ());
			Console.WriteLine (new Abstraction (new ImpB ()).Operation ());
		}
	}
}

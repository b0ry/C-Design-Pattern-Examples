using System;
using System.Collections.Generic;

class Bank {
	class Handler {
		Levels level;
		int id;
		public Handler (int id, Levels level) {
			this.id = id;
			this.level = level;
		}
		public string HandleRequest (int data) {
			if (data < structure [level].limit) {
				return "Request for " + data + "handled by " + level + " " + id;
			} else if (level > First) {
				Levels nextlevel = --level;
				int which = choice.Next (structure [nextlevel].Positions);
				return handlersAtLevel [nextlevel] [which].HandleRequest (data);
			} else {
				Exception chainException = new ChainException();
				chainException.Data.Add ("Limit", data);
				throw chainException;
			}
		}
	}
	public class ChainException : Exception {
		public ChainException(){}
	}
	void AdjustChain(){}

	enum Levels {Manager,Supervisor,Clerk}
	static Random choice = new Random (11);
	static Levels First {
		get { return ((Levels[])Enum.GetValues (typeof(Levels))) [0];}
	}

	static Dictionary <Levels, Structure> structure = 
		new Dictionary <Levels, Structure> {
		{Levels.Manager, new Structure {Limit = 9000, Positions = 1}},
		{Levels.Supervisor, new Structure {Limit = 4000, Positions = 3}},
		{Levels.Clerk, new Structure {Limit = 1000, Positions = 10}}};

	static Dictionary <Levels, List<Handler>> handlersAtLevel = 
		new Dictionary <Levels,  List<Handler>> {
		{Levels.Manager, new List<Handler>()},
		{Levels.Supervisor, new List<Handler>()},
		{Levels.Clerk, new List<Handler>()}};

	class Structure {
		public int Limit { get; set; }
		public int Positions { get; set; }
	}

	void RunTheOrganisation() {
		Console.WriteLine ("Bank employs");
		foreach (Levels level in Enum.GetValues (typeof(Levels))) {
			for (int i = 0; i < structure[level].Positions; i++) {
				handlersAtLevel[level].Add (new Handler(i, level));
			}
			Console.WriteLine (structure[level].Positions + " " + level + "(s) who deal up to a limit of " + structure[level].Limit);
		}
		Console.WriteLine ();

		int[] customers = {50,2000,1500,10000,175,4500,2000};
		foreach (int customer in customers) {
			try {
				int which = choice.Next (structure[Levels.Clerk][which].HandleRequest(customer));
				AdjustChain ();
			} catch (ChainException e) {
				Console.WriteLine ("\nNo facility to handle a request of" + e.Data["Limit"] + "\nTry breaking it down into smaller requests\n");
			}
		}
	}
	static void Main () {
		new Bank ().RunTheOrganisation ();
	}
}
using System;
using System.Collections;
using System.Collections.Generic;

//Abstraction
class Portal {
	Bridge bridge;
	public Portal (Bridge aFarceBook) {
		bridge = aFarceBook;
	}
	public void AddPost (string message) {
		bridge.AddPost (message);
	}
	public void Chat (string friend, string message) {
		bridge.Chat(friend, message);
	}
	public void Jab (string who) {
		bridge.Jab (who);
	}
}

//Bridge
interface Bridge {
	void AddPost (string message) ;
	void Chat (string friend, string message) ;
	void Jab (string who); 
}

class FarceBookSystem{
private class FarceBook {
	static SortedList<string ,FarceBook> community = new SortedList<string,FarceBook> (100);
	string pages;
	string name;
	public string gap = "\n\t\t\t\t";

	public static bool IsUnique(string name) {
		return community.ContainsKey (name);
	}
	internal FarceBook (string n) {
		name = n;
		community [n] = this;
	}
	internal void AddPost (string s) {
		pages += gap + s;
		Console.WriteLine (gap + "========= " + name + "'s FarceBook ========");
		Console.Write (pages);
	}
	internal void Chat(string friend, string message) {
		community [friend].AddPost (message);
	}
	internal void Jab (string who, string name) {
			community [who].AddPost (gap + name + " jabbed you.");
		}
}

public class MyFarceBook {
	FarceBook myFarceBook;
	string password;
	public string name;
	bool loggedIn = false;

	void Register () {
		Console.WriteLine ("\n\n Registration");
		Console.WriteLine ("Type in your user name: ");
		name = Console.ReadLine ();
		Console.WriteLine ("Type in a password: ");
		password = Console.ReadLine ();
		Console.WriteLine ("Registration Complete");
	}
	bool Authenticate () {
		Console.Write ("Welcome " + name + ". Please type in your password: ");
		string supplied = Console.ReadLine ();
		if (supplied == password) {
			loggedIn = true;
			Console.WriteLine("Logged in to FarceBook, well done.");
			if (myFarceBook == null) {
				myFarceBook = new FarceBook(name);
			}
			return true;
		}
		Console.WriteLine ("Incorrect password");
		return false;
	}
	void Check(){
		if (!loggedIn) {
			if (password ==null) Register ();
			if (myFarceBook ==null) Authenticate ();
		}
	}
	public void AddPost (string message) {
		Check();
		if (loggedIn)
			myFarceBook.AddPost (message);
	}
	public void Chat(string friend, string message) {
		Check();
		if (loggedIn)
			myFarceBook.Chat (friend, name + " said: " + message);
	}
		public void Jab(string who) {
			Check();
			if (loggedIn)
				myFarceBook.Jab(who, name);
		}
	}
	public class MySpam : Bridge {
		FarceBook mySpam;
		string name;
		static int users;
		public MySpam (string n) {
			name = n;
			users++;
			mySpam = new FarceBook(name + "-" + users);
		}
		public void AddPost (string message) {
			mySpam.AddPost (message);
		}
		public void Chat(string friend, string message) {
			mySpam.Chat (friend, name + " : " + message);
		}
		public void Jab(string who) {
			mySpam.Jab(who, name);
		}
	}
}

static class MySpamExt {
	public static void MegaJab (this Portal me, string who, string what) {
		me.Chat (who, what + " you!");
	}
}

class User : FarceBookSystem {
	static void Main () {
		//MyFarceBook me = new MyFarceBook(); // Proxy Pattern
		Portal me = new Portal(new MySpam ("Bory"));
		me.AddPost ("Donald Trump said this this...");
		me.AddPost ("Today I ate this...");
		me.AddPost ("blah blah blah");
		Portal other = new Portal(new MySpam("Fred"));
		other.Jab ("Bory-1");
		other.MegaJab ("Bory-1", "qwirkled");
		other.Chat ("Bory-1", ":P");
		other.AddPost ("Who wants a day-trip to B&Q?\n");
	}
}
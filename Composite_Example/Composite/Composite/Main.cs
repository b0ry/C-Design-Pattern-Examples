using System;
using System.Collections.Generic;
using System.IO;
using CompositePatternNS;

class CompositeExample {
		static void Main() {
		IComponent <string> album = new Composite <string> ("Album");
		IComponent <string> point = album;
		string[] s;
		string command, parameter;
		StreamReader inStream = new StreamReader ("Composite.dat");
		do {
			string t = inStream.ReadLine ();
			Console.WriteLine(t);
			s = t.Split ();
			command = s[0];
			if (s.Length > 1) parameter = s[1]; else parameter = null;
			switch (command) {
			case "Addset" :
				IComponent <string> c = new Composite <string> (parameter);
				point.Add (c);
				point = c;
				break;
			case "AddPhoto" :
				point.Add (new Component<string> (parameter));
				break;
			case "Remove" :
				point.Remove(parameter);
				break;
			case "Find" :
				album.Find(parameter);
				break;
			case "Display" :
				Console.WriteLine (album.Display (0));
				break;
			case "Quit" :
				break;
			}
		} while (!command.Equals ("Quit"));
	}
}

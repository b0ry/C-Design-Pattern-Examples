using System;
using System.Text;
using System.Collections.Generic;

namespace CompositePatternNS
{
	//The interface - This is where to put everything that is common to both components and composites.
	public interface IComponent <T, U> {
		void 							Add (IComponent <T, U> c); 	//Add Method
		byte						 	FindLength (T[] s);					//Find Method ...defined properly later
		T Word 						{ get; set; }				 			// Fizz or Buzz
		U Index 						{ get; set; }							// Index of word
	}

	//The component
	public class Component <T, U> : IComponent <T, U> {
		public T Word { get; set; }
		public U Index { get; set; }

		public Component (T word, U index) {
			Word = word;
			Index = index;
		}
		public void Add (IComponent <T, U> c) {
			Console.WriteLine ("Cannot add to an item.");
		}
		public byte FindLength(T[] s) {
			if (s.Equals (Word))
				return this;
			else
				return 999;
		}
	}

	//The composite
	public class Composite <T, U> : IComponent <T, U> {
		public T Word { get; set; }
		public U Index { get; set; }

		List <IComponent <T, U>> list;

		public Composite (T word, U index) {
			Word = word;
			Index = index;
			list = new List<IComponent <T, U>>();
		}
		public void Add (IComponent <T, U> c) {
			list.Add (c);									// Already defined for generic types
		}
		public byte FindLength(T[] s) {
			if (Word.Equals (s)) {
				return this;
			}
			IComponent <T, U> found = null;
			foreach (IComponent <T, U> c in list) { 		// Search through list
				found = c.Find (s);
				if (found != null) {								// Until object is found 
					break;
				}
			}
			return found;
		}
	}
}
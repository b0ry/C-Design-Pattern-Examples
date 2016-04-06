using System;

namespace ProxyPattern
{
	class SubjAccessor {
		public interface ISubject {
			string Request ();
		}

		private class Subject {
			public string Request () {
				return "Subject Request";
			}
		}
		public class Proxy : ISubject {
			Subject subject;
			public string Request() {
				if (subject == null) {
					Console.WriteLine ("Subject inactive, creating subject");
					subject = new Subject ();
				} 
				Console.WriteLine ("Subject Active");
				return "Proxy: Call to " + subject.Request ();
			}
		}

		public class ProtectionProxy : ISubject {
			Subject subject;
			string password = "LOLLIPOPS!";

			public string Authenticate (string supplied) {
				if (supplied != password) {
					return "Protection Proxy says no.";
				}
				subject = new Subject();
				return "Protection Proxy says ok.";
			}
			public string Request () {
				if (subject == null) {
					return "Protection Proxy says authenticate first.";
				}
				return "Protection Proxy: Call to " + subject.Request();
			}
		}
	}

	class Client : SubjAccessor
	{
		public static void Main ()
		{
			Console.WriteLine ("Authetication Proxy");
			ISubject subject = new Proxy ();
			Console.WriteLine (subject.Request ()); // Actually create the subject
			subject = new ProtectionProxy();
			Console.WriteLine ((subject as ProtectionProxy).Authenticate("LOLLIPOPS!")); // If the password is correct then the subject is requested
			Console.WriteLine (subject.Request ());
		}
	}
}

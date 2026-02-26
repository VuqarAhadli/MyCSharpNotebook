using System;
class HelloWorld {
	static void Main() {
		Console.WriteLine("Hello World");
		Ikamal kamram = new Kamal(14);
		kamram.say();
		kamram.c();
	}

	public interface Ikamal {
		void say();
		int age { get;}
		void c();
	}

	public class Kamal : Ikamal {
		public int age { get;}

		public  Kamal(int age) {
			this.age = age;
		}

		public void say() {
			Console.Write($"salam menim adim kamal yasim ise {this.age}dir\n");
		}
		public void c() {
			for(int i=0 ; i<11; ++i) {
				for(int j=0 ; j<i; ++j) {
					Console.Write("*");
				}
				Console.WriteLine("");
			}
		}
	}
}
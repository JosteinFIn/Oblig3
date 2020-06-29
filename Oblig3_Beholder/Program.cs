using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Oblig2_LegeMiddel;
using Oblig3_Beholder;

namespace Oblig3_Beholder {
    /*
	public class SortertLenkeliste<T> : Lenkeliste<T>
    {
        Node _node;
        public new void LeggTil(T x)
        {
            if (_node == null)
                _node = new Node(x);
            
        }
        public new T Fjern()
        {
            if (this.Count == 0) { throw new UgyldigListeIndeks(-1); }
            T value = this[Stoerrelse() - 1];
            Fjern(Stoerrelse()-1);
            return value;
        }
        public new Exception Sett(int pos, T x)
        {
            throw new NotSupportedException();
        }
        public new Exception LeggTil(int pos, T x)
        {
            throw new NotSupportedException();
        }
    }*/

    class Program
    {
        static void Main(string[] args)
        {
            //Liste.Stoerrelse();
            Liste<String> Liste = new Lenkeliste<String>();
            Liste.LeggTil("Element 0");
            Liste.LeggTil("Element 1");
            Liste.LeggTil("Element 2");
            Liste.LeggTil("Element 3");
            Liste.LeggTil("Element 4");
            Liste.Sett(0, "nyVerdi 0");
            Liste.Sett(2, "nyVerdi 2");

			for (int i = 0; i < Liste.Stoerrelse(); i++)
			{
				Console.WriteLine(Liste.Hent(i));
			}
			Console.WriteLine("Fjern 3");
			Console.WriteLine(Liste.Fjern(3));

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Oblig2_LegeMiddel;
using Oblig3_Beholder;

namespace Oblig3_Beholder
{
    /*public class Node
    {
        public object data;
        public Node next;
        
        public Node(object data, Node next)
        {
            this.data = data;
            this.next = next;
        }
        
    }*/
    public interface Liste<T> 
    {
		public int Stoerrelse();
		public void LeggTil(int pos, T x);
        public void LeggTil(T x);
        public void Sett(int pos, T x);
		public T Hent(int pos);
		public T Fjern(int pos);
		public T Fjern();
    }
    public class Lenkeliste<T> : Liste<T>
    {
        private Node _head;
        //private Node _node;

        public class Node
        {
            public T data;
            public Node next;

            public Node(T data)
            {
                this.data = data;
            }
        }

        
        public int Stoerrelse()
        {
            Node _node = _head;
            int i = 0;
			while (_node.next != null)
			{
                _node = _node.next;
				i++;
			}
            return i+1;
        }
        public void LeggTil(T x)
        {
            Node _node = _head;

            if (_node == null)
                _head = new Node(x);
            else
            {
                while (_node.next != null)
                {
                    _node = _node.next;
                }
                _node.next = new Node(x);
                //_head = first;
            }
        }
        public T Fjern()
        {
            Node _node = _head;

            if (_node == null)
                throw new UgyldigListeIndeks(-1);
            Node tmp = _node;
            _head = _node.next;
            return tmp.data;
        }
        
        public void LeggTil(int pos, T x)
        {
            Node before = null;
            Node _node = _head;

            if (pos == 0)
            {
                Node tmp = _head;
                _head = new Node(x);
                _head.next = tmp;
            }
            else
            {
                for (int i = 0; i < pos; i++)
                {
                    if (_node.next == null)
                        throw new UgyldigListeIndeks(pos);

                    before = _node;
                    _node = _node.next;
                }
                before.next = new Node(x);
                before.next.next = _node;

            }
        }

        public void Sett(int pos, T x)
        {
            Node _node = _head;
            for (int i = 0; i < pos; i++)
            {
                if (_node.next == null)
                    throw new UgyldigListeIndeks(pos);
                _node = _node.next;
            }
            _node.data = x;
        }
        
        public T Hent(int pos)
        {
            Node _node = _head;
			for (int i = 0; i < pos; i++)
			{
                if (_node.next == null)
                    throw new UgyldigListeIndeks(pos);

                _node = _node.next;
			}
            return _node.data;
        }
       
        public T Fjern(int pos) 
        {
			Node _node = _head;
            //Node _this = null;

            for (int i = 0; i < pos; i++)
            {
                if (_node.next == null)
                    throw new UgyldigListeIndeks(pos);
                //_this = _node;
                _node = _node.next;
            }
            var data = _node.data;
            _node = _node.next;
            return data;
        }
    }
    /*
    public class Stabel<T>: Lenkeliste<T>
    {
        public void LeggPaa(T x)
        {
            LeggTil(x);
        }
        public T TaAv()
        {
            if (this.Count == 0) { throw new UgyldigListeIndeks(-1); }
            return Fjern(Stoerrelse()-1);
        }
    }
    public class SortertLenkeliste<T> : Lenkeliste<T>
    {
        public new void LeggTil(T x)
        {
            this.Add(x);
            this.Sort();
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
            /*
            //Console.WriteLine("Hello World!");
            Narkotisk narko = new Narkotisk("Heftig Narkotika", 43.2, 2.4, 2);
            Vanedannende maaHaDet = new Vanedannende("Må Ha Det!!", 12.3, 1.4, 9);
            Vanlig kjedlig = new Vanlig("Boooring", 200, 0);

            Militaer r1 = new Militaer(narko, new Lege("Fres"), 243, 0);
            PResept r2 = new PResept(narko, new Spesialist("Moter", 45323), 243, 2);
            Blaa b1 = new Blaa(maaHaDet, new Spesialist("Fredd", 194), 2343, 2);
            */
            Liste<int> liste = new Lenkeliste<int>();

            liste.LeggTil(3);
            liste.LeggTil(5);
            liste.LeggTil(8);
            //liste.LeggTil(0, 6);
            //liste.Sett(0, 9);

            //Console.WriteLine("Hent 0: " + liste.Hent(0));
            //Console.WriteLine("Hent 3: " + liste.Hent(3));

            //Console.WriteLine("Stoerelse: " + liste.Stoerrelse());

			Console.WriteLine(liste.Fjern(2));
			Console.WriteLine(liste.Fjern());
            Console.WriteLine(liste.Fjern());
            Console.WriteLine(liste.Fjern());



            //Console.WriteLine(liste.Fjern());


            //Console.WriteLine(liste.Fjern());


            //liste.Fjern();



        }
    }
}

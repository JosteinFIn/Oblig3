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
            int i = 1;
            if(_node == null)
			{
                return 0;
			}
			while (_node.next != null)
			{
                _node = _node.next;
				i++;
			}
            return i;
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
            if(pos > Stoerrelse() || pos < 0)
			{
                throw new UgyldigListeIndeks(pos);
            }

            Node _node = _head;

            if (_node == null)
            {
                _head = new Node(x);
            }

            else if (pos == 0)
            {
                Node next = _head;
                _head = new Node(x);
                _head.next = next;
            }
            else
            {
                Node before=null;
                for (int i = 0; i < pos; i++)
                {
                    before = _node;
                    _node = _node.next;
                }
                Node after = _node;
                _node = new Node(x);
                before.next = _node;
                _node.next = after;
                
            }
        }

        public void Sett(int pos, T x)
        {
            if (pos > Stoerrelse()-1 || pos < 0)
            {
                throw new UgyldigListeIndeks(pos);
            }

            Node _node = _head;
            for (int i = 0; i < pos; i++)
            {
                _node = _node.next;
            }
            _node.data = x;
        }
        
        public T Hent(int pos)
        {
            if (pos > Stoerrelse()-1 || pos < 0)
            {
                throw new UgyldigListeIndeks(pos);
            }
            if (pos == 0)
            {
                return _head.data;
            }
            else
            {
                Node _node = _head;
                for (int i = 0; i < pos; i++)
                {
                    _node = _node.next;
                }
            return _node.data;
            }
        }
       
        public T Fjern(int pos) 
        {
			Node _node = _head;
			Node _prev = null;

            if(_head == null)
			{
                throw new UgyldigListeIndeks(pos);
            }
            if (pos == 0)
            {
                _head = _head.next;
                return _node.data;
            }
            else
            {
                if (pos > Stoerrelse() - 1 || pos < 0)
                {
                    throw new UgyldigListeIndeks(pos);
                }
                for (int i = 0; i < pos; i++)
                {
                    _prev = _node;
                    _node = _node.next;
                }
            }
            Node _this = _node;
            Node _next = _this.next;

            var data = _this.data;
            _prev.next = _next;
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

//using Oblig2_LegeMiddel;

namespace Oblig3_Beholder {
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
}

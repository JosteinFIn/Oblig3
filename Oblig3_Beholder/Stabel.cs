//using Oblig2_LegeMiddel;

namespace Oblig3_Beholder {
	public class Stabel<T>: Lenkeliste<T>
    {
        Node _top;
        public void LeggPaa(T x)
        {
            if (_top == null)
                _top = new Node(x);
			else  
                _top.next = new Node(x);
        }
        public T TaAv()
        {
            if (_top == null) 
                throw new UgyldigListeIndeks(-1);

            T data = _top.data;
            _top = null;
            return data;
        }
    }
}

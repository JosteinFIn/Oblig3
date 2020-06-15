using System;

namespace Oblig3_Beholder
{
    public class UgyldigListeIndeks : Exception
    {
        public UgyldigListeIndeks() { }

        public UgyldigListeIndeks(int index)
            : base(String.Format("Ugyldig index: " + index)) 
        { 
        }
    }
}

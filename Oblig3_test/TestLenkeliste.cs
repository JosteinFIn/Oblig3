using Oblig3_Beholder;
using System;

namespace Oblig3_TestLenkeListe
{
    class TestLenkeliste
    {
        private static int antallTester = 0;
        private static int antallPasserte = 0;
        private static int antallFeil = 0;




        static void TestTom()
        {
            Console.WriteLine("Test tom IListe");
            IListe<String> IListe = new Lenkeliste<String>();
            Sjekk(0, IListe.Stoerrelse(), "Stoerrelse() i tom IListe");

        }

        static void TestEttElement()
        {
            Console.WriteLine("\nTest ett element");
            // Her Tester vi med int, det burde fungere like bra som String
            IListe<int> IListe = new Lenkeliste<int>();
            IListe.LeggTil(0);
            Sjekk(1, IListe.Stoerrelse(), "Stoerrelse() i IListe med ett element");
            int HentetElement = IListe.Hent(0);
            Sjekk(0, HentetElement, "Hent(0) i IListe med ett element");
            // Hent(0) skal ikke Fjerne elementet, la oss Sjekke at Fjerning fungerer
            int FjernetElement = IListe.Fjern(0);
            Sjekk(0, FjernetElement, "Fjern(0) i IListe med ett element");
            Sjekk(0, IListe.Stoerrelse(), "Stoerrelse() etter at eneste element er Fjernet");
        }

        static void TestLeggTilOgFjerningAvFlereElementer()
        {
            Console.WriteLine("\nTest LeggTil() og Fjern() med flere elementer");
            IListe<String> IListe = new Lenkeliste<String>();
            IListe.LeggTil("Element 0");
            IListe.LeggTil("Element X");
            IListe.LeggTil("Element X"); // Legg til begge selv om de har samme verdi
            IListe.LeggTil("Element 3");
            Sjekk(4, IListe.Stoerrelse(), "Stoerrelse() i IListe med 4 elementer");
            Sjekk("Element 0", IListe.Hent(0), "Hent(0) i IListe med 4 elementer");
            Sjekk("Element 3", IListe.Hent(3), "Hent(3) i IListe med 4 elementer");
            Sjekk("Element 0", IListe.Fjern(), "Fjern() i IListe med 4 elementer");
            Sjekk("Element X", IListe.Fjern(), "Fjern() (for andre gang) i IListe med 3 elementer");
            Sjekk(2, IListe.Stoerrelse(), "Stoerrelse() etter Fjerning av 2 av 4 elementer");
        }

        static void TestLeggTilPaaIndeks()
        {
            Console.WriteLine("\nTest LeggTil() og LeggTil(pos) paa indeks");
            IListe<String> IListe = new Lenkeliste<String>();
            IListe.LeggTil(0, "Element X");
            Sjekk("Element X", IListe.Hent(0), "Hent(0) etter LeggTil(0, \"Element X\")");
            IListe.LeggTil("Element A");
            IListe.LeggTil("Element B");
            IListe.LeggTil(0, "Foran X");
            IListe.LeggTil(2, "Foran A");
            // Forventet rekkefolge: Foran X, Element X, Foran A, Element A, Element B
            Sjekk("Foran X", IListe.Hent(0), "Hent(0) etter LeggTil(0, \"Foran X\")");
            Sjekk("Foran A", IListe.Hent(2), "Hent(2) etter flere LeggTil med og uten indeks");
            Sjekk("Element B", IListe.Hent(4), "Hent(4) etter flere LeggTil med og uten indeks");
            IListe.LeggTil(5, "Bakerst");
            Sjekk("Bakerst", IListe.Hent(5), "Hent(5) etter LeggTil(5, \"Bakerst\")");
            Sjekk(6, IListe.Stoerrelse(), "Stoerrelse() etter LeggTil med og uten indeks");
        }

        static void TestFjernOgSettMedIndeks()
        {
            Console.WriteLine("\nTest Fjern(pos) og Sett(pos, x) med indekser");
            IListe<String> IListe = new Lenkeliste<String>();
            IListe.LeggTil("Element 0");
            IListe.LeggTil("Element 1");
            IListe.LeggTil("Element 2");
            IListe.LeggTil("Element 3");
            IListe.LeggTil("Element 4");
            IListe.Sett(0, "nyVerdi 0");
            IListe.Sett(2, "nyVerdi 2");
            Sjekk("nyVerdi 0", IListe.Hent(0), "Hent(0) etter Sett(0, \"nyVerdi 0\")");
            Sjekk("nyVerdi 2", IListe.Hent(2), "Hent(2) etter Sett(2, \"nyVerdi 2\")");
            Sjekk("Element 3", IListe.Fjern(3), "Fjern(3)");
            Sjekk("Element 4", IListe.Fjern(3), "Fjern(3) for andre gang");
            IListe.LeggTil("NyttElement");
            Sjekk("NyttElement", IListe.Hent(3), "Hent(3) skal Hente nytt element lagt til etter at andre elementer har blitt Fjernet");
            Sjekk(4, IListe.Stoerrelse(), "Stoerrelse() etter flere kall paa Fjern() og LeggTil()");

        }

        static void TestUnntak()
        {
            Console.WriteLine("\nTest unntak");
            IListe<string> IListe = new Lenkeliste<string>();
            try
            {
                IListe.Fjern(); // skal ikke fungere, men skal kaste et unntak
                                // hit kommer vi ikke om det ble kastet et unntak
                SjekkFeilet("Fjern() paa tom IListe skulle kastet unntak");
            }
            catch(UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Fjern(0);
                SjekkFeilet("Fjern(0) paa tom IListe skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Hent(0);
                SjekkFeilet("Hent(0) paa tom IListe skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Sett(0, "asd");
                SjekkFeilet("Sett(0, ...) paa tom IListe skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.LeggTil(1, "asd");
                SjekkFeilet("LeggTil(1, ...) paa tom IListe skulle kastet unntak - kun indeks 0 er gyldig i tom IListe");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            IListe.LeggTil("Forste element");
            IListe.LeggTil(1, "Andre element"); // Sette inn bakerst skal fungere
            try
            {
                IListe.Fjern(2);
                SjekkFeilet("Fjern(2) paa IListe med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Hent(2);
                SjekkFeilet("Hent(2) paa IListe med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Sett(2, "2 er ugyldig indeks");
                SjekkFeilet("Sett(2, ...) paa IListe med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.LeggTil(3, "3 er ugyldig indeks");
                SjekkFeilet("LeggTil(3, ...) paa IListe med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Fjern(-1);
                SjekkFeilet("Fjern(-1) skal kaste unntaket UgyldigIListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Hent(-1);
                SjekkFeilet("Hent(-1) skal kaste unntaket UgyldigIListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.Sett(-1, "-1 er en ugyldig indeks!");
                SjekkFeilet("Sett(-1, ...) skal kaste unntaket UgyldigIListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                IListe.LeggTil(-1, "-1 er en ugyldig indeks!");
                SjekkFeilet("LeggTil(-1, ...) skal kaste unntaket UgyldigIListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }
        }

        static void Sjekk(Object forventet, Object faktisk, String Testmelding)
        {
            if (forventet.Equals(faktisk))
            {
                SjekkPasserte();
            }
            else
            {
                SjekkFeilet(Testmelding);
                Console.WriteLine("  > Forventet verdi: " + forventet);
                Console.WriteLine("  > Faktisk verdi: " + faktisk);
            }
        }

        static void SjekkPasserte()
        {
            antallTester++;
            antallPasserte++;
            Console.WriteLine("- Test " + antallTester + ": OK");
        }

        static void SjekkFeilet(String Testmelding)
        {
            antallTester++;
            antallFeil++;
            Console.WriteLine("- Test " + antallTester + " feilet: " + Testmelding);
        }

        class Test1
        {
            static void Main(string[] args)
            {
                TestTom();
                TestEttElement();
                TestLeggTilOgFjerningAvFlereElementer();
                TestLeggTilPaaIndeks();
                TestFjernOgSettMedIndeks();
                TestUnntak();
                Console.WriteLine("\n" + antallTester + " tester ferdig");
                Console.WriteLine(antallPasserte + " passerte, " + antallFeil + " feil");
            }
        }
    }
}

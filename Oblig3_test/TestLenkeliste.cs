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
            Console.WriteLine("Test tom Liste");
            Liste<String> Liste = new Lenkeliste<String>();
            Sjekk(0, Liste.Stoerrelse(), "Stoerrelse() i tom Liste");

        }

        static void TestEttElement()
        {
            Console.WriteLine("\nTest ett element");
            // Her Tester vi med int, det burde fungere like bra som String
            Liste<int> Liste = new Lenkeliste<int>();
            Liste.LeggTil(0);
            Sjekk(1, Liste.Stoerrelse(), "Stoerrelse() i Liste med ett element");
            int HentetElement = Liste.Hent(0);
            Sjekk(0, HentetElement, "Hent(0) i Liste med ett element");
            // Hent(0) skal ikke Fjerne elementet, la oss Sjekke at Fjerning fungerer
            int FjernetElement = Liste.Fjern(0);
            Sjekk(0, FjernetElement, "Fjern(0) i Liste med ett element");
            Sjekk(0, Liste.Stoerrelse(), "Stoerrelse() etter at eneste element er Fjernet");
        }

        static void TestLeggTilOgFjerningAvFlereElementer()
        {
            Console.WriteLine("\nTest LeggTil() og Fjern() med flere elementer");
            Liste<String> Liste = new Lenkeliste<String>();
            Liste.LeggTil("Element 0");
            Liste.LeggTil("Element X");
            Liste.LeggTil("Element X"); // Legg til begge selv om de har samme verdi
            Liste.LeggTil("Element 3");
            Sjekk(4, Liste.Stoerrelse(), "Stoerrelse() i Liste med 4 elementer");
            Sjekk("Element 0", Liste.Hent(0), "Hent(0) i Liste med 4 elementer");
            Sjekk("Element 3", Liste.Hent(3), "Hent(3) i Liste med 4 elementer");
            Sjekk("Element 0", Liste.Fjern(), "Fjern() i Liste med 4 elementer");
            Sjekk("Element X", Liste.Fjern(), "Fjern() (for andre gang) i Liste med 3 elementer");
            Sjekk(2, Liste.Stoerrelse(), "Stoerrelse() etter Fjerning av 2 av 4 elementer");
        }

        static void TestLeggTilPaaIndeks()
        {
            Console.WriteLine("\nTest LeggTil() og LeggTil(pos) paa indeks");
            Liste<String> Liste = new Lenkeliste<String>();
            Liste.LeggTil(0, "Element X");
            Sjekk("Element X", Liste.Hent(0), "Hent(0) etter LeggTil(0, \"Element X\")");
            Liste.LeggTil("Element A");
            Liste.LeggTil("Element B");
            Liste.LeggTil(0, "Foran X");
            Liste.LeggTil(2, "Foran A");
            // Forventet rekkefolge: Foran X, Element X, Foran A, Element A, Element B
            Sjekk("Foran X", Liste.Hent(0), "Hent(0) etter LeggTil(0, \"Foran X\")");
            Sjekk("Foran A", Liste.Hent(2), "Hent(2) etter flere LeggTil med og uten indeks");
            Sjekk("Element B", Liste.Hent(4), "Hent(4) etter flere LeggTil med og uten indeks");
            Liste.LeggTil(5, "Bakerst");
            Sjekk("Bakerst", Liste.Hent(5), "Hent(5) etter LeggTil(5, \"Bakerst\")");
            Sjekk(6, Liste.Stoerrelse(), "Stoerrelse() etter LeggTil med og uten indeks");
        }

        static void TestFjernOgSettMedIndeks()
        {
            Console.WriteLine("\nTest Fjern(pos) og Sett(pos, x) med indekser");
            Liste<String> Liste = new Lenkeliste<String>();
            Liste.LeggTil("Element 0");
            Liste.LeggTil("Element 1");
            Liste.LeggTil("Element 2");
            Liste.LeggTil("Element 3");
            Liste.LeggTil("Element 4");
            Liste.Sett(0, "nyVerdi 0");
            Liste.Sett(2, "nyVerdi 2");
            Sjekk("nyVerdi 0", Liste.Hent(0), "Hent(0) etter Sett(0, \"nyVerdi 0\")");
            Sjekk("nyVerdi 2", Liste.Hent(2), "Hent(2) etter Sett(2, \"nyVerdi 2\")");
            Sjekk("Element 3", Liste.Fjern(3), "Fjern(3)");
            Sjekk("Element 4", Liste.Fjern(3), "Fjern(3) for andre gang");
            Liste.LeggTil("NyttElement");
            Sjekk("NyttElement", Liste.Hent(3), "Hent(3) skal Hente nytt element lagt til etter at andre elementer har blitt Fjernet");
            Sjekk(4, Liste.Stoerrelse(), "Stoerrelse() etter flere kall paa Fjern() og LeggTil()");

        }

        static void TestUnntak()
        {
            Console.WriteLine("\nTest unntak");
            Liste<string> Liste = new Lenkeliste<string>();
            try
            {
                Liste.Fjern(); // skal ikke fungere, men skal kaste et unntak
                                // hit kommer vi ikke om det ble kastet et unntak
                SjekkFeilet("Fjern() paa tom Liste skulle kastet unntak");
            }
            catch(UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Fjern(0);
                SjekkFeilet("Fjern(0) paa tom Liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Hent(0);
                SjekkFeilet("Hent(0) paa tom Liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Sett(0, "asd");
                SjekkFeilet("Sett(0, ...) paa tom Liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.LeggTil(1, "asd");
                SjekkFeilet("LeggTil(1, ...) paa tom Liste skulle kastet unntak - kun indeks 0 er gyldig i tom Liste");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            Liste.LeggTil("Forste element");
            Liste.LeggTil(1, "Andre element"); // Sette inn bakerst skal fungere
            try
            {
                Liste.Fjern(2);
                SjekkFeilet("Fjern(2) paa Liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Hent(2);
                SjekkFeilet("Hent(2) paa Liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Sett(2, "2 er ugyldig indeks");
                SjekkFeilet("Sett(2, ...) paa Liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.LeggTil(3, "3 er ugyldig indeks");
                SjekkFeilet("LeggTil(3, ...) paa Liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Fjern(-1);
                SjekkFeilet("Fjern(-1) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Hent(-1);
                SjekkFeilet("Hent(-1) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.Sett(-1, "-1 er en ugyldig indeks!");
                SjekkFeilet("Sett(-1, ...) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                Liste.LeggTil(-1, "-1 er en ugyldig indeks!");
                SjekkFeilet("LeggTil(-1, ...) skal kaste unntaket UgyldigListeIndeks");
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

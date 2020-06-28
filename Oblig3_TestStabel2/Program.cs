using Oblig3_Beholder;
using System;

namespace Oblig3_TesStabel
{
    class TestStabel
    {
        private static int antallTester = 0;
        private static int antallPasserte = 0;
        private static int antallFeil = 0;




        static void TestTom()
        {
            Console.WriteLine("Test tom liste");
            Stabel<String> liste = new Stabel<String>();
            Sjekk(0, liste.Stoerrelse(), "Stoerrelse() i tom liste");
        }

        static void TestEttElement()
        {
            Console.WriteLine("\nTest ett element");
            // Her Tester vi med int, det burde fungere like bra som String
            Stabel<int> liste = new Stabel<int>();
            liste.LeggTil(0);
            Sjekk(1, liste.Stoerrelse(), "Stoerrelse() i liste med ett element");
            int HentetElement = liste.Hent(0);
            Sjekk(0, HentetElement, "Hent(0) i liste med ett element");
            // Hent(0) skal ikke Fjerne elementet, la oss Sjekke at Fjerning fungerer
            int FjernetElement = liste.Fjern(0);
            Sjekk(0, FjernetElement, "Fjern(0) i liste med ett element");
            Sjekk(0, liste.Stoerrelse(), "Stoerrelse() etter at eneste element er Fjernet");
        }

        static void TestLeggTilOgFjerningAvFlereElementer()
        {
            Console.WriteLine("\nTest LeggTil() og Fjern() med flere elementer");
            Stabel<String> liste = new Stabel<String>();
            liste.LeggTil("Element 0");
            liste.LeggTil("Element X");
            liste.LeggTil("Element X"); // Legg til begge selv om de har samme verdi
            liste.LeggTil("Element 3");
            Sjekk(4, liste.Stoerrelse(), "Stoerrelse() i liste med 4 elementer");
            Sjekk("Element 0", liste.Hent(0), "Hent(0) i liste med 4 elementer");
            Sjekk("Element 3", liste.Hent(3), "Hent(3) i liste med 4 elementer");
            Sjekk("Element 0", liste.Fjern(), "Fjern() i liste med 4 elementer");
            Sjekk("Element X", liste.Fjern(), "Fjern() (for andre gang) i liste med 3 elementer");
            Sjekk(2, liste.Stoerrelse(), "Stoerrelse() etter Fjerning av 2 av 4 elementer");
        }

        static void TestLeggTilPaaIndeks()
        {
            Console.WriteLine("\nTest LeggTil() og LeggTil(pos) paa indeks");
            Stabel<String> liste = new Stabel<String>();
            liste.LeggTil(0, "Element X");
            Sjekk("Element X", liste.Hent(0), "Hent(0) etter LeggTil(0, \"Element X\")");
            liste.LeggTil("Element A");
            liste.LeggTil("Element B");
            liste.LeggTil(0, "Foran X");
            liste.LeggTil(2, "Foran A");
            // Forventet rekkefolge: Foran X, Element X, Foran A, Element A, Element B
            Sjekk("Foran X", liste.Hent(0), "Hent(0) etter LeggTil(0, \"Foran X\")");
            Sjekk("Foran A", liste.Hent(2), "Hent(2) etter flere LeggTil med og uten indeks");
            Sjekk("Element B", liste.Hent(4), "Hent(4) etter flere LeggTil med og uten indeks");
            liste.LeggTil(5, "Bakerst");
            Sjekk("Bakerst", liste.Hent(5), "Hent(5) etter LeggTil(5, \"Bakerst\")");
            Sjekk(6, liste.Stoerrelse(), "Stoerrelse() etter LeggTil med og uten indeks");
        }

        static void TestFjernOgSettMedIndeks()
        {
            Console.WriteLine("\nTest Fjern(pos) og Sett(pos, x) med indekser");
            Stabel<String> liste = new Stabel<String>();
            liste.LeggTil("Element 0");
            liste.LeggTil("Element 1");
            liste.LeggTil("Element 2");
            liste.LeggTil("Element 3");
            liste.LeggTil("Element 4");
            liste.Sett(0, "nyVerdi 0");
            liste.Sett(2, "nyVerdi 2");
            Sjekk("nyVerdi 0", liste.Hent(0), "Hent(0) etter Sett(0, \"nyVerdi 0\")");
            Sjekk("nyVerdi 2", liste.Hent(2), "Hent(2) etter Sett(2, \"nyVerdi 2\")");
            Sjekk("Element 3", liste.Fjern(3), "Fjern(3)");
            Sjekk("Element 4", liste.Fjern(3), "Fjern(3) for andre gang");
            liste.LeggTil("NyttElement");
            Sjekk("NyttElement", liste.Hent(3), "Hent(3) skal Hente nytt element lagt til etter at andre elementer har blitt Fjernet");
            Sjekk(4, liste.Stoerrelse(), "Stoerrelse() etter flere kall paa Fjern() og LeggTil()");
        }

        static void TestUnntak()
        {
            Console.WriteLine("\nTest unntak");
            Stabel<String> liste = new Stabel<String>();
            try
            {
                liste.Fjern(); // skal ikke fungere, men skal kaste et unntak
                               // hit kommer vi ikke om det ble kastet et unntak
                SjekkFeilet("Fjern() paa tom liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Fjern(0);
                SjekkFeilet("Fjern(0) paa tom liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Hent(0);
                SjekkFeilet("Hent(0) paa tom liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Sett(0, "asd");
                SjekkFeilet("Sett(0, ...) paa tom liste skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.LeggTil(1, "asd");
                SjekkFeilet("LeggTil(1, ...) paa tom liste skulle kastet unntak - kun indeks 0 er gyldig i tom liste");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            liste.LeggTil("Forste element");
            liste.LeggTil(1, "Andre element"); // Sette inn bakerst skal fungere
            try
            {
                liste.Fjern(2);
                SjekkFeilet("Fjern(2) paa liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Hent(2);
                SjekkFeilet("Hent(2) paa liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Sett(2, "2 er ugyldig indeks");
                SjekkFeilet("Sett(2, ...) paa liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.LeggTil(3, "3 er ugyldig indeks");
                SjekkFeilet("LeggTil(3, ...) paa liste med 2 elementer skulle kastet unntak");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Fjern(-1);
                SjekkFeilet("Fjern(-1) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Hent(-1);
                SjekkFeilet("Hent(-1) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.Sett(-1, "-1 er en ugyldig indeks!");
                SjekkFeilet("Sett(-1, ...) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }

            try
            {
                liste.LeggTil(-1, "-1 er en ugyldig indeks!");
                SjekkFeilet("LeggTil(-1, ...) skal kaste unntaket UgyldigListeIndeks");
            }
            catch (UgyldigListeIndeks)
            {
                SjekkPasserte();
            }
        }

        static void TestStabelMetoder()
        {
            Stabel<String> stabel = new Stabel<String>();
            Console.WriteLine("\nTest stabel-spesifikke metoder LeggPaa og TaAv");
            stabel.LeggPaa("Element 0");
            stabel.LeggPaa("Element 1");
            stabel.LeggPaa("Element 2");
            Sjekk("Element 2", stabel.Hent(2), "Hent(2) paa stabel med 3 elementer");
            Sjekk(3, stabel.Stoerrelse(), "Stoerrelse paa stabel med 3 elementer");
            Sjekk("Element 2", stabel.TaAv(), "TaAv() paa stabel med 3 elementer");
            Sjekk("Element 1", stabel.TaAv(), "TaAv() paa stabel for andre gang");
            Sjekk(1, stabel.Stoerrelse(), "Stoerrelse() paa stabel etter at TaAv() har blitt kalt");
            Sjekk("Element 0", stabel.TaAv(), "TaAv() paa stabel med ett element");
            Sjekk(0, stabel.Stoerrelse(), "Stoerrelse() paa stabel som har blitt tom etter flere TaAv()");

            try
            {
                stabel.TaAv();
                SjekkFeilet("TaAv() paa tom stabel skal kaste et unntak");
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

        class Test2
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

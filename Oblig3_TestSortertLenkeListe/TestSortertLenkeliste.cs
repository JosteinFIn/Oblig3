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
            SortertLenkeliste <String> liste = new SortertLenkeliste <String>();
            Sjekk(0, liste.Stoerrelse(), "Stoerrelse() i tom liste");
        }

        static void TestEttElement()
        {
            Console.WriteLine("\nTest ett element");
            SortertLenkeliste <String> liste = new SortertLenkeliste <String>();
            liste.LeggTil("A");
            Sjekk(1, liste.Stoerrelse(), "Stoerrelse() i liste med ett element");
            String HentetElement = liste.Hent(0);
            Sjekk("A", HentetElement, "Hent(0) i liste med ett element");
            // Hent(0) skal ikke Fjerne elementet, la oss Sjekke at Fjerning fungerer
            String FjernetElement = liste.Fjern();
            Sjekk("A", FjernetElement, "Fjern() i liste med ett element");
            Sjekk(0, liste.Stoerrelse(), "Stoerrelse() etter at eneste element er Fjernet");
        }

        static void TestLeggTilOgFjerningAvFlereElementer()
        {
            Console.WriteLine("\nTest LeggTil() og Fjern() med flere elementer");
            SortertLenkeliste <String> liste = new SortertLenkeliste <String>();
            liste.LeggTil("C");
            liste.LeggTil("A");
            liste.LeggTil("D");
            liste.LeggTil("B");
            Sjekk(4, liste.Stoerrelse(), "Stoerrelse() i liste med 4 elementer");
            Sjekk("A", liste.Hent(0), "Hent(0) i liste med 4 elementer");
            Sjekk("B", liste.Hent(1), "Hent(1) i liste med 4 elementer");
            Sjekk("D", liste.Hent(3), "Hent(3) i liste med 4 elementer");
            Sjekk("D", liste.Fjern(), "Fjern() i liste med 4 elementer");
            Sjekk("C", liste.Fjern(), "Fjern() (for andre gang) i liste med 3 elementer");
            Sjekk(2, liste.Stoerrelse(), "Stoerrelse() etter Fjerning av 2 av 4 elementer");
        }

        static void TestLeggTilMedints()
        {
            Console.WriteLine("\nTest LeggTil() med int i stedet for String");
            SortertLenkeliste <int> liste = new SortertLenkeliste <int>();
            liste.LeggTil(4);
            liste.LeggTil(1337);
            liste.LeggTil(30);
            liste.LeggTil(15);
            String rekkefolge = "" + liste.Hent(0) + " - " + liste.Hent(1) + " - " + liste.Hent(2) + " - " + liste.Hent(3);
            Sjekk("4 - 15 - 30 - 1337", rekkefolge, "Sjekk at sortering blir riktig med ints");
        }

        static void TestFjernMedIndeks()
        {
            Console.WriteLine("\nTest Fjern(pos) med indekser");
            SortertLenkeliste <String> liste = new SortertLenkeliste <String>();
            liste.LeggTil("elementC");
            liste.LeggTil("elementAA");
            liste.LeggTil("elementSist");
            liste.LeggTil("elementAA"); // Duplikater skal vaere tillatt
            liste.LeggTil("elementBBB");
            liste.LeggTil("elementD");

            String rekkefolge = liste.Hent(0) + " - " + liste.Hent(1) + " - " + liste.Hent(2) +
                    " - " + liste.Hent(3) + " - " + liste.Hent(4) + " - " + liste.Hent(5);
            String forventet = "elementAA - elementAA - elementBBB - elementC - elementD - elementSist";

            Sjekk(forventet, rekkefolge, "Sjekk at sortering blir riktig med strings");
            Sjekk("elementBBB", liste.Fjern(2), "Fjern(2) paa listen [" + rekkefolge + "]");
            Sjekk("elementC", liste.Fjern(2), "Fjern(2) andre gang paa listen [" + rekkefolge + "]");
            Sjekk("elementSist", liste.Fjern(), "Fjern() der elementSist skal ligge sist");
            Sjekk(3, liste.Stoerrelse(), "Stoerrelse() etter flere kall paa Fjern() og LeggTil()");
        }

        static void TestUnntak()
        {
            Console.WriteLine("\nTest unntak");
            SortertLenkeliste <String> liste = new SortertLenkeliste <String>();
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
                liste.Sett(0, "forventet UnsupportedOperationException her");
                SjekkFeilet("Sett(0, ...) skulle kastet UnsupportedOperationException");
            }
            catch (NotSupportedException)
            {
                SjekkPasserte();
            }

            try
            {
                liste.LeggTil(1, "forventet UnsupportedOperationException her");
                SjekkFeilet("LeggTil(1, ...) skulle kastet UnsupportedOperationException");
            }
            catch (NotSupportedException)
            {
                SjekkPasserte();
            }

            liste.LeggTil("Forste element");
            liste.LeggTil("Siste element");
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
                liste.Sett(2, "forventet UnsupportedOperationException her");
                SjekkFeilet("Sett(2, ...) skulle kastet UnsupportedOperationException");
            }
            catch (NotSupportedException)
            {
                SjekkPasserte();
            }

            try
            {
                liste.LeggTil(3, "forventet UnsupportedOperationException her");
                SjekkFeilet("LeggTil(3, ...) skulle kastet UnsupportedOperationException");
            }
            catch (NotSupportedException)
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
                liste.Sett(-1, "forventet UnsupportedOperationException her");
                SjekkFeilet("Sett(-1, ...) skulle kastet UnsupportedOperationException");
            }
            catch (NotSupportedException)
            {
                SjekkPasserte();
            }

            try
            {
                liste.LeggTil(-1, "forventet UnsupportedOperationException her");
                SjekkFeilet("LeggTil(-1, ...) skulle kastet UnsupportedOperationException");
            }
            catch (NotSupportedException)
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

        class Test3
        {
            static void Main(string[] args)
            {
                TestTom();
                TestEttElement();
                TestLeggTilOgFjerningAvFlereElementer();
                TestLeggTilMedints();
                TestFjernMedIndeks();
                TestUnntak();
                Console.WriteLine("\n" + antallTester + " Tester ferdig");
                Console.WriteLine(antallPasserte + " passerte, " + antallFeil + " feil");
            }
        }
    }
}

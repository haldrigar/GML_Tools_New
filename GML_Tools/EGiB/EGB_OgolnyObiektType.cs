using System.Collections.Generic;

namespace GML_Tools.EGiB
{
    /// <summary>
    /// Klasa stanowi klasę abstrakcyjną grupującą atrybuty dziedziczone przez klasy obiektów ewidencji gruntów i budynków.
    /// </summary>
    public class EGB_OgolnyObiektType
    {
        /// <summary>
        /// Typ reprezentujący unikalny identyfikator obiektu nadawany przez dostawcę zbioru danych
        /// </summary>
        public EGB_IdentyfikatorIIPType idIIP { get; set; }

        /// <summary>
        /// Data i czas utworzenia obiektu w bazie danych.
        /// </summary>
        public string startObiekt { get; set; }

        /// <summary>
        /// Data i czas utworzenia wersji obiektu w bazie danych.
        /// </summary>
        public string startWersjaObiekt { get; set; }

        /// <summary>
        /// Data i czas przeniesienia wersji obiektu do archiwum.
        /// </summary>
        public string koniecWersjaObiekt { get; set; }

        /// <summary>
        /// Data i czas przeniesienia obiektu do archiwum.
        /// </summary>
        public string koniecObiekt { get; set; }

        /// <summary>
        /// Dokument stanowiący podstawę utworzenia obiektu - referencja
        /// </summary>
        public List<string> dokument2_ref { get; set; }                 // type="egb:EGB_DokumentPropertyType" minOccurs="0" maxOccurs="unbounded"

        /// <summary>
        /// Dokument stanowiący podstawę utworzenia obiektu - referencja
        /// </summary>
        public List<string> operatTechniczny2_ref { get; set; }         // type="egb:EGB_OperatTechnicznyPropertyType" minOccurs="0" maxOccurs="unbounded"

        /// <summary>
        /// Podstawa prawna utworzenia wersji obiektu
        /// </summary>
        public string podstawaUtworzeniaWersjiObiektu_ref { get; set; } // type="egb:EGB_ZmianaPropertyType" minOccurs="0" maxOccurs="1"

        /// <summary>
        /// Podstawa prawna usunięcia obiektu.
        /// </summary>
        public string podstawaUsunieciaObiektu_ref { get; set; }        // type="egb:EGB_ZmianaPropertyType" minOccurs="0" maxOccurs="1"


        public EGB_OgolnyObiektType()
        {
            idIIP = new EGB_IdentyfikatorIIPType();
            startObiekt = string.Empty;
            startWersjaObiekt = string.Empty;
            koniecWersjaObiekt = string.Empty;
            koniecObiekt = string.Empty;
            dokument2_ref = new List<string>();
            operatTechniczny2_ref = new List<string>();
            podstawaUtworzeniaWersjiObiektu_ref = string.Empty;
            podstawaUsunieciaObiektu_ref = string.Empty;
        }
    }
    
}

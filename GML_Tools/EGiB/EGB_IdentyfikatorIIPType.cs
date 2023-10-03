namespace GML_Tools.EGiB
{
    /// <summary>
    /// Typ reprezentujący unikalny identyfikator obiektu nadawany przez dostawcę zbioru danych
    /// </summary>
    public class EGB_IdentyfikatorIIPType
    {
        /// <summary>
        /// Identyfikator lokalny obiektu nadawany przez dostawcę zbioru danych, unikalny w przestrzeni nazw.
        /// </summary>
        public string lokalnyId { get; set; }     

        /// <summary>
        /// Przestrzeń nazw identyfikujaca zbiór danych, z którego pochodzi obiekt.
        /// </summary>
        public string przestrzenNazw { get; set; }

        /// <summary>
        /// Identyfikator wersji obiektu.
        /// </summary>
        public string wersjaId { get; set; }

        public EGB_IdentyfikatorIIPType()
        {
            lokalnyId = string.Empty;
            przestrzenNazw = string.Empty;
            wersjaId = string.Empty;
        }
    }
}

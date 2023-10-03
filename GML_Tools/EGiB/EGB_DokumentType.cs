using System.Collections.Generic;

namespace GML_Tools.EGiB
{
    public class EGB_DokumentType
    {
        public EGB_IdentyfikatorIIPType idIIP { get; set; }
        public string startObiekt { get; set; }
        public string koniecObiekt { get; set; }
        public string tytul { get; set; }
        public string dataDokumentu { get; set; }
        public string nazwaTworcyDokumentu { get; set; }
        public string opisDokumentu { get; set; }
        public string rodzajDokumentu { get; set; }             // type="egb:EGB_RodzajDokumentuType"
        public string sygnaturaDokumentu { get; set; }
        public string oznKancelaryjneDokumentu { get; set; }
        public List<string> zasobSieciowy { get; set; }         // type="gmd:CI_OnlineResource_Type" minOccurs="0" maxOccurs="unbounded"
        public List<string> zalacznikDokumentu { get; set; }    // type="egb:EGB_DokumentPropertyType" minOccurs="0" maxOccurs="unbounded"

        public EGB_DokumentType()
        {
            startObiekt = string.Empty;
            koniecObiekt = string.Empty;
            tytul = string.Empty;
            dataDokumentu = string.Empty;
            nazwaTworcyDokumentu = string.Empty;
            opisDokumentu = string.Empty;
            rodzajDokumentu = string.Empty;
            sygnaturaDokumentu = string.Empty;
            oznKancelaryjneDokumentu = string.Empty;
            idIIP = new EGB_IdentyfikatorIIPType();
            zasobSieciowy = new List<string>();
            zalacznikDokumentu = new List<string>();
        }
    }
}

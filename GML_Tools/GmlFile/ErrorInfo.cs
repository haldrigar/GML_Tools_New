namespace GML_Tools.GmlFile
{
    public class ErrorInfo
    {
        public int ErrorCounter { get; set; }
        public string ErrorType { get; set; }
        public int Line { get; set; }
        public string FeatureMember { get; set; }
        public string FeatureMemberId { get; set; }
        public string LokalnyId { get; set; }
        public string Element { get; set; }
        public string ShortInfo { get; set; }
        public string LongInfo { get; set; }
    }
}

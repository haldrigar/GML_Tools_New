using Microsoft.SqlServer.Types;

namespace GML_Tools.GmlFile
{
    internal class FeatureMember
    {
        public string LocalName { get; set; }
        public string GmlId { get; set; }
        public Geom Geometry { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }

    }
}

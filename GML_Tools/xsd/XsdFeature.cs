using System.Collections.Generic;

namespace GML_Tools.xsd
{
    internal class XsdFeature
    {
        public string Schema { get; set; }
        public string FeatureName { get; set; }
        public List<XsdAttributeWithValues> AttributesList { get; set; }
    }
}

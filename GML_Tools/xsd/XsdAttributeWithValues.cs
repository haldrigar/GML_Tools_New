using System.Collections.Generic;

namespace GML_Tools.xsd
{
    internal class XsdAttributeWithValues
    {
        public string Schema { get; set; }
        public string AttributeName { get; set; } 
        public string AttributeType { get; set; } 
        public List<string> ValuesList { get; set; }
    }
}

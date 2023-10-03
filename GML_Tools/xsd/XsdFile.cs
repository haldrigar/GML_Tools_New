using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;

namespace GML_Tools.xsd
{
    public class XsdFile
    {
        private readonly XmlSchemaSet _gmlSchemaSet;

        private readonly List<XsdFeature> _xsdFeatures = new List<XsdFeature>();

        private readonly List<XsdAttributeWithValues> _xsdAttributesWithValuesList = new List<XsdAttributeWithValues>();

        public XsdFile(string schemaName)
        {
            _gmlSchemaSet = new XmlSchemaSet
            {
                XmlResolver = new XsdUrlResolverReplace()
            };

            switch (schemaName)
            {
                case "BDOT_2015":
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\PODSTAWOWY_2015\\BT_ModelPodstawowy.xsd");
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych500:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\BDOT_2015\\BDOT500.xsd");
                    break;
                    
                case "EGiB_2015":
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\PODSTAWOWY_2015\\BT_ModelPodstawowy.xsd");
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:ewidencjaGruntowBudynkow:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\EGiB_2015\\EGB_OgolnyObiekt.xsd");
                    break;
                    
                case "GESUT_2015":
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\PODSTAWOWY_2015\\BT_ModelPodstawowy.xsd");
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:geodezyjnaEwidencjaSieciUzbrojeniaTerenu:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\GESUT_2015\\GESUT.xsd");
                    break;

                case "BDOT_2021":
                    _gmlSchemaSet.Add("bazaDanychObiektowTopograficznych500:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\BDOT_2021\\BDOT500.xsd");
                    break;

                case "EGiB_2021":
                    _gmlSchemaSet.Add("ewidencjaGruntowIBudynkow:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\EGiB_2021\\EGIB.xsd");
                    break;

                case "GESUT_2021":
                    _gmlSchemaSet.Add("geodezyjnaEwidencjaSieciUzbrojeniaTerenu:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\GESUT_2021\\GESUT.xsd");
                    break;

                case "ALL":
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\PODSTAWOWY_2015\\BT_ModelPodstawowy.xsd");
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych500:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\BDOT_2015\\BDOT500.xsd");
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:ewidencjaGruntowBudynkow:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\EGiB_2015\\EGB_OgolnyObiekt.xsd");
                    _gmlSchemaSet.Add("urn:gugik:specyfikacje:gmlas:geodezyjnaEwidencjaSieciUzbrojeniaTerenu:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\GESUT_2015\\GESUT.xsd");

                    _gmlSchemaSet.Add("bazaDanychObiektowTopograficznych500:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\BDOT_2021\\BDOT500.xsd");
                    _gmlSchemaSet.Add("ewidencjaGruntowIBudynkow:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\EGiB_2021\\EGIB.xsd");
                    _gmlSchemaSet.Add("geodezyjnaEwidencjaSieciUzbrojeniaTerenu:1.0", AppDomain.CurrentDomain.BaseDirectory + "\\xsd\\GESUT_2021\\GESUT.xsd");
                    
                    break;
            }

            _gmlSchemaSet.Compile();
            
            foreach (XmlSchema xmlSchema in _gmlSchemaSet.Schemas().Cast<XmlSchema>())
            {
                // --------------------------------------------------------------------------------------------------------------------------------------
                // WCZYTANIE SŁOWNIKA RODZAJÓW ATRYBUTÓW I ICH WARTOŚCI
                // --------------------------------------------------------------------------------------------------------------------------------------

                IEnumerable<XmlSchemaSimpleType> simpleTypes = xmlSchema.SchemaTypes.Values.OfType<XmlSchemaSimpleType>().Where(t => t.Content is XmlSchemaSimpleTypeRestriction);

                foreach (XmlSchemaSimpleType simpleType in simpleTypes)
                {
                    XmlSchemaSimpleTypeRestriction restriction = (XmlSchemaSimpleTypeRestriction) simpleType.Content;
                    IEnumerable<XmlSchemaEnumerationFacet> enumFacets = restriction.Facets.OfType<XmlSchemaEnumerationFacet>();

                    IEnumerable<XmlSchemaEnumerationFacet> xmlSchemaEnumerationFacets = enumFacets.ToList();

                    List<string> xsdValues = xmlSchemaEnumerationFacets.Select(wartosc => wartosc.Value).ToList();

                    XsdAttributeWithValues xsdAttributeWithValues = new XsdAttributeWithValues
                    {
                        AttributeType = simpleType.Name,
                        Schema = xmlSchema.TargetNamespace,
                        ValuesList = xsdValues
                    };

                    _xsdAttributesWithValuesList.Add(xsdAttributeWithValues);
                }

                // --------------------------------------------------------------------------------------------------------------------------------------
                // WCZYTANIE SŁOWNIKA OBIEKTÓW WRAZ Z ICH ATRYBUTAMI I WARTOŚCIAMI TYCH ATRYBUTÓW
                // --------------------------------------------------------------------------------------------------------------------------------------

                foreach (XmlSchemaElement obiekt in xmlSchema.Elements.Values)
                {
                    XmlSchemaComplexType complexType = obiekt.ElementSchemaType as XmlSchemaComplexType;

                    if (complexType?.ContentTypeParticle is XmlSchemaSequence sequence)
                    {
                        XsdFeature xsdFeature = new XsdFeature
                        {
                            Schema = xmlSchema.TargetNamespace,
                            FeatureName = obiekt.Name,
                            AttributesList = new List<XsdAttributeWithValues>()
                        };

                        foreach (XmlSchemaObject o in sequence.Items)
                        {
                            if (o.GetType() == typeof(XmlSchemaElement)) // sprawdzić co to są te choice
                            {
                                XmlSchemaElement atrybut = (XmlSchemaElement)o;

                                if (atrybut.Name != null)
                                {
                                    if (atrybut.ElementSchemaType.Name != null)
                                    {
                                        if (_xsdAttributesWithValuesList.Any(a => a.AttributeType == atrybut.ElementSchemaType.Name && a.Schema == xmlSchema.TargetNamespace))
                                        {
                                            XsdAttributeWithValues xsdAttributeWithValues = _xsdAttributesWithValuesList.First(a => a.AttributeType == atrybut.ElementSchemaType.Name && a.Schema == xmlSchema.TargetNamespace);

                                            xsdAttributeWithValues.AttributeName = atrybut.Name;

                                            xsdFeature.AttributesList.Add(xsdAttributeWithValues);
                                        }
                                    }
                                    else
                                    {
                                        if (atrybut.ElementSchemaType.BaseXmlSchemaType.Name != null)
                                        {
                                            if (_xsdAttributesWithValuesList.Any(a => a.AttributeType == atrybut.ElementSchemaType.BaseXmlSchemaType.Name && a.Schema == xmlSchema.TargetNamespace))
                                            {
                                                XsdAttributeWithValues xsdAttributeWithValues = _xsdAttributesWithValuesList.First(a => a.AttributeType == atrybut.ElementSchemaType.BaseXmlSchemaType.Name && a.Schema == xmlSchema.TargetNamespace);

                                                xsdAttributeWithValues.AttributeName = atrybut.Name;

                                                xsdFeature.AttributesList.Add(xsdAttributeWithValues);
                                            }
                                        }
                                        else
                                        {
                                            if (_xsdAttributesWithValuesList.Any(a => a.AttributeType == atrybut.ElementSchemaType.TypeCode.ToString() && a.Schema == xmlSchema.TargetNamespace))
                                            {
                                                XsdAttributeWithValues xsdAttributeWithValues = _xsdAttributesWithValuesList.First(a => a.AttributeType == atrybut.ElementSchemaType.TypeCode.ToString() && a.Schema == xmlSchema.TargetNamespace);

                                                xsdAttributeWithValues.AttributeName = atrybut.Name;

                                                xsdFeature.AttributesList.Add(xsdAttributeWithValues);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        _xsdFeatures.Add(xsdFeature);
                    }
                }
            }
        }

        public string GetAttributeValues(string schemaName, string featureName, string atrybut)
        {
            if (_xsdFeatures.Any(feature => feature.Schema == schemaName && feature.FeatureName == featureName))
            {
                XsdFeature xsdFeature = _xsdFeatures.First(feature => feature.Schema == schemaName && feature.FeatureName == featureName);

                if (xsdFeature.AttributesList.Any(atr => atr.AttributeName == atrybut))
                {
                    XsdAttributeWithValues attribs = xsdFeature.AttributesList.First(atr => atr.AttributeName == atrybut);

                    return "[" + attribs.ValuesList.Aggregate(string.Empty, (current, attrib) => current + attrib + ", ").TrimEnd(',', ' ') + "]";
                }
            }
            
            return string.Empty;
        }

        public XmlSchemaSet GetSchema()
        {
            return _gmlSchemaSet;
        }

        public static void XSDUpdate()
        {
            List<string> xsdList = new List<string>
            {
                "BDOT_2015", "BDOT_2021", "EGiB_2015", "EGiB_2021", "GESUT_2015", "GESUT_2021", "PODSTAWOWY_2015", "OSNOWA"
            };

            DirectoryInfo[] directories = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd")).GetDirectories();

            foreach (DirectoryInfo directory in directories)
            {
                if (!xsdList.Contains(directory.Name))
                {
                    Directory.Delete(directory.FullName, true);
                }   
            }
 
            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "PODSTAWOWY_2015", "BT_ModelPodstawowy.xsd"));
            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "BDOT_2015", "BDOT500.xsd"));
            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "EGiB_2015", "EGB_OgolnyObiekt.xsd"));
            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "GESUT_2015", "GESUT.xsd"));

            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "BDOT_2021", "BDOT500.xsd"));
            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "EGiB_2021", "EGIB.xsd"));
            DownloadXsdImports(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xsd", "GESUT_2021", "GESUT.xsd"));
        }

        private static void DownloadXsdImports(string fileName)
        {
            XmlSchemaSet gmlSchemaSet = new XmlSchemaSet
            {
                XmlResolver = new XsdUrlResolverDownload()
            };

            gmlSchemaSet.Add(null, fileName);
        }
    }
}
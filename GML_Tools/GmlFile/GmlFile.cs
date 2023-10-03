using GML_Tools.xsd;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace GML_Tools.GmlFile;

public class GmlFile
{
    private int lineCounter = 0;
    private int linesCount = 0;
    
    private readonly string _fileName;

    private readonly XsdFile _xsdFile;

    private readonly List<FeatureMember> _features = new();

    private readonly List<ErrorInfo> _errors = new();

    public GmlFile(string fileName, XsdFile xsdFile)
    {
        _fileName = fileName;
        _xsdFile = xsdFile;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public void LoadGml()
    {
        using XmlReader xmlReader = XmlReader.Create(_fileName);

        xmlReader.MoveToContent();

        while (!xmlReader.EOF)
        {
            xmlReader.ReadToFollowing("gml:featureMember");

            if (xmlReader.EOF) break; 

            FeatureMember featureMember = new()
            {
                StartLine = ((IXmlLineInfo)xmlReader).LineNumber
            };

            string xml = xmlReader.ReadInnerXml();

            featureMember.EndLine = ((IXmlLineInfo)xmlReader).LineNumber;
                    
            XmlDocument doc = new();
            doc.LoadXml(xml);

            XmlNamespaceManager xnmgr = GetAllNamespaces(doc);

            XmlElement xmlElement = doc.DocumentElement;

            if (xmlElement != null)
            {
                featureMember.LocalName = xmlElement.LocalName;
                featureMember.GmlId = xmlElement.Attributes["gml:id"].Value;

                //XmlNode xmlGeom = xmlElement.SelectSingleNode(xmlElement.Prefix + ":geometria", xnmgr);
                //featureMember.Geometry = new Geom(xmlGeom);
                //featureMember.Geometry = new Geom();
            }

            _features.Add(featureMember);
        }
    }

    private static XmlNamespaceManager GetAllNamespaces(XmlDocument xDoc)
    {
        XmlNamespaceManager result = new(xDoc.NameTable);

        XPathNavigator xNav = xDoc.CreateNavigator();

        while (xNav != null && xNav.MoveToFollowing(XPathNodeType.Element))
        {
            IDictionary<string, string> localNamespaces = xNav.GetNamespacesInScope(XmlNamespaceScope.Local);

            if (localNamespaces != null)
            {                    
                foreach (KeyValuePair<string, string> localNamespace in localNamespaces)
                {
                    string prefix = localNamespace.Key;

                    if (string.IsNullOrEmpty(prefix)) prefix = "DEFAULT";

                    result.AddNamespace(prefix, localNamespace.Value);
                }
            }
        }

        return result;
    }

    public List<ErrorInfo> ValidateSchemaErrors()
    {
        XmlReaderSettings gmlReaderSettings = new()
        {
            IgnoreWhitespace = true,
            IgnoreComments = true,
        };

        using (XmlReader gmlReader = XmlReader.Create(_fileName, gmlReaderSettings))
        {
            while (gmlReader.Read())
            {
                linesCount++;
            }
        }

        gmlReaderSettings = new XmlReaderSettings
        {
            IgnoreWhitespace = true,
            IgnoreComments = true,
            ValidationType = ValidationType.Schema,
            Schemas = _xsdFile.GetSchema(),
            ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                              XmlSchemaValidationFlags.ProcessInlineSchema |
                              XmlSchemaValidationFlags.ReportValidationWarnings
        };

        gmlReaderSettings.ValidationEventHandler += GmlReaderSettingsOnValidationEventHandler;

        using (XmlReader gmlReader = XmlReader.Create(_fileName, gmlReaderSettings))
        {
            while (gmlReader.Read())
            {
                lineCounter++;
            }
        }

        return _errors;
    }

    public string GetWalidacjaStatus()
    {
        return $"Walidacja pliku... [{lineCounter} / {linesCount} - {Math.Round((double)lineCounter / linesCount * 100, 2)} %]";
    }


    private void GmlReaderSettingsOnValidationEventHandler(object sender, ValidationEventArgs e)
    {
        Dictionary<string, string> replacements = new()
        {
            {"http://www.w3.org/2001/XMLSchema:", ""},
            {"http://www.opengis.net/gml/3.2:", ""},
                
            {"urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", "modelPodstawowy"},
            {"urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych500:1.0", "BDOT500"},
            {"urn:gugik:specyfikacje:gmlas:ewidencjaGruntowBudynkow:1.0", "EGB"},
            {"urn:gugik:specyfikacje:gmlas:geodezyjnaEwidencjaSieciUzbrojeniaTerenu:1.0", "GESUT"},
                
            {"bazaDanychObiektowTopograficznych500:1.0", "BDOT500"},
            {"ewidencjaGruntowIBudynkow:1.0", "EGB" },
            {"geodezyjnaEwidencjaSieciUzbrojeniaTerenu:1.0", "GESUT"},
        };

        int lineNumber = e.Exception.LineNumber;

        FeatureMember featureMember = _features.First(f => lineNumber > f.StartLine && lineNumber < f.EndLine);

        ErrorInfo errorInfo = new()
        {
            ErrorCounter = _errors.Count + 1,
            FeatureMember = featureMember.LocalName,
            FeatureMemberId = featureMember.GmlId,
            LokalnyId = featureMember.GmlId != null ? featureMember.GmlId.Split('_')[1] : string.Empty,
            Element = ((XmlReader) sender).LocalName,
            Line = e.Exception.LineNumber,
            ErrorType = e.Severity.ToString(),
            LongInfo = replacements.Aggregate(e.Exception.Message, (current, replacement) => current.Replace(replacement.Key, replacement.Value))
        };

        string attributeValues = _xsdFile.GetAttributeValues( ((XmlReader) sender).NamespaceURI , errorInfo.FeatureMember, errorInfo.Element);

        if (e.Exception.InnerException != null)
        {
            if (string.IsNullOrEmpty(attributeValues))
            {
                errorInfo.ShortInfo = e.Exception.InnerException.Message;
            }
            else
            {
                errorInfo.ShortInfo = e.Exception.InnerException.Message + " " + attributeValues;
            }
        }
        else
        {
            errorInfo.ShortInfo = string.Empty;
        }

        _errors.Add(errorInfo);
    }

    public void GetShp()
    {
        
    }
}
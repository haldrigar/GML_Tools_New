using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SqlServer.Types;

namespace GML_Tools.GmlFile
{
    public class Geom
    {
        private readonly SqlGeometry _sqlGeom;

        private readonly string _voidValue;

        public Geom()
        {
            _sqlGeom = SqlGeometry.Null;
            _voidValue = string.Empty;
        }

        public Geom(XmlNode featureChild)
        {
            if (featureChild != null)
            {
                // pobierz wszystkie węzły dla geometrii
                XmlNodeList gmlNodes = featureChild.SelectNodes(".//*");

                // numer układu odniesienia dla geometrii
                int srid = 0;

                // jeśli XML zawiera informację o geometrii
                if (gmlNodes is { Count: > 0 })
                {
                    // pobranie srid geometrii a następnie
                    // usunięcie wszystkich atrybutów ze względu na specyfikę SQL Serwera
                    foreach (XmlElement geomNode in gmlNodes)
                    {
                        if (geomNode.Attributes["srsName"] != null && srid == 0)
                        {
                            string srsName = geomNode.Attributes["srsName"].Value;

                            srsName = srsName.Replace("urn:ogc:def:crs:EPSG::", string.Empty);

                            srid = int.Parse(srsName);
                        }

                        geomNode.Attributes.RemoveAll();
                    }

                    // pobranie wartości geometrii oraz zmiana przestrzeni ze względu na specyfikę SQL Serwera
                    string xmlString = featureChild.InnerXml.Replace("http://www.opengis.net/gml/3.2", "http://www.opengis.net/gml");

                    // sprawdzenie czy jest to Curve. Jeśli tak to zamiana na LineString
                    if (featureChild.FirstChild.Name == "gml:Curve")
                    {
                        xmlString = xmlString.Replace("gml:Curve", "gml:LineString");
                        xmlString = xmlString.Replace("/gml:Curve", "/gml:LineString");
                        xmlString = xmlString.Replace("<gml:segments>", "");
                        xmlString = xmlString.Replace("</gml:segments>", "");
                        xmlString = xmlString.Replace("<gml:LineStringSegment>", "");
                        xmlString = xmlString.Replace("</gml:LineStringSegment>", "");
                    }

                    //if (featureChild.FirstChild.Name == "gml:MultiCurve")
                    //{
                    //    XNamespace gml = "http://www.opengis.net/gml";
                        
                    //    XDocument xDoc = XDocument.Parse(xmlString);
                        
                    //    XElement[] multiCurves = xDoc.Descendants(gml + "MultiCurve").ToArray();
                        
                    //    foreach (XElement mc in multiCurves)
                    //    {
                    //        mc.Name = gml + "MultiLineString";
                            
                    //        //variant 1: curveMembers(1)/LineString(n) -> lineStringMember(n)/LineString(1)
                    //        if (mc.Element(gml + "curveMembers") is XElement linesRoot) 
                    //        {
                    //            var lines = linesRoot.Elements(gml + "LineString").ToList();
                    //            linesRoot.Remove();
                    //            lines.ForEach(line => mc.Add(new XElement(gml + "lineStringMember", line)));
                    //        }
                    //        else //variant 2: curveMember(n)/LineString(1) -> lineStringMember(n)/LineString(1)
                    //        {
                    //            var members = mc.Elements(gml + "curveMember").ToList();
                    //            members.ForEach(m => m.Name = gml + "lineStringMember");
                    //        }
                    //    }
                        
                    //}


                    using MemoryStream stream = new();

                    using StreamWriter writer = new(stream) { AutoFlush = true };

                    writer.Write(xmlString);

                    SqlXml xmlSql = new(stream);

                    _sqlGeom = xmlSql.IsNull ? SqlGeometry.Null : SqlGeometry.GeomFromGml(xmlSql, srid);
                }
                else
                {
                    _sqlGeom = SqlGeometry.Null;

                    _voidValue = featureChild.Attributes?["nilReason"]?.Value;
                }
            }
            else
            {
                _sqlGeom = SqlGeometry.Null;
                _voidValue = string.Empty;
            }
        }

        public bool Istnienie()
        {
            return !_sqlGeom.IsNull;
        }

        public string GetVoidValue()
        {
            return _voidValue;
        }

        public string GetWkt()
        {
            return _sqlGeom.IsNull ? string.Empty : new string(_sqlGeom.STAsText().Value);
        }

        public List<Point> GetPunkty()
        {
            if (_sqlGeom.IsNull)
            {
                return new List<Point>();
            }

            List<Point> punkty = new();

            // liczba punktów geometrii
            int liczbaPunktowBudynku = _sqlGeom.STNumPoints().Value;
            
            for (int i = 1; i <= liczbaPunktowBudynku; i++)
            {
                Point punkt = new()
                {
                    X = _sqlGeom.STPointN(i).STY.Value,
                    Y = _sqlGeom.STPointN(i).STX.Value,
                };
                
                punkty.Add(punkt);
            }

            return punkty;
        }

        public Point GetPunktWewnatrz()
        {
            SqlGeometry point = _sqlGeom.STCentroid();

            return _sqlGeom.STContains(point) ? new Point(point.STY.Value, point.STX.Value) : new Point(_sqlGeom.STPointOnSurface().STY.Value, _sqlGeom.STPointOnSurface().STX.Value);
        }
    }
}

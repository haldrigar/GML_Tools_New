using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace OsnowaRead
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string fileName = args[0];

            // utowrzenie obiektu dokumentu XML
            XmlDocument doc = new()
            {
                PreserveWhitespace = false
            };
            
            try // wczytanie pliku GML
            {
                doc.Load(fileName);
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);

                return;
            }

            XmlNamespaceManager xnmgr = GetAllNamespaces(doc);

            // pobierz listę wszystkich obiektów "featureMember" z pliku
            XmlNodeList featureCollection = doc.GetElementsByTagName("gml:featureMember");

            int liczbaWszystkichObiektow = 0; // ilość wszystkich obiektów w pliku

            List<PunktOsnowyWysokosciowej> punktyOsnowyWysokosciowej = new();

            foreach (XmlNode featureMember in featureCollection)
            {
                liczbaWszystkichObiektow++;

                if (featureMember.HasChildNodes)
                {
                    XmlNode feature = featureMember.FirstChild;

                    switch (feature.Name)
                    {
                        case "os:OS_PunktOsnowyWysokosciowej":
                        {
                            PunktOsnowyWysokosciowej punktOsnowy = new()
                            {
                                LocalName = feature.LocalName,
                                GmlId = feature.Attributes?["gml:id"].Value,
                                Zakres = feature.Attributes?["os:zakres"].Value,
                            };

                            foreach (XmlNode featureChild in feature.ChildNodes)
                            {
                                switch (featureChild.Name)
                                {
                                    case "idIIP":
                                    {
                                        punktOsnowy.LokalnyId = featureChild.SelectSingleNode(".//bt:BT_Identyfikator/bt:lokalnyId", xnmgr)?.InnerText;
                                        punktOsnowy.PrzestrzenNazw = featureChild.SelectSingleNode(".//bt:BT_Identyfikator/bt:przestrzenNazw", xnmgr)?.InnerText;
                                        punktOsnowy.WersjaId = featureChild.SelectSingleNode(".//bt:BT_Identyfikator/bt:wersjaId", xnmgr)?.InnerText;
                                        break;
                                    }

                                    case "nrPkt":
                                    {
                                        switch (featureChild.Attributes?["przestrzen_nazw"].Value)
                                        {
                                            case "IDPUNKTU":
                                            {
                                                punktOsnowy.NrPktIdPunktu = featureChild.InnerText;
                                                break;
                                            }

                                            case "IDIIP":
                                            {
                                                punktOsnowy.NrPktIdIIP = featureChild.InnerText;
                                                break;
                                            }

                                            case "STARYID":
                                            {
                                                punktOsnowy.NrPktStaryId = featureChild.InnerText;
                                                break;
                                            }

                                            default:
                                            {
                                                throw new Exception("Błąd w nrPkt!");
                                            }
                                        }

                                        break;
                                    }

                                    case "idD":
                                    {
                                        break;
                                    }

                                    case "nazwaPkt":
                                    {
                                        punktOsnowy.NazwaPkt = featureChild.InnerText;
                                        break;
                                    }

                                    case "dataOstatniejAkt":
                                    {
                                        punktOsnowy.DataOstatniejAkt = featureChild.InnerText;
                                        break;
                                    }

                                    case "nrGlow":
                                    {
                                        punktOsnowy.NrGlow = featureChild.InnerText;
                                        break;
                                    }

                                    case "stanPkt":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetStanPkt(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w stanPkt");
                                        }

                                        break;
                                    }

                                    case "typPkt":
                                    {
                                        punktOsnowy.TypPkt = featureChild.InnerText;
                                        break;
                                    }

                                    case "typStab":
                                    {
                                        punktOsnowy.TypStab = featureChild.InnerText;
                                        break;
                                    }

                                    case "typZab":
                                    {
                                        punktOsnowy.TypZab = featureChild.InnerText;
                                        break;
                                    }

                                    case "rodzajPkt":
                                    {
                                        punktOsnowy.RodzajPkt = featureChild.InnerText;
                                        break;
                                    }

                                    case "klasaOsn":
                                    {
                                        punktOsnowy.KlasaOsn = featureChild.InnerText;
                                        break;
                                    }

                                    case "geometria":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetGeometria(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w stanPkt");
                                        }

                                        break;
                                    }

                                    case "metFiLa":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetMetFiLa(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w metFiLa");
                                        }

                                        break;
                                    }

                                    case "ukladGeod":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetUkladGeod(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w ukladGeod");
                                        }

                                        break;
                                    }

                                    case "ukladGeodGRS":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetUkladGeodGRS(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w ukladGeodGRS");
                                        }

                                        break;
                                    }

                                    case "rodzajWsp":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetRodzajWsp(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w rodzajWsp");
                                        }

                                        break;
                                    }

                                    case "strefa":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetStrefa(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w strefa");
                                        }

                                        break;
                                    }

                                    case "mP":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetMP(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w mP");
                                        }

                                        break;
                                    }

                                    case "celh":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetCelh(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w celh");
                                        }

                                        break;
                                    }

                                    case "typPom":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetTypPom(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w typPom");
                                        }

                                        break;
                                    }

                                    case "wysPkt":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetWysPkt(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w wysPkt");
                                        }

                                        break;
                                    }

                                    case "typH":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetTypH(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w typH");
                                        }

                                        break;
                                    }

                                    case "bladH":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetBladH(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w bladH");
                                        }

                                        break;
                                    }

                                    case "uH":
                                    {
                                        if (featureChild.Attributes != null)
                                        {
                                            int occurrence = int.Parse(featureChild.Attributes["os:occurrence"].Value);
                                            string wartosc = featureChild.InnerText;

                                            punktOsnowy.SetUH(occurrence, wartosc);
                                        }
                                        else
                                        {
                                            throw new Exception("Błąd w uH");
                                        }

                                        break;
                                    }

                                    case "cyklZycia":
                                    {
                                        punktOsnowy.CyklZycia = featureChild.InnerText;
                                        break;
                                    }

                                    case "centr":
                                    {
                                        punktOsnowy.Centr = featureChild.Attributes?["xlink:href"].Value;
                                        break;
                                    }

                                    case "idwpktgeos":
                                    {
                                        punktOsnowy.Idwpktgeos = featureChild.InnerText;
                                        break;
                                    }

                                    case "bazageos":
                                    {
                                        punktOsnowy.Bazageos = featureChild.InnerText;
                                        break;
                                    }
                                        
                                    default:
                                    {
                                        throw new Exception($"Brak obsługi atrybutu: {featureChild.Name}");
                                    }
                                }
                            }

                            punktyOsnowyWysokosciowej.Add(punktOsnowy);

                            break;
                        }

                        default:
                        {
                            throw new Exception("Nieznany obiekt!");
                        }
                    }
                }
            }

            Console.WriteLine($"Liczba obiektów w pliku: {liczbaWszystkichObiektow}");

            string outputFile = Path.Combine(Path.GetDirectoryName(fileName) ?? throw new InvalidOperationException(), Path.GetFileNameWithoutExtension(fileName) + ".xlsx"); 

            FileInfo xlsFile = new(outputFile);
            if (xlsFile.Exists) xlsFile.Delete(); 

            ExcelPackage xlsWorkbook = new(xlsFile);

            xlsWorkbook.Workbook.Properties.Title = "Punkty osnowy z pliku GML";
            xlsWorkbook.Workbook.Properties.Author = "Grzegorz Gogolewski";
            xlsWorkbook.Workbook.Properties.Comments = "Punkty osnowy z pliku GML";
            xlsWorkbook.Workbook.Properties.Company = "GISNET";

            ExcelWorksheet xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("OSNOWA");

            xlsSheet.Cells[1, 1].LoadFromCollection(punktyOsnowyWysokosciowej, true, TableStyles.Medium2);

            xlsSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            xlsSheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            xlsSheet.View.FreezePanes(2, 1);
            xlsSheet.Cells.Style.Font.Size = 10;

            //xlsSheet.Cells["A1:I" + xlsSheet.Dimension.End.Row].AutoFilter = true;

            xlsWorkbook.Save();

            Console.WriteLine("Koniec!");

            //Console.ReadKey(true);
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
    }

}

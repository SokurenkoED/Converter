using Converter__from_xml_to_dat_.Files.Hstr.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Hstr.Functions
{
    static class ReadParamsFromFile
    {

        public static void ReadFile(XDocument xdoc,ref List<Structure> Structures)
        {
            SetStructures(xdoc, ref Structures);
        }

        private static void SetStructures(XDocument xdoc, ref List<Structure> Structures)
        {
            foreach (XElement Strctrs in xdoc.Element("JTHS").Elements("HSTR_NAME"))
            {
                Structure Str = null;

                if (Strctrs.Element("HSTR_LEFTTYPE") == null)
                {
                    Str = SetParamsToHeatSource(xdoc, Strctrs);
                }
                else
                {
                    Str = SetParamsToTempJun(xdoc, Strctrs);
                }
                Structures.Add(Str);
            }
        }

        private static Structure SetParamsToHeatSource(XDocument xdoc, XElement Strctrs)
        {
            HeatSource HS = new HeatSource();

            foreach (var item in Strctrs.Descendants("HSTR_PROP"))
            {
                XAttribute Attr = item.Attribute("Numb");
                HS.Number = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_LEFTNAME"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_LEFTNAME = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_RIGHTNAME"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_RIGHTNAME = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_RIGHTTYPE"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_RIGHTTYPE = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JHHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_JHHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JHSTIP"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_JHSTIP = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_ALHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_ALHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_QADHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_QADHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_ASU"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_ASU = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_HBOTHSR"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_HBOTHSR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_HTOPHSR"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_HTOPHSR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JDIRHSR"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_JDIRHSR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_CM"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_CM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_RM"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_RM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_ALMD"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_ALMD = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_DLHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_DLHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_DRHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_DRHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JTRHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                HS.HSTR_JTRHS = Attr.Value;
            }

            return HS;
        }

        private static TempJun SetParamsToTempJun(XDocument xdoc, XElement Strctrs)
        {
            TempJun TJ = new TempJun();

            foreach (var item in Strctrs.Descendants("HSTR_LEFTNAME"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_LEFTNAME = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_LEFTTYPE"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_LEFTTYPE = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_RIGHTNAME"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_RIGHTNAME = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_RIGHTTYPE"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_RIGHTTYPE = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_PROP"))
            {
                XAttribute Attr = item.Attribute("Numb");
                TJ.Number = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JHHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JHHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JHSTIP"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JHSTIP = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_ALHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_ALHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_HBOTHSL"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_HBOTHSL = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_HTOPHSL"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_HTOPHSL = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JDIRHSL"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JDIRHSL = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_HBOTHSR"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_HBOTHSR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_HTOPHSR"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_HTOPHSR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JDIRHSR"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JDIRHSR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_J1RUL"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_J1RUL = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JADDL"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JADDL = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_J1RUR"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_J1RUR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JADDR"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JADDR = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_CM"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_CM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_RM"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_RM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_ALMD"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_ALMD = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_DLHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_DLHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_DRHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_DRHS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("HSTR_JTRHS"))
            {
                XAttribute Attr = item.Attribute("Value");
                TJ.HSTR_JTRHS = Attr.Value;
            }

            return TJ;
        }
    }
}

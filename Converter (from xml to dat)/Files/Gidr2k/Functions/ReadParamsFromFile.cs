using Converter__from_xml_to_dat_.Files.Gidr2k.Junctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Gidr2k.Functions
{
    static class ReadParamsFromFile
    {

        public static void ReadFIle(ref List<Jun> Juns, XDocument xdoc)
        {
            SetJun(ref Juns, xdoc);
        }

        /// <summary>
        /// Создаем массив связей с записью его параметров
        /// </summary>
        /// <param name="Juns"></param>
        private static void SetJun(ref List<Jun> Juns, XDocument xdoc)
        {
            foreach (XElement JunFromFIle in xdoc.Element("JUN_CNT").Elements("JUN_NAME"))
            {
                XAttribute AttrName = JunFromFIle.Attribute("Value");
                Jun jun = null;
                foreach (XElement Tip in JunFromFIle.Descendants("JUN_JJNTIP"))
                {
                    XAttribute AttrType = Tip.Attribute("Value");

                    if (AttrType.Value == "1")
                    {
                        jun = SetParamsToStandart(AttrName.Value, JunFromFIle);

                    }
                    else if (AttrType.Value == "2")
                    {
                        jun = new Turb(AttrName.Value);
                    }
                    jun.Type = AttrType.Value;
                }
                foreach (XElement Num in JunFromFIle.Descendants("JUN_PROP"))
                {
                    XAttribute AttrNum = Num.Attribute("Numb");
                    jun.Number = AttrNum.Value;
                }
                foreach (XElement From in JunFromFIle.Descendants("JUN_FROM"))
                {
                    XAttribute Attr = From.Attribute("Value");
                    jun.JUN_FROM = Attr.Value;
                }
                foreach (XElement To in JunFromFIle.Descendants("JUN_TO"))
                {
                    XAttribute Attr = To.Attribute("Value");
                    jun.JUN_TO = Attr.Value;
                }
                Juns.Add(jun);
            }
        }

        private static Standart SetParamsToStandart(string name, XElement JunFromFIle)
        {
            Standart stdrt = new Standart(name);

            foreach (XElement Param in JunFromFIle.Descendants("JUN_AJNMLT"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_AJNMLT = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JCRFLJ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_JCRFLJ = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VJ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VJ = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_SG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_SG = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_DGG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_DGG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_LG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_LG = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_DZG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_DZG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HJ1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_HJ1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HJ2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_HJ2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_KSIG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_KSIG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_SHRG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_SHRG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_INMG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_INMG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_PUMPDISCR"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_PUMPDISCR = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JPUG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_JPUG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HP0G2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_HP0G2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_MP0G2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_MP0G2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_QP0G2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_QP0G2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_OMP02K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_OMP02K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JWTG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_JWTG2K = Attr.Value;
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_WTG2K_ARG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_WTG2K_ARG.Add(Attr.Value);
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_WTG2K_FRQ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_WTG2K_FRQ.Add(Attr.Value);
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVDISCR"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VLVDISCR = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVNAM"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VLVNAM = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_S0VLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_S0VLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_KSIVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_KSIVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_DGVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_DGVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_LVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_LVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_CVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_CVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVA1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VLVA1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVA2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VLVA2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JVTBL"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_JVTBL = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_KCI2KJ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_KCI2KJ = Attr.Value;
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVTBL_ARG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VLVTBL_ARG.Add(Attr.Value);
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVTBL_S"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_VLVTBL_S.Add(Attr.Value);
            }
            return stdrt;
        }

        
    }
}

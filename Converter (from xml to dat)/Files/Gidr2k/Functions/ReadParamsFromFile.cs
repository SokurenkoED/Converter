using Converter__from_xml_to_dat_.Files.Gidr2k.HomolChar;
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

        public static void ReadFIle(ref List<Jun> Juns,ref List<Homol> Homols,ref List<string> LastParams, XDocument xdoc)
        {
            SetJun(ref Juns, xdoc);
            SetHomols(ref Homols, xdoc);
            SetLastTwoParams(ref LastParams, xdoc);
        }

        private static void SetLastTwoParams(ref List<string> LastParams, XDocument xdoc)
        {
            foreach (XElement item in xdoc.Element("JUN_CNT").Elements("JUN_AMUG2K"))
            {
                XAttribute Attr = item.Attribute("Value");
                LastParams.Add(Attr.Value);
            }
            foreach (XElement item in xdoc.Element("JUN_CNT").Elements("JUN_EPUG2K"))
            {
                XAttribute Attr = item.Attribute("Value");
                LastParams.Add(Attr.Value);
            }
        }

        private static void SetHomols(ref List<Homol> Homols, XDocument xdoc)
        {
            foreach (XElement HC in xdoc.Element("JUN_CNT").Element("JUN_HC_GENERAL").Elements("JUN_HC_N"))
            {
                Homol hom = new Homol();
                foreach (var item in HC.Element("JUN_JHG2K").Elements("JUN_SQHCC1").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQHCC1.Add(Attr.Value);
                }
                foreach (var item in HC.Element("JUN_JHG2K").Elements("JUN_SQHCC2").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQHCC2.Add(Attr.Value);
                }
                foreach (var item in HC.Element("JUN_JHG2K").Elements("JUN_SQHCC3").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQHCC3.Add(Attr.Value);
                }
                foreach (var item in HC.Element("JUN_JHG2K").Elements("JUN_SQHCC4").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQHCC4.Add(Attr.Value);
                }

                foreach (var item in HC.Element("JUN_JMG2K").Elements("JUN_SQMCC1").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQMCC1.Add(Attr.Value);
                }
                foreach (var item in HC.Element("JUN_JMG2K").Elements("JUN_SQMCC2").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQMCC2.Add(Attr.Value);
                }
                foreach (var item in HC.Element("JUN_JMG2K").Elements("JUN_SQMCC3").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQMCC3.Add(Attr.Value);
                }
                foreach (var item in HC.Element("JUN_JMG2K").Elements("JUN_SQMCC4").Descendants())
                {
                    XAttribute Attr = item.Attribute("Value");
                    hom.JUN_SQMCC4.Add(Attr.Value);
                }
                Homols.Add(hom);
            }
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
                        jun = SetParamsToStandart(AttrName.Value, JunFromFIle); // В отличии от стандартного соединения - нет клапанов и насосов
                    }
                    else if (AttrType.Value == "0")
                    {
                        jun = SetParamsToDep(AttrName.Value, JunFromFIle);
                    }
                    else if (AttrType.Value == "3")
                    {
                        jun = SetParamsToGas(AttrName.Value, JunFromFIle);
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

        private static Gas SetParamsToGas(string name, XElement JunFromFIle)
        {
            Gas gas = new Gas(name);

            foreach (XElement Param in JunFromFIle.Descendants("JUN_AJNMLT"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_AJNMLT = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VJ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VJ = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_SG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_SG = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_DGG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_DGG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_LG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_LG = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_DZG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_DZG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HJ1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_HJ1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HJ2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_HJ2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_V0KDI1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_V0KDI1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_V1KDI1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_V1KDI1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_V0KDI2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_V0KDI2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_V1KDI2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_V1KDI2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_KSIG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_KSIG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_SHRG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_SHRG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_INMG2K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_INMG2K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVDISCR"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VLVDISCR = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVNAM"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VLVNAM = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_S0VLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_S0VLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_KSIVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_KSIVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_DGVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_DGVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_LVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_LVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_CVLV"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_CVLV = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVA1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VLVA1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVA2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VLVA2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JVTBL"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_JVTBL = Attr.Value;
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVTBL_ARG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VLVTBL_ARG.Add(Attr.Value);
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_VLVTBL_S"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_VLVTBL_S.Add(Attr.Value);
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_KCI2KJ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                gas.JUN_KCI2KJ = Attr.Value;
            }

            return gas;
        }

        private static Dep SetParamsToDep(string name, XElement JunFromFIle)
        {
            Dep dep = new Dep(name);

            foreach (XElement Param in JunFromFIle.Descendants("JUN_AJNMLT"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_AJNMLT = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HJ1"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_HJ1 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_HJ2"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_HJ2 = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JJNPAR"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_JJNPAR = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JJNT"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_JJNT = Attr.Value;
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_KC2KT_ARG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_KC2KT_ARG.Add(Attr.Value);
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_KC2KT"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_KC2KT.Add(Attr.Value);
            }

            foreach (XElement Param in JunFromFIle.Descendants("JUN_KCI2KJ"))
            {
                XAttribute Attr = Param.Attribute("Value");
                dep.JUN_KCI2KJ = Attr.Value;
            }

            return dep;
        }

        private static Standart SetParamsToStandart(string name, XElement JunFromFIle)
        {
            Standart stdrt = new Standart(name);

            foreach (XElement Param in JunFromFIle.Descendants("JUN_DP02K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_DP02K = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_AJNMLT"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_AJNMLT = Attr.Value;
            }
            foreach (XElement Param in JunFromFIle.Descendants("JUN_JOBR"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_JOBR = Attr.Value;
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
            foreach (XElement Param in JunFromFIle.Descendants("JUN_GAM02K"))
            {
                XAttribute Attr = Param.Attribute("Value");
                stdrt.JUN_GAM02K = Attr.Value;
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

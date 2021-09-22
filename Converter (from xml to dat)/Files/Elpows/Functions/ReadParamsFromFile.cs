using Converter__from_xml_to_dat_.Files.Elpows.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Elpows.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref List<Elem> Elems)
        {
            SetShaft(xdoc, ref Elems);

            SetTurb(xdoc, ref Elems);

            SetElg(xdoc, ref Elems);

            SetNet(xdoc, ref Elems);

            SetElm(xdoc, ref Elems);

            SetPump(xdoc, ref Elems);



        }

        private static void SetShaft(XDocument xdoc, ref List<Elem> Elems)
        {
            foreach (XElement Elms in xdoc.Element("EQP_DATA").Element("SHAFT_CNT").Elements("SHAFT_NAME"))
            {
                Shaft shaft = new Shaft();

                shaft.Name = Elms.Attribute("Value").Value;

                foreach (var item in Elms.Descendants("SHAFT_PROP"))
                {
                    shaft.Number = item.Attribute("SHAFT_NUM").Value;
                }

                foreach (var item in Elms.Descendants("SHAFT_OMTUR"))
                {
                    shaft.SHAFT_OMTUR = item.Attribute("Value").Value;
                }

                Elems.Add(shaft);
            }
        }

        private static void SetTurb(XDocument xdoc, ref List<Elem> Elems)
        {
            foreach (XElement Elms in xdoc.Element("EQP_DATA").Element("TURB_CNT").Elements("TURB_NAME"))
            {
                Turb TB = new Turb();

                TB.Name = Elms.Attribute("Value").Value;

                foreach (var item in Elms.Descendants("TURB_PROP"))
                {
                    TB.Number = item.Attribute("TURB_NUM").Value;
                    TB.Description = item.Attribute("Description").Value;
                }
                foreach (var item in Elms.Descendants("TURB_SHAFTNAME"))
                {
                    TB.TURB_SHAFTNAME = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_SHAFTNUM"))
                {
                    TB.TURB_SHAFTNUM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_MJTUR"))
                {
                    TB.TURB_MJTUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_MDIS1"))
                {
                    TB.TURB_MDIS1 = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_MDIS2"))
                {
                    TB.TURB_MDIS2 = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_MDIS3"))
                {
                    TB.TURB_MDIS3 = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_RETUR"))
                {
                    TB.TURB_RETUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_FITUR"))
                {
                    TB.TURB_FITUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_NUTUR"))
                {
                    TB.TURB_NUTUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_G0TUR"))
                {
                    TB.TURB_G0TUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_GA0TUR"))
                {
                    TB.TURB_GA0TUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_ET0TUR"))
                {
                    TB.TURB_ET0TUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("TURB_OM0TUR"))
                {
                    TB.TURB_OM0TUR = item.Attribute("Value").Value;
                }

                Elems.Add(TB);
            }
        }

        private static void SetElg(XDocument xdoc, ref List<Elem> Elems)
        {
            foreach (XElement Elms in xdoc.Element("EQP_DATA").Element("ELG_CNT").Elements("ELG_NAME"))
            {
                var EG = new Elg();

                EG.Name = Elms.Attribute("Value").Value;

                foreach (var item in Elms.Descendants("ELG_PROP"))
                {
                    EG.Number = item.Attribute("Numb").Value;
                    EG.Description = item.Attribute("Description").Value;
                }
                foreach (var item in Elms.Descendants("ELG_SHAFTNAME"))
                {
                    EG.ELG_SHAFTNAME = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_SHAFTNUM"))
                {
                    EG.ELG_SHAFTNUM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_NETNAME"))
                {
                    EG.ELG_NETNAME = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_NETNUM"))
                {
                    EG.ELG_NETNUM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_ASU"))
                {
                    EG.ELG_ASU = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_MJGEN"))
                {
                    EG.ELG_MJGEN = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_NASIN"))
                {
                    EG.ELG_NASIN = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_TAUALT"))
                {
                    EG.ELG_TAUALT = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_DEDI"))
                {
                    EG.ELG_DEDI = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_IMAXA"))
                {
                    EG.ELG_IMAXA = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_UMAXA"))
                {
                    EG.ELG_UMAXA = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_NNOM"))
                {
                    EG.ELG_NNOM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_FIN"))
                {
                    EG.ELG_FIN = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELG_IALTER"))
                {
                    EG.ELG_IALTER = item.Attribute("Value").Value;
                }

                Elems.Add(EG);
            }
        }

        private static void SetNet(XDocument xdoc, ref List<Elem> Elems)
        {
            foreach (XElement Elms in xdoc.Element("EQP_DATA").Element("NET_CNT").Elements("NET_NAME"))
            {
                var NT = new Net();

                NT.Name = Elms.Attribute("Value").Value;

                foreach (var item in Elms.Descendants("ELG_PROP"))
                {
                    NT.Number = item.Attribute("TURB_NUM").Value;
                    NT.Description = item.Attribute("Description").Value;
                }
                foreach (var item in Elms.Descendants("NET_JSOUR"))
                {
                    NT.NET_JSOUR = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("NET_OMNET"))
                {
                    NT.NET_OMNET = item.Attribute("Value").Value;
                }

                foreach (XElement item in Elms.Descendants("NET_PSOUR_ARG"))
                {
                    NT.NET_PSOUR_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (XElement item in Elms.Descendants("DEP_PVOLT"))
                {
                    NT.NET_PSOUR.Add(item.Attribute("Value").Value);
                }

                Elems.Add(NT);
            }
        }

        private static void SetElm(XDocument xdoc, ref List<Elem> Elems)
        {
            foreach (XElement Elms in xdoc.Element("EQP_DATA").Element("ELM_CNT").Elements("ELM_NAME"))
            {
                var EM = new Elm();

                EM.Name = Elms.Attribute("Value").Value;

                foreach (var item in Elms.Descendants("ELG_PROP"))
                {
                    EM.Number = item.Attribute("TURB_NUM").Value;
                    EM.Description = item.Attribute("Description").Value;
                }
                foreach (var item in Elms.Descendants("ELM_JMAC"))
                {
                    EM.ELM_JMAC = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_NETNAME"))
                {
                    EM.ELM_NETNAME = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_NETNUM"))
                {
                    EM.ELM_NETNUM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_MJMAC"))
                {
                    EM.ELM_MJMAC = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_M1MAC"))
                {
                    EM.ELM_M1MAC = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_M2MAC"))
                {
                    EM.ELM_M2MAC = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_M3MAC"))
                {
                    EM.ELM_M3MAC = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("ELM_JVFMAC"))
                {
                    EM.ELM_JVFMAC = item.Attribute("Value").Value;
                }

                foreach (XElement item in Elms.Descendants("ELM_VFMAC_ARG"))
                {
                    EM.ELM_VFMAC_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (XElement item in Elms.Descendants("ELM_VFMAC"))
                {
                    EM.ELM_VFMAC.Add(item.Attribute("Value").Value);
                }

                Elems.Add(EM);
            }
        }

        private static void SetPump(XDocument xdoc, ref List<Elem> Elems)
        {
            foreach (XElement Elms in xdoc.Element("EQP_DATA").Element("PUMP_CNT").Elements("JUN_PUMPNUM"))
            {
                var PMP = new Pump();

                PMP.Number = Elms.Attribute("Value").Value;

                foreach (var item in Elms.Descendants("PUMP_TUREM"))
                {
                    PMP.PUMP_TUREM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("PUMP_ELMNAME"))
                {
                    PMP.PUMP_ELMNAME = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("PUMP_ELMNUM"))
                {
                    PMP.PUMP_ELMNUM = item.Attribute("Value").Value;
                }
                foreach (var item in Elms.Descendants("PUMP_MJPUMP"))
                {
                    PMP.PUMP_MJPUMP = item.Attribute("Value").Value;
                }

                Elems.Add(PMP);
            }
        }

    }
}

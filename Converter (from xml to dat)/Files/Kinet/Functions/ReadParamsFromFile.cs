using Converter__from_xml_to_dat_.Files.Kinet.Elems;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Kinet.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref List<CrodsData> CDs, ref GeneralData GD, ref ReaceffData RD)
        {
            ReadParamsFormGeneralData(xdoc, ref GD);
            ReadParamsFormCrodsData(xdoc, ref CDs);
            ReadParamsFormReaceffData(xdoc, ref RD);
        }

        private static void ReadParamsFormGeneralData(XDocument xdoc, ref GeneralData GD)
        {
            foreach (XElement Data in xdoc.Element("KIN_DATA").Elements("GENERAL_DATA"))
            {
                foreach (var item in Data.Descendants("KIN_NN"))
                {
                    GD.KIN_NN = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_S0"))
                {
                    GD.KIN_S0 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_PNL"))
                {
                    GD.KIN_PNL = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN_LM"))
                {
                    GD.KIN_LM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_BE"))
                {
                    GD.KIN_BE.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_BGAM"))
                {
                    GD.KIN_BGAM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_BLAM"))
                {
                    GD.KIN_BLAM.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_POWFIS"))
                {
                    GD.KIN_POWFIS = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN_NETJOB_ARG"))
                {
                    GD.KIN_NETJOB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_NETJOB"))
                {
                    GD.KIN_NETJOB.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_TOST"))
                {
                    GD.KIN_TOST = item.Attribute("Value").Value;
                }
            }
        }

        private static void ReadParamsFormCrodsData(XDocument xdoc, ref List<CrodsData> CDs)
        {
            foreach (XElement Data in xdoc.Element("KIN_DATA").Element("CRODS_DATA").Element("KIN_JGRUP").Elements("KIN_JGRUP_N"))
            {
                CrodsData CD = new CrodsData();
                foreach (var item in Data.Descendants("KIN_ASUOR"))
                {
                    CD.KIN_ASUOR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_ASUHRO0"))
                {
                    CD.KIN_ASUHRO0 = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN_DKGRUP_ARG"))
                {
                    CD.KIN_DKGRUP_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_DKGRUP"))
                {
                    CD.KIN_DKGRUP.Add(item.Attribute("Value").Value);
                }

                CDs.Add(CD);
            }
        }

        private static void ReadParamsFormReaceffData(XDocument xdoc, ref ReaceffData RD)
        {
            foreach (XElement Data in xdoc.Element("KIN_DATA").Elements("REACEFF_DATA"))
            {
                foreach (var item in Data.Descendants("KIN_STEPFT"))
                {
                    RD.KIN_STEPFT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_STEPHT"))
                {
                    RD.KIN_STEPHT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_STEPHG"))
                {
                    RD.KIN_STEPHG = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_ALFFT"))
                {
                    RD.KIN_ALFFT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_TFT0"))
                {
                    RD.KIN_TFT0 = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN_ARHT_ARG"))
                {
                    RD.KIN_ARHT_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_ARHT"))
                {
                    RD.KIN_ARHT.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_ARHTM_ARG"))
                {
                    RD.KIN_ARHTM_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_ARHTM"))
                {
                    RD.KIN_ARHTM.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_ARHG_ARG"))
                {
                    RD.KIN_ARHG_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_ARHG"))
                {
                    RD.KIN_ARHG.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_ARHCB_ARG"))
                {
                    RD.KIN_ARHCB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_ARHCB"))
                {
                    RD.KIN_ARHCB.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_DKT_ARG"))
                {
                    RD.KIN_DKT_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_DKT"))
                {
                    RD.KIN_DKT.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN_DRONE0"))
                {
                    RD.KIN_DRONE0 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_DTNOM"))
                {
                    RD.KIN_DTNOM = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN_ALFCR"))
                {
                    RD.KIN_ALFCR = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN_FKTF_ARG"))
                {
                    RD.KIN_FKTF_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_FKTF"))
                {
                    RD.KIN_FKTF.Add(item.Attribute("Value").Value);
                }
            }
        }
    }
}

using Converter__from_xml_to_dat_.Files.Kin_sp.Elems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Kin_sp.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref GENERAL_DATA_SP GD, ref INT_PARAM_SP IP, ref RESIDUAL_DATA_SP RD, ref CRODS_DATA_SP CD)
        {
            ReadParamsFromGeneralData(xdoc, ref GD);

            ReadParamsFromIntParam(xdoc, ref IP);

            ReadParamsFromResidualData(xdoc, ref RD);

            ReadParamsFromCrodsData(xdoc, ref CD);

        }

        private static void ReadParamsFromGeneralData(XDocument xdoc, ref GENERAL_DATA_SP GD)
        {
            foreach (XElement Data in xdoc.Element("KINSP_DATA").Elements("GENERAL_DATA"))
            {
                foreach (var item in Data.Descendants("KIN7_PNKIN"))
                {
                    GD.KIN7_PNKIN = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_SIST"))
                {
                    GD.KIN7_SIST = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_NIST"))
                {
                    GD.KIN7_NIST = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_NKIST"))
                {
                    GD.KIN7_NKIST = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_PNL"))
                {
                    GD.KIN7_PNL = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_JGRKIN"))
                {
                    GD.KIN7_JGRKIN = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN7_LM"))
                {
                    GD.KIN_LM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN7_BE"))
                {
                    GD.KIN_BE.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN7_BETA0"))
                {
                    GD.KIN7_BETA0 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_ALFCR"))
                {
                    GD.KIN7_ALFCR = item.Attribute("Value").Value;
                }
            }
        }

        private static void ReadParamsFromIntParam(XDocument xdoc, ref INT_PARAM_SP IP)
        {
            foreach (XElement Data in xdoc.Element("KINSP_DATA").Elements("INT_PARAM"))
            {
                foreach (var item in Data.Descendants("KIN7_DELT"))
                {
                    IP.KIN7_DELT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_DTMIN"))
                {
                    IP.KIN7_DTMIN = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_DTMAX"))
                {
                    IP.KIN7_DTMAX = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_XD"))
                {
                    IP.KIN7_XD = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_TAUAZ"))
                {
                    IP.KIN7_TAUAZ = item.Attribute("Value").Value;
                }

            }
        }

        private static void ReadParamsFromResidualData(XDocument xdoc, ref RESIDUAL_DATA_SP RD)
        {
            foreach (XElement Data in xdoc.Element("KINSP_DATA").Elements("RESIDUAL_DATA"))
            {
                foreach (var item in Data.Descendants("KIN7_JGROST"))
                {
                    RD.KIN7_JGROST = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN7_BGAM"))
                {
                    RD.KIN7_BGAM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN7_BLAM"))
                {
                    RD.KIN7_BLAM.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("KIN7_POWFIS"))
                {
                    RD.KIN7_POWFIS = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("KIN7_JNJOB"))
                {
                    RD.KIN7_JNJOB = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN7_NETJOB_ARG"))
                {
                    RD.KIN7_NETJOB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN7_NETJOB"))
                {
                    RD.KIN7_NETJOB.Add(item.Attribute("Value").Value);
                }

            }
        }
        
        private static void ReadParamsFromCrodsData(XDocument xdoc, ref CRODS_DATA_SP CD)
        {
            foreach (XElement Data in xdoc.Element("KINSP_DATA").Elements("CRODS_DATA"))
            {
                foreach (var item in Data.Descendants("KIN7_JGRUP"))
                {
                    CD.KIN7_JGRUP = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("KIN7_ASUOR"))
                {
                    CD.KIN7_ASUOR.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN7_ASUHRO0"))
                {
                    CD.KIN7_ASUHRO0.Add(item.Attribute("Value").Value);
                }

            }
        }


    }
}

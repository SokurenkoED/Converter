using Converter__from_xml_to_dat_.Files.Kin_sp.Elems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Kin_sp.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref GENERAL_DATA_SP GD, ref INT_PARAM_SP IP, ref RESIDUAL_DATA_SP RD)
        {
            ReadParamsFromGeneralData(xdoc, ref GD);

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

                foreach (var item in Data.Descendants("KIN_LM"))
                {
                    GD.KIN_LM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("KIN_BE"))
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


    }
}

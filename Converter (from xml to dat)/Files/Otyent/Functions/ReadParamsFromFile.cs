using Converter__from_xml_to_dat_.Files.Otyent.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Otyent.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref OtyElem OTY)
        {
            ReadParamsFormELLs(xdoc, ref OTY);
        }

        private static void ReadParamsFormELLs(XDocument xdoc, ref OtyElem OTY)
        {
            foreach (XElement Data in xdoc.Elements("OTY_DATA"))
            {
                foreach (var item in Data.Descendants("OTY_JOMEOT"))
                {
                    OTY.OTY_JOMEOT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_POPRBO"))
                {
                    OTY.OTY_POPRBO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_SOTY"))
                {
                    OTY.OTY_SOTY = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_DOTY"))
                {
                    OTY.OTY_DOTY = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_AKSI"))
                {
                    OTY.OTY_AKSI = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_SHEROT"))
                {
                    OTY.OTY_SHEROT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_VO"))
                {
                    OTY.OTY_VO.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OTY_HO"))
                {
                    OTY.OTY_HO.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OTY_KSIMO"))
                {
                    OTY.OTY_KSIMO.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OTY_PMOTY"))
                {
                    OTY.OTY_PMOTY = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_CMO"))
                {
                    OTY.OTY_CMO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_RMO"))
                {
                    OTY.OTY_RMO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_DELTMO"))
                {
                    OTY.OTY_DELTMO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_ALMO"))
                {
                    OTY.OTY_ALMO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_JGROTY"))
                {
                    OTY.OTY_JGROTY = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OTY_JMCOUT"))
                {
                    OTY.OTY_JMCOUT.Add(item.Attribute("Value").Value);
                }

            }
        }
    }
}

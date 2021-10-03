using Converter__from_xml_to_dat_.Files.Upper.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Upper.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref UppElem UPP)
        {
            ReadParamsFormELLs(xdoc, ref UPP);
        }

        private static void ReadParamsFormELLs(XDocument xdoc, ref UppElem UPP)
        {
            foreach (XElement Data in xdoc.Elements("UPP_DATA"))
            {
                foreach (var item in Data.Descendants("UPP_AWC"))
                {
                    UPP.UPP_AWC = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_BWC"))
                {
                    UPP.UPP_BWC = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_AZA"))
                {
                    UPP.UPP_AZA = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_BZA"))
                {
                    UPP.UPP_BZA = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_VOUTR"))
                {
                    UPP.UPP_VOUTR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_SK3"))
                {
                    UPP.UPP_SK3 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_FOUTR"))
                {
                    UPP.UPP_FOUTR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_DER1"))
                {
                    UPP.UPP_DER1 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_DER2"))
                {
                    UPP.UPP_DER2 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_CKRR"))
                {
                    UPP.UPP_CKRR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_GKRR"))
                {
                    UPP.UPP_GKRR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_LKRR"))
                {
                    UPP.UPP_LKRR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_AKKR"))
                {
                    UPP.UPP_AKKR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_AKOKR"))
                {
                    UPP.UPP_AKOKR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_TOR"))
                {
                    UPP.UPP_TOR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_JMIXUP"))
                {
                    UPP.UPP_JMIXUP = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_PC"))
                {
                    UPP.UPP_PC = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("UPP_TM"))
                {
                    UPP.UPP_TM = item.Attribute("Value").Value;
                }


                foreach (var item in Data.Descendants("UPP_ALFA0"))
                {
                    UPP.UPP_ALFA0.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("UPP_JMHOIN"))
                {
                    UPP.UPP_JMHOIN.Add(item.Attribute("Value").Value);
                }

            }
        }
    }
}

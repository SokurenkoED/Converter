using Converter__from_xml_to_dat_.Files.Asuelk.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuelk.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref List<ELL> Elms)
        {
            ReadParamsFormELLs(xdoc, ref Elms);
        }

        private static void ReadParamsFormELLs(XDocument xdoc, ref List<ELL> ELLs)
        {
            foreach (XElement ell in xdoc.Element("ELL_DATA").Element("ELL_CNT").Elements("ELL_NAME"))
            {
                ELL EL = new ELL();

                EL.Name = ell.Attribute("Value").Value;
                foreach (var item in ell.Descendants("ELL_PROP"))
                {
                    EL.Number = item.Attribute("ELL_NUM").Value;
                    EL.Description = item.Attribute("Description").Value;
                }
                foreach (var item in ell.Descendants("ELL_NETNAME"))
                {
                    EL.ELL_NETNAME = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_NETNUM"))
                {
                    EL.ELL_NETNUM = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_PNKEY"))
                {
                    EL.ELL_PNKEY = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_FOFF"))
                {
                    EL.ELL_FOFF = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_DELOFF"))
                {
                    EL.ELL_DELOFF = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_FON"))
                {
                    EL.ELL_FON = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_DELON"))
                {
                    EL.ELL_DELON = item.Attribute("Value").Value;
                }
                foreach (var item in ell.Descendants("ELL_VOLKEY"))
                {
                    EL.ELL_VOLKEY = item.Attribute("Value").Value;
                }

                ELLs.Add(EL);
            }
        }
    }
}

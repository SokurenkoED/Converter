using Converter__from_xml_to_dat_.Files.Asuval.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuval.Functions
{
    static class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc,  ref List<Valve> Valves)
        {
            ReadParamsFormValves(xdoc,ref Valves);
        }

        private static void ReadParamsFormValves(XDocument xdoc, ref List<Valve> Valves)
        {
            foreach (XElement Elms in xdoc.Element("VLV_CNT").Elements("JUN_VLVNAM"))
            {
                Valve VLV = new Valve();

                VLV.Name = Elms.Attribute("Value").Value;
                VLV.Description = Elms.Attribute("Description").Value;

                foreach (var item in Elms.Descendants("VLV_HHVAL"))
                {
                    VLV.VLV_HHVAL = item.Attribute("Value").Value;
                }

                Valves.Add(VLV);
            }
        }
    }
}

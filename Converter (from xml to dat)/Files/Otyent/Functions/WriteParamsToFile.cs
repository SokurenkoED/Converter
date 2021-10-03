
using Converter__from_xml_to_dat_.Files.Otyent.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Otyent.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref OtyElem OTY)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/otyent.dat", false, Encoding.Default))
            {
                WriteParamsFromELLs(sw, OTY);
            }
        }

        private static void WriteParamsFromELLs(StreamWriter sw, OtyElem OTY)
        {
            sw.WriteLine($" {"-2"}");
            sw.WriteLine($" {OTY.OTY_JOMEOT}");
            sw.WriteLine($" {OTY.OTY_PMOTY}");
            sw.WriteLine($" {OTY.OTY_SHEROT} {OTY.OTY_POPRBO}");
            sw.WriteLine($" {OTY.OTY_AKSI}");
            for (int i = 0; i < OTY.OTY_HO.Count; i++)
            {
                sw.WriteLine($" {OTY.OTY_HO[i]} {OTY.OTY_VO[i]} {OTY.OTY_KSIMO[i]}");
                sw.WriteLine($" {OTY.OTY_SOTY}");
                sw.WriteLine($" {OTY.OTY_DOTY}");
            }
            sw.WriteLine($" {OTY.OTY_CMO} {OTY.OTY_RMO} {OTY.OTY_DELTMO} {OTY.OTY_ALMO}");
            sw.WriteLine($" {OTY.OTY_JGROTY}");
            if (OTY.OTY_JMCOUT.Count > 0)
            {
                foreach (var item in OTY.OTY_JMCOUT)
                {
                    sw.Write(" " + item);
                }
            }
        }
    }
}

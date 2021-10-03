using Converter__from_xml_to_dat_.Files.Asuval.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Asuval.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref List<Valve> VLV)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/asuval.dat", false, Encoding.Default))
            {
                WriteParamsFromValves(sw, VLV);
            }
        }

        private static void WriteParamsFromValves(StreamWriter sw, List<Valve> VLV)
        {
            int k = 0;
            sw.WriteLine($" {VLV.Count}");
            foreach (var item in VLV)
            {
                sw.WriteLine($"{"/USU/"} {item.Name}    {"!"}{item.Description}");
            }
            foreach (var item in VLV)
            {
                sw.Write(item.VLV_HHVAL + "  ");
                k++;
                if (k > 20)
                {
                    sw.WriteLine();
                    k = 0;
                }
            }
        }

    }
}

using Converter__from_xml_to_dat_.Files.Asuelm.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Asuelm.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref List<Elm> Elms)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/asuelm.dat", false, Encoding.Default))
            {
                WriteParamsFromElms(sw, Elms);
            }
        }

        private static void WriteParamsFromElms(StreamWriter sw, List<Elm> Elms)
        {
            sw.WriteLine($" {Elms.Count}");
            foreach (var item in Elms)
            {
                sw.WriteLine($"{"/USU/"} {item.Name}");
                sw.WriteLine($" {item.ELM_ASU1} {item.ELM_ASU2} {item.ELM_ASU3} {item.ELM_ASU4}");
                sw.WriteLine($" {item.ELM_PHAND} {item.ELM_PHAND} {item.ELM_PHAND} {item.ELM_PHAND}");
                sw.WriteLine($" {item.ELM_MHAND} {item.ELM_MHAND} {item.ELM_MHAND} {item.ELM_MHAND}");
                sw.WriteLine($" {item.ELM_SHAND} {item.ELM_SHAND} {item.ELM_SHAND} {item.ELM_SHAND}");
                sw.WriteLine($" {"0"}");
            }
            sw.WriteLine();
            foreach (var item in Elms)
            {
                sw.WriteLine($" {item.ELM_PHAND} {item.ELM_MHAND} {item.ELM_SHAND}");
            }
        }
    }
}

using Converter__from_xml_to_dat_.Files.Asuelk.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Asuelk.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref List<ELL> ELLs)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/asuelk.dat", false, Encoding.Default))
            {
                WriteParamsFromELLs(sw, ELLs);
            }
        }

        private static void WriteParamsFromELLs(StreamWriter sw, List<ELL> ELLs)
        {
            sw.WriteLine($" {ELLs.Count}     {"Количество электрических ключей"}");
            foreach (var item in ELLs)
            {
                sw.WriteLine();
                sw.WriteLine($" {item.ELL_NETNUM} {item.ELL_PNKEY} {"("}{item.Number}{")"} {"Номер сети, Мощность нагрузки - подключенная к сети через ключ (ват)."}");
                sw.WriteLine(" 0");
                sw.WriteLine($" {item.ELL_FOFF} {item.ELL_FON} {item.ELL_DELOFF} {item.ELL_DELON}");
            }
            sw.WriteLine();
            foreach (var item in ELLs)
            {
                sw.Write($" {item.ELL_VOLKEY}");
            }
            sw.Write(" 0." + " Начальное состояние ключей");
        }
    }
}

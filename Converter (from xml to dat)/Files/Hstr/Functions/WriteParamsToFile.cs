using Converter__from_xml_to_dat_.Files.Hstr.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Hstr.Functions
{
    static class WriteParamsToFile
    {
        public static void WriteFile(ref List<Structure> Structures)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/hstr.dat", false, Encoding.Default))
            {
                sw.WriteLine(Structures.Count);
                foreach (var item in Structures)
                {
                    WriteParamsFromHeatSourse(sw, item);

                    WriteParamsFromTempJun(sw, item);
                }
            }
        }

        private static void WriteParamsFromHeatSourse(StreamWriter sw, Structure Str)
        {
            if (Str.HSTR_LEFTTYPE == null)
            {
                var HS = (HeatSource)Str;
                sw.WriteLine(HS.Number);

            }
        }

        private static void WriteParamsFromTempJun(StreamWriter sw, Structure Str)
        {
            if (Str.HSTR_LEFTTYPE != null)
            {
                var TJ = (TempJun)Str;
                sw.WriteLine(TJ.Number);

            }
        }

    }
}

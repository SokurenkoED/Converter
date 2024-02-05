using Converter__from_xml_to_dat_.Files.Hstr.Structures;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                var HS = (HeatSource)Str;
                sw.WriteLine($"{HS.HSTR_JHHS} {"0"} {HS.HSTR_JHSTIP}");
                sw.WriteLine($"{HS.HSTR_CM} {HS.HSTR_RM} {HS.HSTR_ALMD} {HS.HSTR_DLHS} {HS.HSTR_DRHS} {HS.HSTR_JTRHS}");
                if (double.Parse(HS.HSTR_JHSTIP, formatter) > 0)
                {
                    sw.WriteLine($"{HS.HSTR_ALHS}");
                }
                sw.WriteLine($"{HS.HSTR_QADHS} {HS.HSTR_ASU}");
                sw.WriteLine($"{"2"}");
                sw.WriteLine($"{"0. 1.E20  2*"}{HS.HSTR_QADHS}");
                sw.WriteLine($" {"NO"}");
                sw.WriteLine($" {HS.HSTR_RIGHTNAME}");
                if (HS.HSTR_HBOTHSR != null)
                {
                    sw.WriteLine($"{HS.HSTR_HBOTHSR} {HS.HSTR_HTOPHSR} {HS.HSTR_JDIRHSR}");
                }
                sw.WriteLine($"{HS.HSTR_J1RUL} {HS.HSTR_JADDL} {HS.HSTR_JADDR} {HS.HSTR_JADDR}");
                sw.WriteLine($"{"C"}");
            }
        }

        private static void WriteParamsFromTempJun(StreamWriter sw, Structure Str)
        {
            if (Str.HSTR_LEFTTYPE != null)
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                var TJ = (TempJun)Str;
                sw.WriteLine($"{TJ.HSTR_JHHS} {"0"} {TJ.HSTR_JHSTIP}");
                sw.WriteLine($"{TJ.HSTR_CM} {TJ.HSTR_RM} {TJ.HSTR_ALMD} {TJ.HSTR_DLHS} {TJ.HSTR_DRHS} {TJ.HSTR_JTRHS}");
                if (double.Parse(TJ.HSTR_JHSTIP, formatter) > 0)
                {
                    sw.WriteLine($"{TJ.HSTR_ALHS}");
                }
                sw.WriteLine($"{"0"} {"NO"}");
                sw.WriteLine($" {TJ.HSTR_LEFTNAME}");
                if (TJ.HSTR_HBOTHSL != null)
                {
                    sw.WriteLine($"{TJ.HSTR_HBOTHSL} {TJ.HSTR_HTOPHSL} {TJ.HSTR_JDIRHSL}");
                }
                sw.WriteLine($" {TJ.HSTR_RIGHTNAME}");
                if (TJ.HSTR_HBOTHSR != null)
                {
                    sw.WriteLine($"{TJ.HSTR_HBOTHSR} {TJ.HSTR_HTOPHSR} {TJ.HSTR_JDIRHSR}");
                }
                sw.WriteLine($"{TJ.HSTR_J1RUL} {TJ.HSTR_JADDL} {TJ.HSTR_J1RUR} {TJ.HSTR_JADDR}");
                sw.WriteLine($"{"C"}");
            }
        }

    }
}

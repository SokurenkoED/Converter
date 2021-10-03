using Converter__from_xml_to_dat_.Files.Upper.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Upper.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref UppElem UPP)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/upper.dat", false, Encoding.Default))
            {
                WriteParamsFromELLs(sw,ref UPP);
            }
        }

        private static void WriteParamsFromELLs(StreamWriter sw, ref UppElem UPP)
        {
            sw.WriteLine($" {UPP.UPP_JMIXUP}");
            sw.WriteLine($" {UPP.UPP_VOUTR} {UPP.UPP_FOUTR} {UPP.UPP_DER1} {UPP.UPP_DER2} {UPP.UPP_CKRR} {UPP.UPP_GKRR} {UPP.UPP_LKRR}" +
                $" {UPP.UPP_AKKR} {UPP.UPP_AKOKR} {UPP.UPP_TOR} {UPP.UPP_SK3} {UPP.UPP_AWC} {UPP.UPP_BWC} {UPP.UPP_AZA} {UPP.UPP_BZA}");
            sw.WriteLine($" {UPP.UPP_PC} {UPP.UPP_TM}");
            if (UPP.UPP_ALFA0.Count > 0)
            {
                foreach (var item in UPP.UPP_ALFA0)
                {
                    sw.Write(" " + item);
                }
                sw.WriteLine();
                foreach (var item in UPP.UPP_JMHOIN)
                {
                    sw.Write(" " + item);
                }
                sw.WriteLine();
            }
        }
    }
}

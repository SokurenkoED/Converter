
using Converter__from_xml_to_dat_.Files.Oopent.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Oopent.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref OopElem OOU)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/oopent.dat", false, Encoding.Default))
            {
                WriteParamsFromELLs(sw, OOU);
            }
        }

        private static void WriteParamsFromELLs(StreamWriter sw, OopElem OOU)
        {
            sw.WriteLine($" {OOU.OOU_JMIXOY}");
            sw.WriteLine($" {OOU.OOU_GOOP} {OOU.OOU_KCIOOP} {OOU.OOU_IOOP} {OOU.OOU_PC}");
            sw.WriteLine($" {OOU.OOU_KSIEOO}");
            sw.WriteLine($" {OOU.OOU_JMACRO}");
            for (int i = 0; i < int.Parse(OOU.OOU_JMACRO); i++)
            {
                sw.WriteLine($" {OOU.OOU_V[i]} {OOU.OOU_AL[i]} {OOU.OOU_S[i]} {OOU.OOU_DG[i]} {OOU.OOU_AKS[i]}" +
                    $" {OOU.OOU_AKSIN[i]} {OOU.OOU_AKSOUT[i]} {OOU.OOU_SHER[i]} {OOU.OOU_ACOS[i]} {OOU.OOU_PM[i]} {OOU.OOU_CM[i]}" +
                    $" {OOU.OOU_RM[i]} {OOU.OOU_DL[i]} {OOU.OOU_ALMD[i]} {OOU.OOU_KOS[i]} {OOU.OOU_Q[i]} {OOU.OOU_JV[i]}");
            }
            sw.WriteLine($" {OOU.OOU_ALFK} {OOU.OOU_POPR} {OOU.OOU_JALFA}");
            sw.WriteLine($" {OOU.OOU_TOC}");
            if (OOU.OOU_ALFA0.Count > 0)
            {
                foreach (var item in OOU.OOU_ALFA0)
                {
                    sw.Write(" " + item);
                }
                sw.WriteLine();
                foreach (var item in OOU.OOU_JMCIN)
                {
                    sw.Write(" " + item);
                }
                sw.WriteLine();
            }
            sw.WriteLine($" {OOU.OOU_JCBOR}");
            sw.WriteLine($" {OOU.OOU_CBBXAZ}");
        }
    }
}

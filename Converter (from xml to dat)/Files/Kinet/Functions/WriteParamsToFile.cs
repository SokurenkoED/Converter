using Converter__from_xml_to_dat_.Files.Kinet.Elems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Kinet.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref List<CrodsData> CDs,ref GeneralData GD,ref ReaceffData RD)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/kinet.dat", false, Encoding.Default))
            {
                WriteParamsFromGeneralData(sw, ref GD);

                WriteParamsFromReaceffDataAndCrodsData(sw, ref RD, ref CDs);
            }
        }

        private static void WriteParamsFromGeneralData (StreamWriter sw, ref GeneralData GD)
        {
            sw.WriteLine($" {"ost6_9%"}");
            sw.WriteLine($" {GD.KIN_LM.Count} {GD.KIN_BGAM.Count}");
            foreach (var item in GD.KIN_LM)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            foreach (var item in GD.KIN_BE)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            sw.WriteLine($" {GD.KIN_PNL} {GD.KIN_S0} {GD.KIN_NN} {GD.KIN_TOST}");
            if (GD.KIN_BGAM.Count > 0)
            {
                sw.WriteLine($" {GD.KIN_POWFIS}");
                sw.WriteLine($" {GD.KIN_NETJOB_ARG.Count}");
                foreach (var item in GD.KIN_NETJOB_ARG)
                {
                    sw.Write($" {item}");
                }
                sw.WriteLine();
                foreach (var item in GD.KIN_NETJOB)
                {
                    sw.Write($" {item}");
                }
                sw.WriteLine();
            }
            using (StreamWriter sw2 = new StreamWriter("OldFormat-TIGR/ost6_9%", false, Encoding.Default))
            {
                int k = 0;
                foreach (var item in GD.KIN_BGAM)
                {
                    if (k == 5)
                    {
                        sw2.WriteLine();
                        k = 0;
                    }
                    sw2.Write($" {item}");
                    k++;
                }
                sw2.WriteLine("      BGAM(" + GD.KIN_BGAM.Count + ")");
                sw2.WriteLine();
                k = 0;
                foreach (var item in GD.KIN_BLAM)
                {
                    if (k == 5)
                    {
                        sw2.WriteLine();
                        k = 0;
                    }
                    sw2.Write($" {item}");
                    k++;
                }
                sw2.WriteLine("      BLAM(" + GD.KIN_BLAM.Count + ")");
                sw2.WriteLine();
            }
        }

        private static void WriteParamsFromReaceffDataAndCrodsData(StreamWriter sw, ref ReaceffData RD, ref List<CrodsData> CDs)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            sw.WriteLine($" {RD.KIN_STEPFT} {RD.KIN_STEPHT} {RD.KIN_STEPHG}");
            sw.WriteLine($" {RD.KIN_ALFFT} {RD.KIN_TFT0}");
            if (double.Parse(RD.KIN_TFT0, formatter) < 1)
            {
                sw.WriteLine($" {RD.KIN_FKTF_ARG.Count}");
                foreach (var item in RD.KIN_FKTF_ARG)
                {
                    sw.Write($" {item}");
                }
                sw.WriteLine();
                foreach (var item in RD.KIN_FKTF)
                {
                    sw.Write($" {item}");
                }
                sw.WriteLine();
            }

            sw.WriteLine($" {RD.KIN_ARHT_ARG.Count}");
            foreach (var item in RD.KIN_ARHT_ARG)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            foreach (var item in RD.KIN_ARHT)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();

            sw.WriteLine($" {RD.KIN_ARHTM_ARG.Count}");
            foreach (var item in RD.KIN_ARHTM_ARG)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            foreach (var item in RD.KIN_ARHTM)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();

            sw.WriteLine($" {RD.KIN_ARHG_ARG.Count}");
            foreach (var item in RD.KIN_ARHG_ARG)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            foreach (var item in RD.KIN_ARHG)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();

            sw.WriteLine($" {RD.KIN_ARHCB_ARG.Count}");
            foreach (var item in RD.KIN_ARHCB_ARG)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            foreach (var item in RD.KIN_ARHCB)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();

            sw.WriteLine($" {RD.KIN_DRONE0} {RD.KIN_DTNOM}");

            sw.WriteLine(" 566.  288.  695.  1000."); 

            sw.WriteLine($" {RD.KIN_ALFCR}");

            sw.WriteLine($" {RD.KIN_DKT_ARG.Count} {CDs[0].KIN_DKGRUP_ARG.Count}");
            foreach (var item in RD.KIN_DKT_ARG)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            foreach (var item in RD.KIN_DKT)
            {
                sw.Write($" {item}");
            }
            sw.WriteLine();
            sw.WriteLine($" {CDs.Count}");
            foreach (var item in CDs)
            {
                int k = 0;
                foreach (var item2 in item.KIN_DKGRUP_ARG)
                {
                    if (k == 10)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                    sw.Write($" {item2}");
                    k++;
                }
                sw.WriteLine();
                k = 0;
                foreach (var item2 in item.KIN_DKGRUP)
                {
                    if (k == 10)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                    sw.Write($" {item2}");
                    k++;
                }
                sw.WriteLine();
                sw.WriteLine(" 0");
                sw.Write(" 0");
                using (StreamWriter sw3 = new StreamWriter("OldFormat-TIGR/asupo.dat", false, Encoding.Default))
                {
                    foreach (var item3 in CDs)
                    {
                        sw3.WriteLine($"{"/USU/"} {item3.KIN_ASUOR}");
                    }
                    foreach (var item3 in CDs)
                    {
                        sw3.Write($" {item3.KIN_ASUHRO0}");
                    }
                }
            }

        }
    }
}

using Converter__from_xml_to_dat_.Files.Hstr.Structures;
using Converter__from_xml_to_dat_.Files.Kin_sp.Elems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Kin_sp.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref GENERAL_DATA_SP GD, ref INT_PARAM_SP IP, ref RESIDUAL_DATA_SP RD, ref CRODS_DATA_SP CD)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/kin_sp.dat", false, Encoding.Default))
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                sw.WriteLine(RD.KIN7_JGROST);
                if (double.Parse(RD.KIN7_JGROST, formatter) != 0)
                {
                    sw.WriteLine($"ost");
                }
                sw.WriteLine($"{IP.KIN7_DELT} {IP.KIN7_DTMIN} {IP.KIN7_DTMAX} {IP.KIN7_XD} {IP.KIN7_TAUAZ}");
                for (int i = 0; i < GD.KIN_LM.Count; i++)
                {
                    sw.Write($"{GD.KIN_LM[i]} ");
                }
                sw.WriteLine();
                for (int i = 0; i < GD.KIN_BE.Count; i++)
                {
                    sw.Write($"{GD.KIN_BE[i]} ");
                }
                sw.WriteLine();
                sw.WriteLine($"{GD.KIN7_BETA0} ");
                sw.WriteLine($"{GD.KIN7_PNL} {GD.KIN7_SIST} {GD.KIN7_NIST} {GD.KIN7_NKIST}");
                sw.WriteLine($"{GD.KIN7_PNKIN} {GD.KIN7_ALFCR}");
                for (int i = 1; i < 164; i++)
                {
                    sw.Write($"{i} ");
                }
                sw.WriteLine();
                // строчка READ(25,*) JCCAN(J),QQT(1,J) будет когда-то выполняться?
                sw.WriteLine($"{RD.KIN7_POWFIS} ");
                sw.WriteLine($"{RD.KIN7_JNJOB} ");
                for (int i = 0; i < RD.KIN7_NETJOB_ARG.Count; i++)
                {
                    sw.Write($"{RD.KIN7_NETJOB_ARG[i]} ");
                }
                for (int i = 0; i < RD.KIN7_NETJOB.Count; i++)
                {
                    sw.Write($"{RD.KIN7_NETJOB[i]} ");
                }
                sw.WriteLine();
            }
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/ost", false, Encoding.Default))
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                for (int i = 0; i < RD.KIN7_BGAM.Count; i++)
                {
                    sw.Write($"{RD.KIN7_BGAM[i]} ");
                }
                sw.WriteLine();
                for (int i = 0; i < RD.KIN7_BLAM.Count; i++)
                {
                    sw.Write($"{RD.KIN7_BLAM[i]} ");
                }
            }
        }
    }
}

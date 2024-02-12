using Converter__from_xml_to_dat_.Files.Gidr2k.HomolChar;
using Converter__from_xml_to_dat_.Files.Gidr2k.Junctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Gidr2k.Functions
{
    static class WriteParamsToFile
    {

        public static void WriteFile(ref List<Jun> Juns, ref List<Homol> Homols, ref List<string> LastParams)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/gidr2k.dat", false, Encoding.Default))
            {
                sw.WriteLine(Juns.Count);
                WriteJuns(ref Juns, sw);
                sw.WriteLine("C");
                foreach (var Jun in Juns)
                {
                    WriteParamsFromStandart(Jun, sw);
                    WriteParamsFromTurb(Jun, sw);
                    WriteParamsFromDep(Jun, sw);
                    WriteParamsFromGas(Jun, sw);
                    
                }
                WriteHomols(ref Homols, sw);
                sw.WriteLine("C  Crit Flow    Eps PUMP");
                WriteLastTwoParams(ref LastParams, sw); // Почему-то не находит последние 2 параметра, поэтому запишем сами
                sw.WriteLine("0.7 0.01");
                sw.WriteLine("CCCCCCCCCCCCCCCCCCCCCCC INITIAL CONDITION CCCCCCCCCCCCCCCCCCCCCCCCCCCC");
                WriteIC(ref Juns, sw);
                WriteCheckValve(ref Juns, sw);
            }
        }
        private static void WriteCheckValve(ref List<Jun> Juns, StreamWriter sw)
        {
            foreach (var Jun in Juns)
            {
                if (Jun.Type == "1")
                {
                    Standart stndrt = (Standart)Jun;
                    if (stndrt.JUN_JOBR == "2")
                    {
                        sw.WriteLine($"{stndrt.JUN_KCICLS} {stndrt.JUN_KCIUNOPN} {stndrt.JUN_KCILAM} {stndrt.JUN_TAUBCV} {stndrt.JUN_SBCVLV}");
                    }
                }
            }
        }

        private static void WriteJuns(ref List<Jun> Juns, StreamWriter sw)
        {
            foreach (var Jun in Juns)
            {
                sw.WriteLine($" {Jun.Name} {Jun.JUN_FROM} {Jun.JUN_TO}   {"#"}{Jun.Number}");
            }
        }

        private static void WriteIC(ref List<Jun> Juns, StreamWriter sw)
        {
            foreach (var Jun in Juns)
            {
                sw.Write(Jun.JUN_KCI2KJ + " ");
            }
            sw.WriteLine();
        }

        private static void WriteLastTwoParams(ref List<string> LastParams, StreamWriter sw)
        {
            foreach (var item in LastParams)
            {
                sw.Write($"{item} {""}");
            }
        }

        private static void WriteHomols(ref List<Homol> Homols, StreamWriter sw)
        {
            sw.WriteLine($"CC******************** TABLES FOR PUMPS *****************************");
            foreach (var Homol in Homols)
            {
                sw.WriteLine($" {Homol.JUN_SQHCC1.Count/2} {Homol.JUN_SQMCC1.Count/2}");
                foreach (var item in Homol.JUN_SQHCC1)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQHCC2)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQHCC3)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQHCC4)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQMCC1)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQMCC2)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQMCC3)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
                foreach (var item in Homol.JUN_SQMCC4)
                {
                    sw.Write($"{item} {""}");
                }
                sw.WriteLine();
            }
            
        }

        private static void WriteParamsFromGas(Jun Jun, StreamWriter sw)
        {
            if (Jun.Type == "3")
            {
                Gas stndrt = (Gas)Jun;
                sw.WriteLine($" {stndrt.Name} {"/"}{stndrt.JUN_FROM} {stndrt.JUN_TO}   {"#"}{stndrt.Number}{"/"}");
                sw.WriteLine($" {stndrt.JUN_AJNMLT}");
                sw.WriteLine($" {"0"} {stndrt.JUN_VLVNAM} {"0"} {"0"} {stndrt.Type} {stndrt.JUN_VJ} {stndrt.JUN_HJ1} {stndrt.JUN_HJ2}");
                sw.WriteLine($"C SG       KSI     DG      LG      SHER    INER      DZ");
                sw.WriteLine($" {stndrt.JUN_SG} {stndrt.JUN_KSIG2K} {stndrt.JUN_DGG2K} {stndrt.JUN_LG} {stndrt.JUN_SHRG2K} {stndrt.JUN_INMG2K} {stndrt.JUN_DZG2K}");
                sw.WriteLine($" {stndrt.JUN_V0KDI1} {stndrt.JUN_V1KDI1} {stndrt.JUN_V0KDI2} {stndrt.JUN_V1KDI2}");
                if (stndrt.JUN_VLVNAM != "NO")
                {
                    sw.WriteLine($"C Input data for Valve");
                    sw.WriteLine($"C Lengh   Area   KSI    C   Hidr.Diam");
                    sw.WriteLine($" {stndrt.JUN_LVLV} {stndrt.JUN_S0VLV} {stndrt.JUN_KSIVLV} {stndrt.JUN_CVLV} {stndrt.JUN_DGVLV}");
                }
                sw.WriteLine("C");
            }
        }

        private static void WriteParamsFromDep(Jun Jun, StreamWriter sw)
        {
            if (Jun.Type == "0")
            {
                Dep dep = (Dep)Jun;
                sw.WriteLine($" {dep.Name} {"/"}{dep.JUN_FROM} {dep.JUN_TO}   {"#"}{dep.Number}{"/"}");
                sw.WriteLine($" {dep.JUN_AJNMLT}");
                sw.WriteLine($" 0 NO 0 0 {dep.Type} 0.0011 {dep.JUN_HJ1} {dep.JUN_HJ2}");
                sw.WriteLine($" {dep.JUN_JJNT} {"0"} {dep.JUN_JJNPAR}");
                for (int i = 0; i < dep.JUN_KC2KT_ARG.Count; i++)
                {
                    sw.Write($"{dep.JUN_KC2KT_ARG[i]} ");
                }
                sw.WriteLine();
                for (int i = 0; i < dep.JUN_KC2KT.Count; i++)
                {
                    sw.Write($"{dep.JUN_KC2KT[i]} ");
                }
                sw.WriteLine();
            }
        }

        private static void WriteParamsFromStandart(Jun Jun, StreamWriter sw)
        {
            CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            if (Jun.Type == "1")
            {
                Standart stndrt = (Standart)Jun;
                sw.WriteLine($" {stndrt.Name} {"/"}{stndrt.JUN_FROM} {stndrt.JUN_TO}   {"#"}{stndrt.Number}{"/"}");
                sw.WriteLine($" {stndrt.JUN_AJNMLT}");
                sw.WriteLine($" {stndrt.JUN_JPUG2K} {stndrt.JUN_VLVNAM} {stndrt.JUN_JCRFLJ} {stndrt.JUN_JOBR} {stndrt.Type} {stndrt.JUN_VJ} {stndrt.JUN_HJ1} {stndrt.JUN_HJ2}");
                sw.WriteLine($"C SG       KSI     DG      LG      SHER    INER      DZ");
                sw.Write($" {stndrt.JUN_SG} {stndrt.JUN_KSIG2K} {stndrt.JUN_DGG2K} {stndrt.JUN_LG} {stndrt.JUN_SHRG2K} {stndrt.JUN_INMG2K} {stndrt.JUN_DZG2K}");
                if (stndrt.JUN_DP02K != null)
                {
                    sw.WriteLine($" {stndrt.JUN_DP02K}");
                }
                else
                {
                    sw.WriteLine();
                }
                if (stndrt.JUN_VLVNAM != "NO")
                {
                    sw.WriteLine($"C Input data for Valve");
                    sw.WriteLine($"C Lengh   Area   KSI    C   Hidr.Diam   Diment_KSI");
                    if (stndrt.JUN_JVTBL2 != "0")
                    {
                        stndrt.JUN_KSIVLV =(double.Parse(stndrt.JUN_KSIVLV) * -1).ToString();
                    }
                    sw.WriteLine($" {stndrt.JUN_LVLV} {stndrt.JUN_S0VLV} {stndrt.JUN_KSIVLV} {stndrt.JUN_CVLV} {stndrt.JUN_DGVLV} {stndrt.JUN_JVTBL2}");
                    if(stndrt.JUN_JVTBL2 != "0")
                    {
                        for (int i = 0; i < stndrt.JUN_VLVTBL_H.Count; i++)
                        {
                            sw.Write($"{stndrt.JUN_VLVTBL_H[i]} ");
                        }
                        sw.WriteLine();
                        for (int i = 0; i < stndrt.JUN_VLVTBL_R.Count; i++)
                        {
                            sw.Write($"{stndrt.JUN_VLVTBL_R[i]} ");
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine($" {stndrt.JUN_JVTBL}");
                    if (stndrt.JUN_JVTBL != "0")
                    {
                        for (int i = 0; i < stndrt.JUN_VLVTBL_ARG.Count; i++)
                        {
                            sw.Write($"{stndrt.JUN_VLVTBL_ARG[i]} ");
                        }
                        sw.WriteLine();
                        for (int i = 0; i < stndrt.JUN_VLVTBL_S.Count; i++)
                        {
                            sw.Write($"{stndrt.JUN_VLVTBL_S[i]} ");
                        }
                        sw.WriteLine();
                    }
                }
                if (stndrt.JUN_JPUG2K != "0")
                {
                    sw.WriteLine($"C Head  Torque   Flow   Velocity   Dencity");
                    sw.WriteLine($" {stndrt.JUN_HP0G2K} {stndrt.JUN_MP0G2K} {stndrt.JUN_QP0G2K} {stndrt.JUN_OMP02K} {stndrt.JUN_GAM02K}");
                    sw.WriteLine($" {stndrt.JUN_JWTG2K}");
                    if (stndrt.JUN_JWTG2K != "0")
                    {
                        for (int i = 0; i < stndrt.JUN_WTG2K_ARG.Count; i++)
                        {
                            sw.Write($"{stndrt.JUN_WTG2K_ARG[i]} ");
                        }
                        sw.WriteLine();
                        for (int i = 0; i < stndrt.JUN_WTG2K_FRQ.Count; i++)
                        {
                            sw.Write($"{stndrt.JUN_WTG2K_FRQ[i]} ");
                        }
                        sw.WriteLine();
                    }
                }
                sw.WriteLine("C");
            }
        }

        private static void WriteParamsFromTurb(Jun Jun, StreamWriter sw)
        {
            if (Jun.Type == "2")
            {
                Standart stndrt = (Standart)Jun;
                sw.WriteLine($" {stndrt.Name} {"/"}{stndrt.JUN_FROM} {stndrt.JUN_TO}   {"#"}{stndrt.Number}{"/"}");
                sw.WriteLine($" {stndrt.JUN_AJNMLT}");
                sw.WriteLine($" {"0"} {"NO"} {"0"} {"0"} {stndrt.Type} {stndrt.JUN_VJ} {stndrt.JUN_HJ1} {stndrt.JUN_HJ2}");
                sw.WriteLine($" {stndrt.JUN_JCVD}");
                sw.WriteLine($"C SG       KSI     DG      LG      SHER    INER      DZ");
                sw.WriteLine($" {stndrt.JUN_SG} {stndrt.JUN_KSIG2K} {stndrt.JUN_DGG2K} {stndrt.JUN_LG} {stndrt.JUN_SHRG2K} {stndrt.JUN_INMG2K} {stndrt.JUN_DZG2K}");
                sw.WriteLine("C");
            }
        }
    }
}

using Converter__from_xml_to_dat_.Files.Canent.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Canent.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref GeneralCore GC, ref List<SeparateCore> SCs, ref ModlimitCore MC,
            ref FaCore FaC, ref UnheatCore UC, ref CoreGengeom CG, ref CoreGeom CGeom,
            ref List<FuelrodCore> FCs, ref StrmatFaCore SFC, ref StrmatUnheatCore SUC, ref CoreCross CC, ref CoreTFT CTFT)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/canent.dat", false, Encoding.Default))
            {


                int k = 0;
                foreach (var item in SCs)
                {
                    sw.Write(item.CORE_JRC + " ");
                    k++;
                    if (k == 60)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                }
                sw.WriteLine(); sw.WriteLine();
                k = 0;
                foreach (var item in SCs)
                {
                    sw.Write(item.CORE_JRCTIP + " ");
                    k++;
                    if (k == 60)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                }
                sw.WriteLine(); sw.WriteLine();
                k = 0;

                sw.WriteLine($" {GC.CORE_NNOM} {GC.CORE_ALFC} {GC.CORE_ALFB} {GC.CORE_P} {GC.CORE_JJRC}");
                sw.WriteLine($" {GC.CORE_EPSMK}");
                sw.WriteLine("CCC  /dksi.dx/№ II(t)/P(z)/dp.dt/fi_medi/Xmin /");
                sw.WriteLine($" {MC.CORE_JDGDZ} {MC.CORE_JNF} {MC.CORE_JDPDZ} {MC.CORE_JDPDT} {MC.CORE_JPRFI} {MC.CORE_EPSX}");
                sw.WriteLine("CCC  / № MIDDLE CHAN./ BYPAS CHAN.");
                sw.WriteLine($" {MC.CORE_JCB} {MC.CORE_JCPROT}");
                sw.WriteLine("CCC /Пмет/Пмкп/Rтепл/Kгидр/Кто/Птепл/равновес/проскальз/Qкрит/alfa_крит/Geom_Qкрит ! №");

                for (int i = 0; i < FaC.CORE_PMI.Count; i++)
                {
                    sw.WriteLine($" {FaC.CORE_PMI[i]} {FaC.CORE_PPROT[i]} {FaC.CORE_DELMD[i]} {FaC.CORE_POPRB[i]} {FaC.CORE_ALFKDH[i]} {FaC.CORE_DTCRIT[i]}" +
                        $" {FaC.CORE_JUNEQ[i]} {FaC.CORE_JOMEG[i]} {FaC.CORE_JQCRIT[i]} {FaC.CORE_JACRIT[i]} {FaC.CORE_JBUNDL[i]}");
                }
                for (int i = 0; i < UC.CORE_PMI2.Count; i++)
                {
                    sw.WriteLine($" {UC.CORE_PMI2[i]} {UC.CORE_PPROT2[i]} {UC.CORE_DELMD2[i]} {UC.CORE_POPRB2[i]} {UC.CORE_ALFKDH2[i]} {UC.CORE_JUNEQ2[i]} {UC.CORE_JOMEG2[i]}");
                }

                foreach (var item in SCs)
                {
                    sw.Write(item.CORE_KSIMJ + " ");
                    k++;
                    if (k == 30)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                }
                sw.WriteLine(); sw.WriteLine();

                for (int i = 0; i < CG.CORE_H.Count; i++)
                {
                    sw.WriteLine($" {CG.CORE_H[i]} {CG.CORE_COSC[i]} {CG.CORE_JV[i]}");
                }

                for (int i = 0; i < CGeom.CORE_VC.Count; i++)
                {
                    sw.WriteLine($" {CGeom.CORE_VC[i]} {CGeom.CORE_SC[i]} {CGeom.CORE_DC[i]} {CGeom.CORE_KSIM[i]} {CGeom.CORE_SHER[i]} {CGeom.CORE_JV2[i]}");
                }

                foreach (var item in FCs)
                {
                    sw.WriteLine($" {item.CORE_TVEL}");
                    sw.WriteLine($" {item.CORE_EPSTF}");
                    sw.WriteLine("CC (Тип-1) признак геометрии ТВЭЛов (0- данные задаются, j- данные берутся из канала с номером типа j )");
                    sw.WriteLine($" {"0"} {item.CORE_JVVOD}");
                    sw.WriteLine($" {item.CORE_RT1} {item.CORE_RT2} {item.CORE_RIO} {item.CORE_ROO}");
                    sw.WriteLine($" {item.CORE_GAT} {item.CORE_GAOB} {item.CORE_DELST}");
                    sw.WriteLine($" {item.CORE_CTTAB_ARG.Count}");
                    int iter = 0;
                    foreach (var item2 in item.CORE_CTTAB_ARG)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == 12)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;
                    foreach (var item2 in item.CORE_CTTAB)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == 12)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;
                    sw.WriteLine($" {item.CORE_LTTAB_ARG.Count}");
                    foreach (var item2 in item.CORE_LTTAB_ARG)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == 12)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;
                    foreach (var item2 in item.CORE_LTTAB)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == 12)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;

                    sw.WriteLine($" {item.CORE_COTAB_ARG.Count}");
                    foreach (var item2 in item.CORE_COTAB_ARG)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == item.CORE_COTAB_ARG.Count)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;
                    foreach (var item2 in item.CORE_COTAB)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == item.CORE_COTAB_ARG.Count)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;

                    sw.WriteLine($" {item.CORE_LOTAB_ARG.Count}");
                    foreach (var item2 in item.CORE_LOTAB_ARG)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == item.CORE_LOTAB_ARG.Count)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;
                    foreach (var item2 in item.CORE_LOTAB)
                    {
                        sw.Write($" { item2}");
                        iter++;
                        if (iter == item.CORE_LOTAB_ARG.Count)
                        {
                            sw.WriteLine();
                            iter = 0;
                        }
                    }
                    iter = 0;

                    sw.WriteLine($" {item.CORE_LGTAB_ARG.Count}");
                    foreach (var item2 in item.CORE_LGTAB_ARG)
                    {
                        sw.Write(" " + item2);
                    }
                    sw.WriteLine();
                    foreach (var item2 in item.CORE_LGTAB)
                    {
                        sw.Write(" " + item2);
                    }
                    sw.WriteLine();
                    sw.WriteLine("C   D0ZAZ     DGZAZ   AOZAZ");
                    sw.WriteLine("C   IF D0ZAZ=0. ALF GAP =  LAMBDA/(RIO-RT(JR))");
                    sw.WriteLine($" {item.CORE_D0ZAZ} {item.CORE_DGZAZ} {item.CORE_AOZAZ}");
                    sw.WriteLine("C NEW DATA FOR GAP PRESSURE CALCULATION (not used now)");
                    sw.WriteLine($" {item.CORE_JGTVL}");
                    if (item.CORE_JGTVL != "0")
                    {
                        sw.WriteLine($" {item.CORE_PTVL} {item.CORE_AMGTVL} {item.CORE_RGTVL} {item.CORE_VGSTVL} {item.CORE_FTTVL}");
                    }
                }
                k = 0;
                foreach (var item in SCs)
                {
                    sw.Write(item.CORE_KV1 + " ");
                    k++;
                    if (k == 20)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                }
                sw.WriteLine(); sw.WriteLine();

                k = 0;
                foreach (var item in SCs)
                {
                    foreach (var item2 in item.CORE_KV2)
                    {
                        sw.Write(item2 + " ");
                        k++;
                        if (k == 20)
                        {
                            sw.WriteLine();
                            k = 0;
                        }
                    }
                }
                sw.WriteLine();

                for (int i = 0; i < SFC.CORE_CMI.Count; i++)
                {
                    sw.WriteLine($" {SFC.CORE_CMI[i]} {SFC.CORE_RMI[i]} {SFC.CORE_DELMI[i]} {SFC.CORE_ALMI[i]}");
                }
                for (int i = 0; i < SUC.CORE_CMI2.Count; i++)
                {
                    sw.WriteLine($" {SUC.CORE_CMI2[i]} {SUC.CORE_RMI2[i]} {SUC.CORE_DELMI2[i]} {SUC.CORE_ALMI2[i]}");
                }

                k = 0;
                foreach (var item in SCs)
                {
                    sw.Write(item.CORE_KSI + " ");
                    k++;
                    if (k == 20)
                    {
                        sw.WriteLine();
                        k = 0;
                    }
                }
                sw.WriteLine();
                sw.WriteLine("-2.0");
                sw.WriteLine(CC.CORE_JCROSS);
                sw.WriteLine("C Число т/ф каналов");
                sw.WriteLine(CTFT.CORE_JTFT);
                sw.WriteLine("0");
            }
        }
    }
}

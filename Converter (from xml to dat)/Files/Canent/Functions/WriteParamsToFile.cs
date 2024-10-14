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
                sw.WriteLine($" {MC.CORE_JDGDZ} {MC.CORE_JNF} {MC.CORE_JDPDZ} {MC.CORE_JDPDT} {MC.CORE_JPRFI} {MC.CORE_JFZ} {MC.CORE_QPOPR} {MC.CORE_EPSX}");
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
                    sw.WriteLine($" {"0"}");
                    sw.WriteLine($" {item.CORE_JVVOD}");
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
                sw.WriteLine(CTFT.CORETT_JTFCAN);
                if (CTFT.CORETT_JTFCAN != "0")
                {
                    sw.WriteLine("C номер тг канала, моделируемого набором  ячеек  / число расчетных ячеек");
                    sw.WriteLine($"{CTFT.CORETT_JTFJ} {CTFT.CORETT_JCELL1.Count}");
                    sw.WriteLine("C число конструктивнvх  ячеек в каждой  расчетной  ячейке");
                    k = 0;
                    foreach (var JRC in CTFT.CORETT_JRC)
                    {
                        sw.Write(JRC + " ");
                        k++;
                        if (k == 20)
                        {
                            sw.WriteLine();
                            k = 0;
                        }
                    }
                    sw.WriteLine();

                    sw.WriteLine("C номера геометрических типов конструктивых  ячеек");
                    k = 0;
                    foreach (var JRCTIP in CTFT.CORETT_JRCTIP)
                    {
                        sw.Write(JRCTIP + " ");
                        k++;
                        if (k == 20)
                        {
                            sw.WriteLine();
                            k = 0;
                        }
                    }
                    sw.WriteLine();

                    sw.WriteLine("C Коэффициент гидравлического сопротивлени на входе");
                    k = 0;
                    foreach (var KSIMJ in CTFT.CORETT_KSIMJ)
                    {
                        sw.Write(KSIMJ + " ");
                        k++;
                        if (k == 20)
                        {
                            sw.WriteLine();
                            k = 0;
                        }
                    }
                    sw.WriteLine();

                    sw.WriteLine("C  /імет/імкп/Rтепл/Kгидр/іто/Dтепл/равновес/проскальз/Qкрит/alfa_крит/Geom_Qкрит/  і");
                    for (int i = 0; i < CTFT.CORETT_PMI.Count; i++)
                    {
                        sw.WriteLine($"{CTFT.CORETT_PMI[i]} {CTFT.CORETT_PPROT[i]} {CTFT.CORETT_DELMD[i]} {CTFT.CORETT_POPRB[i]} {CTFT.CORETT_ALFKDH[i]} {CTFT.CORETT_DTCRIT[i]} {CTFT.CORETT_JUNEQ[i]}  {CTFT.CORETT_JOMEG[i]} {CTFT.CORETT_JQCRIT[i]} {CTFT.CORETT_JACRIT[i]} {CTFT.CORETT_JBUNDL[i]} ({i + 1 + 1})");
                    }

                    int gap = 0;
                    for (int i = 0; i < CTFT.CORETT_PMI.Count; i++)
                    {
                        sw.WriteLine($"C Геом. характеристики ячейки типа {i + 1} V/S/Dгидр/Kгидр/Rough/Nh");
                        for (int j = 0; j < Int32.Parse(CTFT.CORETT_K2[i]); j++)
                        {
                            sw.WriteLine($"{CTFT.CORETT_VC[gap + j]} {CTFT.CORETT_SC[gap + j]} {CTFT.CORETT_DC[gap + j]} {CTFT.CORETT_KSIM[gap + j]} {CTFT.CORETT_SHER[gap + j]} {CTFT.CORETT_JV2[gap + j]}");
                        }
                        gap = gap + Int32.Parse(CTFT.CORETT_K2[i]);
                    }

                    sw.WriteLine($"C Число ТВЭЛ в чейках по типам");
                    foreach (var TVEL in CTFT.CORETT_TVELCOUNT)
                    {
                        sw.Write($"{TVEL} ");
                    }
                    sw.WriteLine();

                    sw.WriteLine($"CC признак геометрии типов твэл (0- данные задаются, j- данные берутся из канала с номером типа j)");
                    foreach (var JGEOM in CTFT.CORETT_JGEOM)
                    {
                        sw.WriteLine($"{JGEOM}");
                    }

                    sw.WriteLine($"C Свойства конструкционных материалов ячеек различных типов ТТК");
                    for (int i = 0; i < CTFT.CORETT_CMI.Count; i++)
                    {
                        sw.WriteLine($"{CTFT.CORETT_CMI[i]} {CTFT.CORETT_RMI[i]} {CTFT.CORETT_DELMI[i]} {CTFT.CORETT_ALMI[i]}");
                    }

                    sw.WriteLine($"C Площадь проходного сечени и эффективный зазор между ТВЭЛ  стандартной чейки, JCOV");
                    sw.WriteLine($"{CTFT.CORETT_FSTAND} {CTFT.CORETT_DSTAND} {1}");

                    for (int i = 0; i < CTFT.CORETT_JCELL1.Count; i++)
                    {
                        sw.WriteLine($"{CTFT.CORETT_KR[i]} {CTFT.CORETT_JCELL1[i]} {CTFT.CORETT_JCELL2[i]} {CTFT.CORETT_JCELL3[i]} {CTFT.CORETT_JCELL4[i]} ({i+1})");
                        sw.WriteLine($"            {CTFT.CORETT_DELT1[i]} {CTFT.CORETT_DELT2[i]} {CTFT.CORETT_DELT3[i]} {CTFT.CORETT_DELT4[i]}");
                        sw.WriteLine($"            {CTFT.CORETT_CDELT1[i]} {CTFT.CORETT_CDELT2[i]} {CTFT.CORETT_CDELT3[i]} {CTFT.CORETT_CDELT4[i]}");
                    }
                }
                sw.WriteLine("0");
            }
        }
    }
}

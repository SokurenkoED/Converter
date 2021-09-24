using Converter__from_xml_to_dat_.Files.Elpows.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Elpows.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(ref List<Elg> EG, ref List<Elm> EM, ref List<Net> NT, ref List<Pump> PMP, ref List<Shaft> Shft, ref List<Turb> TB)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/elpows.dat", false, Encoding.Default))
            {
                WriteParamsFromShaftAndTurb(sw, Shft, TB);

                WriteParamsFromElg(sw, EG);

                WriteParamsFromNet(sw, NT);

                WriteParamsFromElm(sw, EM);

                WriteParamsFromPump(sw, PMP);

                sw.WriteLine("0");
            }
        }
        private static void WriteParamsFromShaftAndTurb(StreamWriter sw, List<Shaft> Shft, List<Turb> TB)
        {
            sw.WriteLine($" {Shft.Count} {TB.Count}   {"Количество валов,  количество турбин"}");
            foreach (var item in Shft)
            {
                sw.WriteLine(" " + item.Name);
            }
            foreach (var item in TB)
            {
                sw.WriteLine($" {item.TURB_SHAFTNUM} {item.TURB_MJTUR} {item.TURB_MDIS1} {item.TURB_MDIS2} {item.TURB_MDIS3}  {"/(1)Номер вала, Момент инерции и коэффициенты потерь"}");
                sw.WriteLine($" {item.TURB_FITUR} {item.TURB_NUTUR} {item.TURB_RETUR} {item.TURB_G0TUR} {item.TURB_GA0TUR} {item.TURB_ET0TUR} {item.TURB_OM0TUR}  {"/Красх., НЮ опт, реакт-ть, Ном.расход,Ном.плотн, КПД, Ном. част"}");
                sw.WriteLine();
            }
            foreach (var item in Shft)
            {
                sw.Write(" " + item.SHAFT_OMTUR);
            }
            sw.WriteLine(); sw.WriteLine();
        }

        private static void WriteParamsFromElg(StreamWriter sw, List<Elg> EG)
        {
            sw.WriteLine($" {EG.Count} {"Количество электрогенераторов"}");
            foreach (var item in EG)
            {
                sw.WriteLine($" {item.Name}");
                sw.WriteLine($" {item.ELG_SHAFTNUM} {item.ELG_NETNUM} {"50"}   {"/(JTURB,JNET,JESMI)"}");
                sw.WriteLine($" {item.ELG_MJGEN} {item.ELG_NNOM} {item.ELG_NASIN}");
                sw.WriteLine($" {item.ELG_ASU}");
                sw.WriteLine($" {item.ELG_TAUALT} {item.ELG_DEDI} {item.ELG_IMAXA} {item.ELG_UMAXA}");
            }
            sw.WriteLine();
            foreach (var item in EG)
            {
                sw.Write($" {item.ELG_FIN} {item.ELG_IALTER}");
            }
            sw.Write("/(начальные данные генераторов)");
            sw.WriteLine(); sw.WriteLine();
        }

        private static void WriteParamsFromNet(StreamWriter sw, List<Net> NT) 
        {
            sw.WriteLine($" {NT.Count} {"Количество электрических сетей"}");
            foreach (var item in NT)
            {
                sw.WriteLine($" {item.Name}");
                sw.WriteLine($" {item.NET_JSOUR} {"/количество точек временной зависимости внешнего источника"}");
                foreach (var item2 in item.NET_PSOUR_ARG)
                {
                    sw.Write($" {item2}");
                }
                sw.WriteLine();
                foreach (var item2 in item.NET_PSOUR)
                {
                    sw.Write($" {item2}");
                }
                sw.WriteLine();
                sw.WriteLine($" {item.NET_OMNET} {"/Начальные частоты электросетей"}");
            }
        }

        private static void WriteParamsFromElm(StreamWriter sw, List<Elm> EM)
        {
            sw.WriteLine($" {EM.Count} {"Количество электроприводов"}");
            foreach (var item in EM)
            {
                sw.WriteLine($" {item.Name}");
                sw.WriteLine($" {item.ELM_NETNUM} {item.ELM_MJMAC} {item.ELM_M1MAC} {item.ELM_M2MAC} {item.ELM_M3MAC} {"Номер сети,Момент инерции, коэффициенты потерь"}");
                sw.WriteLine($" {item.ELM_JMAC} {item.ELM_JVFMAC} {"(Признак эл-да 0-асхр,1-асхр.через преобраз,2-коллектор.), К-во точек"} {"ОБРАТИТЬ ВНИМАНИЕ НА ТАБЛИЦУ!!!!!!!!!!!!!!!!!!!!!!!"}");
                if (item.ELM_JMAC == "1")
                {
                    foreach (var item2 in item.ELM_VFMAC_ARG)
                    {
                        sw.Write($" {item2}");
                    }
                    sw.WriteLine();
                    foreach (var item2 in item.ELM_VFMAC)
                    {
                        sw.Write($" {item2}");
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();
            }
            foreach (var item in EM)
            {
                sw.Write($" {item.ELM_OMELMA}");
            }
            sw.Write($" { "/Начальные частоты электросетей"}");
            sw.WriteLine(); sw.WriteLine();

        }

        private static void WriteParamsFromPump(StreamWriter sw, List<Pump> PMP)
        {
            sw.WriteLine($" {PMP.Count} {"Количество насосов"}");
            foreach (var item in PMP)
            {
                if (int.Parse(item.Number)<10)
                {
                    sw.WriteLine($" {item.PUMP_TUREM}{"0"}{item.Number} {item.PUMP_MJPUMP} {"("}{item.PUMP_ELMNAME}{")"} {"(10-турб,20-эл.прив.),Мом инерции"}");
                }
                else
                {
                    sw.WriteLine($" {item.PUMP_TUREM}{item.Number} {item.PUMP_MJPUMP} {"("}{item.PUMP_ELMNAME}{")"} {"(10-турб,20-эл.прив.),Мом инерции"}");
                }
                
            }
            sw.WriteLine();
        }
    }
}

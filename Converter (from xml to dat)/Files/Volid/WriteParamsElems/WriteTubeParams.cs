using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    class WriteTubeParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw, IFormatProvider formatter)
        {
            if (Elem.Type == "2" || Elem.Type == "5")
            {
                Tube tube = (Tube)Elem;
                sw.WriteLine($"{" "}{tube.Name}               {"!"}{tube.Description}               {"#"}{tube.Number}");
                sw.WriteLine($"{tube.Type}");
                sw.WriteLine($"{tube.ELEM_VOLMLT}");
                sw.WriteLine($"{tube.PIPE_JMACRO}");
                sw.WriteLine($"C Volume   Lengh  Area   Diam.   Press.Drop     Rough. Inert Height Volume Numb");
                for (int i = 0; i < int.Parse(tube.PIPE_JMACRO); i++)
                {
                    sw.WriteLine($"{tube.PIPE_V2SG[i]} {tube.PIPE_AL[i]} {tube.PIPE_S2SG[i]} {tube.PIPE_DGSG[i]} {tube.PIPE_AKSSG[i]} {tube.PIPE_SHERSG[i]} {tube.PIPE_INMPG[i]} {tube.PIPE_DZSG[i]} {tube.PIPE_JV[i]}");
                }
                sw.WriteLine($"{tube.PIPE_JMACRV}");
                if (double.Parse(tube.PIPE_JMACRV, formatter) > 0)
                {
                    for (int i = 0; i < int.Parse(tube.PIPE_JMACRV); i++)
                    {
                        sw.WriteLine($"{tube.PIPE_JNM[i]} {tube.PIPE_JVV[i]}");
                        if (double.Parse(tube.PIPE_JNM[i], formatter) > 0)
                        {
                            sw.WriteLine($"{tube.PIPE_CM[i]} {tube.PIPE_RM[i]} {tube.PIPE_ALMD[i]} {tube.PIPE_DL[i]} {tube.PIPE_ALOCSG[i]} {tube.PIPE_FMSG[i]}");
                        }
                    }
                }
                sw.WriteLine($"{tube.PIPE_ALFKSG} {tube.PIPE_POPSG} {tube.PIPE_JALFA}");
                if (tube.PIPE_TBX == null)
                {
                    sw.WriteLine($"{tube.PIPE_PVOL} {tube.PIPE_IPG} {tube.PIPE_IPB} {tube.PIPE_DP} {tube.PIPE_CBOL}");
                }
                else
                {
                    sw.WriteLine($"{tube.PIPE_PVOL} {tube.PIPE_TBYX} {tube.PIPE_TBX} {tube.PIPE_DP}");
                }
                sw.WriteLine("C");
            }
        } 
    }
}

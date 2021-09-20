using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    class WriteVolGasParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw, IFormatProvider formatter)
        {
            if (Elem.Type == "4")
            {
                VolGas volgas = (VolGas)Elem;
                sw.WriteLine($"{" "}{volgas.Name}               {"!"}{volgas.Description}               {"#"}{volgas.Number}");
                sw.WriteLine($"{volgas.Type}");
                sw.WriteLine($"{volgas.ELEM_VOLMLT}");
                sw.WriteLine($"{volgas.VolGas_VVOL} {volgas.VolGas_DZVOL} {volgas.VolGas_FTOVOL} {volgas.VolGas_SKDJ} {volgas.VolGas_AURKDJ}");
                if (double.Parse(volgas.VolGas_FTOVOL, formatter) > 0)
                {
                    sw.WriteLine($"{volgas.VolGas_CMET} {volgas.VolGas_GAMMET} {volgas.VolGas_DLMVOL} {volgas.VolGas_ALMET} {volgas.VolGas_KOCVOL} {volgas.VolGas_JNM}");
                }
                sw.WriteLine($"{volgas.VolGas_PVOL} {volgas.VolGas_VWKDJ} {volgas.VolGas_IVOL} {volgas.VolGas_TGKDJ} {volgas.VolGas_CBOL}");
            }
        }
    }
}

using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    class WriteChambParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw, IFormatProvider formatter)
        {
            if (Elem.Type == "1")
            {
                Chamb chamb = (Chamb)Elem;
                sw.WriteLine($"{" "}{chamb.Name}               {"!"}{chamb.Description}               {"#"}{chamb.Number}");
                sw.WriteLine($"{chamb.Type}");
                sw.WriteLine($"{chamb.ELEM_VOLMLT}");
                sw.WriteLine($"{chamb.CHAMB_VVOL} {chamb.CHAMB_FTOVOL} {chamb.CHAMB_DZVOL}");
                if (double.Parse(chamb.CHAMB_FTOVOL, formatter)>0)
                {
                    sw.WriteLine($"{chamb.CHAMB_CMVOL} {chamb.CHAMB_RMVOL} {chamb.CHAMB_DLVOL} {chamb.CHAMB_LAMBDA} {chamb.CHAMB_KOCVOL} {chamb.CHAMB_JNM}");
                }
                if (chamb.CHAMB_TETVOL == null)
                {
                    sw.WriteLine($"{chamb.CHAMB_PVOL} {chamb.CHAMB_PSVOL} {chamb.CHAMB_IVOL} {chamb.CHAMB_CBOL}");
                }
                else
                {
                    sw.WriteLine($"{chamb.CHAMB_PVOL} {chamb.CHAMB_TETVOL}");
                }
                sw.WriteLine($"{"C"}");
            }
        }
    }
}

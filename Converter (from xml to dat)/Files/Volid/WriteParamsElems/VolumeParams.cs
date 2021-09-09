using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    static class VolumeParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw)
        {
            if (Elem.Type == "0")
            {
                Volume volume = (Volume)Elem;
                //sw.WriteLine(volume.);
            }
        }
    }
}

using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    class WriteAZParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw, IFormatProvider formatter)
        {
            if (Elem.Type == "AZ_IN" || Elem.Type == "AZ_OUT")
            {
                sw.WriteLine($"{" "}{Elem.Name}               {"!"}{Elem.Description}               {"#"}{Elem.Number}");
                sw.WriteLine("C");
            }
        }
    }
}

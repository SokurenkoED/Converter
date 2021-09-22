using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Elpows.Elems
{
    class Pump : Elem
    {
        public string Number { get; set; }
        public string PUMP_TUREM { get; set; }
        public string PUMP_ELMNAME { get; set; }
        public string PUMP_ELMNUM { get; set; }
        public string PUMP_MJPUMP { get; set; }
    }
}

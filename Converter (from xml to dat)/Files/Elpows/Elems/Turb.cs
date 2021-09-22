using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Elpows.Elems
{
    class Turb : Elem
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string TURB_SHAFTNAME { get; set; }
        public string TURB_SHAFTNUM { get; set; }
        public string TURB_MJTUR { get; set; }
        public string TURB_MDIS1 { get; set; }
        public string TURB_MDIS2 { get; set; }
        public string TURB_MDIS3 { get; set; }
        public string TURB_RETUR { get; set; }
        public string TURB_FITUR { get; set; }
        public string TURB_NUTUR { get; set; }
        public string TURB_G0TUR { get; set; }
        public string TURB_GA0TUR { get; set; }
        public string TURB_ET0TUR { get; set; }
        public string TURB_OM0TUR { get; set; }
    }
}

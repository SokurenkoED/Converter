using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Upper.Elems
{
    class UppElem
    {
        public string UPP_AWC { get; set; }
        public string UPP_BWC { get; set; }
        public string UPP_AZA { get; set; }
        public string UPP_BZA { get; set; }
        public string UPP_VOUTR { get; set; }
        public string UPP_SK3 { get; set; }
        public string UPP_FOUTR { get; set; }
        public string UPP_DER1 { get; set; }
        public string UPP_DER2 { get; set; }
        public string UPP_CKRR { get; set; }
        public string UPP_GKRR { get; set; }
        public string UPP_LKRR { get; set; }
        public string UPP_AKKR { get; set; }
        public string UPP_AKOKR { get; set; }
        public string UPP_TOR { get; set; }
        public string UPP_PC { get; set; }
        public string UPP_TM { get; set; }

        public string UPP_JMIXUP { get; set; }
        public List<string> UPP_ALFA0 = new List<string>();
        public List<string> UPP_JMHOIN = new List<string>();
    }
}

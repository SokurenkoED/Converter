using System;
using System.Collections.Generic;
using System.Text;

namespace Converter__from_xml_to_dat_.Files.Kin_sp.Elems
{
    class GENERAL_DATA_SP
    {
        public string KIN7_PNKIN { get; set; }
        public string KIN7_SIST { get; set; }
        public string KIN7_NIST { get; set; }
        public string KIN7_NKIST { get; set; }
        public string KIN7_PNL { get; set; }
        public string KIN_JGRKIN { get; set; }
        public string KIN7_BETA0 { get; set; }
        public string KIN7_ALFCR { get; set; }

        public List<string> KIN_LM = new List<string>();

        public List<string> KIN_BE = new List<string>();

    }
}

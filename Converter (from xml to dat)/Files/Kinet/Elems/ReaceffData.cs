using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Kinet.Elems
{
    class ReaceffData
    {
        public string KIN_STEPFT { get; set; }
        public string KIN_STEPHT { get; set; }
        public string KIN_STEPHG { get; set; }
        public string KIN_ALFFT { get; set; }
        public string KIN_TFT0 { get; set; }

        public List<string> KIN_ARHT_ARG = new List<string>();

        public List<string> KIN_ARHT = new List<string>();

        public List<string> KIN_ARHTM_ARG = new List<string>();

        public List<string> KIN_ARHTM = new List<string>();

        public List<string> KIN_ARHG_ARG = new List<string>();

        public List<string> KIN_ARHG = new List<string>();

        public List<string> KIN_ARHCB_ARG = new List<string>();

        public List<string> KIN_ARHCB = new List<string>();

        public List<string> KIN_DKT_ARG = new List<string>();

        public List<string> KIN_DKT = new List<string>();
        public string KIN_DRONE0 { get; set; }
        public string KIN_DTNOM { get; set; }
        public string KIN_ALFCR { get; set; }

        public List<string> KIN_FKTF_ARG = new List<string>();

        public List<string> KIN_FKTF = new List<string>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Kinet.Elems
{
    class GeneralData
    {
        public string KIN_NN { get; set; }
        public string KIN_S0 { get; set; }
        public string KIN_PNL { get; set; }

        public List<string> KIN_LM = new List<string>();

        public List<string> KIN_BE = new List<string>();

        public List<string> KIN_BGAM = new List<string>();

        public List<string> KIN_BLAM = new List<string>();
        public string KIN_POWFIS { get; set; }

        public List<string> KIN_NETJOB_ARG = new List<string>();

        public List<string> KIN_NETJOB = new List<string>();
        public string KIN_TOST { get; set; }

    }
}

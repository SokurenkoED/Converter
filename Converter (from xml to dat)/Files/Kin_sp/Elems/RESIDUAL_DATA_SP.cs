using System;
using System.Collections.Generic;
using System.Text;

namespace Converter__from_xml_to_dat_.Files.Kin_sp.Elems
{
    class RESIDUAL_DATA_SP
    {
        public string KIN7_JGROST { get; set; }

        public List<string> KIN7_BGAM = new List<string>();

        public List<string> KIN7_BLAM = new List<string>();
        public string KIN7_POWFIS { get; set; }
        public string KIN7_JNJOB { get; set; }

        public List<string> KIN7_NETJOB_ARG = new List<string>();

        public List<string> KIN7_NETJOB = new List<string>();
    }
}

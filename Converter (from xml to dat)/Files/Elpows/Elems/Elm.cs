using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Elpows.Elems
{
    class Elm
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string ELM_JMAC { get; set; }
        public string ELM_NETNAME { get; set; }
        public string ELM_NETNUM { get; set; }
        public string ELM_MJMAC { get; set; }
        public string ELM_M1MAC { get; set; }
        public string ELM_M2MAC { get; set; }
        public string ELM_M3MAC { get; set; }
        public string ELM_OMELMA { get; set; }
        public string ELM_JVFMAC { get; set; }

        public List<string> ELM_VFMAC_ARG = new List<string>();

        public List<string> ELM_VFMAC = new List<string>();

    }
}

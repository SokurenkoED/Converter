using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Canent.Elems
{
    class FuelrodCore
    {
        public string CORE_TVEL { get; set; }
        public string CORE_EPSTF { get; set; }
        public string CORE_JVVOD { get; set; }
        public string CORE_RT1 { get; set; }
        public string CORE_RT2 { get; set; }
        public string CORE_GAT { get; set; }
        public string CORE_DELST { get; set; }

        public List<string> CORE_CTTAB_ARG = new List<string>();
        public List<string> CORE_CTTAB = new List<string>();

        public List<string> CORE_LTTAB_ARG = new List<string>();
        public List<string> CORE_LTTAB = new List<string>();

        public List<string> CORE_PTVL = new List<string>();
        public List<string> CORE_AMGTVL = new List<string>();
        public List<string> CORE_RGTVL = new List<string>();
        public List<string> CORE_VGSTVL = new List<string>();
        public List<string> CORE_FTTVL = new List<string>();

        public string CORE_D0ZAZ { get; set; }

        public List<string> CORE_LGTAB_ARG = new List<string>();
        public List<string> CORE_LGTAB = new List<string>();

        public string CORE_RIO { get; set; }
        public string CORE_ROO { get; set; }
        public string CORE_GAOB { get; set; }
        public string CORE_DGZAZ { get; set; }
        public string CORE_AOZAZ { get; set; }

        public List<string> CORE_COTAB_ARG = new List<string>();
        public List<string> CORE_COTAB = new List<string>();

        public List<string> CORE_LOTAB_ARG = new List<string>();
        public List<string> CORE_LOTAB = new List<string>();
    }
}

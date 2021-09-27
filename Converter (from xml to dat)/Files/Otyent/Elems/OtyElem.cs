using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Otyent.Elems
{
    class OtyElem
    {
        public string OTY_JOMEOT { get; set; }
        public string OTY_POPRBO { get; set; }
        public string OTY_SOTY { get; set; }
        public string OTY_DOTY { get; set; }
        public string OTY_AKSI { get; set; }
        public string OTY_SHEROT { get; set; }

        public List<string> OTY_VO = new List<string>();
        public List<string> OTY_HO = new List<string>();
        public List<string> OTY_KSIMO = new List<string>();

        public string OTY_PMOTY { get; set; }
        public string OTY_CMO { get; set; }
        public string OTY_RMO { get; set; }
        public string OTY_DELTMO { get; set; }
        public string OTY_ALMO { get; set; }

        public string OTY_JGROTY { get; set; }

        public List<string> OTY_JMCOUT = new List<string>();
    }
}

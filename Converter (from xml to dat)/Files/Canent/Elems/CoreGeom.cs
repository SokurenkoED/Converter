using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Canent.Elems
{
    class CoreGeom
    {
        public string CORE_K2 { get; set; }

        public List<string> CORE_VC = new List<string>();

        public List<string> CORE_SC = new List<string>();

        public List<string> CORE_DC = new List<string>();

        public List<string> CORE_KSIM = new List<string>();

        public List<string> CORE_SHER = new List<string>();

        public List<string> CORE_JV2 = new List<string>();
    }
}

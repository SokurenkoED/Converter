using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Canent.Elems
{
    class SeparateCore
    {
        public string CORE_JRC { get; set; }
        public string CORE_JRCTIP { get; set; }
        public string CORE_KSIMJ { get; set; }
        public string CORE_KSI { get; set; }
        public string CORE_KV1 { get; set; }

        public List<string> CORE_KV2 = new List<string>();
    }
}

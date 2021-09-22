using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Elpows.Elems
{
    class Net : Elem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string NET_JSOUR { get; set; }
        public string NET_OMNET { get; set; }

        public List<string> NET_PSOUR_ARG = new List<string>();
        public List<string> NET_PSOUR = new List<string>();
    }
}

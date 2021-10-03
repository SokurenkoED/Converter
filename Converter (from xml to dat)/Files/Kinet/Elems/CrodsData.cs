using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Kinet.Elems
{
    class CrodsData
    {
        public string KIN_ASUOR { get; set; }
        public string KIN_ASUHRO0 { get; set; }

        public List<string> KIN_DKGRUP_ARG = new List<string>();

        public List<string> KIN_DKGRUP = new List<string>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Gidr2k.Junctions
{
    class Dep : Jun
    {
        public Dep(string name) : base(name)
        {
        }

        public string JUN_AJNMLT { get; set; }
        public string JUN_VJ { get; set; }
        public string JUN_JPUG2K { get; set; }
        public string JUN_VLVNAM { get; set; }
        public string JUN_JCRFLJ { get; set; }
        public string JUN_JOBR { get; set; }
        public string JUN_HJ1 { get; set; }
        public string JUN_HJ2 { get; set; }
        public string JUN_JJNPAR { get; set; }
        public string JUN_JJNT { get; set; }

        public List<string> JUN_KC2KT_ARG = new List<string>();
        public List<string> JUN_KC2KT = new List<string>();
    }
}

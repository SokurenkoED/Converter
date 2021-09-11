using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Gidr2k.Junctions
{
    class Jun
    {
        public Jun(string name)
        {
            this.Name = name;
        }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string JUN_FROM { get; set; }
        public string JUN_TO { get; set; }

    }
}

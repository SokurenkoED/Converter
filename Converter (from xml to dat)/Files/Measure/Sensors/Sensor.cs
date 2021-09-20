using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Measure.Sensors
{
    class Sensor
    {
        public string Discription { get; set; }
        public string Name { get; set; }
        public string SENS_NameMP { get; set; }
        public string SENS_NameEq { get; set; }
        public string SENS_MPARAM { get; set; }
        public string SENS_LPARAM { get; set; }
        public string SENS_WPARAM { get; set; }
        public string SENS_TAU { get; set; }
        public string SENS_KUS { get; set; }
        public string SENS_JTAUN { get; set; }

        public List<string> DEP_PSOUR_ARG = new List<string>();
        public List<string> DEP_PSOUR = new List<string>();

    }
}

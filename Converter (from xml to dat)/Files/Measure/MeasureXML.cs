using Converter__from_xml_to_dat_.Files.Measure.Functions;
using Converter__from_xml_to_dat_.Files.Measure.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Measure
{
    class MeasureXML
    {
        XDocument xdoc = XDocument.Load("measure.xml");
        List<Sensor> Sensors = new List<Sensor>(); // Массив из всех датчиков

        public MeasureXML()
        {

            ReadParamsFromFile.ReadFile(xdoc, ref Sensors);

            WriteParamsToFile.WriteFile(ref Sensors);

        }
    }
}

using Converter__from_xml_to_dat_.Files.Measure.Sensors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Measure.Functions
{
    static class WriteParamsToFile
    {
        public static void WriteFile(ref List<Sensor> Sensors)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/measure.dat", false, Encoding.Default))
            {
                foreach (var item in Sensors)
                {
                    WriteParamsFromSensor(sw, item);
                }
                sw.WriteLine("endmeas");
            }
        }

        private static void WriteParamsFromSensor(StreamWriter sw, Sensor sensor)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            sw.Write($" {sensor.Name} {sensor.SENS_NameMP} {sensor.SENS_NameEq} {sensor.SENS_MPARAM} {sensor.SENS_LPARAM} {sensor.SENS_WPARAM} {sensor.SENS_TAU} {sensor.SENS_KUS}");
            if (sensor.Discription != " ")
            {
                sw.WriteLine($" {"/"}{sensor.Discription}");
            }
            else
            {
                sw.WriteLine();
            }
            if (sensor.SENS_JTAUN != null)
            {
                sw.WriteLine($"{sensor.SENS_JTAUN}");
                foreach (var item in sensor.DEP_PSOUR_ARG)
                {
                    sw.Write($"{item} ");
                }
                sw.WriteLine();
                foreach (var item in sensor.DEP_PSOUR)
                {
                    sw.Write($"{item} ");
                }
                sw.WriteLine();
            }
        }
    }
}
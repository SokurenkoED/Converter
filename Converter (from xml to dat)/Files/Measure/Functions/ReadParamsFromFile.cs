using Converter__from_xml_to_dat_.Files.Measure.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Measure.Functions
{
    static class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref List<Sensor> Sensors)
        {
            SetSensors(xdoc, ref Sensors);
        }

        public static void SetSensors(XDocument xdoc, ref List<Sensor> Sensors)
        {
            foreach (XElement Sens in xdoc.Element("GENERAL_SENS").Elements("SENS_NAME"))
            {
                Sensor sens = null;
                
                sens = SetParamsToSensor(xdoc, Sens);

                Sensors.Add(sens);
            }
        }

        private static Sensor SetParamsToSensor(XDocument xdoc, XElement Strctrs)
        {
            Sensor Sens = new Sensor();
            XAttribute NameAtr = Strctrs.Attribute("Value");
            Sens.Name = NameAtr.Value;
            foreach (var item in Strctrs.Descendants("SENS_NameMP"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_NameMP = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_NameEq"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_NameEq = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_MPARAM"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_MPARAM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_LPARAM"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_LPARAM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_WPARAM"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_WPARAM = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_TAU"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_TAU = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_KUS"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_KUS = Attr.Value;
            }
            foreach (var item in Strctrs.Descendants("SENS_PROP"))
            {
                XAttribute Attr = item.Attribute("Description");
                if (Attr != null)
                {
                    Sens.Discription = Attr.Value;
                }
            }
            foreach (var item in Strctrs.Descendants("SENS_JTAUN"))
            {
                XAttribute Attr = item.Attribute("Value");
                Sens.SENS_JTAUN = Attr.Value;
            }
            foreach (XElement Param in Strctrs.Descendants("DEP_PSOUR_ARG"))
            {
                XAttribute Attr = Param.Attribute("Value");
                Sens.DEP_PSOUR_ARG.Add(Attr.Value);
            }
            foreach (XElement Param in Strctrs.Descendants("DEP_PSOUR"))
            {
                XAttribute Attr = Param.Attribute("Value");
                Sens.DEP_PSOUR.Add(Attr.Value);
            }
            return Sens;
        }
    }
}

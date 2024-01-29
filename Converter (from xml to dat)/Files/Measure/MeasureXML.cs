using Converter__from_xml_to_dat_.Files.Measure.Functions;
using Converter__from_xml_to_dat_.Files.Measure.Sensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Measure
{
    class MeasureXML
    {
        XDocument xdoc;
        List<Sensor> Sensors = new List<Sensor>(); // Массив из всех датчиков

        public MeasureXML()
        {
            try
            {
                FileInfo file = new FileInfo("measure.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл measure.xml пустой.");
                    return;
                }

                xdoc = XDocument.Load("measure.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref Sensors);

                WriteParamsToFile.WriteFile(ref Sensors);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл measure.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл measure.xml. Неверный формат записи");
            }
        }
    }
}

using Converter__from_xml_to_dat_.Files.Asuval.Elems;
using Converter__from_xml_to_dat_.Files.Asuval.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuval
{
    class AsuvalXML
    {
        XDocument xdoc;
        List<Valve> Valves = new List<Valve>();
        public AsuvalXML()
        {
            try
            {
                FileInfo file = new FileInfo("asuval.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл asuval.xml пустой.");
                    return;
                }

                xdoc = XDocument.Load("asuval.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref Valves);

                WriteParamsToFile.WriteFile(ref Valves);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Asuval.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Asuval.xml. Неверный формат записи");
            }
        }
    }
}

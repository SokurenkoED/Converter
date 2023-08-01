using Converter__from_xml_to_dat_.Files.Hstr.Functions;
using Converter__from_xml_to_dat_.Files.Hstr.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Hstr
{
    class HstrXML
    {
        XDocument xdoc;
        List<Structure> Structures = new List<Structure>(); // Массив из всех структур


        public HstrXML()
        {
            try
            {
                xdoc = XDocument.Load("hstr.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref Structures);

                WriteParamsToFile.WriteFile(ref Structures);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл hstr.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл hstr.xml. Неверный формат записи");
            }
        }
    }
}

using Converter__from_xml_to_dat_.Files.Upper.Functions;
using Converter__from_xml_to_dat_.Files.Upper.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace Converter__from_xml_to_dat_.Files.Upper
{
    class UpperXML
    {
        XDocument xdoc;
        UppElem Upp = new UppElem();
        public UpperXML()
        {
            try
            {
                xdoc = XDocument.Load("Upper.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref Upp);

                WriteParamsToFile.WriteFile(ref Upp);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Upper.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Upper.xml. Неверный формат записи");
            }
        }
    }
}

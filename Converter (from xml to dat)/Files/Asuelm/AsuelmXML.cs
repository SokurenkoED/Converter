using Converter__from_xml_to_dat_.Files.Asuelm.Elems;
using Converter__from_xml_to_dat_.Files.Asuelm.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuelm
{
    class AsuelmXML
    {
        XDocument xdoc;
        List<Elm> elms = new List<Elm>();
        public AsuelmXML()
        {
            try
            {
                FileInfo file = new FileInfo("asuelm.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл asuelm.xml пустой.");
                    return;
                }

                xdoc = XDocument.Load("asuelm.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref elms);

                WriteParamsToFile.WriteFile(ref elms);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Asuelm.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Asuelm.xml. Неверный формат записи");
            }
        }
    }
}

using Converter__from_xml_to_dat_.Files.Otyent.Functions;
using Converter__from_xml_to_dat_.Files.Otyent.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Otyent
{
    class OtyentXML
    {
        XDocument xdoc;
        OtyElem OTY = new OtyElem();
        public OtyentXML()
        {
            try
            {
                FileInfo file = new FileInfo("Otyent.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл Otyent.xml пустой.");
                    return;
                }

                xdoc = XDocument.Load("Otyent.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref OTY);

                WriteParamsToFile.WriteFile(ref OTY);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Otyent.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Otyent.xml. Неверный формат записи");
            }
        }
    }
}

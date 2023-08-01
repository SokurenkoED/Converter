using Converter__from_xml_to_dat_.Files.Kinet.Elems;
using Converter__from_xml_to_dat_.Files.Kinet.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Kinet
{
    class KinetXML
    {
        XDocument xdoc;
        List<CrodsData> CDs = new List<CrodsData>();
        GeneralData GD = new GeneralData();
        ReaceffData RD = new ReaceffData();
        public KinetXML()
        {
            try
            {
                xdoc = XDocument.Load("kinet.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref CDs, ref GD, ref RD);

                WriteParamsToFile.WriteFile(ref CDs, ref GD, ref RD);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Kinet.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Kinet.xml. Неверный формат записи");
            }
        }
    }
}

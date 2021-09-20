using Converter__from_xml_to_dat_.Files.Gidr2k.Functions;
using Converter__from_xml_to_dat_.Files.Gidr2k.HomolChar;
using Converter__from_xml_to_dat_.Files.Gidr2k.Junctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files
{
    class Gidr2kXML
    {
        XDocument xdoc = XDocument.Load("gidr2k.xml");
        List<Jun> Juns = new List<Jun>(); // Массив из всех связей
        List<Homol> Homols = new List<Homol>(); // Гомологическая характеристика
        List<string> LastParams = new List<string>();

        public Gidr2kXML()
        {
            ReadParamsFromFile.ReadFIle(ref Juns, ref Homols,ref LastParams, xdoc);

            WriteParamsToFile.WriteFile(ref Juns, ref Homols, ref LastParams);

        }
    }
}

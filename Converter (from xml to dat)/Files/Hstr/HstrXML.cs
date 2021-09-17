using Converter__from_xml_to_dat_.Files.Hstr.Functions;
using Converter__from_xml_to_dat_.Files.Hstr.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Hstr
{
    class HstrXML
    {
        XDocument xdoc = XDocument.Load("hstr.xml");
        List<Structure> Structures = new List<Structure>(); // Массив из всех структур


        public HstrXML()
        {

            ReadParamsFromFile.ReadFile(xdoc, ref Structures);

            WriteParamsToFile.WriteFile(ref Structures);

        }
    }
}

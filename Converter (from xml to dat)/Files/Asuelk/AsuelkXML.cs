using Converter__from_xml_to_dat_.Files.Asuelk.Elems;
using Converter__from_xml_to_dat_.Files.Asuelk.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuelk
{
    class AsuelkXML
    {
        XDocument xdoc;
        List<ELL> ELLs = new List<ELL>();
        public AsuelkXML()
        {
            try
            {
                xdoc = XDocument.Load("asuelk.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref ELLs);

                WriteParamsToFile.WriteFile(ref ELLs);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Asuelk.xml не был найден");
            }
        }
    }
}

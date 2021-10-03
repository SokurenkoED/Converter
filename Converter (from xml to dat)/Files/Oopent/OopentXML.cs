using Converter__from_xml_to_dat_.Files.Oopent.Functions;
using Converter__from_xml_to_dat_.Files.Oopent.Elems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Oopent
{
    class OopentXML
    {
        XDocument xdoc;
        OopElem OOU = new OopElem();
        public OopentXML()
        {
            try
            {
                xdoc = XDocument.Load("Oopent.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref OOU);

                WriteParamsToFile.WriteFile(ref OOU);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Oopent.xml не был найден");
            }
        }
    }
}

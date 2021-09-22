using Converter__from_xml_to_dat_.Files.Elpows.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Elpows
{
    class ElpowsXML
    {
        XDocument xdoc;
        public ElpowsXML()
        {
            try
            {
                xdoc = XDocument.Load("elpows.xml");

                //ReadParamsFromFile.ReadFIle(ref Juns, ref Homols, ref LastParams, xdoc);

                //WriteParamsToFile.WriteFile(ref Juns, ref Homols, ref LastParams);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Elpows.xml не был найден");
            }
        }
    }
}

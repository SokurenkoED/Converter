using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuval
{
    class AsuvalXML
    {
        XDocument xdoc;
        public AsuvalXML()
        {
            try
            {
                xdoc = XDocument.Load("asuval.xml");

                //ReadParamsFromFile.ReadFIle(ref Juns, ref Homols, ref LastParams, xdoc);

                //WriteParamsToFile.WriteFile(ref Juns, ref Homols, ref LastParams);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Asuval.xml не был найден");
            }
        }
    }
}

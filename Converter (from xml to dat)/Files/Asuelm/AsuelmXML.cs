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
        public AsuelmXML()
        {
            try
            {
                xdoc = XDocument.Load("asuelm.xml");

                //ReadParamsFromFile.ReadFIle(ref Juns, ref Homols, ref LastParams, xdoc);

                //WriteParamsToFile.WriteFile(ref Juns, ref Homols, ref LastParams);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Asuelm.xml не был найден");
            }
        }
    }
}

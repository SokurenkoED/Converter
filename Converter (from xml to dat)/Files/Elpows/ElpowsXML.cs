using Converter__from_xml_to_dat_.Files.Elpows.Elems;
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
        List<Elg> EG = new List<Elg>();
        List<Elm> EM = new List<Elm>();
        List<Net> NT = new List<Net>();
        List<Pump> PMP = new List<Pump>();
        List<Shaft> Shft = new List<Shaft>();
        List<Turb> TB = new List<Turb>();
        public ElpowsXML()
        {
            try
            {
                xdoc = XDocument.Load("elpows.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref EG, ref EM, ref NT, ref PMP, ref Shft, ref TB );

                WriteParamsToFile.WriteFile(ref EG, ref EM, ref NT, ref PMP, ref Shft, ref TB);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Elpows.xml не был найден");
            }
        }
    }
}

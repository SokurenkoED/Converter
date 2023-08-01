using Converter__from_xml_to_dat_.Files.Canent.Elems;
using Converter__from_xml_to_dat_.Files.Canent.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Canent
{
    class CanentXML
    {
        XDocument xdoc;

        GeneralCore GC = new GeneralCore();
        List<SeparateCore> SCs = new List<SeparateCore>();
        ModlimitCore MC = new ModlimitCore();
        FaCore FaC = new FaCore();
        UnheatCore UC = new UnheatCore();
        CoreGengeom CG = new CoreGengeom();
        CoreGeom CGeom = new CoreGeom();
        List<FuelrodCore> FCs = new List<FuelrodCore>();
        StrmatFaCore SFC = new StrmatFaCore();
        StrmatUnheatCore SUC = new StrmatUnheatCore();
        CoreCross CC = new CoreCross();
        CoreTFT CTFT = new CoreTFT();


        public CanentXML()
        {
            try
            {
                xdoc = XDocument.Load("canent.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref GC, ref SCs, ref MC, ref FaC, ref UC, ref CG, ref CGeom, ref FCs, ref SFC, ref SUC, ref CC, ref CTFT);

                WriteParamsToFile.WriteFile(ref GC, ref SCs, ref MC, ref FaC, ref UC, ref CG, ref CGeom, ref FCs, ref SFC, ref SUC, ref CC, ref CTFT);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Canent.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Canent.xml. Неверный формат записи");
            }
        }
    }
}

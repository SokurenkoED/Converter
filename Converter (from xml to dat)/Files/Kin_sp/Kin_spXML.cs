using Converter__from_xml_to_dat_.Files.Kin_sp.Elems;
using Converter__from_xml_to_dat_.Files.Kin_sp.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Kin_sp
{
    class Kin_spXML
    {
        XDocument xdoc;
        GENERAL_DATA_SP GD = new GENERAL_DATA_SP();
        INT_PARAM_SP IP = new INT_PARAM_SP();
        RESIDUAL_DATA_SP RD = new RESIDUAL_DATA_SP();
        CRODS_DATA_SP CD = new CRODS_DATA_SP();


        public Kin_spXML()
        {
            try
            {
                xdoc = XDocument.Load("kin_sp.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref GD, ref IP, ref RD, ref CD);

                WriteParamsToFile.WriteFile(ref GD, ref IP, ref RD, ref CD);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл kin_sp.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл kin_sp.xml. Неверный формат записи");
            }
        }
    }
}

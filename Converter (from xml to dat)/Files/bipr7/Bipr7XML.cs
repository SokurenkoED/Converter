using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter__from_xml_to_dat_.Files.bipr7
{
    internal class Bipr7XML
    {
        public Bipr7XML() 
        {
			try
			{

                FileInfo file = new FileInfo("Bipr7.dat");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл Bipr7.dat пустой.");
                    return;
                }

                if (!File.Exists("OldFormat-TIGR/Bipr7.dat"))
                {
                    File.Copy("Bipr7.dat", "OldFormat-TIGR/Bipr7.dat");
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Bipr7.dat не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Bipr7.dat. Неверный формат записи");
            }
        }

    }
}

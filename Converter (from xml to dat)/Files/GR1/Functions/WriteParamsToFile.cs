using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter__from_xml_to_dat_.Files.GR1.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile()
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/GR1.dat", false, Encoding.Default))
            {
                sw.WriteLine(" 0 0");
                sw.WriteLine(" 0 0 0");
                sw.WriteLine(" -2.001");
            }
        }
    }
}

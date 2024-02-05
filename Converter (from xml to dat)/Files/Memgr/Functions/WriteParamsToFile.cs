using Converter__from_xml_to_dat_.Files.Canent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter__from_xml_to_dat_.Files.Memgr.Functions
{
    class WriteParamsToFile
    {
        public static void WriteFile(CanentXML Canent)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/memgr.dat", false, Encoding.Default))
            {
                if (Canent.GC.CORE_NNOM != null)
                {
                    sw.WriteLine(" 0 0 0 ");
                    sw.WriteLine(" 0 ");
                }
                sw.WriteLine(" YES YES");
                sw.WriteLine(" 0");
                sw.WriteLine(" AFULLI AFULLM");
            }
        }
    }
}

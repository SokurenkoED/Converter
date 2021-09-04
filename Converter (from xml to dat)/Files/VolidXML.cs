using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files
{
    class VolidXML
    {
        public VolidXML()
        {
            try
            {
                XDocument xdoc = XDocument.Load("volid.xml");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Volid.xml не был найден");
            }


            List<string>[] kavo = new List<string>[2];
            List<string> wo = new List<string>();
            wo.Add("Один");
            List<string> mmm = new List<string>();
            mmm.Add("Два");
            kavo[0] = wo;
            kavo[1] = mmm;
        }
    }
}

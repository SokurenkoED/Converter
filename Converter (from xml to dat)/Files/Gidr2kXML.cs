using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files
{
    class Gidr2kXML
    {
        public Gidr2kXML()
        {
            XDocument xdoc = XDocument.Load("gidr2k.xml");
        }
    }
}

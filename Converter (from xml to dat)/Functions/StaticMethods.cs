using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Functions
{
    static class StaticMethods
    {
        /// <summary>
        /// Возвращает значение value у родительского узла высшего уровня (Например число контуров в файле volid)
        /// </summary>
        /// <returns></returns>
        public static string GetFirstCountFile(XDocument xdoc, string Elements, string nameAttribute)
        {
            string Value = null;
            foreach (XElement Conts in xdoc.Elements(Elements))
            {
                XAttribute Attribute = Conts.Attribute(nameAttribute);

                Value = Attribute.Value;
            }
            return Value;
        }

    }
}

using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid.ReadParamsElems
{
    class DepParams
    {
        public static void ReadParams(ref Elems Elem, XElement Elems)
        {
            if (Elem.Type == "0")
            {
                Dep dep = (Dep)Elem;
                foreach (XElement VOLMLT in Elems.Descendants("DEP_JJARG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_JJARG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_JPVT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_JPVT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_JPSVT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_JPSVT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_JIVT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_JIVT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_JCBVT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_JCBVT = AttributeValue.Value;
                }

                foreach (XElement VOLMLT in Elems.Descendants("DEP_PVOLT_ARG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_PVOLT_ARG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_PVOLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_PVOLT.Add(AttributeValue.Value);
                }

                foreach (XElement VOLMLT in Elems.Descendants("DEP_PSVOLT_ARG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_PSVOLT_ARG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_PSVOLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_PSVOLT.Add(AttributeValue.Value);
                }

                foreach (XElement VOLMLT in Elems.Descendants("DEP_IVOLT_ARG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_IVOLT_ARG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_IVOLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_IVOLT.Add(AttributeValue.Value);
                }

                foreach (XElement VOLMLT in Elems.Descendants("DEP_CBVOLT_ARG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_CBVOLT_ARG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Descendants("DEP_CBVOLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    dep.DEP_CBVOLT.Add(AttributeValue.Value);
                }
                Elem = dep;
            }
        }
    }
}

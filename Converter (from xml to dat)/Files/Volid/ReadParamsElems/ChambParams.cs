using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid
{
    static class ChambParams
    {
        /// <summary>
        /// Записывает массив с параметрами эелмента "Камера смешения"
        /// </summary>
        /// <param name="Elem"></param>
        /// <param name="Elems"></param>
        public static void ReadParams(ref Elems Elem, XElement Elems)
        {
            if (Elem.Type == "1")
            {
                Chamb chamb = (Chamb)Elem;
                foreach (XElement VOLMLT in Elems.Element("GENERAL_CHAMB").Elements("ELEM_VOLMLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.ELEM_VOLMLT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_CHAMB").Elements("CHAMB_VVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_VVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_CHAMB").Elements("CHAMB_DZVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_DZVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_CHAMB").Elements("CHAMB_FTOVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_FTOVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("CHAMB_CMVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_CMVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("CHAMB_RMVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_RMVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("CHAMB_DLVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_DLVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("CHAMB_LAMBDA"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_LAMBDA = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("CHAMB_KOCVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_KOCVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("CHAMB_JNM"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_JNM = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("INITDATA_CHAMB").Elements("CHAMB_PVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_PVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants().Elements("CHAMB_PSVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_PSVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("INITDATA_CHAMB").Elements("CHAMB_IVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_IVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("INITDATA_CHAMB").Elements("CHAMB_CBOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_CBOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Element("INITDATA_CHAMB").Elements("CHAMB_TETVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    chamb.CHAMB_TETVOL = AttributeValue.Value;
                }
                Elem = chamb;
            }
        }
    }
}

using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid.ReadParamsElems
{
    class VolGasParams
    {
        public static void ReadParams(ref Elems Elem, XElement Elems)
        {
            if (Elem.Type == "4")
            {
                VolGas volgas = (VolGas)Elem;
                foreach (XElement VOLMLT in Elems.Descendants("ELEM_VOLMLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.ELEM_VOLMLT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_AURKDJ"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_AURKDJ = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_VVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_VVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_DZVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_DZVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_SKDJ"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_SKDJ = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_FTOVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_FTOVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_JNM"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_JNM = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_CMET"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_CMET = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_GAMMET"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_GAMMET = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_DLMVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_DLMVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_ALMET"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_ALMET = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_KOCVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_KOCVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_PVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_PVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_IVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_IVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_TGKDJ"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_TGKDJ = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_VWKDJ"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_VWKDJ = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VolGas_CBOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volgas.VolGas_CBOL = AttributeValue.Value;
                }
                Elem = volgas;
            }
        }
    }
}

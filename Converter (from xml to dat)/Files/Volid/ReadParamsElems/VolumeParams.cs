using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid.ReadParamsElems
{
    static class VolumeParams
    {
        public static void ReadParams(ref Elems Elem, XElement Elems)
        {
            if (Elem.Type == "3" || Elem.Type == "31")
            {
                Volume volume = (Volume)Elem;

                foreach (XElement VOLMLT in Elems.Descendants("ELEM_VOLMLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.ELEM_VOLMLT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_KGVSP"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_KGVSP = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_JTRCON"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_JTRCON = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_DGSGCN"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_DGSGCN = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_DGWSCN"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_DGWSCN = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_PSGCON"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_PSGCON = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_PWSCON"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_PWSCON = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_JVTAB"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_JVTAB = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_TABH"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_TABH.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_TABV"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_TABV.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_JNMG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_JNMG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_CMSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_CMSG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_RMSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_RMSG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_DLSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_DLSG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_LMBDG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_LMBDG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_KOCSGC"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_KOCSGC = AttributeValue.Value;
                }

                foreach (XElement VOLMLT in Elems.Descendants("VOL_JNMW"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_JNMW = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_CMWS"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_CMWS = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_RMWS"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_RMWS = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_DLWS"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_DLWS = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_LMBDW"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_LMBDW = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_KOCVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_KOCVOL = AttributeValue.Value;
                }

                foreach (XElement VOLMLT in Elems.Descendants("VOL_PVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_PVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_PSVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_PSVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_ISG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_ISG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_IVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_IVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_HL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_HL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("VOL_CBVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    volume.VOL_CBVOL = AttributeValue.Value;
                }
                Elem = volume;
            }
        }
    }
}

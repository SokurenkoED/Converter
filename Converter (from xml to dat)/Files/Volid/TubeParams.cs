using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid
{
    static class TubeParams
    {
        public static void ReadParams(ref Elems Elem, XElement Elems)
        {
            if (Elem.Type == "2")
            {
                Tube tube = (Tube)Elem;
                foreach (XElement VOLMLT in Elems.Descendants("ELEM_VOLMLT"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.ELEM_VOLMLT = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_JMACRO"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_JMACRO = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_JMACRV"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_JMACRV = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_ALFKSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_ALFKSG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_POPSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_POPSG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_JALFA"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_JALFA = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_JNEGOM"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_JNEGOM = AttributeValue.Value;
                }

                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_V2SG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_V2SG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_S2SG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_S2SG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_DGSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_DGSG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_AL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_AL.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_DZSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_DZSG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_AKSSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_AKSSG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_SHERSG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_SHERSG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_INMPG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_INMPG.Add(AttributeValue.Value);
                }
                foreach (XElement VOLMLT in Elems.Element("GEOM_PIPE").Elements("PIPE_JMACRO_N").Descendants("PIPE_JV"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_JV.Add(AttributeValue.Value);
                }
                if (tube.PIPE_JMACRV != "0")
                {
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_JVV"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_JVV.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_JNM"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_JNM.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_CM"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_CM.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_RM"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_RM.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_DL"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_DL.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_ALMD"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_ALMD.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_ALOCSG"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_ALOCSG.Add(AttributeValue.Value);
                    }
                    foreach (XElement VOLMLT in Elems.Element("STRMAT_PIPE").Elements("PIPE_JMACRV_N").Descendants("PIPE_FMSG"))
                    {
                        XAttribute AttributeValue = VOLMLT.Attribute("Value");
                        tube.PIPE_FMSG.Add(AttributeValue.Value);
                    }
                }

                foreach (XElement VOLMLT in Elems.Descendants("PIPE_PVOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_PVOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_DP"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_DP = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_IPG"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_IPG = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_IPB"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_IPB = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_CBOL"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_CBOL = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_TBX"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_TBX = AttributeValue.Value;
                }
                foreach (XElement VOLMLT in Elems.Descendants("PIPE_TBYX"))
                {
                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                    tube.PIPE_TBYX = AttributeValue.Value;
                }
                Elem = tube;
            }
        }
    }
}

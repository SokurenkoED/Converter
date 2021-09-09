using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Converter__from_xml_to_dat_.Files.VolidXML;

namespace Converter__from_xml_to_dat_.Functions
{
    static class StaticMethods
    {
        /// <summary>
        /// Определили тип элемента, записали его номер и описание
        /// </summary>
        /// <param name="Elems"></param>
        /// <param name="Elem"></param>
        public static void SetTypeOfElem(XElement Elems, ref Elems Elem, XAttribute AttrValue)
        {
            if (AttrValue.Value == "AZ_IN" || AttrValue.Value == "AZ_OUT")
            {
                XAttribute AttributeValueFromDiscr = Elems.Attribute("Discription");
                XAttribute AttributeValueFromNumb = Elems.Attribute("Numb");
                Elem.Type = AttrValue.Value;
                Elem.Description = AttributeValueFromDiscr.Value;
                Elem.Number = AttributeValueFromNumb.Value;
            }
            foreach (XElement Elem_Type in Elems.Descendants("ELEM_TYPE"))
            {
                
                XAttribute AttributeValue = Elem_Type.Attribute("Value");

                foreach (XElement Elem_Prop in Elems.Elements("ELEM_PROP"))
                {
                    XAttribute AttributeNumb = Elem_Prop.Attribute("Numb");
                    XAttribute AttributeDescription = Elem_Prop.Attribute("Description");
                    if (AttributeValue.Value == "1")
                    {
                        if (AttributeDescription != null)
                        {
                            Elem = new Chamb(AttributeNumb.Value, AttributeDescription.Value);
                        }
                        else
                        {
                            Elem = new Chamb(AttributeNumb.Value);
                        }
                    }
                    else if (AttributeValue.Value == "2" || AttributeValue.Value == "5")
                    {
                        if (AttributeDescription != null)
                        {
                            Elem = new Tube(AttributeNumb.Value, AttributeDescription.Value, AttributeValue.Value);
                        }
                        else
                        {
                            Elem = new Tube(AttributeNumb.Value, AttributeValue.Value);
                        }
                    }
                    else if (AttributeValue.Value == "3" || AttributeValue.Value == "31")
                    {
                        if (AttributeDescription != null)
                        {
                            Elem = new Volume(AttributeNumb.Value, AttributeDescription.Value, AttributeValue.Value);
                        }
                        else
                        {
                            Elem = new Volume(AttributeNumb.Value, AttributeValue.Value);
                        }
                    }
                    else if (AttributeValue.Value == "0")
                    {
                        if (AttributeDescription != null)
                        {
                            Elem = new Dep(AttributeNumb.Value, AttributeDescription.Value, AttributeValue.Value);
                        }
                        else
                        {
                            Elem = new Dep(AttributeNumb.Value, AttributeValue.Value);
                        }
                    }
                }
            }
        }
    }
}

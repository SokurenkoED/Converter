using Converter__from_xml_to_dat_.ElemsOfVolid;
using Converter__from_xml_to_dat_.Files.Volid;
using Converter__from_xml_to_dat_.Files.Volid.ReadParamsElems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files
{
    class VolidXML
    {
        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        /// <summary>
        /// Определили тип элемента, записали его номер и описание
        /// </summary>
        /// <param name="Elems"></param>
        /// <param name="Elem"></param>
        private void SetTypeOfElem(XElement Elems,ref Elems Elem)
        {
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
        public VolidXML()
        {
            try
            {
                XDocument xdoc = XDocument.Load("volid.xml");
                List<Cont> Conts = new List<Cont>();

                foreach (XElement ContNode in xdoc.Element("JCNTR").Elements("CONT"))
                {
                    Cont cont = new Cont(); // Создали конутр
                    foreach (XElement Elems in ContNode.Elements("ELEM_NAME"))
                    {
                        Elems Elem = new Elems(); // Создали элемент

                        SetTypeOfElem(Elems,ref Elem);

                        ChambParams.ReadParams(ref Elem, Elems);

                        TubeParams.ReadParams(ref Elem, Elems);

                        VolumeParams.ReadParams(ref Elem, Elems);

                        DepParams.ReadParams(ref Elem, Elems);

                        cont.Add(Elem);// Записали элемент в контур
                    }
                    Conts.Add(cont); // Записали контур
                }


            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Volid.xml не был найден");
            }
        }

        class Cont
        {
            List<Elems> Elems = new List<Elems>();

            public void Add(Elems elem)
            {
                Elems.Add(elem);
            }
        }
    }
}

using Converter__from_xml_to_dat_.ElemsOfVolid;
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
                            if (double.Parse(chamb.CHAMB_FTOVOL, formatter) != 0)
                            {
                                foreach (XElement VOLMLT in Elems.Element("STRMAT_CHAMB").Elements("CHAMB_CMVOL"))
                                {
                                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                                    chamb.CHAMB_CMVOL = AttributeValue.Value;
                                }
                                foreach (XElement VOLMLT in Elems.Element("STRMAT_CHAMB").Elements("CHAMB_RMVOL"))
                                {
                                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                                    chamb.CHAMB_RMVOL = AttributeValue.Value;
                                }
                                foreach (XElement VOLMLT in Elems.Element("STRMAT_CHAMB").Elements("CHAMB_DLVOL"))
                                {
                                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                                    chamb.CHAMB_DLVOL = AttributeValue.Value;
                                }
                                foreach (XElement VOLMLT in Elems.Element("STRMAT_CHAMB").Elements("CHAMB_LAMBDA"))
                                {
                                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                                    chamb.CHAMB_LAMBDA = AttributeValue.Value;
                                }
                                foreach (XElement VOLMLT in Elems.Element("STRMAT_CHAMB").Elements("CHAMB_KOCVOL"))
                                {
                                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                                    chamb.CHAMB_KOCVOL = AttributeValue.Value;
                                }
                                foreach (XElement VOLMLT in Elems.Element("STRMAT_CHAMB").Elements("CHAMB_JNM"))
                                {
                                    XAttribute AttributeValue = VOLMLT.Attribute("Value");
                                    chamb.CHAMB_JNM = AttributeValue.Value;
                                }
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

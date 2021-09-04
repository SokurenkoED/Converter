using Converter__from_xml_to_dat_.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files
{
    class VolidXML
    {
        /// <summary>
        /// Определили тип элемента
        /// </summary>
        /// <param name="Elems"></param>
        /// <param name="Elem"></param>
        private void SetTypeOfElem(XElement Elems,ref Elems Elem)
        {
            foreach (XElement Elem_Prop in Elems.Elements("ELEM_PROP"))
            {
                XAttribute AttributeComment = Elem_Prop.Attribute("Comment");
                XAttribute AttributeNumb = Elem_Prop.Attribute("Numb");
                XAttribute AttributeDescription = Elem_Prop.Attribute("Description");

                if (AttributeComment.Value == "Камера смешения")
                {
                    if (AttributeDescription != null)
                    {
                        Elem = new Chamb(AttributeNumb.Value, AttributeDescription.Value);
                    }
                    else
                    {
                        Elem = new Chamb(AttributeNumb.Value, "НЕТ ОПИСАНИЯ");
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
                            Console.WriteLine("1");
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

        class Elems
        {
            public string Type { get; set; }
        }
        class Chamb : Elems
        {
            public Chamb(string Numb, string Discr)
            {
                Number = Numb;
                Description = Discr;
                Type = "1";
            }
            public string Number { get; set; }
            public string Description { get; set; }
        }
    }
}

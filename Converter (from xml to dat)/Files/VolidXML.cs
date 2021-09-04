using Converter__from_xml_to_dat_.ElemsOfVolid;
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
                        Elem = new Chamb(AttributeNumb.Value);
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

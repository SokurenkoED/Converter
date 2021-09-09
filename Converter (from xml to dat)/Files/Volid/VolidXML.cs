using Converter__from_xml_to_dat_.ElemsOfVolid;
using Converter__from_xml_to_dat_.Files.Volid;
using Converter__from_xml_to_dat_.Files.Volid.ReadParamsElems;
using Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems;
using Converter__from_xml_to_dat_.Functions;
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
        public void WriteParamsToFile()
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/volid.dat", false, Encoding.Default))
            {
                foreach (var Cont in Conts)
                {
                    sw.WriteLine($"{" "}{Cont.Value}");

                    foreach (var Elem in Cont.Elems)
                    {
                        WriteVolumeParams.WriteParams(Elem, sw);
                    }

                }
            }
        }
        public VolidXML()
        {
            try
            {
                XDocument xdoc = XDocument.Load("volid.xml");

                foreach (XElement ContNode in xdoc.Element("JCNTR").Elements("CONT"))
                {
                    XAttribute AttrValueCont = ContNode.Attribute("Value");

                    Cont cont = new Cont(AttrValueCont.Value); // Создали конутр

                    foreach (XElement Elems in ContNode.Elements("ELEM_NAME"))
                    {
                        XAttribute AttrValue = Elems.Attribute("Value");

                        Elems Elem = new Elems(); // Создали элемент

                        StaticMethods.SetTypeOfElem(Elems,ref Elem, AttrValue);

                        ChambParams.ReadParams(ref Elem, Elems);

                        TubeParams.ReadParams(ref Elem, Elems);

                        VolumeParams.ReadParams(ref Elem, Elems);

                        DepParams.ReadParams(ref Elem, Elems);

                        cont.Add(Elem);// Записали элемент в контур
                    }
                    Conts.Add(cont); // Записали контур
                }

                WriteParamsToFile();

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Volid.xml не был найден");
            }
        }

        public List<Cont> Conts = new List<Cont>();

        public class Cont
        {
            public Cont(string Value)
            {
                this.Value = Value;
            }
            public List<Elems> Elems = new List<Elems>();

            public string Value { get; set; }

            public void Add(Elems elem)
            {
                Elems.Add(elem);
            }
        }
    }
}

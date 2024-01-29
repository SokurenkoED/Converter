using Converter__from_xml_to_dat_.ElemsOfVolid;
using Converter__from_xml_to_dat_.Files.Volid;
using Converter__from_xml_to_dat_.Files.Volid.ReadParamsElems;
using Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems;
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
        /// Определили тип элемента, записали его номер и описание, файл volid.dat
        /// </summary>
        /// <param name="Elems"></param>
        /// <param name="Elem"></param>
        public static void SetTypeOfElem(XElement Elems, ref Elems Elem, XAttribute AttrValue)
        {
            if (AttrValue.Value == "AZ_IN" || AttrValue.Value == "AZ_OUT")
            {
                XAttribute AttributeValueFromDiscr = Elems.Attribute("Description");
                XAttribute AttributeValueFromNumb = Elems.Attribute("Numb");
                Elem.Type = AttrValue.Value;
                Elem.Name = AttrValue.Value;
                if (AttributeValueFromDiscr != null)
                {
                    Elem.Description = AttributeValueFromDiscr.Value;
                }
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
                    else if (AttributeValue.Value == "3" || AttributeValue.Value == "31" || AttributeValue.Value == "32")
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
                    else if (AttributeValue.Value == "4")
                    {
                        if (AttributeDescription != null)
                        {
                            Elem = new VolGas(AttributeNumb.Value, AttributeDescription.Value, AttributeValue.Value);
                        }
                        else
                        {
                            Elem = new VolGas(AttributeNumb.Value, AttributeValue.Value);
                        }
                    }
                    Elem.Name = AttrValue.Value;
                }
            }
        }

        public void WriteParamsFromFile()
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/volid.dat", false, Encoding.Default))
            {
                foreach (var Cont in Conts)
                {
                    sw.WriteLine($"C ***********************************CONTUR NAME ************************");
                    sw.WriteLine($"{" "}{Cont.Value}");
                    foreach (var Elem in Cont.Elems)
                    {
                        WriteDepParams.WriteParams(Elem, sw);
                        WriteChambParams.WriteParams(Elem, sw, formatter);
                        WriteTubeParams.WriteParams(Elem, sw, formatter);
                        WriteVolumeParams.WriteParams(Elem, sw, formatter);
                        WriteVolGasParams.WriteParams(Elem, sw, formatter);
                        WriteAZParams.WriteParams(Elem, sw, formatter);
                    }
                }
            }
        }

        public void ReadParamsToFile()
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

                    SetTypeOfElem(Elems, ref Elem, AttrValue);

                    ChambParams.ReadParams(ref Elem, Elems);

                    TubeParams.ReadParams(ref Elem, Elems);

                    VolumeParams.ReadParams(ref Elem, Elems);

                    DepParams.ReadParams(ref Elem, Elems);

                    VolGasParams.ReadParams(ref Elem, Elems);

                    cont.Add(Elem);// Записали элемент в контур
                }
                Conts.Add(cont); // Записали контур
            }
        }

        public VolidXML()
        {
            try
            {
                FileInfo file = new FileInfo("Volid.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл Volid.xml пустой.");
                    return;
                }

                ReadParamsToFile();

                WriteParamsFromFile();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Volid.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Volid.xml. Неверный формат записи");
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

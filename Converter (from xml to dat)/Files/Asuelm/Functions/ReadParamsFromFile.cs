using Converter__from_xml_to_dat_.Files.Asuelm.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Asuelm.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref List<Elm> Elms)
        {
            ReadParamsFormElms(xdoc, ref Elms);
        }

        private static void ReadParamsFormElms(XDocument xdoc, ref List<Elm> Elms)
        {
            foreach (XElement Elm in xdoc.Element("ASUEQP_DATA").Element("ELM_CNT").Elements("ELM_NAME"))
            {
                Elm EM = new Elm();

                EM.Name = Elm.Attribute("Value").Value;
                foreach (var item in Elm.Descendants("ELM_PROP"))
                {
                    EM.Number = item.Attribute("Numb").Value;
                }
                foreach (var item in Elm.Descendants("ELM_ASU1"))
                {
                    EM.ELM_ASU1 = item.Attribute("Value").Value;
                }
                foreach (var item in Elm.Descendants("ELM_ASU2"))
                {
                    EM.ELM_ASU2 = item.Attribute("Value").Value;
                }
                foreach (var item in Elm.Descendants("ELM_ASU3"))
                {
                    EM.ELM_ASU3 = item.Attribute("Value").Value;
                }
                foreach (var item in Elm.Descendants("ELM_ASU4"))
                {
                    EM.ELM_ASU4 = item.Attribute("Value").Value;
                }
                foreach (var item in Elm.Descendants("ELM_PHAND"))
                {
                    EM.ELM_PHAND = item.Attribute("Value").Value;
                }
                foreach (var item in Elm.Descendants("ELM_MHAND"))
                {
                    EM.ELM_MHAND = item.Attribute("Value").Value;
                }
                foreach (var item in Elm.Descendants("ELM_SHAND"))
                {
                    EM.ELM_SHAND = item.Attribute("Value").Value;
                }

                Elms.Add(EM);
            }
        }
    }
}

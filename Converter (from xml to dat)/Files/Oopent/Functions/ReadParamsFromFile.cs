using Converter__from_xml_to_dat_.Files.Asuelk.Elems;
using Converter__from_xml_to_dat_.Files.Oopent.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Oopent.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref OopElem OOU)
        {
            ReadParamsFormELLs(xdoc, ref OOU);
        }

        private static void ReadParamsFormELLs(XDocument xdoc, ref OopElem OOU)
        {
            foreach (XElement Data in xdoc.Elements("OOU_DATA"))
            {
                foreach (var item in Data.Descendants("OOU_JMACRO"))
                {
                    OOU.OOU_JMACRO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_ALFK"))
                {
                    OOU.OOU_ALFK = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_POPR"))
                {
                    OOU.OOU_POPR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_JALFA"))
                {
                    OOU.OOU_JALFA = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_GOOP"))
                {
                    OOU.OOU_GOOP = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_KSIEOO"))
                {
                    OOU.OOU_KSIEOO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_JCBOR"))
                {
                    OOU.OOU_JCBOR = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("OOU_V"))
                {
                    OOU.OOU_V.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_S"))
                {
                    OOU.OOU_S.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_DG"))
                {
                    OOU.OOU_DG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_AL"))
                {
                    OOU.OOU_AL.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_ACOS"))
                {
                    OOU.OOU_ACOS.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_AKSIN"))
                {
                    OOU.OOU_AKSIN.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_AKS"))
                {
                    OOU.OOU_AKS.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_AKSOUT"))
                {
                    OOU.OOU_AKSOUT.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_SHER"))
                {
                    OOU.OOU_SHER.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_JV"))
                {
                    OOU.OOU_JV.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("OOU_PM"))
                {
                    OOU.OOU_PM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_CM"))
                {
                    OOU.OOU_CM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_RM"))
                {
                    OOU.OOU_RM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_DL"))
                {
                    OOU.OOU_DL.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_ALMD"))
                {
                    OOU.OOU_ALMD.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_KOS"))
                {
                    OOU.OOU_KOS.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_Q"))
                {
                    OOU.OOU_Q.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_TOC"))
                {
                    OOU.OOU_TOC = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("OOU_JMIXOY"))
                {
                    OOU.OOU_JMIXOY = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_JL1"))
                {
                    OOU.OOU_JL1 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_ALFA0"))
                {
                    OOU.OOU_ALFA0.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("OOU_JMCIN"))
                {
                    OOU.OOU_JMCIN.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("OOU_PC"))
                {
                    OOU.OOU_PC = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_IOOP"))
                {
                    OOU.OOU_IOOP = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_KCIOOP"))
                {
                    OOU.OOU_KCIOOP = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("OOU_CBBXAZ"))
                {
                    OOU.OOU_CBBXAZ = item.Attribute("Value").Value;
                }
            }
        }
    }
}

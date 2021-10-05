using Converter__from_xml_to_dat_.Files.Canent.Elems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Canent.Functions
{
    class ReadParamsFromFile
    {
        public static void ReadFile(XDocument xdoc, ref GeneralCore GC, ref List<SeparateCore> SCs, ref ModlimitCore MC, 
            ref FaCore FaC, ref UnheatCore UC, ref CoreGengeom CG, ref CoreGeom CGeom, 
            ref List<FuelrodCore> FCs, ref StrmatFaCore SFC, ref StrmatUnheatCore SUC, ref CoreCross CC, ref CoreTFT CTFT)
        {
            ReadParamsFormGeneralCore(xdoc, ref GC);
            ReadParamsFormSeparateCore(xdoc, ref SCs);
            ReadParamsFormModlimitCore(xdoc, ref MC);
            ReadParamsFormFACore(xdoc, ref FaC);
            ReadParamsFormUnheatCore(xdoc, ref UC);
            ReadParamsFormCoreGengeom(xdoc, ref CG);
            ReadParamsFormCoreGeom(xdoc, ref CGeom);
            ReadParamsFormFuelRodCore(xdoc, ref FCs);
            ReadParamsFormStrmatFaCore(xdoc, ref SFC);
            ReadParamsFormStrmatUnheatCore(xdoc, ref SUC);
        }

        public static void ReadParamsFormGeneralCore(XDocument xdoc, ref GeneralCore GC)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("GENERAL_CORE"))
            {
                foreach (var item in Data.Descendants("CORE_NNOM"))
                {
                    GC.CORE_NNOM = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_ALFC"))
                {
                    GC.CORE_ALFC = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_ALFB"))
                {
                    GC.CORE_ALFB = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_P"))
                {
                    GC.CORE_P = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_EPSMK"))
                {
                    GC.CORE_EPSMK = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JJRC"))
                {
                    GC.CORE_JJRC = item.Attribute("Value").Value;
                }
            }
        }

        public static void ReadParamsFormSeparateCore(XDocument xdoc, ref List<SeparateCore> SCs)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Element("SEPARATE_CORE").Element("JCAN").Elements("JCAN_N"))
            {
                SeparateCore SC = new SeparateCore();

                foreach (var item in Data.Descendants("CORE_JRC"))
                {
                    SC.CORE_JRC = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JRCTIP"))
                {
                    SC.CORE_JRCTIP = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_KSIMJ"))
                {
                    SC.CORE_KSIMJ = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_KSI"))
                {
                    SC.CORE_KSI = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_KV1"))
                {
                    SC.CORE_KV1 = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("CORE_KV2"))
                {
                    SC.CORE_KV2.Add(item.Attribute("Value").Value);
                }

                SCs.Add(SC);
            }
        }

        public static void ReadParamsFormModlimitCore(XDocument xdoc, ref ModlimitCore MC)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("MODLIMIT_CORE"))
            {
                foreach (var item in Data.Descendants("CORE_JDGDZ"))
                {
                    MC.CORE_JDGDZ = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JDPDZ"))
                {
                    MC.CORE_JDPDZ = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JDPDT"))
                {
                    MC.CORE_JDPDT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JCPROT"))
                {
                    MC.CORE_JCPROT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JNF"))
                {
                    MC.CORE_JNF = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JPRFI"))
                {
                    MC.CORE_JPRFI = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JFZ"))
                {
                    MC.CORE_JFZ = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_QPOPR"))
                {
                    MC.CORE_QPOPR = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_EPSX"))
                {
                    MC.CORE_EPSX = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JCB"))
                {
                    MC.CORE_JCB = item.Attribute("Value").Value;
                }
            }
        }

        public static void ReadParamsFormFACore(XDocument xdoc, ref FaCore FaC)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("FA_CORE"))
            {
                foreach (var item in Data.Descendants("CORE_PMI"))
                {
                    FaC.CORE_PMI.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_PPROT"))
                {
                    FaC.CORE_PPROT.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_DELMD"))
                {
                    FaC.CORE_DELMD.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_POPRB"))
                {
                    FaC.CORE_POPRB.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_ALFKDH"))
                {
                    FaC.CORE_ALFKDH.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_DTCRIT"))
                {
                    FaC.CORE_DTCRIT.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JUNEQ"))
                {
                    FaC.CORE_JUNEQ.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JOMEG"))
                {
                    FaC.CORE_JOMEG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JQCRIT"))
                {
                    FaC.CORE_JQCRIT.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JACRIT"))
                {
                    FaC.CORE_JACRIT.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JBUNDL"))
                {
                    FaC.CORE_JBUNDL.Add(item.Attribute("Value").Value);
                }
            }
        }

        public static void ReadParamsFormUnheatCore(XDocument xdoc, ref UnheatCore UC)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("UNHEAT_CORE"))
            {
                foreach (var item in Data.Descendants("CORE_PMI2"))
                {
                    UC.CORE_PMI2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_PPROT2"))
                {
                    UC.CORE_PPROT2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_DELMD2"))
                {
                    UC.CORE_DELMD2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_POPRB2"))
                {
                    UC.CORE_POPRB2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_ALFKDH2"))
                {
                    UC.CORE_ALFKDH2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JUNEQ2"))
                {
                    UC.CORE_JUNEQ2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JOMEG2"))
                {
                    UC.CORE_JOMEG2.Add(item.Attribute("Value").Value);
                }
            }
        }

        public static void ReadParamsFormCoreGengeom(XDocument xdoc, ref CoreGengeom CG)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("CORE_GENGEOM"))
            {
                foreach (var item in Data.Descendants("CORE_K1"))
                {
                    CG.CORE_K1 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_H"))
                {
                    CG.CORE_H.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_COSC"))
                {
                    CG.CORE_COSC.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JV"))
                {
                    CG.CORE_JV.Add(item.Attribute("Value").Value);
                }
            }
        }

        public static void ReadParamsFormCoreGeom(XDocument xdoc, ref CoreGeom CGeom)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("CORE_GEOM"))
            {
                foreach (var item in Data.Descendants("CORE_VC"))
                {
                    CGeom.CORE_VC.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_SC"))
                {
                    CGeom.CORE_SC.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_DC"))
                {
                    CGeom.CORE_DC.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_KSIM"))
                {
                    CGeom.CORE_KSIM.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_SHER"))
                {
                    CGeom.CORE_SHER.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_JV2"))
                {
                    CGeom.CORE_JV2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_K2"))
                {
                    CGeom.CORE_K2 = item.Attribute("Value").Value;
                }
            }
        }

        public static void ReadParamsFormFuelRodCore(XDocument xdoc, ref List<FuelrodCore> FCs)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Element("FUELROD_CORE").Elements("CORE_FUELRODTYPE_N"))
            {
                FuelrodCore FC = new FuelrodCore();

                foreach (var item in Data.Descendants("CORE_TVEL"))
                {
                    FC.CORE_TVEL = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_EPSTF"))
                {
                    FC.CORE_EPSTF = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_JVVOD"))
                {
                    FC.CORE_JVVOD = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_RT1"))
                {
                    FC.CORE_RT1 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_RT2"))
                {
                    FC.CORE_RT2 = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_GAT"))
                {
                    FC.CORE_GAT = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_DELST"))
                {
                    FC.CORE_DELST = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("CORE_CTTAB_ARG"))
                {
                    FC.CORE_CTTAB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_CTTAB"))
                {
                    FC.CORE_CTTAB.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("CORE_LTTAB_ARG"))
                {
                    FC.CORE_LTTAB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_LTTAB"))
                {
                    FC.CORE_LTTAB.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("CORE_JGTVL"))
                {
                    FC.CORE_JGTVL = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_PTVL"))
                {
                    FC.CORE_PTVL = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_AMGTVL"))
                {
                    FC.CORE_AMGTVL = item.Attribute("Value").Value ;
                }
                foreach (var item in Data.Descendants("CORE_RGTVL"))
                {
                    FC.CORE_RGTVL = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_VGSTVL"))
                {
                    FC.CORE_VGSTVL = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_FTTVL"))
                {
                    FC.CORE_FTTVL = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("CORE_D0ZAZ"))
                {
                    FC.CORE_D0ZAZ = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_LGTAB_ARG"))
                {
                    FC.CORE_LGTAB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_LGTAB"))
                {
                    FC.CORE_LGTAB.Add(item.Attribute("Value").Value);
                }

                foreach (var item in Data.Descendants("CORE_RIO"))
                {
                    FC.CORE_RIO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_ROO"))
                {
                    FC.CORE_ROO = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_GAOB"))
                {
                    FC.CORE_GAOB = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_DGZAZ"))
                {
                    FC.CORE_DGZAZ = item.Attribute("Value").Value;
                }
                foreach (var item in Data.Descendants("CORE_AOZAZ"))
                {
                    FC.CORE_AOZAZ = item.Attribute("Value").Value;
                }

                foreach (var item in Data.Descendants("CORE_COTAB_ARG"))
                {
                    FC.CORE_COTAB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_COTAB"))
                {
                    FC.CORE_COTAB.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_LOTAB_ARG"))
                {
                    FC.CORE_LOTAB_ARG.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_LOTAB"))
                {
                    FC.CORE_LOTAB.Add(item.Attribute("Value").Value);
                }

                FCs.Add(FC);
            }
        }

        public static void ReadParamsFormStrmatFaCore(XDocument xdoc, ref StrmatFaCore SFC)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("STRMAT_FA_CORE"))
            {
                foreach (var item in Data.Descendants("CORE_CMI"))
                {
                    SFC.CORE_CMI.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_RMI"))
                {
                    SFC.CORE_RMI.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_DELMI"))
                {
                    SFC.CORE_DELMI.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_ALMI"))
                {
                    SFC.CORE_ALMI.Add(item.Attribute("Value").Value);
                }
            }
        }

        public static void ReadParamsFormStrmatUnheatCore(XDocument xdoc, ref StrmatUnheatCore SUC)
        {
            foreach (XElement Data in xdoc.Element("CORE_DATA").Elements("STRMAT_UNHEAT_CORE"))
            {
                foreach (var item in Data.Descendants("CORE_CMI2"))
                {
                    SUC.CORE_CMI2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_RMI2"))
                {
                    SUC.CORE_RMI2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_DELMI2"))
                {
                    SUC.CORE_DELMI2.Add(item.Attribute("Value").Value);
                }
                foreach (var item in Data.Descendants("CORE_ALMI2"))
                {
                    SUC.CORE_ALMI2.Add(item.Attribute("Value").Value);
                }
            }
        }
    }
}

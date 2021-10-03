using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Oopent.Elems
{
    class OopElem
    {
        public string OOU_JMACRO { get; set; }
        public string OOU_ALFK { get; set; }
        public string OOU_POPR { get; set; }
        public string OOU_JALFA { get; set; }
        public string OOU_GOOP { get; set; }
        public string OOU_KSIEOO { get; set; }
        public string OOU_TOC { get; set; }
        public string OOU_JCBOR { get; set; }

        public List<string> OOU_V = new List<string>();
        public List<string> OOU_S = new List<string>();
        public List<string> OOU_DG = new List<string>();
        public List<string> OOU_AL = new List<string>();
        public List<string> OOU_ACOS = new List<string>();
        public List<string> OOU_AKSIN = new List<string>();
        public List<string> OOU_AKS = new List<string>();
        public List<string> OOU_AKSOUT = new List<string>();
        public List<string> OOU_SHER = new List<string>();
        public List<string> OOU_JV = new List<string>();

        public List<string> OOU_PM = new List<string>();
        public List<string> OOU_CM = new List<string>();
        public List<string> OOU_RM = new List<string>();
        public List<string> OOU_DL = new List<string>();
        public List<string> OOU_ALMD = new List<string>();
        public List<string> OOU_KOS = new List<string>();
        public List<string> OOU_Q = new List<string>();

        public string OOU_JMIXOY { get; set; }
        public string OOU_JL1 { get; set; }
        public List<string> OOU_ALFA0 = new List<string>();
        public List<string> OOU_JMCIN = new List<string>();

        public string OOU_PC { get; set; }
        public string OOU_IOOP { get; set; }
        public string OOU_KCIOOP { get; set; }
        public string OOU_CBBXAZ { get; set; }
    }
}

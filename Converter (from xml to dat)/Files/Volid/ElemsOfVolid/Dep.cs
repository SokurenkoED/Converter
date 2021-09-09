using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.ElemsOfVolid
{
    class Dep : Elems
    {
        public Dep(string Numb, string Discr, string TypeV)
        {
            Number = Numb;
            Description = Discr;
            Type = TypeV;
        }
        public Dep(string Numb, string TypeV)
        {
            Number = Numb;
            Type = TypeV;
        }

        public string DEP_JJARG { get; set; }
        public string DEP_JPVT { get; set; }
        public string DEP_JPSVT { get; set; }
        public string DEP_JIVT { get; set; }
        public string DEP_JCBVT { get; set; }

        public string DEP_JTVT { get; set; }

        public List<string> DEP_PVOLT_ARG = new List<string>();
        public List<string> DEP_PVOLT = new List<string>();
        public List<string> DEP_PSVOLT_ARG = new List<string>();
        public List<string> DEP_PSVOLT = new List<string>();
        public List<string> DEP_IVOLT_ARG = new List<string>();
        public List<string> DEP_IVOLT = new List<string>();
        public List<string> DEP_CBVOLT_ARG = new List<string>();
        public List<string> DEP_CBVOLT = new List<string>();
        public List<string> DEP_TVOLT_ARG = new List<string>();
        public List<string> DEP_TVOLT = new List<string>();

    }
}

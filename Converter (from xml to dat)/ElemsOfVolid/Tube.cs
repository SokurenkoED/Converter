using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.ElemsOfVolid
{
    class Tube : Elems
    {
        public Tube(string Numb, string Discr, string TypeV)
        {
            Number = Numb;
            Description = Discr;
            Type = TypeV;
        }
        public Tube(string Numb, string TypeV)
        {
            Number = Numb;
            Type = TypeV;
        }

        public string ELEM_VOLMLT { get; set; }
        public string PIPE_JMACRO { get; set; }
        public string PIPE_JMACRV { get; set; }
        public string PIPE_ALFKSG { get; set; }
        public string PIPE_POPSG { get; set; }
        public string PIPE_JALFA { get; set; }
        public string PIPE_JNEGOM { get; set; }

        public List<string> PIPE_JNM = new List<string>();
        public List<string> PIPE_JVV = new List<string>();
        public List<string> PIPE_CM = new List<string>();
        public List<string> PIPE_RM = new List<string>();
        public List<string> PIPE_ALMD = new List<string>();
        public List<string> PIPE_DL = new List<string>();
        public List<string> PIPE_ALOCSG = new List<string>();
        public List<string> PIPE_FMSG = new List<string>();

        public List<string> PIPE_V2SG = new List<string>();
        public List<string> PIPE_S2SG = new List<string>();
        public List<string> PIPE_DGSG = new List<string>();
        public List<string> PIPE_AL = new List<string>();
        public List<string> PIPE_DZSG = new List<string>();
        public List<string> PIPE_AKSSG = new List<string>();
        public List<string> PIPE_SHERSG = new List<string>();
        public List<string> PIPE_INMPG = new List<string>();
        public List<string> PIPE_JV = new List<string>();

        public string PIPE_PVOL { get; set; }
        public string PIPE_DP { get; set; }
        public string PIPE_IPG { get; set; }
        public string PIPE_IPB { get; set; }
        public string PIPE_CBOL { get; set; }
        public string PIPE_TBYX { get; set; }
        public string PIPE_TBX { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Gidr2k.Junctions
{
    class Standart : Jun
    {
        public Standart(string name) : base(name)
        {
        }
        public string JUN_GAM02K { get; set; }
        public string JUN_DP02K { get; set; }
        public string JUN_AJNMLT { get; set; }
        public string JUN_JOBR { get; set; }
        public string JUN_JCRFLJ { get; set; }
        public string JUN_VJ { get; set; }
        public string JUN_SG { get; set; }
        public string JUN_DGG2K { get; set; }
        public string JUN_LG { get; set; }
        public string JUN_DZG2K { get; set; }
        public string JUN_HJ1 { get; set; }
        public string JUN_HJ2 { get; set; }
        public string JUN_KSIG2K { get; set; }
        public string JUN_SHRG2K { get; set; }
        public string JUN_INMG2K { get; set; }
        public string JUN_PUMPDISCR { get; set; }
        public string JUN_JPUG2K { get; set; }
        public string JUN_HP0G2K { get; set; }
        public string JUN_MP0G2K { get; set; }
        public string JUN_QP0G2K { get; set; }
        public string JUN_OMP02K { get; set; }
        public string JUN_JWTG2K { get; set; }

        public List<string> JUN_WTG2K_ARG = new List<string>();
        public List<string> JUN_WTG2K_FRQ = new List<string>();
        public string JUN_VLVDISCR { get; set; }
        public string JUN_VLVNAM { get; set; }
        public string JUN_S0VLV { get; set; }
        public string JUN_KSIVLV { get; set; }
        public string JUN_DGVLV { get; set; }
        public string JUN_LVLV { get; set; }
        public string JUN_CVLV { get; set; }
        public string JUN_VLVA1 { get; set; }
        public string JUN_VLVA2 { get; set; }
        public string JUN_JVTBL { get; set; }

        public List<string> JUN_VLVTBL_ARG = new List<string>();
        public List<string> JUN_VLVTBL_S = new List<string>();


    }
}

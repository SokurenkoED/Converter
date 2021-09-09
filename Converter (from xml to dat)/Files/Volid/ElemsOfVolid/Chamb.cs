using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.ElemsOfVolid
{
    class Chamb : Elems
    {
        public Chamb(string Numb, string Discr)
        {
            Number = Numb;
            Description = Discr;
            Type = "1";
        }
        public Chamb(string Numb)
        {
            Number = Numb;
            Type = "1";
        }
        public string ELEM_VOLMLT { get; set; }
        public string CHAMB_VVOL { get; set; }
        public string CHAMB_DZVOL { get; set; }
        public string CHAMB_FTOVOL { get; set; }
        public string CHAMB_CMVOL { get; set; }
        public string CHAMB_RMVOL { get; set; }
        public string CHAMB_DLVOL { get; set; }
        public string CHAMB_LAMBDA { get; set; }
        public string CHAMB_KOCVOL { get; set; }
        public string CHAMB_JNM { get; set; }
        public string CHAMB_PVOL { get; set; }
        public string CHAMB_PSVOL { get; set; }
        public string CHAMB_IVOL { get; set; }
        public string CHAMB_CBOL { get; set; }
        public string CHAMB_TETVOL { get; set; }

    }
}

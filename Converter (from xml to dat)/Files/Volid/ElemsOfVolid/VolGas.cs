using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.ElemsOfVolid
{
    class VolGas : Elems
    {
        public VolGas(string Numb, string Discr, string TypeV)
        {
            Number = Numb;
            Description = Discr;
            Type = TypeV;
        }
        public VolGas(string Numb, string TypeV)
        {
            Number = Numb;
            Type = TypeV;
        }
        public string ELEM_VOLMLT { get; set; }
        public string VolGas_AURKDJ { get; set; }
        public string VolGas_VVOL { get; set; }
        public string VolGas_DZVOL { get; set; }
        public string VolGas_SKDJ { get; set; }
        public string VolGas_FTOVOL { get; set; }

        public string VolGas_JNM { get; set; }
        public string VolGas_CMET { get; set; }
        public string VolGas_GAMMET { get; set; }
        public string VolGas_DLMVOL { get; set; }
        public string VolGas_ALMET { get; set; }
        public string VolGas_KOCVOL { get; set; }
        public string VolGas_PVOL { get; set; }
        public string VolGas_IVOL { get; set; }
        public string VolGas_TGKDJ { get; set; }
        public string VolGas_VWKDJ { get; set; }
        public string VolGas_CBOL { get; set; }
    }
}

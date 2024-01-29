using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.ElemsOfVolid
{
    class Volume : Elems
    {
        public Volume(string Numb, string Discr, string TypeV)
        {
            Number = Numb;
            Description = Discr;
            Type = TypeV;
        }
        public Volume(string Numb, string TypeV)
        {
            Number = Numb;
            Type = TypeV;
        }

        public string ELEM_VOLMLT { get; set; }
        public string VOL_KGVSP { get; set; }
        public string VOL_JTRCON { get; set; }
        public string VOL_DGSGCN { get; set; }
        public string VOL_DGWSCN { get; set; }
        public string VOL_PSGCON { get; set; }
        public string VOL_PWSCON { get; set; }
        public string VOL_JVTAB { get; set; }

        public List<string> VOL_TABH = new List<string>();
        public List<string> VOL_TABV = new List<string>();

        public string VOL_JNMG { get; set; }
        public string VOL_CMSG { get; set; }
        public string VOL_RMSG { get; set; }
        public string VOL_DLSG { get; set; }
        public string VOL_LMBDG { get; set; }
        public string VOL_KOCSGC { get; set; }
        public string VOL_FTOVOL { get; set; }


        public string VOL_JNMW { get; set; }
        public string VOL_CMWS { get; set; }
        public string VOL_RMWS { get; set; }
        public string VOL_DLWS { get; set; }
        public string VOL_LMBDW { get; set; }
        public string VOL_KOCVOL { get; set; }

        public string VOL_JNM { get; set; }
        public string VOL_CMVOL { get; set; }
        public string VOL_RMVOL { get; set; }
        public string VOL_DLVOL { get; set; }
        public string VOL_LAMBDA { get; set; }
        public string VOL_KOCVOLEQ { get; set; }

        public string VOL_PVOL { get; set; }
        public string VOL_PSVOL { get; set; }
        public string VOL_IVOLEQU { get; set; }
        public string VOL_ISG { get; set; }
        public string VOL_IVOL { get; set; }
        public string VOL_HL { get; set; }
        public string VOL_CBVOL { get; set; }
    }
}

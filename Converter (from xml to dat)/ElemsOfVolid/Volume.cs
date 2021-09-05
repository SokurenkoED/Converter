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


    }
}

using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    class WriteVolumeParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw, IFormatProvider formatter)
        {
            if (Elem.Type == "3" || Elem.Type == "31" || Elem.Type == "32")
            {
                Volume volume = (Volume)Elem;
                sw.WriteLine($"{" "}{volume.Name}               {"!"}{volume.Description}               {"#"}{volume.Number}");
                sw.WriteLine($"{volume.Type}");
                sw.WriteLine($"{volume.ELEM_VOLMLT}");
                sw.WriteLine($"C Total Height Hidr.Diam on the level & under the level &   Kwspl   Condeser tubes");
                sw.WriteLine($"{volume.VOL_TABH[(volume.VOL_TABH.Count)-1]} {volume.VOL_DGSGCN} {volume.VOL_DGWSCN} {volume.VOL_KGVSP} {volume.VOL_JTRCON}");
                sw.WriteLine($"C Table Volume versus Height");
                sw.WriteLine($"{volume.VOL_JVTAB}");
                for (int i = 0; i < volume.VOL_TABH.Count; i++)
                {
                    sw.Write($"{volume.VOL_TABH[i]} ");
                }
                sw.WriteLine();
                for (int i = 0; i < volume.VOL_TABV.Count; i++)
                {
                    sw.Write($"{volume.VOL_TABV[i]} ");
                }
                sw.WriteLine();
                if (Elem.Type == "3" || Elem.Type == "31")
                {
                    sw.WriteLine($"C Wall perimeter on the level & under the level");
                    sw.WriteLine($"{volume.VOL_PSGCON} {volume.VOL_PWSCON}");
                    if (double.Parse(volume.VOL_PSGCON, formatter) > 0)
                    {
                        sw.WriteLine($"C Heat Cap Density Fikness  Condact   HTR AIR   Wall Areac");
                        sw.WriteLine($"{volume.VOL_CMSG} {volume.VOL_RMSG} {volume.VOL_DLSG} {volume.VOL_LMBDG} {volume.VOL_KOCSGC} {volume.VOL_JNMG}");
                    }
                    if (double.Parse(volume.VOL_PWSCON, formatter) > 0)
                    {
                        sw.WriteLine($"C Heat Cap Density Fikness  Condact   HTR AIR   Wall Areac");
                        sw.WriteLine($"{volume.VOL_CMWS} {volume.VOL_RMWS} {volume.VOL_DLWS} {volume.VOL_LMBDW} {volume.VOL_KOCVOL} {volume.VOL_JNMW}");
                    }
                }
                else if (Elem.Type == "32")
                {
                    sw.WriteLine($"C Wall perimeter");
                    sw.WriteLine($"{volume.VOL_FTOVOL}");
                    if (double.Parse(volume.VOL_FTOVOL, formatter) > 0)
                    {
                        sw.WriteLine($"C Heat Cap Density Fikness  Condact   HTR AIR   Wall Areac");
                        sw.WriteLine($"{volume.VOL_CMVOL} {volume.VOL_RMVOL} {volume.VOL_DLVOL} {volume.VOL_LAMBDA} {volume.VOL_KOCVOLEQ} {volume.VOL_JNM}");
                    }
                }
                sw.WriteLine($"C Initial Conditions");
               
                if (Elem.Type == "32")
                {
                    sw.WriteLine($"C Total Press Partial Enthalpy Height Bor");
                    sw.WriteLine($"{volume.VOL_PVOL} {volume.VOL_IVOLEQU} {volume.VOL_HL} {volume.VOL_CBVOL}");
                }
                else
                {
                    sw.WriteLine($"C Total Press Partial Steam Press Enthalpy On the level & Under  Height   Bor");
                    sw.WriteLine($"{volume.VOL_PVOL} {volume.VOL_PSVOL} {volume.VOL_ISG} {volume.VOL_IVOL} {volume.VOL_HL} {volume.VOL_CBVOL}");
                }
                sw.WriteLine($"C");
            }
        }
    }
}

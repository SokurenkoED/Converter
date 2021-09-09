using Converter__from_xml_to_dat_.ElemsOfVolid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Volid.WriteParamsElems
{
    static class WriteDepParams
    {
        public static void WriteParams(Elems Elem, StreamWriter sw)
        {
            if (Elem.Type == "0")
            {
                Dep dep = (Dep)Elem;
                sw.WriteLine($"{" "}{dep.Name}               {"!"}{dep.Description}               {"#"}{dep.Number}");
                if (dep.DEP_JTVT == null)
                {
                    sw.WriteLine($"{dep.DEP_JPVT} {dep.DEP_JPSVT} {dep.DEP_JIVT} {dep.DEP_JCBVT} {dep.DEP_JJARG}");
                    for (int i = 0; i < dep.DEP_PVOLT_ARG.Count; i++)
                    {
                        sw.Write($"{dep.DEP_PVOLT_ARG[i]} ");
                    }
                    for (int i = 0; i < dep.DEP_PVOLT.Count; i++)
                    {
                        sw.Write($"{dep.DEP_PVOLT[i]} ");
                        
                    }
                    sw.WriteLine();
                    for (int i = 0; i < dep.DEP_PSVOLT_ARG.Count; i++)
                    {
                        sw.Write($"{dep.DEP_PSVOLT_ARG[i]} ");
                        
                    }
                    for (int i = 0; i < dep.DEP_PSVOLT.Count; i++)
                    {
                        sw.Write($"{dep.DEP_PSVOLT[i]} ");
                        
                    }
                    sw.WriteLine();
                    for (int i = 0; i < dep.DEP_IVOLT_ARG.Count; i++)
                    {
                        sw.Write($"{dep.DEP_IVOLT_ARG[i]} ");
                    }
                    for (int i = 0; i < dep.DEP_IVOLT.Count; i++)
                    {
                        sw.Write($"{dep.DEP_IVOLT[i]} ");
                    }
                    sw.WriteLine();
                    for (int i = 0; i < dep.DEP_CBVOLT_ARG.Count; i++)
                    {
                        sw.Write($"{dep.DEP_CBVOLT_ARG[i]} ");
                        
                    }
                    for (int i = 0; i < dep.DEP_CBVOLT.Count; i++)
                    {
                        sw.Write($"{dep.DEP_CBVOLT[i]} ");
                        
                    }
                    sw.WriteLine();
                    sw.WriteLine("C");
                }
                else
                {
                    sw.WriteLine($"{dep.DEP_JPVT} {dep.DEP_JTVT}");
                    for (int i = 0; i < dep.DEP_PVOLT_ARG.Count; i++)
                    {
                        sw.Write($"{dep.DEP_PVOLT_ARG[i]} ");
                    }
                    for (int i = 0; i < dep.DEP_PVOLT.Count; i++)
                    {
                        sw.Write($"{dep.DEP_PVOLT[i]} ");
                        
                    }
                    sw.WriteLine();

                    for (int i = 0; i < dep.DEP_TVOLT_ARG.Count; i++)
                    {
                        sw.Write($"{dep.DEP_TVOLT_ARG[i]} ");
                        
                    }
                    for (int i = 0; i < dep.DEP_TVOLT.Count; i++)
                    {
                        sw.Write($"{dep.DEP_TVOLT[i]} ");
                    }
                    sw.WriteLine();
                    sw.WriteLine("C");
                }
            }
        }
    }
}

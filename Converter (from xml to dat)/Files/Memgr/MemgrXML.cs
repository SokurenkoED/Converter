using Converter__from_xml_to_dat_.Files.Canent;
using Converter__from_xml_to_dat_.Files.Memgr.Functions;

namespace Converter__from_xml_to_dat_.Files.Hstr
{
    class MemgrXML
    {
        public MemgrXML(CanentXML Canent)
        {
            WriteParamsToFile.WriteFile(Canent);
        }
    }
}

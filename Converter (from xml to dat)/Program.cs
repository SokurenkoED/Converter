using Converter__from_xml_to_dat_.Files;
using Converter__from_xml_to_dat_.Files.Asuelk;
using Converter__from_xml_to_dat_.Files.Asuelm;
using Converter__from_xml_to_dat_.Files.Asuval;
using Converter__from_xml_to_dat_.Files.Elpows;
using Converter__from_xml_to_dat_.Files.Hstr;
using Converter__from_xml_to_dat_.Files.Kinet;
using Converter__from_xml_to_dat_.Files.Measure;
using Converter__from_xml_to_dat_.Files.Oopent;
using Converter__from_xml_to_dat_.Files.Otyent;
using Converter__from_xml_to_dat_.Files.Upper;
using System;
using System.IO;

namespace Converter__from_xml_to_dat_
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Создаем папку OldFormat-TIGR

            string DirPath = "OldFormat-TIGR";
            DirectoryInfo dirInfo = new DirectoryInfo(DirPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            #endregion

            MainXML Main = new MainXML();

            VolidXML Volid = new VolidXML();

            Gidr2kXML Gidr2k = new Gidr2kXML();

            HstrXML Hstr = new HstrXML();

            MeasureXML Measure = new MeasureXML();

            ElpowsXML Elpows = new ElpowsXML();

            AsuvalXML Asuval = new AsuvalXML();

            AsuelmXML Asuelm = new AsuelmXML();

            AsuelkXML Asuelk = new AsuelkXML();

            OopentXML Oopent = new OopentXML();

            OtyentXML Otyent = new OtyentXML();

            UpperXML Upper = new UpperXML();

            KinetXML Kinet = new KinetXML();

            Console.ReadKey();


        }
    }
}

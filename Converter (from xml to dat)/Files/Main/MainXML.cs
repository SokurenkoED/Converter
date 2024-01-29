
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files
{
    class MainXML
    {
        #region Методы

        private List<string> ParseParams(XDocument xdoc, string name)
        {
            List<string> ReturnParams = new List<string>();
            foreach (XElement Params in xdoc.Element("GENERAL_DATA").Element(name).Descendants())
            {
                XAttribute nameAttribute = Params.Attribute("Value");

                ReturnParams.Add(nameAttribute.Value);
            }
            return ReturnParams;
        }

        private List<string> ParseCONT_PARAM(XDocument xdoc)
        {
            List<string> ReturnParams = new List<string>();
            foreach (XElement Params in xdoc.Element("GENERAL_DATA").Element("CONT_PARAM").Elements("JCNTR_N"))
            {

                foreach (var item in Params.Descendants())
                {
                    XAttribute nameAttribute = item.Attribute("Value");
                    ReturnParams.Add(nameAttribute.Value);
                }
            }
            return ReturnParams;
        }
        private void WriteParams(XDocument xdoc, REAC_PARAM RParams, INT_PARAM IParams, REST_PRINT_PARAM RPParams, CONT_PARAM CParams)
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/main.dat", false, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{ RParams.JCAN} { RParams.JK1} { RParams.JK2} { RParams.JO} { RParams.JR} { RParams.JTFT} { RParams.JBORAZ}");
                sw.WriteLine($"{ RPParams.JTIME} { RPParams.PRINT_STEP1} { RPParams.PRINT_STEP2} { RPParams.PRINT_STEP3} { RPParams.PRINT_TIME1} { RPParams.PRINT_TIME2} { RPParams.PRINT_TIME3} { RPParams.JREAD} { RPParams.JWRITE} { RPParams.JNP}");
                sw.WriteLine($"{ IParams.DTMIN} { IParams.DTMAX} { IParams.EPSMIN} { IParams.EPSMAX}");
                sw.WriteLine($"{ RPParams.JNGR} { RPParams.TGRAF}");
                sw.WriteLine($"{ RParams.JKIN} { RParams.JJSTAT} { RPParams.DTDISK}");
                if (RParams.JKIN == "7")
                {
                    sw.WriteLine($"{ RParams.JSTAT}");
                }
                sw.WriteLine(CParams.JVTOT.Count);
                for (int i = 0; i < CParams.JVTOT.Count; i++)
                {
                    sw.WriteLine(CParams.JVTOT[i]);
                    sw.WriteLine(CParams.Q02K[i]);
                    sw.WriteLine(CParams.G02K[i]);
                    sw.WriteLine(CParams.TOCVOL[i]);
                    sw.WriteLine($"{CParams.CPGAS2[i]} {CParams.AMGAS2[i]} {CParams.ALGAS2[i]} {CParams.RGAS2[i]}");
                    sw.WriteLine(CParams.JGASCN[i]);
                }
                foreach (XElement Params in xdoc.Element("GENERAL_DATA").Elements("JNEV_T"))
                {
                    XAttribute nameAttribute = Params.Attribute("Value");
                    sw.WriteLine(nameAttribute.Value);
                }
                sw.WriteLine(0);
            }
        }

        #endregion

        public MainXML()
        {
            try
            {
                FileInfo file = new FileInfo("main.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл main.xml пустой.");
                    return;
                }

                XDocument xdoc = XDocument.Load("main.xml");
                REAC_PARAM RParams = new REAC_PARAM(ParseParams(xdoc, "REAC_PARAM"));
                INT_PARAM IParams = new INT_PARAM(ParseParams(xdoc, "INT_PARAM"));
                REST_PRINT_PARAM RPParams = new REST_PRINT_PARAM(ParseParams(xdoc, "REST_PRINT_PARAM"));
                CONT_PARAM CParams = new CONT_PARAM(ParseCONT_PARAM(xdoc));
                WriteParams(xdoc, RParams, IParams, RPParams, CParams);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Main.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Main.xml. Неверный формат записи");
            }
        }
    }
    class REAC_PARAM
    {
        public REAC_PARAM(List<string> ReturnParams)
        {
            JCAN = ReturnParams[0];
            JK1 = ReturnParams[1];
            JK2 = ReturnParams[2];
            JO = ReturnParams[3];
            JR = ReturnParams[4];
            JTFT = ReturnParams[5];
            JBORAZ = ReturnParams[6];
            JKIN = ReturnParams[7];
            JJSTAT = ReturnParams[8];
            if (JKIN == "7") 
            {
                JSTAT = ReturnParams[9];
            }
        }
        public string JCAN { get; set; }
        public string JK1 { get; set; }
        public string JK2 { get; set; }
        public string JO { get; set; }
        public string JR { get; set; }
        public string JTFT { get; set; }
        public string JBORAZ { get; set; }
        public string JKIN { get; set; }
        public string JJSTAT { get; set; }
        public string JSTAT { get; set; }
    }
    class INT_PARAM
    {
        public INT_PARAM(List<string> ReturnParams)
        {
            DTMIN = ReturnParams[0];
            DTMAX = ReturnParams[1];
            EPSMIN = ReturnParams[2];
            EPSMAX = ReturnParams[3];
        }
        public string DTMIN { get; set; }
        public string DTMAX { get; set; }
        public string EPSMIN { get; set; }
        public string EPSMAX { get; set; }
    }
    class REST_PRINT_PARAM
    {
        public REST_PRINT_PARAM(List<string> ReturnParams)
        {
            JREAD = ReturnParams[0];
            DTDISK = ReturnParams[1];
            JWRITE = ReturnParams[2];
            JTIME = ReturnParams[3];
            PRINT_STEP1 = ReturnParams[4];
            PRINT_STEP2 = ReturnParams[5];
            PRINT_STEP3 = ReturnParams[6];
            PRINT_TIME1 = ReturnParams[7];
            PRINT_TIME2 = ReturnParams[8];
            PRINT_TIME3 = ReturnParams[9];
            JNGR = ReturnParams[10];
            TGRAF = ReturnParams[11];
            JNP = ReturnParams[12];
        }

        public string JREAD { get; set; }
        public string DTDISK { get; set; }
        public string JWRITE { get; set; }
        public string JTIME { get; set; }
        public string PRINT_STEP1 { get; set; }
        public string PRINT_STEP2 { get; set; }
        public string PRINT_STEP3 { get; set; }
        public string PRINT_TIME1 { get; set; }
        public string PRINT_TIME2 { get; set; }
        public string PRINT_TIME3 { get; set; }
        public string JNGR { get; set; }
        public string TGRAF { get; set; }
        public string JNP { get; set; }
    }
    class CONT_PARAM
    {
        public CONT_PARAM(List<string> ReturnParams)
        {
            for (int i = 0; i < ReturnParams.Count/9; i++)
            {
                JVTOT.Add(ReturnParams[0 + i*9]);
                Q02K.Add(ReturnParams[1 + i * 9]);
                G02K.Add(ReturnParams[2 + i * 9]);
                TOCVOL.Add(ReturnParams[3 + i * 9]);
                JGASCN.Add(ReturnParams[4 + i * 9]);
                CPGAS2.Add(ReturnParams[5 + i * 9]);
                AMGAS2.Add(ReturnParams[6 + i * 9]);
                ALGAS2.Add(ReturnParams[7 + i * 9]);
                RGAS2.Add(ReturnParams[8 + i * 9]);
            }
        }
        public List<string> JVTOT = new List<string>();
        public List<string> Q02K = new List<string>();
        public List<string> G02K = new List<string>();
        public List<string> TOCVOL = new List<string>();
        public List<string> JGASCN = new List<string>();
        public List<string> CPGAS2 = new List<string>();
        public List<string> AMGAS2 = new List<string>();
        public List<string> ALGAS2 = new List<string>();
        public List<string> RGAS2 = new List<string>();

    }
}

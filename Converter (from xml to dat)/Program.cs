using Converter__from_xml_to_dat_.Files;
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

            Console.ReadKey();


        }
    }
}

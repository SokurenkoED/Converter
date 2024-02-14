using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;

namespace Converter__from_xml_to_dat_.Files.Copy_Files
{
    internal class CopyFilesXML
    {
        public void Copy_file_from_SIT_to_TIGR_Dir(string FileSystemName)
        {
            try
            {

                FileInfo file = new FileInfo(FileSystemName);

                if (File.Exists($"{FileSystemName}"))
                {
                    File.Copy(FileSystemName, $"OldFormat-TIGR/{FileSystemName}", true);
                }
                else
                {
                    Console.WriteLine($"Ошибка! Не был найден файл {FileSystemName}.");
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл {FileSystemName} не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine($"Проверить файл {FileSystemName}. Неверный формат записи");
            }
        }

        public void Create_files()
        {
            using (StreamWriter sw = new StreamWriter("OldFormat-TIGR/algoritm", false, Encoding.Default))
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                sw.WriteLine($" 0 0 0 0");
                sw.WriteLine($" empty");
            }
        }

        public void Create_directory(string Path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public void Copy_file_for_extantion(string ext, string NewName)
        {
            string[] FileName = Directory.GetFiles("./", ext, SearchOption.TopDirectoryOnly);

            if (FileName.Length != 0)
            {
                File.Copy(FileName[0], $"OldFormat-TIGR/{NewName}", true);
            }
           

        }

        public CopyFilesXML()
        {
            Copy_file_from_SIT_to_TIGR_Dir("Bipr7.dat");

            Copy_file_from_SIT_to_TIGR_Dir("fort.8");

            Copy_file_from_SIT_to_TIGR_Dir("fort.10");

            Copy_file_from_SIT_to_TIGR_Dir("list");

            Copy_file_from_SIT_to_TIGR_Dir("list");

            Create_directory($"OldFormat-TIGR/SVRK");

            Create_files();

            Copy_file_for_extantion("*.drm", "rest.drm");

            Copy_file_for_extantion("*.all", "rest.all");

            Copy_file_for_extantion("*.tfc", "rest.tfc");

            Copy_file_for_extantion("*.res7", "PRO.res");

            Copy_file_for_extantion("*.res7D", "B7D_14.res");


        }
    }
}

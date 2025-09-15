using Converter__from_xml_to_dat_.Files.Canent.Elems;
using Converter__from_xml_to_dat_.Files.Canent.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter__from_xml_to_dat_.Files.Canent
{
    class CanentXML
    {
        XDocument xdoc;

        public GeneralCore GC = new GeneralCore();
        List<SeparateCore> SCs = new List<SeparateCore>();
        ModlimitCore MC = new ModlimitCore();
        FaCore FaC = new FaCore();
        UnheatCore UC = new UnheatCore();
        CoreGengeom CG = new CoreGengeom();
        CoreGeom CGeom = new CoreGeom();
        List<FuelrodCore> FCs = new List<FuelrodCore>();
        StrmatFaCore SFC = new StrmatFaCore();
        StrmatUnheatCore SUC = new StrmatUnheatCore();
        CoreCross CC = new CoreCross();
        public CoreTFT CTFT = new CoreTFT();

        // Функция для запроса "да/нет" с возвратом 1 или 0
        static int AskYesNo(string question)
        {
            while (true)
            {
                Console.WriteLine($"{question} (1 - да, 0 - нет)");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int answer) && (answer == 0 || answer == 1))
                {
                    return answer;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите 1 (да) или 0 (нет).");
                }
            }
        }

        int TypeNumber(string question)
        {
            while (true)
            {
                Console.WriteLine($"{question}");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int answer))
                {
                    if (CTFT.CORETT_JRCTIP.Contains((answer + 1).ToString()))
                    {
                        return answer + 1;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, такого типа не существует.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка, введите число типа int.");
                }
            }
        }

        public CanentXML()
        {
            try
            {
                FileInfo file = new FileInfo("canent.xml");
                long size = file.Length;
                if (size == 0)
                {
                    Console.WriteLine("Файл canent.xml пустой.");
                    return;
                }

                xdoc = XDocument.Load("canent.xml");

                ReadParamsFromFile.ReadFile(xdoc, ref GC, ref SCs, ref MC, ref FaC, ref UC, ref CG, ref CGeom, ref FCs, ref SFC, ref SUC, ref CC, ref CTFT);

                int answer = AskYesNo("Нужно ли программе изменять зону задания для ТТК?");
                if (answer == 1)
                {
                    int JRCTIP = TypeNumber($"Укажите тип ячейки, который необходимо убрать.");

                    // Редактируем зону задания для ТТК
                    CTFT.ChangeTTK(JRCTIP);


                }
                else
                {
                    Console.WriteLine("Вы выбрали: Нет. Изменения не требуются.");
                }

                WriteParamsToFile.WriteFile(ref GC, ref SCs, ref MC, ref FaC, ref UC, ref CG, ref CGeom, ref FCs, ref SFC, ref SUC, ref CC, ref CTFT);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Canent.xml не был найден");
            }
            catch (System.Xml.XmlException)
            {
                Console.WriteLine("Проверить файл Canent.xml. Неверный формат записи");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Converter__from_dat_to_xml_
{
    class Program
    {

        #region Методы
        
        /// <summary>
        /// Функция, которая находит начало следующего контура
        /// 0 - конец файла
        /// 1 - текущий контур
        /// 2 - новый контур
        /// 3 - для AZ_IN и AZ_OUT
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int IsNewCont(ref int CountOfLine, ref List<string> ArrayOfVolid)
        {

            if (CountOfLine + 1 == ArrayOfVolid.Count)
            {
                return 0; // Значит последний элемент в файле
            }
            else
            {
                string str1 = ArrayOfVolid[CountOfLine + 2];
                string[] ArrOfStr = ArrayOfVolid[CountOfLine + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int int1;

                bool success = int.TryParse(str1, out int1);

                if (success == false)
                {
                    if (ArrOfStr[0] == "AZ_IN" || ArrOfStr[0] == "AZ_OUT")
                    {
                        return 3;
                    }
                    else
                    {
                        string[] ArrOfStr2 = ArrayOfVolid[CountOfLine + 1].Split(new string[] { "!", "#" }, StringSplitOptions.RemoveEmptyEntries);
                        string[] NameAndCountOfString = ArrOfStr2[0].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (NameAndCountOfString.Length == 2)
                        {
                            return 1;
                        }
                        else
                        {
                            return 2; // Значит новый контур
                        }
                    }
                }
                else
                {
                    return 1; // Значит есть еще элементы в контуре
                }

            }



        }

        /// <summary>
        /// Функция которая проверяет конец каждого элемента
        /// </summary>
        /// <param name="CountOfLine"></param>
        /// <param name="ArrayOfVolid"></param>
        /// <param name="CountOfVolid"></param>
        /// <param name="CountOfLineInElem"></param>
        /// <param name="TypeOfElem"></param>
        /// <param name="sw"></param>
        static void EndOfElem(ref int CountOfLine, ref List<string> ArrayOfVolid,
            ref int CountOfLineInElem, ref int TypeOfElem, ref int Count, StreamWriter sw)
        {
            if (IsNewCont(ref Count, ref ArrayOfVolid) == 0)
            {
                CountOfLineInElem = 0;
                sw.WriteLine(" </CONT>");
                sw.Write("</JCNTR>");
            }
            else if (IsNewCont(ref Count, ref ArrayOfVolid) == 1)
            {
                CountOfLineInElem = 0;
                CountOfLine = 0;
                TypeOfElem = -1;
            }
            else if (IsNewCont(ref Count, ref ArrayOfVolid) == 2)
            {
                CountOfLineInElem = 0;
                CountOfLine = -1;
                TypeOfElem = -1;
                sw.WriteLine(" </CONT>");
            }
            else if (IsNewCont(ref Count, ref ArrayOfVolid) == 3)
            {
                CountOfLineInElem = 0;
                CountOfLine = 2;
                TypeOfElem = 101;
            }

        }

        /// <summary>
        /// Считывает "*"
        /// </summary>
        /// <param name="ArrayOfVolid"></param>
        /// <param name="ArrOfStr"></param>
        /// <param name="i"></param>
        /// <param name="formatter"></param>
        static void ReadStar(ref List<string> ArrayOfVolid, ref string[] ArrOfStr, int i, IFormatProvider formatter)
        {
            if (ArrayOfVolid[i].IndexOf("*") != -1)
            {
                List<string> TimeStr = new List<string>();
                string[] MultStr = new string[2];
                foreach (var item in ArrOfStr)
                {
                    if (item.IndexOf("*") == -1)
                    {
                        TimeStr.Add(item);
                    }
                    else
                    {

                        MultStr = item.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < (int)double.Parse(MultStr[0], formatter); j++)
                        {
                            TimeStr.Add(MultStr[1]);
                        }

                    }
                }
                ArrOfStr = TimeStr.ToArray();
            }
        }

        #endregion

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            int CountContVolid = 0;

            Dictionary<string, string> NameAndTypeForHSTR = new Dictionary<string, string>(); // Словарь с названием элементов и типов в файле volid.dat

            string[] REAC_PARAM = new string[9];

            string CountMix = null;

            string OOU_JL1 = null;

            List<string> GENERAL_OOU = new List<string>(); // GOOP KSIEOO JMACRO ALFK POPR JALFA JCBOR

            List<string> MODLIMIT_CORE = new List<string>(); // Первое значение в general 

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            #region Создаем папку NewFormat-TIGR

            string DirPath = "NewFormat-TIGR";
            DirectoryInfo dirInfo = new DirectoryInfo(DirPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            #endregion

            #region Путь до файла VOLID.DAT

            string ReadPathVolid = "volid.dat";
            string WritePathVolid = "NewFormat-TIGR/volid.xml";

            #endregion

            #region Путь до файла MAIN.DAT

            string ReadPathMain = "main.dat";
            string WritePathMain = "NewFormat-TIGR/main.xml";

            #endregion

            #region Путь до файла GIDR2K.DAT

            string ReadPathGidr2k = "gidr2k.dat";
            string WritePathGidr2k = "NewFormat-TIGR/gidr2k.xml";

            #endregion

            #region Путь до файла MEASURE.DAT

            string ReadPathMeasure = "measure.dat";
            string WritePathMeasure = "NewFormat-TIGR/measure.xml";

            #endregion

            #region Путь до файла ELPOWS.DAT

            string ReadPathElpows = "elpows.dat";
            string WritePathElpows = "NewFormat-TIGR/elpows.xml";

            #endregion

            #region Путь до файла ASUELK.DAT

            string ReadPathAsuelk = "asuelk.dat";
            string WritePathAsuelk = "NewFormat-TIGR/asuelk.xml";

            #endregion

            #region Путь до файла ASUELM.DAT

            string ReadPathAsuelm = "asuelm.dat";
            string WritePathAsuelm = "NewFormat-TIGR/asuelm.xml";

            #endregion

            #region Путь до файла HSTR.DAT

            string ReadPathHstr = "hstr.dat";
            string WritePathHstr = "NewFormat-TIGR/hstr.xml";

            #endregion

            #region Путь до файла CANENT.DAT

            string ReadPathCanent = "canent.dat";
            string WritePathCanent = "NewFormat-TIGR/canent.xml";

            #endregion

            #region Путь до файла ASUVAL.DAT

            string ReadPathAsuval = "Asuval.dat";
            string WritePathAsuval = "NewFormat-TIGR/asuval.xml";

            #endregion

            #region Путь до файла OTYENT.DAT

            string ReadPathOtyent = "Otyent.dat";
            string WritePathOtyent = "NewFormat-TIGR/otyent.xml";

            #endregion

            #region Путь до файла OOPENT.DAT

            string ReadPathOopent = "Oopent.dat";
            string WritePathOopent = "NewFormat-TIGR/oopent.xml";

            #endregion

            #region Путь до файла OOPENT.DAT

            string ReadPathUpper = "Upper.dat";
            string WritePathUpper = "NewFormat-TIGR/upper.xml";

            #endregion

            #region Путь до файла KINET.DAT

            string ReadPathKinet = "Kinet.dat";
            string WritePathKinet = "NewFormat-TIGR/kinet.xml";

            #endregion

            #region MAIN.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathMain))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathMain, false, System.Text.Encoding.Default))
                    {

                        #region Общие данные

                        int CountCont;

                        //string[] REAC_PARAM = new string[9];
                        string[] INT_PARAM = new string[4];
                        string[] REST_PRINT_PARAM = new string[13];
                        List<List<string>> CONT_PARAM = new List<List<string>>();
                        string JNEV_T;
                        double СheckErrorMain;

                        #endregion

                        #region Прочитали, убрали комментарии и лишние пробелы

                        List<string> ArrayOfMain = new List<string>(); // Массив строк с файла Main
                        string LineOfMain;
                        while ((LineOfMain = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfMain.StartsWith("C") && !LineOfMain.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfMain) && !LineOfMain.StartsWith("!"))
                            {
                                ArrayOfMain.Add(LineOfMain.Trim());
                            }
                        }
                        // Проверяем количество элементов в файле main.dat
                        CountCont = int.Parse(ArrayOfMain[5]);

                        #endregion

                        #region Составляем массивы

                        try
                        {

                            string[] FirstStr = ArrayOfMain[0].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < 7; i++)
                            {
                                СheckErrorMain = double.Parse(FirstStr[i]);
                                REAC_PARAM[i] = FirstStr[i];
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Файл MAIN.DAT, ошибка в 1 строке, неверный формат");
                        }

                        try
                        {
                            string[] SecondStr = ArrayOfMain[1].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < 7; i++)
                            {
                                СheckErrorMain = double.Parse(SecondStr[i], formatter);
                                REST_PRINT_PARAM[i + 3] = SecondStr[i];
                            }
                            REST_PRINT_PARAM[0] = SecondStr[7];
                            REST_PRINT_PARAM[2] = SecondStr[8];
                            REST_PRINT_PARAM[12] = SecondStr[9];
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Файл MAIN.DAT, ошибка в 2 строке, неверный формат");
                        }

                        try
                        {
                            string[] ThirdStr = ArrayOfMain[2].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < 4; i++)
                            {
                                СheckErrorMain = double.Parse(ThirdStr[i], formatter);
                                INT_PARAM[i] = ThirdStr[i];
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Файл MAIN.DAT, ошибка в 3 строке, неверный формат");
                        }

                        try
                        {
                            string[] FourthStr = ArrayOfMain[3].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < 2; i++)
                            {
                                СheckErrorMain = double.Parse(FourthStr[i], formatter);
                                REST_PRINT_PARAM[i + 10] = FourthStr[i];
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Файл MAIN.DAT, ошибка в 4 строке, неверный формат");
                        }

                        try
                        {
                            string[] FifthStr = ArrayOfMain[4].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            REAC_PARAM[7] = FifthStr[0];
                            REAC_PARAM[8] = FifthStr[1];
                            REST_PRINT_PARAM[1] = FifthStr[2];
                            for (int i = 0; i < 3; i++)
                            {
                                СheckErrorMain = double.Parse(FifthStr[i], formatter);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Файл MAIN.DAT, ошибка в 5 строке, неверный формат");
                        }

                        List<string> JCNTR = new List<string>();
                        JCNTR.Add(ArrayOfMain[5]);
                        CONT_PARAM.Add(JCNTR);

                        try
                        {

                            for (int i = 1; i <= CountCont; i++)
                            {
                                List<string> Cont = new List<string>();
                                for (int j = 6 * i; j < 5 + i * 6; j++)
                                {
                                    if (j == 6 * i + 4)
                                    {
                                        string[] StrArray = ArrayOfMain[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        Cont.Add(ArrayOfMain[6 * i + 5]);
                                        for (int k = 0; k < StrArray.Length; k++)
                                        {
                                            Cont.Add(StrArray[k]);
                                            СheckErrorMain = double.Parse(StrArray[k], formatter);
                                        }
                                    }
                                    else
                                    {
                                        Cont.Add(ArrayOfMain[j]);
                                        СheckErrorMain = double.Parse(ArrayOfMain[j], formatter);
                                    }

                                }
                                CONT_PARAM.Add(Cont);
                            }

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Файл MAIN.DAT, ошибка в 5 строке, неверный формат");
                        }

                        JNEV_T = ArrayOfMain[CountCont * 6 + 6];

                        #endregion

                        #region Записываем в файл

                        sw.WriteLine("<GENERAL_DATA>");
                        sw.WriteLine(" <REAC_PARAM Comment=\"Общие ИД для реактора\">");
                        sw.WriteLine("  <JCAN Value=\"{0}\" Comment=\"Число расчётных каналов в активной зоне, включая байпас\"/>", REAC_PARAM[0]);
                        sw.WriteLine("  <JK1 Value=\"{0}\" Comment=\"Число расчётных слоев по высоте в активной зоне\"/>", REAC_PARAM[1]);
                        sw.WriteLine("  <JK2 Value=\"{0}\" Comment=\"Число расчётных слоев по высоте в необогреваемых хвостовиках ТВС\"/>", REAC_PARAM[2]);
                        sw.WriteLine("  <JO Value=\"{0}\" Comment=\"Число расчетных слоев по высоте в общем подъемном участке\"/>", REAC_PARAM[3]);
                        sw.WriteLine("  <JR Value=\"{0}\" Comment=\"Число расчетных слоев по радиусу твэлов\"/>", REAC_PARAM[4]);
                        sw.WriteLine("  <JTFT Value=\"{0}\" Comment=\"Число расчетных групп ячеек в каналах с поячейковым расчетом обогреваемой части активной зоны\"/>", REAC_PARAM[5]);
                        sw.WriteLine("  <JBORAZ Value=\"{0}\" Comment=\"Число участков, на которые делится каждый расчетный участок при описании транспорта бора\"/>", REAC_PARAM[6]);
                        sw.WriteLine("  <JKIN Value=\"{0}\" Comment=\"Модель кинетики\"/>", REAC_PARAM[7]);
                        sw.WriteLine("  <JJSNAT Value=\"{0}\" Comment=\"Тип расчета пространственной кинетики\"/>", REAC_PARAM[8]);
                        sw.WriteLine(" </REAC_PARAM>");
                        sw.WriteLine(" <INT_PARAM Comment=\"ИД интегрирования\">");
                        sw.WriteLine("  <DTMIN Value=\"{0}\" Comment=\"Минимальный шаг интегрирования\"/>", INT_PARAM[0]);
                        sw.WriteLine("  <DTMAX Value=\"{0}\" Comment=\"Максимальный шаг интегрирования\"/>", INT_PARAM[1]);
                        sw.WriteLine("  <EPSMIN Value=\"{0}\" Comment=\"Нижняя граница интервала  приращений для продолжения расчета без изменения шага\"/>", INT_PARAM[2]);
                        sw.WriteLine("  <EPSMAX Value=\"{0}\" Comment=\"Верхняя граница интервала  приращений для продолжения расчета без изменения шага\"/>", INT_PARAM[3]);
                        sw.WriteLine(" </INT_PARAM>");
                        sw.WriteLine(" <REST_PRINT_PARAM Comment=\"ИД печати и рестарта\">");
                        sw.WriteLine("  <JREAD Value=\"{0}\" Comment=\"Номер рестарта\"/>", REST_PRINT_PARAM[0]);
                        sw.WriteLine("  <DTDISK Value=\"{0}\" Comment=\"Шаг записи данных для рестарта\"/>", REST_PRINT_PARAM[1]);
                        sw.WriteLine("  <JWRITE Value=\"{0}\" Comment=\"Управление записью рестарта\"/>", REST_PRINT_PARAM[2]);
                        sw.WriteLine("  <JTIME Value=\"{0}\" Comment=\"Момент времени начала записи результатов расчета\"/>", REST_PRINT_PARAM[3]);
                        sw.WriteLine("  <PRINT_STEP1 Value=\"{0}\" Comment=\"Шаг печати №1 в файл результатов\"/>", REST_PRINT_PARAM[4]);
                        sw.WriteLine("  <PRINT_STEP2 Value=\"{0}\" Comment=\"Шаг печати №2 в файл результатов\"/>", REST_PRINT_PARAM[5]);
                        sw.WriteLine("  <PRINT_STEP3 Value=\"{0}\" Comment=\"Шаг печати №3 в файл результатов\"/>", REST_PRINT_PARAM[6]);
                        sw.WriteLine("  <PRINT_TIME1 Value=\"{0}\" Comment=\"Время переключения шага печати №1\"/>", REST_PRINT_PARAM[7]);
                        sw.WriteLine("  <PRINT_TIME2 Value=\"{0}\" Comment=\"Время переключения шага печати №2\"/>", REST_PRINT_PARAM[8]);
                        sw.WriteLine("  <PRINT_TIME3 Value=\"{0}\" Comment=\"Время окончания расчета\"/>", REST_PRINT_PARAM[9]);
                        sw.WriteLine("  <JNGR Value=\"{0}\" Comment=\"Число временных точек записи данных для графопостроителя\"/>", REST_PRINT_PARAM[10]);
                        sw.WriteLine("  <TGRAF Value=\"{0}\" Comment=\"Момент времени, начиная с которого производится запись данных для графопостроителя\"/>", REST_PRINT_PARAM[11]);
                        sw.WriteLine("  <JNP Value=\"{0}\" Comment=\"Вывод на печать дополнительной информации\"/>", REST_PRINT_PARAM[12]);
                        sw.WriteLine(" </REST_PRINT_PARAM>");
                        sw.WriteLine(" <CONT_PARAM Comment=\"ИД контуров циркуляции\">");
                        sw.WriteLine("  <JCNTR Value=\"{0}\" Comment=\"Количество контуров циркуляции\"/>", CONT_PARAM[0][0]);
                        for (int i = 1; i < CONT_PARAM.Count; i++)
                        {
                            sw.WriteLine("  <JCNTR_N Value=\"{0}\" Comment=\"Номер контура циркуляции\">", i);
                            sw.WriteLine("   <JVTOT Value=\"{0}\" Comment=\"Число расчетных элементов в контуре\"/>", CONT_PARAM[i][0]);
                            sw.WriteLine("   <Q02K Value=\"{0}\" Comment=\"Нормировочное значение мощности\"/>", CONT_PARAM[i][1]);
                            sw.WriteLine("   <G02K Value=\"{0}\" Comment=\"Нормировочное значение расхода\"/>", CONT_PARAM[i][2]);
                            sw.WriteLine("   <TOCVOL Value=\"{0}\" Comment=\"Температура окружающей среды\"/>", CONT_PARAM[i][3]);
                            sw.WriteLine("   <JGASCN Value=\"{0}\" Comment=\"Тип теплоносителя в контурах\"/>", CONT_PARAM[i][4]);
                            sw.WriteLine("   <CPGAS2 Value=\"{0}\" Comment=\"Теплоемкость неконденсирующегося газа\"/>", CONT_PARAM[i][5]);
                            sw.WriteLine("   <AMGAS2 Value=\"{0}\" Comment=\"Динамическая вязкость неконденсирующегося газа\"/>", CONT_PARAM[i][6]);
                            sw.WriteLine("   <ALGAS2 Value=\"{0}\" Comment=\"Теплопроводность неконденсирующегося газа\"/>", CONT_PARAM[i][7]);
                            sw.WriteLine("   <RGAS2 Value=\"{0}\" Comment=\"Газовая постоянная неконденсирующегося газа\"/>", CONT_PARAM[i][8]);
                            sw.WriteLine("  </JCNTR_N>");
                        }
                        sw.WriteLine(" </CONT_PARAM>");
                        sw.WriteLine(" <JNEV_T Value=\"{0}\" Comment=\"Учет невязки массы теплоносителя\"/>", JNEV_T);
                        sw.Write("</GENERAL_DATA>");

                        #endregion

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Main.dat не был найден");
            }

            #endregion

            #region VOLID.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathVolid, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathVolid, false, Encoding.Default))
                    {

                        #region Прочитали, убрали комментарии и лишние пробелы

                        List<string> ArrayOfVolid = new List<string>(); // Массив строк с файла Volid
                        string LineOfVolid;
                        while ((LineOfVolid = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfVolid.StartsWith("C") && !LineOfVolid.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfVolid) && !LineOfVolid.StartsWith("!"))
                            {
                                LineOfVolid = LineOfVolid.Trim();
                                ArrayOfVolid.Add(LineOfVolid);
                            }
                        }
                        #endregion

                        #region Все данные

                        #region Общие данные

                        int CntZiro = 0; // Для количества контуров
                        int Count = 0; // Счетчик всех строк в файле
                        int CountOfLine = 0; // Счетчик строк для каждого элемента
                        int CountOfLineInElem = 0; // Cчетчик строк в элементе (без 3-х первых)
                        int CountOfElem = 0; // Счетчик элементов в файле
                        int TypeOfElem = -1; // Тип элементов
                        List<string> NameOfElem = new List<string>(); // Сохраняем имя элемента
                        string[] NameAndCountOfString = null; // Разбиваем название элемента
                        int[] CopyData = new int[2]; // 1 число - Count, 2 число - CountOfLine
                        bool IsCopyElem = false; // Флаг, который показывает, копируются ли свойства элементов

                        #endregion

                        #region Данные для параметрозависимого объема

                        int PVOLT = 0; // Размерность давления
                        int PSVOLT = 0; // Размерность парциального давления
                        int IVOLT = 0; // Размерность энтальпии
                        int CBVOLT = 0; // Размерность концентрации борной кислоты
                        int TVOLT = 0; // Размерность температуры
                        bool IsWater = true;
                        string DVArg = null;
                        string DVType = null;

                        List<string> PVERSUST_DEP = new List<string>();
                        List<string> PSVERSUST_DEP = new List<string>();
                        List<string> IVERSUST_DEP = new List<string>();
                        List<string> CBVERSUST_DEP = new List<string>();
                        List<string> TVERSUST_DEP = new List<string>();

                        #endregion

                        #region Данные для трубы

                        int CntJMacro = 0;
                        int CntJMacrv = 0;
                        int JNMFlag = 0;

                        int K = 0;

                        string[] PipeGeneral = new string[8];
                        string[] PipeInit = new string[5];
                        List<List<string>> JMacro = new List<List<string>>();
                        List<List<string>> JNM = new List<List<string>>();
                        List<List<string>> JMacrv = new List<List<string>>();




                        #endregion

                        #region Данные для объема с уровнем

                        string[] GENERAL_VOL = new string[4];
                        string[] GEOM_VOL = new string[5];
                        List<string> VOL_TAB_H_V = new List<string>();
                        string[] STRMAT_VOLG = new string[6];
                        string[] STRMAT_VOLW = new string[6];
                        string[] VolInit = new string[6];

                        #endregion

                        #region Данные для камеры смешения

                        double FTOVOL = 0;

                        string VOLMLT_CHAMB = null;
                        string TYPE_CHAMB = null;
                        List<string> GEOM_CHAMB = new List<string>();
                        List<string> INITDATA_CHAMB = new List<string>();
                        List<string> STRMAT_CHAMB = new List<string>();


                        #endregion

                        #region Данные для объема с поддавлением

                        List<string> GENERAL_VOLGAS = new List<string>();
                        List<string> GEOM_VOLGAS = new List<string>();
                        List<string> STRMAT_VOLGAS = new List<string>();
                        List<string> INITDATA_VOLGAS = new List<string>();

                        double FTOVOL_GAS = 0;


                        #endregion

                        #endregion

                        for (int i = 0; i < ArrayOfVolid.Count; i++) // Массив для обработки всех строк
                        {

                            #region Встречаем новый элемент, прописываем первые 3 строчки: Конутр (если новый конутр), название элемента, тип элемента

                            if (TypeOfElem == -1)
                            {
                                if (CountOfLine == 0)
                                {
                                    if (CntZiro == 0)
                                    {
                                        CntZiro++;
                                        sw.WriteLine("<JCNTR Value=\"???\" Comment=\"Количество контуров\">");
                                    }
                                    CountContVolid++;
                                    sw.WriteLine(" <CONT Value=\"" + ArrayOfVolid[i] + "\">");
                                }
                                if (CountOfLine == 1)
                                {
                                    CountOfElem++;
                                    string[] ArrOfStr = ArrayOfVolid[i].Split(new string[] { "!", "#", "/" }, StringSplitOptions.RemoveEmptyEntries);
                                    NameAndCountOfString = ArrOfStr[0].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (ArrOfStr.Length >= 3)
                                    {
                                        if (NameAndCountOfString.Length == 1)
                                        {
                                            NameOfElem.Add(NameAndCountOfString[0]);
                                            NameOfElem.Add(ArrOfStr[1].Trim());
                                        }
                                        else if (NameAndCountOfString.Length == 2)
                                        {
                                            NameOfElem.Add(NameAndCountOfString[0]);
                                            NameOfElem.Add(NameAndCountOfString[1]);
                                            NameOfElem.Add(ArrOfStr[1].Trim());
                                        }
                                    }
                                    else
                                    {
                                        if (NameAndCountOfString.Length == 1)
                                        {
                                            NameOfElem.Add(NameAndCountOfString[0]);
                                        }
                                        else if (NameAndCountOfString.Length == 2)
                                        {
                                            NameOfElem.Add(NameAndCountOfString[0]);
                                            NameOfElem.Add(NameAndCountOfString[1]);
                                        }
                                    }

                                    sw.WriteLine("  <ELEM_NAME Value=\"{0}\">", NameOfElem[0]);

                                }
                                if (CountOfLine == 2)
                                {
                                    if (NameAndCountOfString.Length == 2 && NameAndCountOfString[1] != null)
                                    {
                                        TypeOfElem = 102;
                                        IsCopyElem = true;
                                        CountOfLine++;
                                        CopyData[0] = Count;
                                        CopyData[1] = CountOfLine;
                                        continue;
                                    }
                                    NameAndTypeForHSTR.Add(NameOfElem[0], ArrayOfVolid[i]);
                                    switch (ArrayOfVolid[i])
                                    {
                                        case "0":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Параметрозависимый объем\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Параметрозависимый объем\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }
                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Параметрозависимый объем\"/>", CountOfElem);
                                            }
                                            DVType = ArrayOfVolid[i];
                                            TypeOfElem = 0;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        case "1":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    if (NameOfElem.Count == 2)
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Камера смешения\"/>", CountOfElem);
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Камера смешения\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                    }

                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Камера смешения\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }
                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Камера смешения\"/>", CountOfElem);
                                            }
                                            TYPE_CHAMB = ArrayOfVolid[i];
                                            TypeOfElem = 1;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        case "2":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    if (NameOfElem.Count == 2)
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Труба\"/>", CountOfElem);
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Труба\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                    }

                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Труба\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }

                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Труба\"/>", CountOfElem);
                                            }
                                            PipeGeneral[0] = ArrayOfVolid[i];
                                            TypeOfElem = 2;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        case "3":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    if (NameOfElem.Count == 2)
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\"/>", CountOfElem);
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                    }

                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }

                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\"/>", CountOfElem);
                                            }
                                            TypeOfElem = 3;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        case "4":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    if (NameOfElem.Count == 2)
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с поддавлением\"/>", CountOfElem);
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с поддавлением\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                    }

                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с поддавлением\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }

                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с поддавлением\"/>", CountOfElem);
                                            }
                                            TypeOfElem = 4;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        case "5":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    if (NameOfElem.Count == 2)
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Обратная труба\"/>", CountOfElem);
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Обратная труба\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                    }

                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Обратная труба\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }

                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Обратная труба\"/>", CountOfElem);
                                            }
                                            PipeGeneral[0] = ArrayOfVolid[i];
                                            TypeOfElem = 5;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        case "31":
                                            if (NameOfElem.Count != 1)
                                            {
                                                if (IsCopyElem == true)
                                                {
                                                    if (NameOfElem.Count == 2)
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\"/>", CountOfElem);
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\" Description=\"{1}\"/>", CountOfElem, NameOfElem[2]);
                                                    }

                                                }
                                                else
                                                {
                                                    sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\" Description=\"{1}\"/>", CountOfElem, NameOfElem[1]);
                                                }

                                            }
                                            else
                                            {
                                                sw.WriteLine("   <ELEM_PROP Numb=\"{0}\" Comment=\"Объем с уровнем\"/>", CountOfElem);
                                            }
                                            TypeOfElem = 31;
                                            Count++;
                                            CountOfLine++;
                                            continue;
                                        default:
                                            try
                                            {
                                                throw new Exception();
                                            }
                                            finally
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в \"Тип элемента\", название элемента - {0}", ArrayOfVolid[i - 1]);
                                                Console.ReadKey(true);
                                            }
                                    }
                                }
                            }
                            #endregion

                            else
                            {

                                if (TypeOfElem == 101) // AZ_IN или AZ_OUT (Обязательно элемент должен стоять в этом if в начале, перед другими элементами)
                                {

                                    CountOfElem++;
                                    string[] ArrOfStr = ArrayOfVolid[i].Split(new string[] { "!", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (ArrOfStr.Length == 1)
                                    {
                                        sw.WriteLine("  <ELEM_NAME Value=\"" + ArrOfStr[0].Trim() + "\"" + " Comment=\"Имя расчетного элемента\"" + " Numb=\"" + CountOfElem + "\">");
                                    }
                                    else
                                    {
                                        sw.WriteLine("  <ELEM_NAME Value=\"" + ArrOfStr[0].Trim() + "\"" + " Comment=\"Имя расчетного элемента\"" + " Discription=\"{0}\"" + " Numb=\"" + CountOfElem + "\">", ArrOfStr[1]);
                                    }
                                    sw.WriteLine("  </ELEM_NAME>");

                                    EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                            ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                } // AZ_IN или AZ_OUT (Обязательно элемент должен стоять в этом if в начале, перед другими элементами)

                                if (TypeOfElem == 102) // Копирование
                                {
                                    int k = 0;
                                    foreach (var item in ArrayOfVolid)
                                    {
                                        k++;
                                        if (item.IndexOf(NameOfElem[1]) != -1)
                                        {
                                            string[] ArrOfStr = item.Split(new string[] { "!", "#" }, StringSplitOptions.RemoveEmptyEntries);
                                            if (ArrOfStr[0].Trim().Length == NameOfElem[1].Length)
                                            {
                                                i = k - 1;
                                                CountOfLine = 1;
                                                Array.Clear(NameAndCountOfString, 1, 1);
                                                TypeOfElem = -1;
                                                break;
                                            }
                                        }
                                    }
                                } // Копирование

                                if (TypeOfElem == 0) // Элемент "Параметрозависимый объем"
                                {
                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Считываем размеры таблицы

                                    if (CountOfLineInElem == 1)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *

                                        switch (ArrOfStr.Length)
                                        {
                                            case 5:
                                                try
                                                {
                                                    IsWater = true;
                                                    PVOLT = int.Parse(ArrOfStr[0]);
                                                    PSVOLT = int.Parse(ArrOfStr[1]);
                                                    IVOLT = int.Parse(ArrOfStr[2]);
                                                    CBVOLT = int.Parse(ArrOfStr[3]);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Размерности таблиц\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                PVERSUST_DEP.Add(ArrOfStr[0]);
                                                PSVERSUST_DEP.Add(ArrOfStr[1]);
                                                IVERSUST_DEP.Add(ArrOfStr[2]);
                                                CBVERSUST_DEP.Add(ArrOfStr[3]);
                                                DVArg = ArrOfStr[4];
                                                break;
                                            case 3:
                                                IsWater = false;
                                                PVOLT = int.Parse(ArrOfStr[0]);
                                                TVOLT = int.Parse(ArrOfStr[1]);
                                                PVERSUST_DEP.Add(ArrOfStr[0]);
                                                TVERSUST_DEP.Add(ArrOfStr[1]);
                                                DVArg = ArrOfStr[2];
                                                break;

                                            default:
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Размерности таблиц\" (количество символов), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                break;
                                        }
                                    }

                                    #endregion

                                    #region Считываем зависимость давления от аргумента

                                    if (CountOfLineInElem == 2)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        if (ArrOfStr.Length != 2 * PVOLT)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости давления\" (количество символов), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        double ChekErr = 0;
                                        try
                                        {
                                            for (int j = 0; j < 2 * PVOLT; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                PVERSUST_DEP.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости давления\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Считываем зависимость парциального давления от аргумента

                                    if (CountOfLineInElem == 3)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        try
                                        {
                                            switch (IsWater)
                                            {
                                                case true:
                                                    for (int j = 0; j < PSVOLT * 2; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        PSVERSUST_DEP.Add(ArrOfStr[j]);
                                                    }
                                                    if (ArrOfStr.Length != 2 * PSVOLT)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости парциального давления\" (количество символов), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    break;
                                                case false:
                                                    for (int j = 0; j < TVOLT * 2; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        TVERSUST_DEP.Add(ArrOfStr[j]);
                                                    }
                                                    if (ArrOfStr.Length != 2 * TVOLT)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости температуры\" (количество символов), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }

                                                    #region Запись в файл

                                                    sw.WriteLine("   <GENERAL_DEP Comment=\"Общие\">");
                                                    sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", DVType);
                                                    sw.WriteLine("    <DEP_JJARG Value=\"{0}\" Comment=\"Порядковый номер из файла measure.dat\"/>", DVArg);
                                                    sw.WriteLine("   </GENERAL_DEP>");
                                                    sw.WriteLine("   <PVERSUST_DEP Comment=\"Зависимость давления газа теплоносителя от внешнего аргумента\">");
                                                    sw.WriteLine("    <DEP_JPVT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", PVERSUST_DEP[0]);
                                                    for (int j = 1; j <= PVOLT; j++)
                                                    {
                                                        sw.WriteLine("    <DEP_PVOLT_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Аргумент №{1}\"/>", PVERSUST_DEP[j], j);
                                                    }
                                                    for (int j = PVOLT + 1; j < PVERSUST_DEP.Count; j++)
                                                    {
                                                        sw.WriteLine("    <DEP_PVOLT Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, МПа\"/>", PVERSUST_DEP[j], j - PVOLT);
                                                    }
                                                    sw.WriteLine("   </PVERSUST_DEP>");

                                                    sw.WriteLine("   <TVERSUST_DEP Comment=\"Зависимость температуры газа от внешнего аргумента\">");
                                                    sw.WriteLine("    <DEP_JTVT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", TVERSUST_DEP[0]);
                                                    for (int j = 1; j <= TVOLT; j++)
                                                    {
                                                        sw.WriteLine("    <DEP_TVOLT_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Аргумент №{1}\"/>", TVERSUST_DEP[j], j);
                                                    }
                                                    for (int j = TVOLT + 1; j < TVERSUST_DEP.Count; j++)
                                                    {
                                                        sw.WriteLine("    <DEP_TVOLT Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, °К\"/>", TVERSUST_DEP[j], j - TVOLT);
                                                    }
                                                    sw.WriteLine("   </TVERSUST_DEP>");
                                                    sw.WriteLine("  </ELEM_NAME>");

                                                    #endregion

                                                    EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                    ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                    #region Обнуляем переменные для "Параметрозависимого объема"

                                                    PVOLT = 0;
                                                    TVOLT = 0;
                                                    DVArg = null;
                                                    PVERSUST_DEP.Clear();
                                                    TVERSUST_DEP.Clear();

                                                    #endregion

                                                    break;
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            if (IsWater == true)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости давления\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости энтальпии\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Считываем зависимость энтальпии от аргумента

                                    if (CountOfLineInElem == 4)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        try
                                        {
                                            switch (IsWater)
                                            {
                                                case true:
                                                    for (int j = 0; j < IVOLT * 2; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        IVERSUST_DEP.Add(ArrOfStr[j]);
                                                    }
                                                    if (ArrOfStr.Length != 2 * IVOLT)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости энтальпии\" (количество символов), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    break;
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости энтальпии\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Считываем зависимость концетрации борной кислоты от аргумента, конец файла

                                    if (CountOfLineInElem == 5)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *

                                        double ChekErr = 0;
                                        try
                                        {
                                            switch (IsWater)
                                            {
                                                case true:
                                                    for (int j = 0; j < CBVOLT * 2; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        CBVERSUST_DEP.Add(ArrOfStr[j]);
                                                    }
                                                    if (ArrOfStr.Length != 2 * CBVOLT)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости концентрации бора\" (количество символов), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    break;
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Таблица зависимости концентрации бора\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }

                                        #region Запись в файл

                                        sw.WriteLine("   <GENERAL_DEP Comment=\"Общие\">");
                                        sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", DVType);
                                        sw.WriteLine("    <DEP_JJARG Value=\"{0}\" Comment=\"Порядковый номер из файла measure.dat\"/>", DVArg);
                                        sw.WriteLine("   </GENERAL_DEP>");
                                        sw.WriteLine("   <PVERSUST_DEP Comment=\"Зависимость давления теплоносителя от внешнего аргумента\">");
                                        sw.WriteLine("    <DEP_JPVT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", PVERSUST_DEP[0]);
                                        for (int j = 1; j <= PVOLT; j++)
                                        {
                                            sw.WriteLine("    <DEP_PVOLT_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Аргумент №{1}\"/>", PVERSUST_DEP[j], j);
                                        }
                                        for (int j = PVOLT + 1; j < PVERSUST_DEP.Count; j++)
                                        {
                                            sw.WriteLine("    <DEP_PVOLT Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, МПа\"/>", PVERSUST_DEP[j], j - PVOLT);
                                        }
                                        sw.WriteLine("   </PVERSUST_DEP>");
                                        sw.WriteLine("   <PSVERSUST_DEP Comment=\"Зависимость парциального давления теплоносителя от внешнего аргумента\">");
                                        sw.WriteLine("    <DEP_JPSVT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", PSVERSUST_DEP[0]);
                                        for (int j = 1; j <= PSVOLT; j++)
                                        {
                                            sw.WriteLine("    <DEP_PSVOLT_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Аргумент №{1}\"/>", PSVERSUST_DEP[j], j);
                                        }
                                        for (int j = PSVOLT + 1; j < PSVERSUST_DEP.Count; j++)
                                        {
                                            sw.WriteLine("    <DEP_PSVOLT Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, МПа\"/>", PSVERSUST_DEP[j], j - PSVOLT);
                                        }
                                        sw.WriteLine("   </PSVERSUST_DEP>");
                                        sw.WriteLine("   <IVERSUST_DEP Comment=\"Зависимость энтальпии теплоносителя от внешнего аргумента\">");
                                        sw.WriteLine("    <DEP_JIVT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", IVERSUST_DEP[0]);
                                        for (int j = 1; j <= IVOLT; j++)
                                        {
                                            sw.WriteLine("    <DEP_IVOLT_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Аргумент №{1}\"/>", IVERSUST_DEP[j], j);
                                        }
                                        for (int j = IVOLT + 1; j < IVERSUST_DEP.Count; j++)
                                        {
                                            sw.WriteLine("    <DEP_IVOLT Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, кДж/кг\"/>", IVERSUST_DEP[j], j - IVOLT);
                                        }
                                        sw.WriteLine("   </IVERSUST_DEP>");
                                        sw.WriteLine("   <CBVERSUST_DEP Comment=\"Зависимость концентрации бора от внешнего аргумента\">");
                                        sw.WriteLine("    <DEP_JCBVT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", CBVERSUST_DEP[0]);
                                        for (int j = 1; j <= CBVOLT; j++)
                                        {
                                            sw.WriteLine("    <DEP_CBVOLT_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Аргумент №{1}\"/>", CBVERSUST_DEP[j], j);
                                        }
                                        for (int j = CBVOLT + 1; j < CBVERSUST_DEP.Count; j++)
                                        {
                                            sw.WriteLine("    <DEP_CBVOLT Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, г/кг\"/>", CBVERSUST_DEP[j], j - CBVOLT);
                                        }
                                        sw.WriteLine("   </CBVERSUST_DEP>");
                                        sw.WriteLine("  </ELEM_NAME>");

                                        #endregion

                                        EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                            ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                        #region Обнуляем переменные для "Параметрозависимого объема"

                                        PVOLT = 0;
                                        PSVOLT = 0;
                                        IVOLT = 0;
                                        CBVOLT = 0;
                                        DVArg = null;
                                        PVERSUST_DEP.Clear();
                                        PSVERSUST_DEP.Clear();
                                        IVERSUST_DEP.Clear();
                                        CBVERSUST_DEP.Clear();

                                        IsCopyElem = false;
                                        Array.Clear(CopyData, 0, 2);
                                        NameOfElem.Clear();
                                        NameAndCountOfString[0] = null;

                                        #endregion

                                    }

                                    #endregion

                                } // Элемент "Параметрозависимый объем"

                                if (TypeOfElem == 1) // Камера смешения
                                {

                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Множитель параллельных геометрий

                                    if (CountOfLineInElem == 1)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                            VOLMLT_CHAMB = ArrayOfVolid[i];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), ошибка в числе \"{1}\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1], ArrayOfVolid[i]);
                                        }
                                    }

                                    #endregion

                                    #region Объем, площадь, высота расчетного элемента

                                    if (CountOfLineInElem == 2)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 3)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Геометрические характеристики\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                GEOM_CHAMB.Add(ArrOfStr[j]);
                                            }
                                            FTOVOL = double.Parse(ArrOfStr[1], formatter);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Геометрические характеристики\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Параметры теплообмена (FTOVOL>0) и начальные условия

                                    if (CountOfLineInElem == 3)
                                    {
                                        if (FTOVOL > 0)
                                        {
                                            #region Если есть теплообмен

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            double ChekErr = 0;
                                            if (ArrOfStr.Length != 6)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            try
                                            {
                                                for (int j = 0; j < ArrOfStr.Length; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    STRMAT_CHAMB.Add(ArrOfStr[j]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Если теплообмена нет

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            double ChekErr = 0;
                                            if (ArrOfStr.Length < 2 || ArrOfStr.Length == 3 || ArrOfStr.Length > 4)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            try
                                            {
                                                for (int j = 0; j < ArrOfStr.Length; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    INITDATA_CHAMB.Add(ArrOfStr[j]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            if (IsCopyElem == true)
                                            {
                                                i = CopyData[0];
                                                Count = i + 1;
                                                CountOfLine = 2;
                                                try
                                                {
                                                    ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                    VOLMLT_CHAMB = ArrayOfVolid[i];
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), ошибка в числе \"{1}\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1], ArrayOfVolid[i]);
                                                }

                                                string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *

                                                if (ArrOfStr2.Length < 2 || ArrOfStr2.Length == 3 || ArrOfStr2.Length > 4)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    INITDATA_CHAMB.Clear();
                                                    for (int j = 0; j < ArrOfStr2.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                        INITDATA_CHAMB.Add(ArrOfStr2[j]);
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                i++;
                                            }

                                            #region Запись в файл

                                            sw.WriteLine("   <GENERAL_CHAMB Comment=\"Общие\">");
                                            sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", TYPE_CHAMB);
                                            sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", VOLMLT_CHAMB);
                                            sw.WriteLine("   </GENERAL_CHAMB>");
                                            sw.WriteLine("   <GEOM_CHAMB Comment=\"Геометрические характеристики\">");
                                            sw.WriteLine("    <CHAMB_VVOL Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM_CHAMB[0]);
                                            sw.WriteLine("    <CHAMB_DZVOL Value=\"{0}\" Comment=\"Высота, м\"/>", GEOM_CHAMB[2]);
                                            sw.WriteLine("    <CHAMB_FTOVOL Value=\"{0}\" Comment=\"Площадь поверхности теплообмена с теплоносителем, м^2\"/>", GEOM_CHAMB[1]);
                                            sw.WriteLine("   </GEOM_CHAMB>");
                                            sw.WriteLine("   <INITDATA_CHAMB Comment=\"Начальные условия\">");
                                            if (INITDATA_CHAMB.Count == 4)
                                            {
                                                sw.WriteLine("    <CHAMB_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", INITDATA_CHAMB[0]);
                                                sw.WriteLine("    <CHAMB_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", INITDATA_CHAMB[1]);
                                                sw.WriteLine("    <CHAMB_IVOL Value=\"{0}\" Comment=\"Энтальпия теплоносителя, кДж/кг\"/>", INITDATA_CHAMB[2]);
                                                sw.WriteLine("    <CHAMB_CBOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", INITDATA_CHAMB[3]);
                                            }
                                            else
                                            {
                                                sw.WriteLine("    <CHAMB_PVOL Value=\"{0}\" Comment=\"Давление, МПа\"/>", INITDATA_CHAMB[0]);
                                                sw.WriteLine("    <CHAMB_TETVOL Value=\"{0}\" Comment=\"Температура газа, °K\"/>", INITDATA_CHAMB[1]);

                                            }
                                            sw.WriteLine("   </INITDATA_CHAMB>");
                                            sw.WriteLine("  </ELEM_NAME>");

                                            #endregion

                                            EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                            #region Обнуляем переменные для "Камеры смешения"

                                            VOLMLT_CHAMB = null;
                                            FTOVOL = 0;
                                            GEOM_CHAMB.Clear();
                                            INITDATA_CHAMB.Clear();

                                            IsCopyElem = false;
                                            Array.Clear(CopyData, 0, 2);
                                            NameOfElem.Clear();
                                            NameAndCountOfString[0] = null;

                                            #endregion

                                            #endregion

                                        }
                                    }

                                    #endregion

                                    #region Начальные условия при FTOVOL > 0

                                    if (CountOfLineInElem == 4 && FTOVOL > 0)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length < 2 || ArrOfStr.Length == 3 || ArrOfStr.Length > 4)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                INITDATA_CHAMB.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }

                                        if (IsCopyElem == true)
                                        {
                                            i = CopyData[0];
                                            Count = i + 1;
                                            CountOfLine = 2;
                                            try
                                            {
                                                ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                VOLMLT_CHAMB = ArrayOfVolid[i];
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), ошибка в числе \"{1}\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1], ArrayOfVolid[i]);
                                            }

                                            string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *

                                            if (ArrOfStr2.Length < 2 || ArrOfStr2.Length == 3 || ArrOfStr2.Length > 4)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            try
                                            {
                                                INITDATA_CHAMB.Clear();
                                                for (int j = 0; j < ArrOfStr2.Length; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                    INITDATA_CHAMB.Add(ArrOfStr2[j]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            i++;
                                        }

                                        #region Запись в файл

                                        sw.WriteLine("   <GENERAL_CHAMB Comment=\"Общие\">");
                                        sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", TYPE_CHAMB);
                                        sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", VOLMLT_CHAMB);
                                        sw.WriteLine("   </GENERAL_CHAMB>");
                                        sw.WriteLine("   <GEOM_CHAMB Comment=\"Геометрические характеристики\">");
                                        sw.WriteLine("    <CHAMB_VVOL Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM_CHAMB[0]);
                                        sw.WriteLine("    <CHAMB_DZVOL Value=\"{0}\" Comment=\"Высота, м\"/>", GEOM_CHAMB[2]);
                                        sw.WriteLine("    <CHAMB_FTOVOL Value=\"{0}\" Comment=\"Площадь поверхности теплообмена с теплоносителем, м^2\"/>", GEOM_CHAMB[1]);
                                        sw.WriteLine("   </GEOM_CHAMB>");
                                        sw.WriteLine("   <STRMAT_CHAMB Comment=\"Свойства кострукционных материалов\">");
                                        sw.WriteLine("    <CHAMB_JNM Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_CHAMB[5]);
                                        sw.WriteLine("    <CHAMB_CMVOL Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_CHAMB[0]);
                                        sw.WriteLine("    <CHAMB_RMVOL Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_CHAMB[1]);
                                        sw.WriteLine("    <CHAMB_DLVOL Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_CHAMB[2]);
                                        sw.WriteLine("    <CHAMB_LAMBDA Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_CHAMB[3]);
                                        sw.WriteLine("    <CHAMB_KOCVOL Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_CHAMB[4]);
                                        sw.WriteLine("   </STRMAT_CHAMB>");
                                        sw.WriteLine("   <INITDATA_CHAMB Comment=\"Начальные условия\">");
                                        if (INITDATA_CHAMB.Count == 4)
                                        {
                                            sw.WriteLine("    <CHAMB_PVOL Value=\"{0}\" Comment=\"Давление, МПа\"/>", INITDATA_CHAMB[0]);
                                            sw.WriteLine("    <CHAMB_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", INITDATA_CHAMB[1]);
                                            sw.WriteLine("    <CHAMB_IVOL Value=\"{0}\" Comment=\"Энтальпия теплоносителя, кДж/кг\"/>", INITDATA_CHAMB[2]);
                                            sw.WriteLine("    <CHAMB_CBOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", INITDATA_CHAMB[3]);
                                        }
                                        else
                                        {
                                            sw.WriteLine("    <CHAMB_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", INITDATA_CHAMB[0]);
                                            sw.WriteLine("    <CHAMB_TETVOL Value=\"{0}\" Comment=\"Температура газа, °K\"/>", INITDATA_CHAMB[1]);

                                        }
                                        sw.WriteLine("   </INITDATA_CHAMB>");
                                        sw.WriteLine("  </ELEM_NAME>");

                                        #endregion

                                        EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                            ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                        #region Обнуляем переменные для "Камеры смешения"

                                        VOLMLT_CHAMB = null;
                                        FTOVOL = 0;
                                        GEOM_CHAMB.Clear();
                                        INITDATA_CHAMB.Clear();
                                        STRMAT_CHAMB.Clear();

                                        IsCopyElem = false;
                                        Array.Clear(CopyData, 0, 2);
                                        NameOfElem.Clear();
                                        NameAndCountOfString[0] = null;

                                        #endregion
                                    }

                                    #endregion

                                } // Элемент "Камера смешения"

                                if (TypeOfElem == 2) // Элемент "Труба"
                                {

                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Множитель параллельных геометрий

                                    if (CountOfLineInElem == 1)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        PipeGeneral[1] = ArrayOfVolid[i];
                                    }

                                    #endregion

                                    #region Количество гидравлических макроучастков

                                    else if (CountOfLineInElem == 2)
                                    {
                                        try
                                        {
                                            PipeGeneral[2] = ArrayOfVolid[i];
                                            CntJMacro = int.Parse(ArrayOfVolid[i]); // Запомним колличество участков
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Количество гидравлических макроучастков\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Считываем все параметры для гидравлических макроучастков

                                    else if (CountOfLineInElem <= (2 + CntJMacro))
                                    {
                                        string[] ArrOfStr2 = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        string[] ArrOfStr = new string[9];
                                        for (int j = 0; j < 9; j++)
                                        {
                                            ArrOfStr[j] = ArrOfStr2[j];
                                        }
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        List<string> J = new List<string>();
                                        double ChekErr = 0;
                                        foreach (var item in ArrOfStr)
                                        {
                                            try
                                            {
                                                ChekErr = double.Parse(item, formatter);
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Геометрические и гидравлические элементы\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            J.Add(item);
                                        }
                                        JMacro.Add(J);
                                    }

                                    #endregion

                                    #region Запись параметров гидравлических макроучастков; Количество макроучастков теплообмена

                                    else if (CountOfLineInElem == ((2 + CntJMacro) + 1))
                                    {
                                        for (int j = 0; j < CntJMacro; j++)
                                        {
                                            if (JMacro[j].Count != 9)
                                            {
                                                Console.WriteLine("Неверное количество параметров в строке \"Геометрические и гидравлические элементы\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                        }

                                        #region Количество макроучастков теплообмена

                                        try
                                        {
                                            CntJMacrv = int.Parse(ArrayOfVolid[i]); // Запомним колличество участков
                                            PipeGeneral[3] = ArrayOfVolid[i];
                                            K = 2 * CntJMacrv;
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Количество макроучастков теплообмена\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }


                                        #endregion

                                    }

                                    #endregion

                                    #region Считываем Количество расчетных участков теплообмена по толщине и по длине

                                    else if ((CountOfLineInElem >= ((2 + CntJMacro) + 2)) && K != 0)
                                    {
                                        if (K % 2 == 0) // На четные числа К мы считываем JNM, на нечетные - параметры для JNM
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            List<string> J = new List<string>();
                                            double ChekErr = 0;
                                            foreach (var item in ArrOfStr)
                                            {
                                                try
                                                {
                                                    ChekErr = double.Parse(item, formatter);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                J.Add(item);
                                            }
                                            JNM.Add(J);
                                            try
                                            {
                                                JNMFlag = int.Parse(ArrOfStr[0]);
                                                if (JNMFlag == 0)
                                                {
                                                    K = K - 2;
                                                }
                                                else if (JNMFlag > 0)
                                                {
                                                    K = K - 1;
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Файл Volid.dat, ошибка, JNM < 0, название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Количество расчетных участков теплообмена по толщине\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                        }
                                        else // Если нечетное число К - считываем параметры для JNM
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            List<string> J = new List<string>();
                                            double ChekErr = 0;
                                            foreach (var item in ArrOfStr)
                                            {
                                                try
                                                {
                                                    ChekErr = double.Parse(item, formatter);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                J.Add(item);
                                            }
                                            JMacrv.Add(J);
                                            K--;

                                        }

                                    }

                                    #endregion

                                    else
                                    {

                                        #region Поправочные коэффициенты

                                        if (CountOfLineInElem == (((2 + CntJMacro) + 2) + JNM.Count + JMacrv.Count))
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            double ChekErr;
                                            try
                                            {
                                                if (ArrOfStr.Length != 3)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Поправочные коэффициенты\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                for (int j = 0; j < 3; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    PipeGeneral[j + 4] = ArrOfStr[j];
                                                }


                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Поправочные коэффициенты\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                        }

                                        #endregion

                                        #region Параматры для водяного/газового контура; Конец расчетного элемента "Труба"

                                        else if (CountOfLineInElem == (((2 + CntJMacro) + 2) + JNM.Count + JMacrv.Count + 1))
                                        {
                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            if (ArrOfStr.Length == 5)
                                            {

                                                #region Если конутр водяной

                                                double ChekErr = 0;
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        PipeInit[j] = ArrOfStr[j];
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    }

                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                #endregion

                                            }
                                            else if (ArrOfStr.Length == 4)
                                            {

                                                #region Если контур газовый

                                                double ChekErr = 0;
                                                try
                                                {
                                                    int j = 0;
                                                    foreach (var item in ArrOfStr)
                                                    {
                                                        PipeInit[j] = item;
                                                        ChekErr = double.Parse(item, formatter);
                                                        j++;
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                #endregion

                                            }
                                            else
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного/газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            PipeGeneral[7] = "0"; // Признак учета негомогенности

                                            if (IsCopyElem == true)
                                            {
                                                i = CopyData[0];
                                                Count = i + 1;
                                                CountOfLine = 2;
                                                double ChekErr = 0;
                                                try
                                                {
                                                    ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                PipeGeneral[1] = ArrayOfVolid[i];

                                                string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                if (ArrOfStr2.Length == 5)
                                                {

                                                    #region Если конутр водяной

                                                    ChekErr = 0;
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            PipeInit[j] = ArrOfStr2[j];
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                        }

                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }

                                                    #endregion

                                                }
                                                else if (ArrOfStr2.Length == 4)
                                                {

                                                    #region Если контур газовый

                                                    ChekErr = 0;
                                                    try
                                                    {
                                                        int j = 0;
                                                        foreach (var item in ArrOfStr2)
                                                        {
                                                            PipeInit[j] = item;
                                                            ChekErr = double.Parse(item, formatter);
                                                            j++;
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }

                                                    #endregion

                                                }
                                                else
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного/газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                i++;
                                            }

                                            #region Записываем в файл

                                            sw.WriteLine("   <GENERAL_PIPE Comment=\"Общие\">");
                                            sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", PipeGeneral[0]);
                                            sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", PipeGeneral[1]);
                                            sw.WriteLine("    <PIPE_JMACRO Value=\"{0}\" Comment=\"Количество гидравлических макроучастков\"/>", PipeGeneral[2]);
                                            sw.WriteLine("    <PIPE_JMACRV Value=\"{0}\" Comment=\"Количество макроучастков теплообмена\"/>", PipeGeneral[3]);
                                            sw.WriteLine("    <PIPE_ALFKSG Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете коэффициента теплоотдачи\"/>", PipeGeneral[4]);
                                            sw.WriteLine("    <PIPE_POPSG Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете коэффициента трения\"/>", PipeGeneral[5]);
                                            sw.WriteLine("    <PIPE_JALFA Value=\"{0}\" Comment=\"Методика расчета коэффициента теплоотдачи\"/>", PipeGeneral[6]);
                                            sw.WriteLine("    <PIPE_JNEGOM Value=\"{0}\" Comment=\"Признак учета негомогенности\"/>", PipeGeneral[7]);
                                            sw.WriteLine("   </GENERAL_PIPE>");
                                            sw.WriteLine("   <GEOM_PIPE Comment=\"Геометрические и гидравлические характеристики\">");
                                            for (int j = 0; j < CntJMacro; j++)
                                            {
                                                sw.WriteLine("    <PIPE_JMACRO_N Value=\"{0}\" Comment=\"Номер гидравлического макроучастка\">", j + 1);

                                                sw.WriteLine("     <PIPE_V2SG Value=\"{0}\" Comment=\"Объем, м^3\"/>", JMacro[j][0]);
                                                sw.WriteLine("     <PIPE_S2SG Value=\"{0}\" Comment=\"Площадь проходного сечения, м^2\"/>", JMacro[j][2]);
                                                sw.WriteLine("     <PIPE_DGSG Value=\"{0}\" Comment=\"Гидравлический диаметр, м\"/>", JMacro[j][3]);
                                                sw.WriteLine("     <PIPE_AL Value=\"{0}\" Comment=\"Длина, м\"/>", JMacro[j][1]);
                                                sw.WriteLine("     <PIPE_DZSG Value=\"{0}\" Comment=\"Разность высотных отметок, м\"/>", JMacro[j][7]);
                                                sw.WriteLine("     <PIPE_AKSSG Value=\"{0}\" Comment=\"Коэффициент местных сопротивлений\"/>", JMacro[j][4]);
                                                sw.WriteLine("     <PIPE_SHERSG Value=\"{0}\" Comment=\"Шероховатость, м\"/>", JMacro[j][5]);
                                                sw.WriteLine("     <PIPE_INMPG Value=\"{0}\" Comment=\"Коэффициент механической инерции теплоносителя, 10^(-6)∙кг/(м∙с)\"/>", JMacro[j][6]);
                                                sw.WriteLine("     <PIPE_JV Value=\"{0}\" Comment=\"Количество гидравлических расчетных участков\"/>", JMacro[j][8]);

                                                sw.WriteLine("    </PIPE_JMACRO_N>");
                                            }
                                            sw.WriteLine("   </GEOM_PIPE>");
                                            if (PipeGeneral[3] != "0")
                                            {
                                                sw.WriteLine("   <STRMAT_PIPE Comment=\"Свойства кострукционных материалов\">");
                                                for (int j = 0; j < JNM.Count; j++)
                                                {
                                                    sw.WriteLine("    <PIPE_JMACRV_N Value=\"{0}\" Comment=\"Номер макроучастка теплообмена\">", j + 1);

                                                    sw.WriteLine("     <PIPE_JVV Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по длине\"/>", JNM[j][1]);
                                                    sw.WriteLine("     <PIPE_JNM Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", JNM[j][0]);

                                                    if (JNM[j][0] != "0")
                                                    {
                                                        sw.WriteLine("     <PIPE_CM Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", JMacrv[j][0]);
                                                        sw.WriteLine("     <PIPE_RM Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", JMacrv[j][1]);
                                                        sw.WriteLine("     <PIPE_DL Value=\"{0}\" Comment=\"Толщина, м\"/>", JMacrv[j][3]);
                                                        sw.WriteLine("     <PIPE_ALMD Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", JMacrv[j][2]);
                                                        sw.WriteLine("     <PIPE_ALOCSG Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", JMacrv[j][4]);
                                                        sw.WriteLine("     <PIPE_FMSG Value=\"{0}\" Comment=\"Площадь поверхности теплообмена с теплоносителем, м^2\"/>", JMacrv[j][5]);
                                                    }

                                                    sw.WriteLine("    </PIPE_JMACRV_N>");
                                                }
                                                sw.WriteLine("   </STRMAT_PIPE>");
                                            }
                                            if (ArrOfStr.Length == 5)
                                            {
                                                sw.WriteLine("   <INITDATA_PIPE Comment=\"Начальные условия\">");
                                                sw.WriteLine("     <PIPE_PVOL Value=\"{0}\" Comment=\"Давление на выходе, МПа\"/>", PipeInit[0]);
                                                sw.WriteLine("     <PIPE_DP Value=\"{0}\" Comment=\"Перепад давления от входа до выхода, МПа\"/>", PipeInit[3]);
                                                sw.WriteLine("     <PIPE_IPG Value=\"{0}\" Comment=\"Энтальпия теплоносителя на выходе, кДж/кг\"/>", PipeInit[1]);
                                                sw.WriteLine("     <PIPE_IPB Value=\"{0}\" Comment=\"Энтальпия теплоносителя на входе, кДж/кг\"/>", PipeInit[2]);
                                                sw.WriteLine("     <PIPE_CBOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", PipeInit[4]);
                                                sw.WriteLine("   </INITDATA_PIPE>");
                                            }
                                            else
                                            {
                                                sw.WriteLine("   <INITDATA_PIPE Comment=\"Начальные условия\">");
                                                sw.WriteLine("     <PIPE_PVOL Value=\"{0}\" Comment=\"Давление на выходе, МПа\"/>", PipeInit[0]);
                                                sw.WriteLine("     <PIPE_TBYX Value=\"{0}\" Comment=\"Температура газа на выходе, K\"/>", PipeInit[1]);
                                                sw.WriteLine("     <PIPE_TBX Value=\"{0}\" Comment=\"Температура газа на входе, K\"/>", PipeInit[2]);
                                                sw.WriteLine("     <PIPE_DP Value=\"{0}\" Comment=\"Перепад давления от входа до выхода, МПа\"/>", PipeInit[3]);
                                            }
                                            sw.WriteLine("  </ELEM_NAME>");

                                            #endregion

                                            EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                            #region Обнуляем переменные для следующих элементов "Труба"

                                            CntJMacro = 0;
                                            CntJMacrv = 0;
                                            JNMFlag = 0;
                                            JMacro.Clear();
                                            JMacrv.Clear();
                                            JNM.Clear();
                                            K = 0;
                                            Array.Clear(PipeGeneral, 0, 8);
                                            Array.Clear(PipeInit, 0, 5);

                                            IsCopyElem = false;
                                            Array.Clear(CopyData, 0, 2);
                                            NameOfElem.Clear();
                                            NameAndCountOfString[0] = null;

                                            #endregion

                                        }

                                        #endregion

                                    }

                                } // Элемент "Труба"

                                if (TypeOfElem == 3) // Элемент "Объем с уровнем"
                                {
                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Множитель параллельных геометрий

                                    if (CountOfLineInElem == 1)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                            GENERAL_VOL[1] = ArrayOfVolid[i];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }


                                    #endregion

                                    #region Геометрические исходные данные

                                    if (CountOfLineInElem == 2)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 5)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Общие\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                            }
                                            GENERAL_VOL[0] = "3";
                                            GENERAL_VOL[2] = ArrOfStr[3];
                                            GENERAL_VOL[3] = ArrOfStr[4];
                                            GEOM_VOL[0] = ArrOfStr[1];
                                            GEOM_VOL[1] = ArrOfStr[2];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Общие\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Размерность таблицы

                                    if (CountOfLineInElem == 3)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                            GEOM_VOL[4] = ArrayOfVolid[i];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Размерность таблицы зависимости объема от высоты\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Значения таблицы (Высоты)

                                    if (CountOfLineInElem == 4)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != double.Parse(GEOM_VOL[4], formatter))
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Высота (таблица)\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                VOL_TAB_H_V.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Высота (таблица)\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Значения таблицы (Объемы)

                                    if (CountOfLineInElem == 5)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != double.Parse(GEOM_VOL[4], formatter))
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Объемы (таблица)\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                VOL_TAB_H_V.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Объемы (таблица)\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Периметр теплообмена

                                    if (CountOfLineInElem == 6)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 2)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Периметр теплообмена\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                            }
                                            GEOM_VOL[2] = ArrOfStr[0];
                                            GEOM_VOL[3] = ArrOfStr[1];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Периметр теплообмена\" (неверный формат, возможно ошибка из-за \"*\"), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    try
                                    {
                                        if (double.Parse(GEOM_VOL[2], formatter) > 0 && double.Parse(GEOM_VOL[3], formatter) > 0)
                                        {

                                            #region Свойства кострукционных материалов над уровнем

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLG[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLG[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Свойства кострукционных материалов под уровнем

                                            if (CountOfLineInElem == 8)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLW[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLW[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Начальные условия

                                            if (CountOfLineInElem == 9)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = (int)double.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - (int)double.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <STRMAT_VOLG Comment=\"Свойства кострукционных материалов над уровнем\">");
                                                sw.WriteLine("    <VOL_JNMG Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLG[0]);
                                                sw.WriteLine("    <VOL_CMSG Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLG[1]);
                                                sw.WriteLine("    <VOL_RMSG Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLG[2]);
                                                sw.WriteLine("    <VOL_DLSG Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLG[3]);
                                                sw.WriteLine("    <VOL_LMBDG Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLG[4]);
                                                sw.WriteLine("    <VOL_KOCSGC Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLG[5]);
                                                sw.WriteLine("   </STRMAT_VOLG>");
                                                sw.WriteLine("   <STRMAT_VOLW Comment=\"Свойства кострукционных материалов под уровнем\">");
                                                sw.WriteLine("    <VOL_JNMW Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLW[0]);
                                                sw.WriteLine("    <VOL_CMWS Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLW[1]);
                                                sw.WriteLine("    <VOL_RMWS Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLW[2]);
                                                sw.WriteLine("    <VOL_DLWS Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLW[3]);
                                                sw.WriteLine("    <VOL_LMBDW Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLW[4]);
                                                sw.WriteLine("    <VOL_KOCVOL Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLW[5]);
                                                sw.WriteLine("   </STRMAT_VOLW>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion

                                        }
                                        else if (double.Parse(GEOM_VOL[2], formatter) == 0 && double.Parse(GEOM_VOL[3], formatter) > 0)
                                        {
                                            #region Свойства кострукционных материалов под уровнем

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLW[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLW[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Начальные условия

                                            if (CountOfLineInElem == 8)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = int.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - int.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <STRMAT_VOLW Comment=\"Свойства кострукционных материалов под уровнем\">");
                                                sw.WriteLine("    <VOL_JNMW Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLW[0]);
                                                sw.WriteLine("    <VOL_CMWS Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLW[1]);
                                                sw.WriteLine("    <VOL_RMWG Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLW[2]);
                                                sw.WriteLine("    <VOL_DLWG Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLW[3]);
                                                sw.WriteLine("    <VOL_LMBDW Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLW[4]);
                                                sw.WriteLine("    <VOL_KOCVOL Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLW[5]);
                                                sw.WriteLine("   </STRMAT_VOLW>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion
                                        }
                                        else if (double.Parse(GEOM_VOL[2], formatter) > 0 && double.Parse(GEOM_VOL[3], formatter) == 0)
                                        {
                                            #region Свойства кострукционных материалов над уровнем

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLG[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLG[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Начальные условия

                                            if (CountOfLineInElem == 8)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = int.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - int.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <STRMAT_VOLG Comment=\"Свойства кострукционных материалов над уровнем\">");
                                                sw.WriteLine("    <VOL_JNMG Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLG[0]);
                                                sw.WriteLine("    <VOL_CMSG Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLG[1]);
                                                sw.WriteLine("    <VOL_RMSG Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLG[2]);
                                                sw.WriteLine("    <VOL_DLSG Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLG[3]);
                                                sw.WriteLine("    <VOL_LMBDG Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLG[4]);
                                                sw.WriteLine("    <VOL_KOCSGC Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLG[5]);
                                                sw.WriteLine("   </STRMAT_VOLG>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Начальные условия

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = int.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - int.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion
                                        }
                                    }
                                    catch (ArgumentNullException)
                                    {


                                    }

                                } // Элемент "Объем с уровнем"

                                if (TypeOfElem == 4) // Элемент "Объем с поддавлением"
                                {
                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Множитель параллельных геометрий

                                    if (CountOfLineInElem == 1)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                            GENERAL_VOLGAS.Add("4");
                                            GENERAL_VOLGAS.Add(ArrayOfVolid[i]);

                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), ошибка в числе \"{1}\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1], ArrayOfVolid[i]);
                                        }
                                    }

                                    #endregion

                                    #region Геометрические характеристики

                                    if (CountOfLineInElem == 2)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 5)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Геометрические характеристики\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length - 1; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                GEOM_VOLGAS.Add(ArrOfStr[j]);
                                            }
                                            GENERAL_VOLGAS.Add(ArrOfStr[4]);
                                            FTOVOL_GAS = double.Parse(ArrOfStr[2], formatter);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Геометрические характеристики\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Свойства конструкционных материалов; Начальные условия

                                    if (CountOfLineInElem == 3)
                                    {
                                        if (FTOVOL_GAS > 0)
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            double ChekErr = 0;
                                            if (ArrOfStr.Length != 6)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства конструкционных материалов\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            try
                                            {
                                                for (int j = 0; j < ArrOfStr.Length; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    STRMAT_VOLGAS.Add(ArrOfStr[j]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства конструкционных материалов\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                        }
                                        else
                                        {
                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            double ChekErr = 0;
                                            if (ArrOfStr.Length != 5)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            try
                                            {
                                                for (int j = 0; j < ArrOfStr.Length; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    INITDATA_VOLGAS.Add(ArrOfStr[j]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            if (IsCopyElem == true)
                                            {
                                                i = CopyData[0];
                                                Count = i + 1;
                                                CountOfLine = 2;

                                                try
                                                {
                                                    ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                    GENERAL_VOLGAS[1] = ArrayOfVolid[i];
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), ошибка в числе \"{1}\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1], ArrayOfVolid[i]);
                                                }

                                                string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                if (ArrOfStr2.Length != 5)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr2.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                        INITDATA_VOLGAS[j] = ArrOfStr2[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                i++;
                                            }

                                            #region Запись в файл

                                            sw.WriteLine("   <GENERAL_VOLGAS Comment=\"Общие\">");
                                            sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOLGAS[0]);
                                            sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOLGAS[1]);
                                            sw.WriteLine("    <VOLGAS_AURKDJ Value=\"{0}\" Comment=\"Коэффициент теплопередачи через уровень, кДж/(м^2∙К) \"/>", GENERAL_VOLGAS[2]);
                                            sw.WriteLine("   </GENERAL_VOLGAS>");
                                            sw.WriteLine("   <GEOM_VOLGAS Comment=\"Геометрические характеристики\">");
                                            sw.WriteLine("    <VOLGAS_VVOL Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM_VOLGAS[0]);
                                            sw.WriteLine("    <VOLGAS_DZVOL Value=\"{0}\" Comment=\"Высота, м\"/>", GEOM_VOLGAS[1]);
                                            sw.WriteLine("    <VOLGAS_SKDJ Value=\"{0}\" Comment=\"Площадь поверхности теплообмена через уровень, м^2\"/>", GEOM_VOLGAS[3]);
                                            sw.WriteLine("    <VOLGAS_FTOVOL Value=\"{0}\" Comment=\"Площадь теплообмена с конструкционными материалами, м^2\"/>", GEOM_VOLGAS[2]);
                                            sw.WriteLine("   </GEOM_VOLGAS>");
                                            sw.WriteLine("   <INITDATA_VOLGAS Comment=\"Начальные условия\">");
                                            sw.WriteLine("    <VOLGAS_PVOL Value=\"{0}\" Comment=\"Давление, МПа\"/>", INITDATA_VOLGAS[0]);
                                            sw.WriteLine("    <VOLGAS_IVOL Value=\"{0}\" Comment=\"Энтальпия теплоносителя, кДж/кг\"/>", INITDATA_VOLGAS[2]);
                                            sw.WriteLine("    <VOLGAS_TGKDJ Value=\"{0}\" Comment=\"Температура газа, °К\"/>", INITDATA_VOLGAS[3]);
                                            sw.WriteLine("    <VOLGAS_VWKDJ Value=\"{0}\" Comment=\"Объем, заполненный теплоносителем, м^3\"/>", INITDATA_VOLGAS[1]);
                                            sw.WriteLine("    <VOLGAS_CBOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", INITDATA_VOLGAS[4]);
                                            sw.WriteLine("   </INITDATA_VOL>");

                                            #endregion

                                            EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                            ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                            #region Обнуляем переменные для "Объем с газовым поддавлением"

                                            GENERAL_VOLGAS.Clear();
                                            GEOM_VOLGAS.Clear();
                                            INITDATA_VOLGAS.Clear();

                                            IsCopyElem = false;
                                            Array.Clear(CopyData, 0, 2);
                                            NameOfElem.Clear();
                                            NameAndCountOfString[0] = null;

                                            #endregion

                                        }

                                    }

                                    #endregion

                                    #region Начальные условия

                                    if (CountOfLineInElem == 4)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 5)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                INITDATA_VOLGAS.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }

                                        if (IsCopyElem == true)
                                        {
                                            i = CopyData[0];
                                            Count = i + 1;
                                            CountOfLine = 2;

                                            try
                                            {
                                                ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                GENERAL_VOLGAS[1] = ArrayOfVolid[i];
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), ошибка в числе \"{1}\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1], ArrayOfVolid[i]);
                                            }

                                            string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                            if (ArrOfStr2.Length != 5)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                            try
                                            {
                                                for (int j = 0; j < ArrOfStr2.Length; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                    INITDATA_VOLGAS[j] = ArrOfStr2[j];
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            i++;
                                        }

                                        #region Запись в файл

                                        sw.WriteLine("   <GENERAL_VOLGAS Comment=\"Общие\">");
                                        sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOLGAS[0]);
                                        sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOLGAS[1]);
                                        sw.WriteLine("    <VOLGAS_AURKDJ Value=\"{0}\" Comment=\"Коэффициент теплопередачи через уровень, кДж/(м^2∙К) \"/>", GENERAL_VOLGAS[2]);
                                        sw.WriteLine("   </GENERAL_VOLGAS>");
                                        sw.WriteLine("   <GEOM_VOLGAS Comment=\"Геометрические характеристики\">");
                                        sw.WriteLine("    <VOLGAS_VVOL Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM_VOLGAS[0]);
                                        sw.WriteLine("    <VOLGAS_DZVOL Value=\"{0}\" Comment=\"Высота, м\"/>", GEOM_VOLGAS[1]);
                                        sw.WriteLine("    <VOLGAS_SKDJ Value=\"{0}\" Comment=\"Площадь поверхности теплообмена через уровень, м^2\"/>", GEOM_VOLGAS[3]);
                                        sw.WriteLine("    <VOLGAS_FTOVOL Value=\"{0}\" Comment=\"Площадь теплообмена с конструкционными материалами, м^2\"/>", GEOM_VOLGAS[2]);
                                        sw.WriteLine("   </GEOM_VOLGAS>");
                                        sw.WriteLine("   <STRMAT_VOLGAS Comment=\"Свойства кострукционных материалов\">");
                                        sw.WriteLine("    <VOLGAS_JNM Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLGAS[5]);
                                        sw.WriteLine("    <VOLGAS_CMET Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLGAS[0]);
                                        sw.WriteLine("    <VOLGAS_GAMMET Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLGAS[1]);
                                        sw.WriteLine("    <VOLGAS_DLMVOL Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLGAS[2]);
                                        sw.WriteLine("    <VOLGAS_ALMET Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLGAS[3]);
                                        sw.WriteLine("    <VOLGAS_KOCVOL Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLGAS[4]);
                                        sw.WriteLine("   </STRMAT_VOLGAS>");
                                        sw.WriteLine("   <INITDATA_VOLGAS Comment=\"Начальные условия\">");
                                        sw.WriteLine("    <VOLGAS_PVOL Value=\"{0}\" Comment=\"Давление, МПа\"/>", INITDATA_VOLGAS[0]);
                                        sw.WriteLine("    <VOLGAS_IVOL Value=\"{0}\" Comment=\"Энтальпия теплоносителя, кДж/кг\"/>", INITDATA_VOLGAS[2]);
                                        sw.WriteLine("    <VOLGAS_TGKDJ Value=\"{0}\" Comment=\"Температура газа, °К\"/>", INITDATA_VOLGAS[3]);
                                        sw.WriteLine("    <VOLGAS_VWKDJ Value=\"{0}\" Comment=\"Объем, заполненный теплоносителем, м^3\"/>", INITDATA_VOLGAS[1]);
                                        sw.WriteLine("    <VOLGAS_CBOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", INITDATA_VOLGAS[4]);
                                        sw.WriteLine("   </INITDATA_VOL>");

                                        #endregion

                                        EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                        #region Обнуляем переменные для "Объем с газовым поддавлением"

                                        GENERAL_VOLGAS.Clear();
                                        GEOM_VOLGAS.Clear();
                                        STRMAT_VOLGAS.Clear();
                                        INITDATA_VOLGAS.Clear();

                                        IsCopyElem = false;
                                        Array.Clear(CopyData, 0, 2);
                                        NameOfElem.Clear();
                                        NameAndCountOfString[0] = null;

                                        #endregion
                                    }

                                    #endregion

                                } // Элемент "Объем с поддавлением"

                                if (TypeOfElem == 5) // Элемент "Обратная(?) Труба"
                                {

                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Множитель параллельных геометрий

                                    if (CountOfLineInElem == 1)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        PipeGeneral[1] = ArrayOfVolid[i];
                                    }

                                    #endregion

                                    #region Количество гидравлических макроучастков

                                    else if (CountOfLineInElem == 2)
                                    {
                                        try
                                        {
                                            PipeGeneral[2] = ArrayOfVolid[i];
                                            CntJMacro = int.Parse(ArrayOfVolid[i]); // Запомним колличество участков
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Количество гидравлических макроучастков\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Считываем все параметры для гидравлических макроучастков

                                    else if (CountOfLineInElem <= (2 + CntJMacro))
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        List<string> J = new List<string>();
                                        double ChekErr = 0;
                                        foreach (var item in ArrOfStr)
                                        {
                                            try
                                            {
                                                ChekErr = double.Parse(item, formatter);
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Геометрические и гидравлические элементы\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            J.Add(item);
                                        }
                                        JMacro.Add(J);
                                    }

                                    #endregion

                                    #region Запись параметров гидравлических макроучастков; Количество макроучастков теплообмена

                                    else if (CountOfLineInElem == ((2 + CntJMacro) + 1))
                                    {
                                        for (int j = 0; j < CntJMacro; j++)
                                        {
                                            if (JMacro[j].Count != 9)
                                            {
                                                Console.WriteLine("Неверное количество параметров в строке \"Геометрические и гидравлические элементы\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                        }

                                        #region Количество макроучастков теплообмена

                                        try
                                        {
                                            CntJMacrv = int.Parse(ArrayOfVolid[i]); // Запомним колличество участков
                                            PipeGeneral[3] = ArrayOfVolid[i];
                                            K = 2 * CntJMacrv;
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Количество макроучастков теплообмена\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }


                                        #endregion

                                    }

                                    #endregion

                                    #region Считываем Количество расчетных участков теплообмена по толщине и по длине

                                    else if ((CountOfLineInElem >= ((2 + CntJMacro) + 2)) && K != 0)
                                    {
                                        if (K % 2 == 0) // На четные числа К мы считываем JNM, на нечетные - параметры для JNM
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            List<string> J = new List<string>();
                                            double ChekErr = 0;
                                            foreach (var item in ArrOfStr)
                                            {
                                                try
                                                {
                                                    ChekErr = double.Parse(item, formatter);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                J.Add(item);
                                            }
                                            JNM.Add(J);
                                            try
                                            {
                                                JNMFlag = int.Parse(ArrOfStr[0]);
                                                if (JNMFlag == 0)
                                                {
                                                    K = K - 2;
                                                }
                                                else if (JNMFlag > 0)
                                                {
                                                    K = K - 1;
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Файл Volid.dat, ошибка, JNM < 0, название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Количество расчетных участков теплообмена по толщине\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                        }
                                        else // Если нечетное число К - считываем параметры для JNM
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            List<string> J = new List<string>();
                                            double ChekErr = 0;
                                            foreach (var item in ArrOfStr)
                                            {
                                                try
                                                {
                                                    ChekErr = double.Parse(item, formatter);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                J.Add(item);
                                            }
                                            JMacrv.Add(J);
                                            K--;

                                        }

                                    }

                                    #endregion

                                    else
                                    {

                                        #region Поправочные коэффициенты

                                        if (CountOfLineInElem == (((2 + CntJMacro) + 2) + JNM.Count + JMacrv.Count))
                                        {

                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                            double ChekErr;
                                            try
                                            {
                                                if (ArrOfStr.Length != 3)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Поправочные коэффициенты\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                for (int j = 0; j < 3; j++)
                                                {
                                                    ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    PipeGeneral[j + 4] = ArrOfStr[j];
                                                }


                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Поправочные коэффициенты\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }
                                        }

                                        #endregion

                                        #region Параматры для водяного/газового контура; Конец расчетного элемента "Труба"

                                        else if (CountOfLineInElem == (((2 + CntJMacro) + 2) + JNM.Count + JMacrv.Count + 1))
                                        {
                                            string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *

                                            if (ArrOfStr.Length == 5)
                                            {

                                                #region Если конутр водяной

                                                double ChekErr = 0;
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        PipeInit[j] = ArrOfStr[j];
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                    }

                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                #endregion

                                            }
                                            else if (ArrOfStr.Length == 4)
                                            {

                                                #region Если контур газовый

                                                double ChekErr = 0;
                                                try
                                                {
                                                    int j = 0;
                                                    foreach (var item in ArrOfStr)
                                                    {
                                                        PipeInit[j] = item;
                                                        ChekErr = double.Parse(item, formatter);
                                                        j++;
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                #endregion

                                            }
                                            else
                                            {
                                                Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного/газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                            }

                                            PipeGeneral[7] = "1"; // Признак учета негомогенности

                                            if (IsCopyElem == true)
                                            {
                                                i = CopyData[0];
                                                Count = i + 1;
                                                CountOfLine = 2;

                                                double ChekErr = 0;
                                                try
                                                {
                                                    ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                PipeGeneral[1] = ArrayOfVolid[i];

                                                string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *

                                                if (ArrOfStr2.Length == 5)
                                                {

                                                    #region Если конутр водяной

                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            PipeInit[j] = ArrOfStr2[j];
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                        }

                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }

                                                    #endregion

                                                }
                                                else if (ArrOfStr2.Length == 4)
                                                {

                                                    #region Если контур газовый

                                                    try
                                                    {
                                                        int j = 0;
                                                        foreach (var item in ArrOfStr2)
                                                        {
                                                            PipeInit[j] = item;
                                                            ChekErr = double.Parse(item, formatter);
                                                            j++;
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }

                                                    #endregion

                                                }
                                                else
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условие для водяного/газового конутра\", название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                i++;
                                            }

                                            #region Записываем в файл

                                            sw.WriteLine("   <GENERAL_PIPE Comment=\"Общие\">");
                                            sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", PipeGeneral[0]);
                                            sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", PipeGeneral[1]);
                                            sw.WriteLine("    <PIPE_JMACRO Value=\"{0}\" Comment=\"Количество гидравлических макроучастков\"/>", PipeGeneral[2]);
                                            sw.WriteLine("    <PIPE_JMACRV Value=\"{0}\" Comment=\"Количество макроучастков теплообмена\"/>", PipeGeneral[3]);
                                            sw.WriteLine("    <PIPE_ALFKSG Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете коэффициента теплоотдачи\"/>", PipeGeneral[4]);
                                            sw.WriteLine("    <PIPE_POPSG Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете коэффициента трения\"/>", PipeGeneral[5]);
                                            sw.WriteLine("    <PIPE_JALFA Value=\"{0}\" Comment=\"Методика расчета коэффициента теплоотдачи\"/>", PipeGeneral[6]);
                                            sw.WriteLine("    <PIPE_JNEGOM Value=\"{0}\" Comment=\"Признак учета негомогенности\"/>", PipeGeneral[7]);
                                            sw.WriteLine("   </GENERAL_PIPE>");
                                            sw.WriteLine("   <GEOM_PIPE Comment=\"Геометрические и гидравлические характеристики\">");
                                            for (int j = 0; j < CntJMacro; j++)
                                            {
                                                sw.WriteLine("    <PIPE_JMACRO_N Value=\"{0}\" Comment=\"Номер гидравлического макроучастка\">", j + 1);

                                                sw.WriteLine("     <PIPE_V2SG Value=\"{0}\" Comment=\"Объем, м^3\"/>", JMacro[j][0]);
                                                sw.WriteLine("     <PIPE_S2SG Value=\"{0}\" Comment=\"Площадь проходного сечения, м^2\"/>", JMacro[j][2]);
                                                sw.WriteLine("     <PIPE_DGSG Value=\"{0}\" Comment=\"Гидравлический диаметр, м\"/>", JMacro[j][3]);
                                                sw.WriteLine("     <PIPE_AL Value=\"{0}\" Comment=\"Длина, м\"/>", JMacro[j][1]);
                                                sw.WriteLine("     <PIPE_DZSG Value=\"{0}\" Comment=\"Разность высотных отметок, м\"/>", JMacro[j][7]);
                                                sw.WriteLine("     <PIPE_AKSSG Value=\"{0}\" Comment=\"Коэффициент местных сопротивлений\"/>", JMacro[j][4]);
                                                sw.WriteLine("     <PIPE_SHERSG Value=\"{0}\" Comment=\"Шероховатость, м\"/>", JMacro[j][5]);
                                                sw.WriteLine("     <PIPE_INMPG Value=\"{0}\" Comment=\"Коэффициент механической инерции теплоносителя, 10^(-6)∙кг/(м∙с)\"/>", JMacro[j][6]);
                                                sw.WriteLine("     <PIPE_JV Value=\"{0}\" Comment=\"Количество гидравлических расчетных участков\"/>", JMacro[j][8]);

                                                sw.WriteLine("    </PIPE_JMACRO_N>");
                                            }
                                            sw.WriteLine("   </GEOM_PIPE>");

                                            sw.WriteLine("   <STRMAT_PIPE Comment=\"Свойства кострукционных материалов\">");
                                            for (int j = 0; j < JNM.Count; j++)
                                            {
                                                sw.WriteLine("    <PIPE_JMACRV_N Value=\"{0}\" Comment=\"Номер макроучастка теплообмена\">", j + 1);

                                                sw.WriteLine("     <PIPE_JVV Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по длине\"/>", JNM[j][1]);
                                                sw.WriteLine("     <PIPE_JNM Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", JNM[j][0]);

                                                if (JNM[j][0] != "0")
                                                {
                                                    sw.WriteLine("     <PIPE_CM Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", JMacrv[j][0]);
                                                    sw.WriteLine("     <PIPE_RM Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", JMacrv[j][1]);
                                                    sw.WriteLine("     <PIPE_DL Value=\"{0}\" Comment=\"Толщина, м\"/>", JMacrv[j][3]);
                                                    sw.WriteLine("     <PIPE_ALMD Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", JMacrv[j][2]);
                                                    sw.WriteLine("     <PIPE_ALOCSG Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", JMacrv[j][4]);
                                                    sw.WriteLine("     <PIPE_FMSG Value=\"{0}\" Comment=\"Площадь поверхности теплообмена с теплоносителем, м^2\"/>", JMacrv[j][5]);
                                                }

                                                sw.WriteLine("    </PIPE_JMACRV_N>");
                                            }
                                            sw.WriteLine("   </STRMAT_PIPE>");

                                            if (ArrOfStr.Length == 5)
                                            {
                                                sw.WriteLine("   <INITDATA_PIPE Comment=\"Начальные условия\">");
                                                sw.WriteLine("     <PIPE_PVOL Value=\"{0}\" Comment=\"Давление на выходе, МПа\"/>", PipeInit[0]);
                                                sw.WriteLine("     <PIPE_DP Value=\"{0}\" Comment=\"Перепад давления от входа до выхода, МПа\"/>", PipeInit[3]);
                                                sw.WriteLine("     <PIPE_IPG Value=\"{0}\" Comment=\"Энтальпия теплоносителя на выходе, кДж/кг\"/>", PipeInit[1]);
                                                sw.WriteLine("     <PIPE_IPB Value=\"{0}\" Comment=\"Энтальпия теплоносителя на входе, кДж/кг\"/>", PipeInit[2]);
                                                sw.WriteLine("     <PIPE_CBOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", PipeInit[4]);
                                                sw.WriteLine("   </INITDATA_PIPE>");
                                            }
                                            else
                                            {
                                                sw.WriteLine("   <INITDATA_PIPE Comment=\"Начальные условия\">");
                                                sw.WriteLine("     <PIPE_PVOL Value=\"{0}\" Comment=\"Давление на выходе, МПа\"/>", PipeInit[0]);
                                                sw.WriteLine("     <PIPE_TBYX Value=\"{0}\" Comment=\"Температура газа на выходе, K\"/>", PipeInit[1]);
                                                sw.WriteLine("     <PIPE_TBX Value=\"{0}\" Comment=\"Температура газа на входе, K\"/>", PipeInit[2]);
                                                sw.WriteLine("     <PIPE_DP Value=\"{0}\" Comment=\"Перепад давления от входа до выхода, МПа\"/>", PipeInit[3]);
                                            }
                                            sw.WriteLine("  </ELEM_NAME>");

                                            #endregion

                                            EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                            #region Обнуляем переменные для следующих элементов "Труба"

                                            CntJMacro = 0;
                                            CntJMacrv = 0;
                                            JNMFlag = 0;
                                            JMacro.Clear();
                                            JMacrv.Clear();
                                            JNM.Clear();
                                            K = 0;
                                            Array.Clear(PipeGeneral, 0, 8);
                                            Array.Clear(PipeInit, 0, 5);

                                            IsCopyElem = false;
                                            Array.Clear(CopyData, 0, 2);
                                            NameOfElem.Clear();
                                            NameAndCountOfString[0] = null;

                                            #endregion

                                        }

                                        #endregion

                                    }

                                } // Элемент "Обратная(?) Труба"

                                if (TypeOfElem == 31) // Элемент "Объем с уровнем"
                                {
                                    CountOfLineInElem++; // Номер строки в элементе

                                    #region Множитель параллельных геометрий

                                    if (CountOfLineInElem == 1)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                            GENERAL_VOL[1] = ArrayOfVolid[i];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }


                                    #endregion

                                    #region Геометрические исходные данные

                                    if (CountOfLineInElem == 2)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 5)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Общие\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                            }
                                            GENERAL_VOL[0] = "31";
                                            GENERAL_VOL[2] = ArrOfStr[3];
                                            GENERAL_VOL[3] = ArrOfStr[4];
                                            GEOM_VOL[0] = ArrOfStr[1];
                                            GEOM_VOL[1] = ArrOfStr[2];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Общие\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Размерность таблицы

                                    if (CountOfLineInElem == 3)
                                    {
                                        double ChekErr = 0;
                                        try
                                        {
                                            ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                            GEOM_VOL[4] = ArrayOfVolid[i];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Размерность таблицы зависимости объема от высоты\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Значения таблицы (Высоты)

                                    if (CountOfLineInElem == 4)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != double.Parse(GEOM_VOL[4], formatter))
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Высота (таблица)\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                VOL_TAB_H_V.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Высота (таблица)\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Значения таблицы (Объемы)

                                    if (CountOfLineInElem == 5)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != double.Parse(GEOM_VOL[4], formatter))
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Объемы (таблица)\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                VOL_TAB_H_V.Add(ArrOfStr[j]);
                                            }
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Объемы (таблица)\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    #region Периметр теплообмена

                                    if (CountOfLineInElem == 6)
                                    {
                                        string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                        double ChekErr = 0;
                                        if (ArrOfStr.Length != 2)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Периметр теплообмена\" (Указано 3 числа, а должно быть 2, тип расчетного элемента - \"31\"), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                        try
                                        {
                                            for (int j = 0; j < ArrOfStr.Length; j++)
                                            {
                                                ChekErr = double.Parse(ArrOfStr[j], formatter);
                                            }
                                            GEOM_VOL[2] = ArrOfStr[0];
                                            GEOM_VOL[3] = ArrOfStr[1];
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Файл Volid.dat, ошибка в строке \"Периметр теплообмена\" (неверный формат, возможно ошибка из-за \"*\"), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                        }
                                    }

                                    #endregion

                                    try
                                    {
                                        if (double.Parse(GEOM_VOL[2], formatter) > 0 && double.Parse(GEOM_VOL[3], formatter) > 0)
                                        {

                                            #region Свойства кострукционных материалов над уровнем

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLG[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLG[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Свойства кострукционных материалов под уровнем

                                            if (CountOfLineInElem == 8)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLW[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLW[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Начальные условия

                                            if (CountOfLineInElem == 9)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = (int)double.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - (int)double.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <STRMAT_VOLG Comment=\"Свойства кострукционных материалов над уровнем\">");
                                                sw.WriteLine("    <VOL_JNMG Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLG[0]);
                                                sw.WriteLine("    <VOL_CMSG Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLG[1]);
                                                sw.WriteLine("    <VOL_RMSG Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLG[2]);
                                                sw.WriteLine("    <VOL_DLSG Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLG[3]);
                                                sw.WriteLine("    <VOL_LMBDG Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLG[4]);
                                                sw.WriteLine("    <VOL_KOCSGC Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLG[5]);
                                                sw.WriteLine("   </STRMAT_VOLG>");
                                                sw.WriteLine("   <STRMAT_VOLW Comment=\"Свойства кострукционных материалов под уровнем\">");
                                                sw.WriteLine("    <VOL_JNMW Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLW[0]);
                                                sw.WriteLine("    <VOL_CMWS Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLW[1]);
                                                sw.WriteLine("    <VOL_RMWS Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLW[2]);
                                                sw.WriteLine("    <VOL_DLWS Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLW[3]);
                                                sw.WriteLine("    <VOL_LMBDW Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLW[4]);
                                                sw.WriteLine("    <VOL_KOCVOL Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLW[5]);
                                                sw.WriteLine("   </STRMAT_VOLW>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion

                                        }
                                        else if (double.Parse(GEOM_VOL[2], formatter) == 0 && double.Parse(GEOM_VOL[3], formatter) > 0)
                                        {
                                            #region Свойства кострукционных материалов под уровнем

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLW[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLW[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов под уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Начальные условия

                                            if (CountOfLineInElem == 8)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = int.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - int.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <STRMAT_VOLW Comment=\"Свойства кострукционных материалов под уровнем\">");
                                                sw.WriteLine("    <VOL_JNMW Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLW[0]);
                                                sw.WriteLine("    <VOL_CMWS Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLW[1]);
                                                sw.WriteLine("    <VOL_RMWG Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLW[2]);
                                                sw.WriteLine("    <VOL_DLWG Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLW[3]);
                                                sw.WriteLine("    <VOL_LMBDW Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLW[4]);
                                                sw.WriteLine("    <VOL_KOCVOL Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLW[5]);
                                                sw.WriteLine("   </STRMAT_VOLW>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion
                                        }
                                        else if (double.Parse(GEOM_VOL[2], formatter) > 0 && double.Parse(GEOM_VOL[3], formatter) == 0)
                                        {
                                            #region Свойства кострукционных материалов над уровнем

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        if (j != 0)
                                                        {
                                                            STRMAT_VOLG[j] = ArrOfStr[j - 1];
                                                        }
                                                        STRMAT_VOLG[0] = ArrOfStr[5];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Свойства кострукционных материалов над уровнем\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                            }

                                            #endregion

                                            #region Начальные условия

                                            if (CountOfLineInElem == 8)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = int.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - int.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <STRMAT_VOLG Comment=\"Свойства кострукционных материалов над уровнем\">");
                                                sw.WriteLine("    <VOL_JNMG Value=\"{0}\" Comment=\"Количество расчетных участков теплообмена по толщине\"/>", STRMAT_VOLG[0]);
                                                sw.WriteLine("    <VOL_CMSG Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_VOLG[1]);
                                                sw.WriteLine("    <VOL_RMSG Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_VOLG[2]);
                                                sw.WriteLine("    <VOL_DLSG Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_VOLG[3]);
                                                sw.WriteLine("    <VOL_LMBDG Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_VOLG[4]);
                                                sw.WriteLine("    <VOL_KOCSGC Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_VOLG[5]);
                                                sw.WriteLine("   </STRMAT_VOLG>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Начальные условия

                                            if (CountOfLineInElem == 7)
                                            {
                                                string[] ArrOfStr = ArrayOfVolid[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref ArrayOfVolid, ref ArrOfStr, i, formatter); // Проверяем на *
                                                double ChekErr = 0;
                                                if (ArrOfStr.Length != 6)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }
                                                try
                                                {
                                                    for (int j = 0; j < ArrOfStr.Length; j++)
                                                    {
                                                        ChekErr = double.Parse(ArrOfStr[j], formatter);
                                                        VolInit[j] = ArrOfStr[j];
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                }

                                                if (IsCopyElem == true)
                                                {
                                                    i = CopyData[0];
                                                    Count = i + 1;
                                                    CountOfLine = 2;
                                                    try
                                                    {
                                                        ChekErr = double.Parse(ArrayOfVolid[i], formatter);
                                                        GENERAL_VOL[1] = ArrayOfVolid[i];
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Множитель параллельных геометрий\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    string[] ArrOfStr2 = ArrayOfVolid[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    ReadStar(ref ArrayOfVolid, ref ArrOfStr2, i + 1, formatter); // Проверяем на *
                                                    if (ArrOfStr2.Length != 6)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (Неверное колличество параметров), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    try
                                                    {
                                                        for (int j = 0; j < ArrOfStr2.Length; j++)
                                                        {
                                                            ChekErr = double.Parse(ArrOfStr2[j], formatter);
                                                            VolInit[j] = ArrOfStr2[j];
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("Файл Volid.dat, ошибка в строке \"Начальные условия\" (неверный формат), название расчетного элемента - {0}", ArrayOfVolid[i - CountOfLineInElem - 1]);
                                                    }
                                                    i++;
                                                }

                                                #region Запись в файл

                                                sw.WriteLine("   <GENERAL_VOL Comment=\"Общие\">");
                                                sw.WriteLine("    <ELEM_TYPE Value=\"{0}\" Comment=\"Тип расчетного элемента\"/>", GENERAL_VOL[0]);
                                                sw.WriteLine("    <ELEM_VOLMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL_VOL[1]);
                                                sw.WriteLine("    <VOL_KGVSP Value=\"{0}\" Comment=\"Поправочный коэффициент на  скорость всплытия пара\"/>", GENERAL_VOL[2]);
                                                sw.WriteLine("    <VOL_JTRCON Value=\"{0}\" Comment=\"Число трубок конденсатора\"/>", GENERAL_VOL[3]);
                                                sw.WriteLine("   </GENERAL_VOL>");
                                                sw.WriteLine("   <GEOM_VOL Comment=\"Геометрические характеристики\">");
                                                sw.WriteLine("    <VOL_DGSGCN Value=\"{0}\" Comment=\"Гидравлический диаметр над уровнем, м\"/>", GEOM_VOL[0]);
                                                sw.WriteLine("    <VOL_DGWSCN Value=\"{0}\" Comment=\"Гидравлический диаметр под уровнем, м\"/>", GEOM_VOL[1]);
                                                sw.WriteLine("    <VOL_PSGCON Value=\"{0}\" Comment=\"Периметр теплообмена над уровнем, м\"/>", GEOM_VOL[2]);
                                                sw.WriteLine("    <VOL_PWSCON Value=\"{0}\" Comment=\"Периметр теплообмена под уровнем, м\"/>", GEOM_VOL[3]);
                                                sw.WriteLine("    <VOL_JVTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости объема от высоты\"/>", GEOM_VOL[4]);
                                                for (int j = 0; j < double.Parse(GEOM_VOL[4], formatter); j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABH Value=\"{0}\" Numb=\"{1}\" Comment=\"Высота №{1}, м\"/>", VOL_TAB_H_V[j], j + 1);
                                                }
                                                for (int j = int.Parse(GEOM_VOL[4], formatter); j < VOL_TAB_H_V.Count; j++)
                                                {
                                                    sw.WriteLine("    <VOL_TABV Value=\"{0}\" Numb=\"{1}\" Comment=\"Объем №{1}, м^3\"/>", VOL_TAB_H_V[j], j - int.Parse(GEOM_VOL[4], formatter) + 1);
                                                }
                                                sw.WriteLine("   </GEOM_VOL>");
                                                sw.WriteLine("   <INITDATA_VOL Comment=\"Начальные условия\">");
                                                sw.WriteLine("    <VOL_PVOL Value=\"{0}\" Comment=\"Давление теплоносителя, МПа\"/>", VolInit[0]);
                                                sw.WriteLine("    <VOL_PSVOL Value=\"{0}\" Comment=\"Парциальное давление пара, МПа\"/>", VolInit[1]);
                                                sw.WriteLine("    <VOL_ISG Value=\"{0}\" Comment=\"Энтальпия пара над уровнем, кДж/кг\"/>", VolInit[2]);
                                                sw.WriteLine("    <VOL_IVOL Value=\"{0}\" Comment=\"Энтальпия воды под уровнем, кДж/кг\"/>", VolInit[3]);
                                                sw.WriteLine("    <VOL_HL Value=\"{0}\" Comment=\"Высотная отметка уровня раздела фаз, м\"/>", VolInit[4]);
                                                sw.WriteLine("    <VOL_CBVOL Value=\"{0}\" Comment=\"Концентрация бора, г/кг\"/>", VolInit[5]);
                                                sw.WriteLine("   </INITDATA_VOL>");
                                                sw.WriteLine("  </ELEM_NAME>");

                                                #endregion

                                                EndOfElem(ref CountOfLine, ref ArrayOfVolid,
                                                        ref CountOfLineInElem, ref TypeOfElem, ref Count, sw); // Конец элемента

                                                #region Обнуляем переменные для "Объем с уровнем"

                                                Array.Clear(GENERAL_VOL, 0, 4);
                                                Array.Clear(GEOM_VOL, 0, 5);
                                                VOL_TAB_H_V.Clear();
                                                Array.Clear(STRMAT_VOLG, 0, 6);
                                                Array.Clear(STRMAT_VOLW, 0, 6);
                                                Array.Clear(VolInit, 0, 6);

                                                IsCopyElem = false;
                                                Array.Clear(CopyData, 0, 2);
                                                NameOfElem.Clear();
                                                NameAndCountOfString[0] = null;

                                                #endregion

                                            }

                                            #endregion
                                        }
                                    }
                                    catch (ArgumentNullException)
                                    {


                                    }

                                } // Элемент "Объем с уровнем"

                            }
                            CountOfLine++;
                            Count++;
                        }
                    }
                }

                #region Подсчет конутров и определение типа конутра для файла VOLID.DAT

                List<string> ArrayOfContStr = new List<string>();
                List<List<string>> NumConts = new List<List<string>>();
                List<string> ValueAndCountOfCont = new List<string>();

                using (StreamReader sr = new StreamReader(WritePathVolid, Encoding.Default))
                {

                    #region Заполнили массив данными
                    int k = 0;
                    string LineOfNewVolid;
                    while ((LineOfNewVolid = await sr.ReadLineAsync()) != null)
                    {
                        k++;
                        ArrayOfContStr.Add(LineOfNewVolid);
                        if (LineOfNewVolid.IndexOf(" <CONT Value=\"") != -1)
                        {
                            string[] words = LineOfNewVolid.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);
                            //ValueAndCountOfCont.Add(words[1]);
                            //ValueAndCountOfCont.Add(k.ToString());
                            NumConts.Add(new List<string>() { words[1], (k - 1).ToString() });
                        }
                    }

                    #endregion

                    #region Поиск конутра, определение его типа, изменение массива

                    string JCNTR = "<JCNTR Value=\"" + CountContVolid + "\" Comment=\"Количество контуров\">";
                    ArrayOfContStr[0] = JCNTR;
                    int[] TypeFlagOfCont = new int[NumConts.Count];
                    int[] DiversNumStr = new int[2];

                    for (int i = 0; i < CountContVolid; i++)
                    {
                        if (NumConts.Count == 1)
                        {
                            for (int j = 0; j < ArrayOfContStr.Count; j++)
                            {
                                if (ArrayOfContStr[j].IndexOf("<INITDATA_PIPE") != -1)
                                {
                                    DiversNumStr[0] = j;
                                }
                                else if (ArrayOfContStr[j].IndexOf("</INITDATA_PIPE") != -1)
                                {
                                    DiversNumStr[1] = j;
                                    break;
                                }
                            }
                            TypeFlagOfCont[0] = DiversNumStr[1] - DiversNumStr[0] - 1;
                        }
                        else
                        {
                            if (i != CountContVolid - 1)
                            {
                                for (int j = int.Parse(NumConts[i][1]); j < int.Parse(NumConts[i + 1][1]); j++)
                                {
                                    if (ArrayOfContStr[j].IndexOf("<INITDATA_PIPE") != -1)
                                    {
                                        DiversNumStr[0] = j;
                                    }
                                    else if (ArrayOfContStr[j].IndexOf("</INITDATA_PIPE") != -1)
                                    {
                                        DiversNumStr[1] = j;
                                        break;
                                    }
                                }
                                TypeFlagOfCont[i] = DiversNumStr[1] - DiversNumStr[0] - 1;
                            }
                            else
                            {
                                for (int j = int.Parse(NumConts[i][1]); j < ArrayOfContStr.Count; j++)
                                {
                                    if (ArrayOfContStr[j].IndexOf("<INITDATA_PIPE") != -1)
                                    {
                                        DiversNumStr[0] = j;
                                    }
                                    else if (ArrayOfContStr[j].IndexOf("</INITDATA_PIPE") != -1)
                                    {
                                        DiversNumStr[1] = j;
                                        break;
                                    }
                                }
                                TypeFlagOfCont[i] = DiversNumStr[1] - DiversNumStr[0] - 1;
                            }
                        }
                    }
                    for (int i = 0; i < NumConts.Count; i++)
                    {
                        if (TypeFlagOfCont[i] == 5)
                        {
                            NumConts[i].Add("Пароводяной контур");
                        }
                        else if (TypeFlagOfCont[i] == 4)
                        {
                            NumConts[i].Add("Газовый контур");
                        }
                        else
                        {
                            NumConts[i].Add("Не получилось определить");
                        }

                        ArrayOfContStr[int.Parse(NumConts[i][1])] = " <CONT Value=\"" + NumConts[i][0] + "\" Numb=\"" + (i + 1) + "\" Comment=\"" + NumConts[i][2] + "\">";

                    }


                    #endregion

                }
                using (StreamWriter sw = new StreamWriter(WritePathVolid, false, Encoding.Default))
                {

                    #region Запись массива в файл

                    int k = 0;
                    foreach (var item in ArrayOfContStr)
                    {
                        k++;
                        // Осталось изменить строки контура
                        if (k == ArrayOfContStr.Count)
                        {
                            sw.Write(item);
                        }
                        else
                        {
                            sw.WriteLine(item);
                        }
                    }

                    #endregion

                }
                ArrayOfContStr.Clear();

                #endregion

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Volid.dat не был найден");
            }

            #endregion

            #region GIDR2K.DAT

            try
            {
                List<string> InitCond = new List<string>();
                using (StreamReader sr = new StreamReader(ReadPathGidr2k, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathGidr2k, false, Encoding.Default))
                    {

                        int IterStr = 0;
                        int MaxTypePump = 0;

                        List<string> Name = new List<string>();

                        List<string> GENERAL = new List<string>();
                        List<string> GEOM = new List<string>();
                        List<string> VLV = new List<string>();
                        List<string> VLV_TBL = new List<string>();
                        List<string> PUMP = new List<string>();
                        List<string> INITDATA = new List<string>();
                        List<string> JUN_WTG2K = new List<string>();
                        List<string> GVERSUST = new List<string>();
                        List<string> JUN_KC2KT = new List<string>();
                        List<string> VKDI = new List<string>();




                        #region Создаем 2 массива

                        List<string> Gidr2kNames = new List<string>(); // Массив строк с именами соединений
                        List<string> Gidr2kProp = new List<string>(); // Массив строк со свойствами соединений
                        int TotalCount = 0;

                        int CountСonnection = 0;

                        string LineOfVolid;
                        while ((LineOfVolid = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfVolid.StartsWith("C") && !LineOfVolid.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfVolid) && !LineOfVolid.StartsWith("!"))
                            {
                                if (CountСonnection == 0)
                                {
                                    TotalCount = int.Parse(LineOfVolid.Trim());
                                    CountСonnection++;
                                    continue;
                                }
                                if (CountСonnection <= TotalCount)
                                {
                                    Gidr2kNames.Add(LineOfVolid.Trim());
                                }
                                else
                                {
                                    Gidr2kProp.Add(LineOfVolid.Trim());
                                }
                                CountСonnection++;
                            }
                        }

                        #endregion

                        for (int i = 0; i < TotalCount; i++) // Итерации по колчиеству соединений
                        {

                            #region Считываем название соединения

                            Name.Add(Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            IterStr++;

                            #endregion

                            #region Множитель параллельных геометрий 

                            GENERAL.Add(Gidr2kProp[IterStr].Trim());
                            IterStr++;

                            #endregion

                            #region Находим откуда и куда оно приходит из превого массива

                            for (int j = 0; j < Gidr2kNames.Count; j++)
                            {
                                if (Gidr2kNames[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].IndexOf(Name[0]) != -1 && Gidr2kNames[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Length == Name[0].Length)
                                {
                                    GENERAL.Add(Gidr2kNames[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                    GENERAL.Add(Gidr2kNames[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2]);
                                }
                            }

                            #endregion

                            #region Обрабатываем первую строку, которая одинакова для всех соединений

                            string[] ArrOfStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref Gidr2kProp, ref ArrOfStr, IterStr, formatter); // Проверяем на *
                            PUMP.Add(ArrOfStr[0]); // Насос
                            MaxTypePump = Math.Max(MaxTypePump, int.Parse(ArrOfStr[0]));
                            VLV.Add(ArrOfStr[1]); // Клапан
                            GENERAL.Add(ArrOfStr[2]); // Критического истечения
                            GENERAL.Add(ArrOfStr[3]); // Обратный клапан
                            GENERAL.Add(ArrOfStr[4]); // Тип соединения
                            GEOM.Add(ArrOfStr[5]); // Объем
                            GEOM.Add(ArrOfStr[6]); // Высотная отметка вх. элемента
                            GEOM.Add(ArrOfStr[7]); // Высотная отметка вых. элемента
                            IterStr++;
                            #endregion

                            #region Обрабатываем все возможные типы соединений

                            if (int.Parse(GENERAL[5]) > 0)
                            {
                                string[] GeomStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref Gidr2kProp, ref GeomStr, IterStr, formatter); // Проверяем на *
                                GEOM.Add(GeomStr[0]); // Площадь
                                GEOM.Add(GeomStr[1]); // Местные сопротивления
                                GEOM.Add(GeomStr[2]); // Гидр. диаметр
                                GEOM.Add(GeomStr[3]); // Длина
                                GEOM.Add(GeomStr[4]); // Шереховатость
                                GEOM.Add(GeomStr[5]); // Механ. инерция
                                GEOM.Add(GeomStr[6]); // Разность высотных отметок
                                if (int.Parse(GENERAL[4]) > 0)
                                {
                                    GENERAL.Add(GeomStr[7]); // Обратный клапан
                                }
                                IterStr++;
                                if (int.Parse(GENERAL[5]) == 3)
                                {
                                    string[] VKDIStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref Gidr2kProp, ref VKDIStr, IterStr, formatter); // Проверяем на *
                                    for (int j = 0; j < 4; j++)
                                    {
                                        VKDI.Add(VKDIStr[j]);
                                    }
                                    IterStr++;
                                }
                                if (ArrOfStr[1] != "NO")
                                {
                                    string[] VLVStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref Gidr2kProp, ref VLVStr, IterStr, formatter); // Проверяем на *
                                    for (int j = 0; j < 5; j++)
                                    {
                                        VLV.Add(VLVStr[j]);
                                    }
                                    if (double.Parse(VLV[4], formatter) < 0)
                                    {
                                        for (int j = 5; j < 7; j++)
                                        {
                                            VLV.Add(VLVStr[j]);
                                        }
                                    }
                                    IterStr++;
                                    VLV_TBL.Add(Gidr2kProp[IterStr].Trim());
                                    IterStr++;
                                    try
                                    {
                                        if (int.Parse(VLV_TBL[0]) != 0)
                                        {
                                            string[] VLV_TBLStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref Gidr2kProp, ref VLV_TBLStr, IterStr, formatter); // Проверяем на *
                                            if (VLV_TBLStr.Length == int.Parse(VLV_TBL[0]))
                                            {
                                                for (int j = 0; j < int.Parse(VLV_TBL[0]); j++)
                                                {
                                                    VLV_TBL.Add(VLV_TBLStr[j]);
                                                }
                                                IterStr++;
                                                string[] VLV_TBLStr2 = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                ReadStar(ref Gidr2kProp, ref VLV_TBLStr2, IterStr, formatter); // Проверяем на *
                                                for (int j = 0; j < int.Parse(VLV_TBL[0]); j++)
                                                {
                                                    VLV_TBL.Add(VLV_TBLStr2[j]);
                                                }
                                            }
                                            else
                                            {
                                                for (int j = 0; j < 2 * int.Parse(VLV_TBL[0]); j++)
                                                {
                                                    VLV_TBL.Add(VLV_TBLStr[j]);
                                                }
                                            }
                                            IterStr++;
                                        }
                                    }
                                    catch (FormatException)
                                    {

                                        Console.WriteLine("Нет табличного значения для элемента " + Name[0]);
                                        throw;
                                    }
                                }
                                if (ArrOfStr[0] != "0")
                                {
                                    string[] PUMPStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref Gidr2kProp, ref PUMPStr, IterStr, formatter); // Проверяем на *
                                    PUMP.Add(PUMPStr[0]);
                                    PUMP.Add(PUMPStr[1]);
                                    PUMP.Add(PUMPStr[2]);
                                    PUMP.Add(PUMPStr[3]);
                                    IterStr++;
                                    JUN_WTG2K.Add(Gidr2kProp[IterStr].Trim()); // размерность
                                    IterStr++;
                                    if (int.Parse(JUN_WTG2K[0]) > 0)
                                    {
                                        string[] JUN_WTG2KStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref Gidr2kProp, ref JUN_WTG2KStr, IterStr, formatter); // Проверяем на *
                                        if (JUN_WTG2KStr.Length == int.Parse(JUN_WTG2K[0]))
                                        {
                                            for (int j = 0; j < int.Parse(JUN_WTG2K[0]); j++)
                                            {
                                                JUN_WTG2K.Add(JUN_WTG2KStr[j]);
                                            }
                                            IterStr++;
                                            string[] JUN_WTG2KStr2 = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref Gidr2kProp, ref JUN_WTG2KStr2, IterStr, formatter); // Проверяем на *
                                            for (int j = 0; j < int.Parse(JUN_WTG2K[0]); j++)
                                            {
                                                JUN_WTG2K.Add(JUN_WTG2KStr2[j]);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 2 * int.Parse(JUN_WTG2K[0]); j++)
                                            {
                                                JUN_WTG2K.Add(JUN_WTG2KStr[j]);
                                            }
                                        }
                                        IterStr++;
                                    }
                                }
                            }
                            else
                            {
                                string[] GVERSUSTStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref Gidr2kProp, ref GVERSUSTStr, IterStr, formatter); // Проверяем на *
                                for (int j = 0; j < 3; j++)
                                {
                                    GVERSUST.Add(GVERSUSTStr[j]);
                                }
                                IterStr++;
                                for (int j = 0; j < 2 * int.Parse(GVERSUST[0]); j++)
                                {
                                    string[] JUN_KC2KTStr = Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref Gidr2kProp, ref JUN_KC2KTStr, IterStr, formatter); // Проверяем на *
                                    for (int k = 0; k < JUN_KC2KTStr.Length; k++)
                                    {
                                        JUN_KC2KT.Add(JUN_KC2KTStr[k]);
                                    }
                                    IterStr++;
                                    if (JUN_KC2KT.Count == 2 * int.Parse(GVERSUST[0]))
                                    {
                                        break;
                                    }
                                }
                                
                            }

                            #endregion

                            #region Записываем в файл

                            if (i == 0)
                            {
                                sw.WriteLine("<JUN_CNT Value=\"{0}\" Comment=\"Количество гидравлических соединений\">", TotalCount);
                            }
                            sw.WriteLine(" <JUN_NAME Value=\"{0}\">", Name[0]);
                            if (GENERAL[5] == "0")
                            {
                                sw.WriteLine("  <JUN_PROP Numb=\"{0}\" Comment=\"Параметрозависимое соединение\"/>", i + 1);
                            }
                            else if (GENERAL[5] == "1")
                            {
                                sw.WriteLine("  <JUN_PROP Numb=\"{0}\" Comment=\"Стандартное соединение\"/>", i + 1);
                            }
                            else if (GENERAL[5] == "2")
                            {
                                sw.WriteLine("  <JUN_PROP Numb=\"{0}\" Comment=\"Проточная часть турбины\"/>", i + 1);
                            }
                            else if (GENERAL[5] == "3")
                            {
                                sw.WriteLine("  <JUN_PROP Numb=\"{0}\" Comment=\"Газовое соединение\"/>", i + 1);
                            }

                            sw.WriteLine("  <JUN_FROM Value=\"{0}\" Comment=\"Элемент, к которому подключен вход соединения\"/>", GENERAL[1]);
                            sw.WriteLine("  <JUN_TO Value=\"{0}\" Comment=\"Элемент, к которому подключен выход соединения\"/>", GENERAL[2]);
                            sw.WriteLine("  <GENERAL_STDJUN Comment=\"Общие\">");
                            sw.WriteLine("   <JUN_JJNTIP Value=\"{0}\" Comment=\"Тип гидравлического соединения\"/>", GENERAL[5]);
                            if (GENERAL[5] == "1")
                            {
                                sw.WriteLine("   <JUN_AJNMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL[0]);
                                sw.WriteLine("   <JUN_JCRFLJ Value=\"{0}\" Comment=\"Критическое истечение\"/>", GENERAL[3]);
                                sw.WriteLine("   <JUN_JOBR Value=\"{0}\" Comment=\"Наличие обратного клапана\"/>", GENERAL[4]);
                                if (int.Parse(GENERAL[4]) > 0)
                                {
                                    sw.WriteLine("   <JUN_DP02K Value=\"{0}\" Comment=\"Перепад давления на закрытие обратного клапана\"/>", GENERAL[5]);
                                }
                            }
                            else if (GENERAL[5] == "2")
                            {
                                sw.WriteLine("   <JUN_AJNMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL[0]);
                                sw.WriteLine("   <JUN_JCRFLJ Value=\"{0}\" Comment=\"Критическое истечение\"/>", GENERAL[3]);
                            }
                            else if (GENERAL[5] == "3")
                            {
                                sw.WriteLine("   <JUN_AJNMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GENERAL[0]);
                            }
                            sw.WriteLine("  </GENERAL_STDJUN>");
                            if (GENERAL[5] != "0")
                            {
                                sw.WriteLine("  <GEOM_STDJUN Comment=\"Геометрические и гидравлические характеристики>\">");
                                sw.WriteLine("   <JUN_VJ Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM[0]);
                                sw.WriteLine("   <JUN_SG Value=\"{0}\" Comment=\"Площадь проходного сечения, м ^ 2\"/>", GEOM[3]);
                                sw.WriteLine("   <JUN_DGG2K Value=\"{0}\" Comment=\"Гидравлический диаметр, м\"/>", GEOM[5]);
                                sw.WriteLine("   <JUN_LG Value=\"{0}\" Comment=\"Длина соединения, м\"/>", GEOM[6]);
                                sw.WriteLine("   <JUN_DZG2K Value=\"{0}\" Comment=\"Разность высотных отметок, м\"/>", GEOM[9]);
                                sw.WriteLine("   <JUN_HJ1 Value=\"{0}\" Comment=\"Высотная отметка точки подключения входного элемента, м\"/>", GEOM[1]);
                                sw.WriteLine("   <JUN_HJ2 Value=\"{0}\" Comment=\"Высотная отметка точки подключения выходного элемента, м\"/>", GEOM[2]);
                                if (GENERAL[5] == "3")
                                {
                                    sw.WriteLine("   <JUN_V0KDI1 Value=\"{0}\" Comment=\"Объем выходного элемента до нижней точки подключения соединения, м ^ 3\"/>", VKDI[0]);
                                    sw.WriteLine("   <JUN_V1KDI1 Value=\"{0}\" Comment=\"Объем выходного элемента до верхней точки подключения соединения, м ^ 3\"/>", VKDI[1]);
                                    sw.WriteLine("   <JUN_V0KDI2 Value=\"{0}\" Comment=\"Объем входного элемента до нижней точки подключения соединения, м ^ 3\"/>", VKDI[2]);
                                    sw.WriteLine("   <JUN_V1KDI2 Value=\"{0}\" Comment=\"Объем входного элемента до верхней точки подключения соединения, м ^ 3\"/>", VKDI[3]);
                                }
                                sw.WriteLine("   <JUN_KSIG2K Value=\"{0}\" Comment=\"Коэффициент местных сопротивлений\"/>", GEOM[4]);
                                sw.WriteLine("   <JUN_SHRG2K Value=\"{0}\" Comment=\"Шероховатость, м\"/>", GEOM[7]);
                                sw.WriteLine("   <JUN_INMG2K Value=\"{0}\" Comment=\"Коэффициент механической инерции, 10 ^ (-6) * кг / (м * с)\"/>", GEOM[8]);
                                sw.WriteLine("  </GEOM_STDJUN>");
                            }
                            else
                            {
                                sw.WriteLine("   <JUN_AJNMLT Value=\"{0}\" Comment=\"Множитель параллельных геометрий\"/>", GEOM[0]);
                                sw.WriteLine("  <GEOM_DEPJUN Comment=\"Геометрические и гидравлические характеристики\">");
                                sw.WriteLine("   <JUN_HJ1 Value=\"{0}\" Comment=\"Высотная отметка точки подключения входного элемента, м\"/>", GEOM[1]);
                                sw.WriteLine("   <JUN_HJ2 Value=\"{0}\" Comment=\"Высотная отметка точки подключения выходного элемента, м\"/>", GEOM[2]);
                                sw.WriteLine("  </GEOM_DEPJUN>");
                            }
                            if (GENERAL[5] == "0")
                            {
                                sw.WriteLine("  <GVERSUST_DEPJUN Comment=\"Зависимость расхода от внешнего аргумента\">");
                                sw.WriteLine("   <JUN_JJNSIG Value=\"{0}\" Comment=\"Номер сигнала 'жесткого' управления, по которому происходит включение таблицы\"/>", GVERSUST[1]);
                                sw.WriteLine("   <JUN_JJNPAR Value=\"{0}\" Comment=\"Имя датчика - аргумент таблиц\"/>", GVERSUST[2]);
                                sw.WriteLine("   <JUN_JJNT Value=\"{0}\" Comment=\"Размерность таблицы\"/>", GVERSUST[0]);
                                for (int j = 0; j < int.Parse(GVERSUST[0]); j++)
                                {
                                    sw.WriteLine("   <JUN_KC2KT_ARG Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", JUN_KC2KT[j], j + 1);
                                }
                                for (int j = int.Parse(GVERSUST[0]); j < 2 * int.Parse(GVERSUST[0]); j++)
                                {
                                    sw.WriteLine("   <JUN_KC2KT Value=\"{0}\" Comment=\"Значение №{1}, отн.ед.\"/>", JUN_KC2KT[j], j - int.Parse(GVERSUST[0]) + 1);
                                }
                                sw.WriteLine("  </GVERSUST_DEPJUN>");
                            }
                            if (GENERAL[5] != "2" && GENERAL[5] != "0")
                            {
                                sw.WriteLine("  <VLV_STDJUN Comment=\"Исходные данные для клапана\">");
                                if (VLV[0] == "NO")
                                {
                                    sw.WriteLine("   <JUN_VLVNAM Value=\"{0}\" Comment=\"Имя клапана\"/>", VLV[0]);
                                }
                                else
                                {
                                    sw.WriteLine("   <JUN_VLVNAM Value=\"{0}\" Comment=\"Имя клапана\"/>", VLV[0]);
                                    sw.WriteLine("   <JUN_S0VLV Value=\"{0}\" Comment=\"Проходное сечение полностью открытого клапана, м ^ 2\"/>", VLV[2]);
                                    sw.WriteLine("   <JUN_KSIVLV Value=\"{0}\" Comment=\"Постоянная составляющая местных сопротивлений\"/>", VLV[3]);
                                    sw.WriteLine("   <JUN_DGVLV Value=\"{0}\" Comment=\"Гидравлический диаметр клапана, м\"/>", VLV[5]);
                                    sw.WriteLine("   <JUN_LVLV Value=\"{0}\" Comment=\"Длина проточной части клапана, м\"/>", VLV[1]);
                                    sw.WriteLine("   <JUN_CVLV Value=\"{0}\" Comment=\"Постоянная величина в зависимости местных сопротивлений\"/>", VLV[4]);
                                    if (double.Parse(VLV[4], formatter) < 0)
                                    {
                                        sw.WriteLine("   <JUN_VLVA1 Value=\"{0}\" Comment=\"Коэффициент при линейном члене зависимости местного сопротивления от площади\"/>", VLV[6]);
                                        sw.WriteLine("   <JUN_VLVA2 Value=\"{0}\" Comment=\"Коэффициент при квадратичном члене зависимости местного сопротивления от площади\"/>", VLV[7]);
                                    }
                                    sw.WriteLine("   <JUN_JVTBL Value=\"{0}\" Comment=\"Размерность таблицы зависимости степени открытия клапана от времени\"/>", VLV_TBL[0]);
                                    if (int.Parse(VLV_TBL[0]) != 0)
                                    {
                                        for (int j = 1; j <= int.Parse(VLV_TBL[0]); j++)
                                        {
                                            sw.WriteLine("   <JUN_VLVTBL_ARG Value=\"{0}\" Comment=\"Момент времени №{1}, с\"/>", VLV_TBL[j], j);
                                        }
                                        for (int j = int.Parse(VLV_TBL[0]) + 1; j <= 2 * int.Parse(VLV_TBL[0]); j++)
                                        {
                                            sw.WriteLine("   <JUN_VLVTBL_S Value=\"{0}\" Comment=\"Степень открытия на момент времени №{1}, отн.ед.\"/>", VLV_TBL[j], j - int.Parse(VLV_TBL[0]));
                                        }
                                    }
                                }
                                sw.WriteLine("  </VLV_STDJUN>");
                                if (GENERAL[5] != "3")
                                {
                                    sw.WriteLine("  <PUMP_STDJUN Comment=\"Исходные данные для насоса\">");
                                    sw.WriteLine("   <JUN_JPUG2K Value=\"{0}\" Comment=\"Номер гомологической характеристики насоса\"/>", PUMP[0]);
                                    if (PUMP[0] != "0")
                                    {
                                        sw.WriteLine("   <JUN_HP0G2K Value=\"{0}\" Comment=\"Номинальный напор, м\"/>", PUMP[1]);
                                        sw.WriteLine("   <JUN_MP0G2K Value=\"{0}\" Comment=\"Номинальный момент сопротивления, H * м\"/>", PUMP[2]);
                                        sw.WriteLine("   <JUN_QP0G2K Value=\"{0}\" Comment=\"Номинальный объемный расход через насос, м ^ 3 / c\"/>", PUMP[3]);
                                        sw.WriteLine("   <JUN_OMP02K Value=\"{0}\" Comment=\"Номинальная скорость вращения насоса, 1 / c\"/>", PUMP[4]);
                                        sw.WriteLine("   <JUN_JWTG2K Value=\"{0}\" Comment=\"Размерность таблицы зависимости оборотов насоса от времени\"/>", JUN_WTG2K[0]);
                                        if (int.Parse(JUN_WTG2K[0]) > 0)
                                        {
                                            for (int j = 1; j <= int.Parse(JUN_WTG2K[0]); j++)
                                            {
                                                sw.WriteLine("   <JUN_WTG2K_ARG Value=\"{0}\" Comment=\"Момент времени №{1}, с\"/>", JUN_WTG2K[j], j);
                                            }
                                            for (int j = int.Parse(JUN_WTG2K[0]) + 1; j <= 2 * int.Parse(JUN_WTG2K[0]); j++)
                                            {
                                                sw.WriteLine("   <JUN_WTG2K_FRQ Value=\"{0}\" Comment=\"Частота вращения на момент времени №{1}, отн.ед.\"/>", JUN_WTG2K[j], j - int.Parse(JUN_WTG2K[0]));
                                            }
                                        }
                                    }
                                    sw.WriteLine("  </PUMP_STDJUN>");
                                }
                            }
                            sw.WriteLine("  <INITDATA_STDJUN Comment=\"Начальные условия\">");
                            sw.WriteLine("   <JUN_KCI2KJ Value=\"???\" Comment=\"Расход теплоносителя, отн.ед.\"/>");
                            sw.WriteLine("  </INITDATA_STDJUN>");
                            sw.WriteLine(" </JUN_NAME>");

                            #endregion

                            #region Обнуление

                            Name.Clear();
                            GENERAL.Clear();
                            GEOM.Clear();
                            VLV.Clear();
                            VLV_TBL.Clear();
                            PUMP.Clear();
                            INITDATA.Clear();
                            JUN_WTG2K.Clear();
                            GVERSUST.Clear();
                            JUN_KC2KT.Clear();
                            VKDI.Clear();

                            #endregion

                        }

                        #region Записали таблицы для насосов

                        if (MaxTypePump != 0)
                        {
                            List<string> PumpTbl0 = new List<string>();
                            List<string> PumpTbl1 = new List<string>();
                            List<string> PumpTbl2 = new List<string>();
                            List<string> PumpTbl3 = new List<string>();
                            List<string> PumpTbl4 = new List<string>();
                            List<string> PumpTbl5 = new List<string>();

                            for (int i = 0; i < MaxTypePump; i++)
                            {
                                if (i == 0)
                                {
                                    sw.WriteLine(" <JUN_HC_GENERAL Comment=\"Гомологические характеристики насосов\">");
                                    sw.WriteLine("  <JUN_HC Value=\"{0}\" Comment=\"Количество типов насосов\"/>", MaxTypePump);
                                }

                                PumpTbl0.Add(Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                PumpTbl0.Add(Gidr2kProp[IterStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                IterStr++;
                                for (int m = 0; m < 2; m++)
                                {
                                    for (int j = 0; j < 2; j++)//Записали 1 таблицу
                                    {
                                        for (int k = 0; k < int.Parse(PumpTbl0[0]); k++)
                                        {
                                            string[] HOMOL = Gidr2kProp[IterStr].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref Gidr2kProp, ref HOMOL, IterStr, formatter); // Проверяем на *
                                            PumpTbl1.Add(HOMOL[k]);
                                        }
                                        IterStr++;
                                    }
                                    for (int j = 0; j < 2; j++)//Записали 2 таблицу
                                    {
                                        for (int k = 0; k < int.Parse(PumpTbl0[0]); k++)
                                        {
                                            string[] HOMOL = Gidr2kProp[IterStr].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref Gidr2kProp, ref HOMOL, IterStr, formatter); // Проверяем на *
                                            PumpTbl2.Add(HOMOL[k]);
                                        }
                                        IterStr++;
                                    }
                                    for (int j = 0; j < 2; j++)//Записали 3 таблицу
                                    {
                                        for (int k = 0; k < int.Parse(PumpTbl0[0]); k++)
                                        {
                                            string[] HOMOL = Gidr2kProp[IterStr].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref Gidr2kProp, ref HOMOL, IterStr, formatter); // Проверяем на *
                                            PumpTbl3.Add(HOMOL[k]);
                                        }
                                        IterStr++;
                                    }
                                    for (int j = 0; j < 2; j++)//Записали 4 таблицу
                                    {
                                        for (int k = 0; k < int.Parse(PumpTbl0[0]); k++)
                                        {
                                            string[] HOMOL = Gidr2kProp[IterStr].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref Gidr2kProp, ref HOMOL, IterStr, formatter); // Проверяем на *
                                            PumpTbl4.Add(HOMOL[k]);
                                        }
                                        IterStr++;
                                    }
                                    if (m == 0)
                                    {
                                        sw.WriteLine("  <JUN_HC_N Value=\"{0}\" Discription=\"Тип насосов {0}\">", i + 1);
                                        sw.WriteLine("   <JUN_JHG2K Value=\"{0}\" Comment=\"Размерность гомологической напорной характеристики насоса\">", PumpTbl0[m]);
                                        sw.WriteLine("    <JUN_SQHCC1 Comment=\"Таблица для задания гомологической напорной характеристики насоса в области(w + q.GE.0.).and.(w - q.GE.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HDNG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl1[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HDNG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl1[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQHCC1>");
                                        sw.WriteLine("    <JUN_SQHCC2 Comment=\"Таблица для задания гомологической напорной характеристики насоса в области(w + q.LT.0.).AND.(w - q.GE.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HDTG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl2[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HDTG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl2[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQHCC2>");
                                        sw.WriteLine("    <JUN_SQHCC3 Comment=\"Таблица для задания гомологической напорной характеристики насоса в области(w + q.LT.0.).AND.(w - q.LT.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HRTG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl3[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HRTG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl3[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQHCC3>");
                                        sw.WriteLine("    <JUN_SQHCC4 Comment=\"Таблица для задания гомологической напорной характеристики насоса в области(w + q.GE.0.).AND.(w - q.LT.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HRNG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl4[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_HRNG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl4[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQHCC4>");
                                        sw.WriteLine("   </JUN_JHG2K>");
                                        PumpTbl1.Clear();
                                        PumpTbl2.Clear();
                                        PumpTbl3.Clear();
                                        PumpTbl4.Clear();
                                        PumpTbl5.Clear();
                                    }

                                    if (m == 1)
                                    {
                                        sw.WriteLine("   <JUN_JMG2K Value=\"{0}\" Comment=\"Размерность гомологической моментной характеристики насоса\">", PumpTbl0[m]);
                                        sw.WriteLine("    <JUN_SQMCC1 Comment=\"Таблица для задания гомологической моментной характеристики насоса в области(w + q.GE.0.).and.(w - q.GE.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MDNG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl1[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MDNG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl1[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQMCC1>");
                                        sw.WriteLine("    <JUN_SQMCC2 Comment=\"Таблица для задания гомологической моментной характеристики насоса в области(w + q.LT.0.).AND.(w - q.GE.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MDTG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl2[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MDTG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl2[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQMCC2>");
                                        sw.WriteLine("    <JUN_SQMCC3 Comment=\"Таблица для задания гомологической моментной характеристики насоса в области(w + q.LT.0.).AND.(w - q.LT.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MRTG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl3[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MRTG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl3[j], j + 1 - int.Parse(PumpTbl0[m]));
                                        }
                                        sw.WriteLine("    </JUN_SQMCC3>");
                                        sw.WriteLine("    <JUN_SQMCC4 Comment=\"Таблица для задания гомологической моментной характеристики насоса в области(w + q.GE.0.).AND.(w - q.LT.0.)\">");
                                        for (int j = 0; j < int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MRNG2K Value=\"{0}\" Comment=\"Аргумент №{1}\"/>", PumpTbl4[j], (j + 1));
                                        }
                                        for (int j = int.Parse(PumpTbl0[m]); j < 2 * int.Parse(PumpTbl0[m]); j++)
                                        {
                                            sw.WriteLine("     <JUN_MRNG2K Value=\"{0}\" Comment=\"Значение №{1}\"/>", PumpTbl4[j], (j + 1 - int.Parse(PumpTbl0[m])));
                                        }
                                        sw.WriteLine("    </JUN_SQMCC4>");
                                        sw.WriteLine("   </JUN_JMG2K>");
                                        sw.WriteLine("  </JUN_HC_N>");

                                        PumpTbl1.Clear();
                                        PumpTbl2.Clear();
                                        PumpTbl3.Clear();
                                        PumpTbl4.Clear();
                                        PumpTbl5.Clear();
                                    }

                                }
                                PumpTbl0.Clear();
                                if (i == MaxTypePump - 1)
                                {
                                    sw.WriteLine(" </JUN_HC_GENERAL>");
                                    PumpTbl5.Add(Gidr2kProp[IterStr].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                    PumpTbl5.Add(Gidr2kProp[IterStr].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                    sw.WriteLine(" <JUN_AMUG2K Value=\"{0}\" Comment=\"Коэффициент критического истечения\"/>", PumpTbl5[0]);
                                    sw.WriteLine(" <JUN_EPUG2K Value=\"{0}\" Comment=\"Коэффициент для численного определения частной производной напора насоса по относительному массовому расходу\"/>", PumpTbl5[1]);
                                    sw.Write("</JUN_CNT>");
                                }
                            }
                        }

                        #endregion

                        #region Считали все расходы

                        IterStr++;
                        for (int i = IterStr; i < Gidr2kProp.Count; i++)
                        {
                            string[] ArrOfStr = Gidr2kProp[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref Gidr2kProp, ref ArrOfStr, i, formatter); // Проверяем на *
                            for (int j = 0; j < ArrOfStr.Length; j++)
                            {
                                InitCond.Add(ArrOfStr[j]);
                            }
                        }

                        #endregion

                    }
                }

                #region Запись расходов

                List<string> Gidr2k = new List<string>(); // Массив строк

                using (StreamReader sr = new StreamReader(WritePathGidr2k, Encoding.Default))
                {
                    #region Проверяем каждую строку и перезаписываем число расхода для каждого соединения

                    string LineOfGidr2k;
                    int k = 0;
                    while ((LineOfGidr2k = await sr.ReadLineAsync()) != null)
                    {
                        if (LineOfGidr2k.IndexOf("<JUN_KCI2KJ Value=\"") != -1)
                        {
                            LineOfGidr2k = "   <JUN_KCI2KJ Value=\"" + InitCond[k] + "\" Comment=\"Расход теплоносителя, отн.ед.\"/>";
                            Gidr2k.Add(LineOfGidr2k);
                            k++;
                        }
                        else
                        {
                            Gidr2k.Add(LineOfGidr2k);
                        }
                    }
                    #endregion
                }
                using (StreamWriter sw = new StreamWriter(WritePathGidr2k, false, Encoding.Default))
                {
                    foreach (var item in Gidr2k)
                    {
                        sw.WriteLine(item);
                    }

                }
                Gidr2k.Clear();

                #endregion

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Gidr2k.dat не был найден");
            }

            #endregion

            #region MEASURE.DAT

            try
            {
                int CountOfSens = 0;

                using (StreamReader sr = new StreamReader(ReadPathMeasure, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathMeasure, false, Encoding.Default))
                    {
                        int Iter = 0;
                        string Comment;
                        string Description;

                        #region Записали массив

                        List<string> ArrayOfMeasure = new List<string>(); // Массив строк с файла Volid
                        string LineOfMeasure;
                        while ((LineOfMeasure = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfMeasure.StartsWith("C") && !LineOfMeasure.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfMeasure) && !LineOfMeasure.StartsWith("!"))
                            {
                                ArrayOfMeasure.Add(LineOfMeasure.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfMeasure.Count; i++)
                        {
                            Iter++;

                            #region Считываем первую строчку

                            string[] ArrOfStrDescr = ArrayOfMeasure[i].Split(new string[] { "/", "!" }, StringSplitOptions.RemoveEmptyEntries);
                            string[] ArrOfStr = ArrayOfMeasure[i].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfMeasure, ref ArrOfStr, i, formatter); // Проверяем на *

                            #endregion

                            #region Проверяем на "endmeas"

                            if (ArrOfStr[0] == "endmeas")
                            {
                                CountOfSens = Iter - 1;
                                sw.Write("</GENERAL_SENS>");
                                break;
                            }

                            #endregion

                            #region Запись comment-ария

                            if (ArrOfStr[1] == "D_Pow")
                            {
                                Comment = "Датчик измерения мощности";
                            }
                            else if (ArrOfStr[1] == "D_Temp")
                            {
                                Comment = "Датчик измерения температуры";
                            }
                            else if (ArrOfStr[1] == "D_Pres")
                            {
                                Comment = "Датчик измерения давления";
                            }
                            else if (ArrOfStr[1] == "D_Flow")
                            {
                                Comment = "Датчик измерения расхода";
                            }
                            else if (ArrOfStr[1] == "D_Pos")
                            {
                                Comment = "Датчик измерения уровня";
                            }
                            else if (ArrOfStr[1] == "D_Velo")
                            {
                                Comment = "Датчик измерения скорости изменения";
                            }
                            else if (ArrOfStr[1] == "D_frec")
                            {
                                Comment = "Датчик измерения частоты";
                            }
                            else if (ArrOfStr[1] == "D_Volt")
                            {
                                Comment = "Датчик измерения напряжения";
                            }
                            else if (ArrOfStr[1] == "D_Reac")
                            {
                                Comment = "Датчик измерения реактивности";
                            }
                            else if (ArrOfStr[1] == "D_TempS")
                            {
                                Comment = "Датчик измерения температуры насыщения";
                            }
                            else if (ArrOfStr[1] == "D_USU")
                            {
                                Comment = "Сигнал АСУ ТП";
                            }
                            else if (ArrOfStr[1] == "D_Cbor")
                            {
                                Comment = "Датчик измерения концентрации борной кислоты";
                            }
                            else if (ArrOfStr[1] == "D_Ofset")
                            {
                                Comment = "Офсет";
                            }
                            else if (ArrOfStr[1] == "D_IIX")
                            {
                                Comment = "Датчик измерения энтальпии";
                            }
                            else if (ArrOfStr[1] == "D_DEN")
                            {
                                Comment = "Датчик измерения плотности";
                            }
                            else if (ArrOfStr[1] == "D_Time")
                            {
                                Comment = "Таймер";
                            }
                            else
                            {
                                Comment = "НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНОНЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО НЕ ОПРЕДЕЛЕНО";
                            }

                            #endregion

                            #region Запись Description-на

                            if (ArrOfStrDescr.Length > 1)
                            {
                                Description = ArrOfStrDescr[ArrOfStrDescr.Length - 1];
                            }
                            else
                            {
                                Description = " ";
                            }

                            #endregion

                            #region Запись в файл

                            if (i == 0)
                            {
                                sw.WriteLine("<GENERAL_SENS Value=\"???\" Comment=\"Количество датчиков измерения значений расчетных параметров\">");
                            }
                            sw.WriteLine(" <SENS_NAME Value=\"{0}\">", ArrOfStr[0]);
                            sw.WriteLine("  <SENS_PROP Numb=\"{0}\" Description=\"{1}\"/>", Iter, Description);
                            sw.WriteLine("  <SENS_NameMP Value=\"{0}\" Comment=\"{1}\"/>", ArrOfStr[1], Comment);
                            sw.WriteLine("  <SENS_NameEq Value=\"{0}\" Comment=\"Место расположения\"/>", ArrOfStr[2]);
                            sw.WriteLine("  <SENS_MPARAM Value=\"{0}\" Comment=\"Положение в реакторе или положение относительно низа объема с уровнем, м\"/>", ArrOfStr[3]);
                            sw.WriteLine("  <SENS_LPARAM Value=\"{0}\" Comment=\"Номер ТВС или участка трубы или метод измерения уровня\"/>", ArrOfStr[4]);
                            sw.WriteLine("  <SENS_WPARAM Value=\"{0}\" Comment=\"Номер участка ТВС по высоте\"/>", ArrOfStr[5]);
                            sw.WriteLine("  <SENS_TAU Value=\"{0}\" Comment=\"Постоянная времени канала измерения, с\"/>", ArrOfStr[6]);
                            sw.WriteLine("  <SENS_KUS Value=\"{0}\" Comment=\"Чувствительность канала измерения, 1 / ед.изм.вел.\"/>", ArrOfStr[7]);
                            if (ArrOfStr[2] == "AZ_OUT")
                            {
                                sw.WriteLine("  <TVERSUSN Comment=\"Зависимость постоянной времени от нейтронного потока\">");
                                i++;
                                string SizeTbl = ArrayOfMeasure[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                sw.WriteLine("   <SENS_JTAUN Value=\"{0}\" Comment=\"Размерность таблицы\"/>", SizeTbl);
                                i++;
                                ArrOfStr = ArrayOfMeasure[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfMeasure, ref ArrOfStr, i, formatter); // Проверяем на *
                                for (int j = 0; j < int.Parse(SizeTbl); j++)
                                {
                                    sw.WriteLine("   <DEP_PSOUR_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Момент времени №{1}, с\"/>", ArrOfStr[j], j + 1);
                                }
                                for (int j = int.Parse(SizeTbl); j < 2 * int.Parse(SizeTbl); j++)
                                {
                                    sw.WriteLine("   <DEP_PSOUR Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение №{1}, отн.ед.\"/>", ArrOfStr[j], j + 1 - int.Parse(SizeTbl));
                                }
                                sw.WriteLine("  </TVERSUSN>");
                            }
                            sw.WriteLine(" </SENS_NAME>");

                            #endregion

                        }

                    }
                }

                #region Запись количества датчиков

                List<string> ArrayOfMeasureNewFormat = new List<string>(); // Массив строк с файла Volid

                using (StreamReader sr = new StreamReader(WritePathMeasure, Encoding.Default))
                {
                    string LineOfMeasureNewFormat;
                    while ((LineOfMeasureNewFormat = await sr.ReadLineAsync()) != null)
                    {
                        ArrayOfMeasureNewFormat.Add(LineOfMeasureNewFormat);
                    }
                    string NewStr = "<GENERAL_SENS Value=\"" + CountOfSens + "\" Comment=\"Количество датчиков измерения значений расчетных параметров\">";
                    ArrayOfMeasureNewFormat[0] = NewStr;
                }
                using (StreamWriter sw = new StreamWriter(WritePathMeasure, false, Encoding.Default))
                {
                    foreach (var item in ArrayOfMeasureNewFormat)
                    {
                        sw.WriteLine(item);
                    }
                }

                ArrayOfMeasureNewFormat.Clear();

                #endregion

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл Measure.dat не был найден");
            }

            #endregion

            #region ELPOWS.DAT Исправить с именем трубин, чтобы если есть имя после считывания параметров - писать это имя, иначе - стандартное

            List<string> NameOfNET = new List<string>(); // Необходим для файлов, которые расположены ниже

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathElpows, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathElpows, false, Encoding.Default))
                    {


                        #region Создаем массивы

                        List<string> NameOfShaft = new List<string>();

                        List<string> NameOfELG = new List<string>();
                        List<string> ELG_StrN1 = new List<string>();
                        List<string> ELG_StrN2 = new List<string>();
                        List<string> ELG_StrN3 = new List<string>();
                        List<string> ELG_StrN4 = new List<string>();
                        List<string> ELG_StrN5 = new List<string>();


                        List<string> NET_StrN1 = new List<string>();
                        List<string> NET_StrN2 = new List<string>();
                        List<string> NET_StrN3 = new List<string>();

                        List<string> NameOfELM = new List<string>();
                        List<string> ELM_StrN1 = new List<string>();
                        List<string> ELM_StrN2 = new List<string>();
                        List<string> ELM_StrN3 = new List<string>();
                        List<string> ELM_StrN4 = new List<string>();

                        List<string> PUMP_StrN1 = new List<string>();

                        #endregion


                        #region Прочитали, убрали комментарии и лишние пробелы

                        List<string> ArrayOfElpows = new List<string>(); // Массив строк с файла Volid
                        string LineOfElpows;
                        while ((LineOfElpows = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfElpows.StartsWith("C") && !LineOfElpows.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfElpows) && !LineOfElpows.StartsWith("!"))
                            {
                                ArrayOfElpows.Add(LineOfElpows.Trim());
                            }
                        }

                        #endregion


                        for (int i = 0; i < ArrayOfElpows.Count; i++)
                        {


                            #region ОПИСАНИЕ ВАЛОВ И ТУРБИН

                            #region Считываем количество валов и турбин

                            string[] CountShaftAndTurb = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfElpows, ref CountShaftAndTurb, i, formatter); // Проверяем на *
                            i++;

                            #endregion 

                            string[][] PropOfTurb = new string[int.Parse(CountShaftAndTurb[1])][];

                            #region Считываем названия валов

                            for (int j = 0; j < int.Parse(CountShaftAndTurb[0]); j++)
                            {
                                NameOfShaft.Add(ArrayOfElpows[i]);
                                i++;
                            }

                            #endregion

                            #region Считываем характеристики всех турбин

                            for (int j = 0; j < int.Parse(CountShaftAndTurb[1]); j++)
                            {
                                string[] StrTotal = new string[12];
                                string[] StrN1 = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfElpows, ref StrN1, i, formatter); // Проверяем на *
                                i++;

                                string[] StrN2 = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfElpows, ref StrN2, i, formatter); // Проверяем на *
                                i++;

                                for (int k = 0; k < 5; k++)
                                {
                                    StrTotal[k] = StrN1[k];
                                }
                                for (int k = 5; k < 12; k++)
                                {
                                    StrTotal[k] = StrN2[k - 5];
                                }
                                PropOfTurb[j] = StrTotal;
                            }

                            #endregion

                            #region Считываем номинальные скорости на валах

                            string[] NomVelos = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfElpows, ref NomVelos, i, formatter); // Проверяем на *

                            i++;

                            #endregion

                            #endregion


                            #region ОПИСАНИЕ ЭЛЕКТРОГЕНЕРАТОРОВ

                            #region Считываем количество электрогенераторов

                            string[] CountELG = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            i++;

                            #endregion

                            #region Считываем каждый электрогенераторы

                            for (int j = 0; j < int.Parse(CountELG[0]); j++)
                            {
                                NameOfELG.Add(ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Название
                                i++;

                                string[] ELG_1 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref ELG_1, i, formatter); // Проверяем на *
                                for (int k = 0; k < 3; k++)
                                {
                                    ELG_StrN1.Add(ELG_1[k]);
                                }
                                i++;

                                string[] ELG_2 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref ELG_2, i, formatter); // Проверяем на *
                                for (int k = 0; k < 3; k++)
                                {
                                    ELG_StrN2.Add(ELG_2[k]);
                                }
                                i++;

                                ELG_StrN3.Add(ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                i++;

                                string[] ELG_4 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref ELG_4, i, formatter); // Проверяем на *
                                for (int k = 0; k < 4; k++)
                                {
                                    ELG_StrN4.Add(ELG_4[k]);
                                }
                                i++;
                            }

                            string[] ELG_5 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                            ReadStar(ref ArrayOfElpows, ref ELG_5, i, formatter); // Проверяем на *
                            for (int k = 0; k < 2 * NameOfELG.Count; k++)
                            {
                                ELG_StrN5.Add(ELG_5[k]);

                            }
                            i++;
                            #endregion

                            #endregion


                            #region ОПИСАНИЕ ЭЛЕКТРОСЕТЕЙ

                            #region Считываем количество электросетей

                            string[] CountNET = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            i++;

                            #endregion

                            #region Считываем каждую электросеть

                            for (int j = 0; j < int.Parse(CountNET[0]); j++)
                            {
                                NameOfNET.Add(ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Название
                                i++;

                                NET_StrN1.Add(ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Название
                                i++;

                                string[] NET_1 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref NET_1, i, formatter); // Проверяем на *
                                for (int k = 0; k < 2 * int.Parse(NET_StrN1[j]); k++)
                                {
                                    NET_StrN2.Add(NET_1[k]);
                                }
                                i++;
                            }

                            #endregion

                            #region Начальные условия электросетей

                            string[] NET_2 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                            ReadStar(ref ArrayOfElpows, ref NET_2, i, formatter); // Проверяем на *
                            for (int k = 0; k < NameOfNET.Count; k++)
                            {
                                NET_StrN3.Add(NET_2[k]);
                            }
                            i++;
                            #endregion

                            #endregion


                            #region ОПИСАНИЕ ЭЛЕКТРОПРИВОДОВ

                            #region Количество электроприводов

                            string[] CountELM = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            i++;

                            #endregion

                            #region Описание электродвигателя

                            for (int j = 0; j < int.Parse(CountELM[0]); j++)
                            {

                                NameOfELM.Add(ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Название
                                i++;

                                string[] ELM_1 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref ELM_1, i, formatter); // Проверяем на *
                                for (int k = 0; k < 5; k++)
                                {
                                    ELM_StrN1.Add(ELM_1[k]);
                                }
                                i++;

                                string[] ELM_2 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref ELM_2, i, formatter); // Проверяем на *
                                for (int k = 0; k < 2; k++)
                                {
                                    ELM_StrN2.Add(ELM_2[k]);
                                }
                                i++;

                                string[] ELM_3 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref ELM_3, i, formatter); // Проверяем на *
                                for (int k = 0; k < 2 * int.Parse(ELM_StrN2[((j + 1) * 2) - 1]); k++)
                                {
                                    ELM_StrN3.Add(ELM_3[k]);
                                }
                                i++;

                            }

                            #endregion

                            #region Начальные условия электродвигателей

                            string[] ELM_4 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                            ReadStar(ref ArrayOfElpows, ref ELM_4, i, formatter); // Проверяем на *
                            for (int k = 0; k < NameOfELM.Count; k++)
                            {
                                ELM_StrN4.Add(ELM_4[k]);

                            }
                            i++;
                            #endregion

                            #endregion


                            #region ОПИСАНИЕ НАСОСОВ

                            #region Считываем количество электросетей

                            string[] CountPUMP = ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            i++;

                            #endregion

                            #region Описание свойств насосов

                            for (int j = 0; j < int.Parse(CountPUMP[0]); j++)
                            {
                                string[] PUMP_1 = (ArrayOfElpows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                ReadStar(ref ArrayOfElpows, ref PUMP_1, i, formatter); // Проверяем на *

                                PUMP_StrN1.Add(PUMP_1[0].Substring(0, 2));
                                PUMP_StrN1.Add(PUMP_1[0].Substring(2, 2));
                                PUMP_StrN1.Add(PUMP_1[1]);
                                i++;
                            }

                            #endregion

                            #endregion


                            #region ЗАПИСЬ В ФАЙЛ

                            sw.WriteLine("<EQP_DATA Comment=\"Данные для расчета электроэнергетической части\">");

                            #region Записали валы

                            sw.WriteLine(" <SHAFT_CNT Value=\"{0}\" Comment=\"Количество валов\">", CountShaftAndTurb[0]);
                            for (int j = 0; j < int.Parse(CountShaftAndTurb[0]); j++)
                            {
                                sw.WriteLine("  <SHAFT_NAME Value=\"{0}\">", NameOfShaft[j]);
                                sw.WriteLine("   <SHAFT_PROP SHAFT_NUM=\"{0}\" Comment=\"Вал турбины\"/>", (j + 1));
                                sw.WriteLine("   <INITDATA_SHAFT Comment=\"Начальное условие\">");
                                sw.WriteLine("    <SHAFT_OMTUR Value=\"{0}\" Comment=\"Cкорость вращения вала, Гц\"/>", NomVelos[j]);
                                sw.WriteLine("   </INITDATA_SHAFT>");
                                sw.WriteLine("  </SHAFT_NAME>");
                            }
                            sw.WriteLine(" </SHAFT_CNT>");

                            #endregion

                            #region Записали турбины

                            sw.WriteLine(" <TURB_CNT Value=\"{0}\" Comment=\"Количество турбин\">", CountShaftAndTurb[1]);
                            for (int j = 0; j < int.Parse(CountShaftAndTurb[1]); j++)
                            {
                                sw.WriteLine("  <TURB_NAME Value=\"TurbineStage{0}\">", (j + 1));
                                sw.WriteLine("   <TURB_PROP TURB_NUM=\"{0}\" Comment=\"Турбина\" Description=\"Ступень №{0} турбины\"/>", (j + 1), PropOfTurb[j][0]);
                                sw.WriteLine("   <GENERAL_TURB Comment=\"ИД для турбины\">");
                                sw.WriteLine("    <TURB_SHAFTNAME Value=\"{0}\" Comment=\"Имя вала на который насажена турбина\"/>", NameOfShaft[int.Parse(PropOfTurb[j][0]) - 1]);
                                sw.WriteLine("    <TURB_SHAFTNUM Value=\"{0}\" Comment=\"Номер вала на который насажена турбина\"/>", PropOfTurb[j][0]);
                                sw.WriteLine("    <TURB_MJTUR Value=\"{0}\" Comment=\"Момент механической инерции турбины, кг * м ^ 2\"/>", PropOfTurb[j][1]);
                                sw.WriteLine("    <TURB_MDIS1 Value=\"{0}\" Comment=\"Момент потерь в сальниках, Н * м\"/>", PropOfTurb[j][2]);
                                sw.WriteLine("    <TURB_MDIS2 Value=\"{0}\" Comment=\"Момент потерь в подшипниках, Н * м\"/>", PropOfTurb[j][3]);
                                sw.WriteLine("    <TURB_MDIS3 Value=\"{0}\" Comment=\"Момент дискового трения, Н * м\"/>", PropOfTurb[j][4]);
                                sw.WriteLine("    <TURB_RETUR Value=\"{0}\" Comment=\"Реактивность турбины\"/>", PropOfTurb[j][7]);
                                sw.WriteLine("    <TURB_FITUR Value=\"{0}\" Comment=\"Коэффициент скорости сопловых лопаток\"/>", PropOfTurb[j][5]);
                                sw.WriteLine("    <TURB_NUTUR Value=\"{0}\" Comment=\"Скоростная характеристика турбины\"/>", PropOfTurb[j][6]);
                                sw.WriteLine("   </GENERAL_TURB>");
                                sw.WriteLine("   <NOMDATA_TURB Comment=\"Номинальные значения\">");
                                sw.WriteLine("    <TURB_G0TUR Value=\"{0}\" Comment=\"Номинальный расход через турбину, кг / с\"/>", PropOfTurb[j][8]);
                                sw.WriteLine("    <TURB_GA0TUR Value=\"{0}\" Comment=\"Номинальная плотность пара на входе в турбину, кг / м ^ 3\"/>", PropOfTurb[j][9]);
                                sw.WriteLine("    <TURB_ET0TUR Value=\"{0}\" Comment=\"КПД турбины в номинальной точке, отн.ед.\"/>", PropOfTurb[j][10]);
                                sw.WriteLine("    <TURB_OM0TUR Value=\"{0}\" Comment=\"Номинальная скорость вращения турбины, Гц\"/>", PropOfTurb[j][11]);
                                sw.WriteLine("   </NOMDATA_TURB>");
                                sw.WriteLine("  </TURB_NAME>");
                            }
                            sw.WriteLine(" </TURB_CNT>");

                            #endregion

                            #region Записали электрогенераторы

                            sw.WriteLine(" <ELG_CNT Value=\"{0}\" Comment=\"Количество электрогенераторов\">", CountELG[0]);
                            for (int j = 0; j < int.Parse(CountELG[0]); j++)
                            {
                                sw.WriteLine("  <ELG_NAME Value=\"{0}\">", NameOfELG[j]);
                                sw.WriteLine("   <ELG_PROP Numb=\"{0}\" Comment=\"Электрогенератор\" Description=\"Электрогенератор №{0}\"/>", (j + 1));
                                sw.WriteLine("   <GENERAL_ELG Comment=\"ИД для электрогенератора\">");
                                sw.WriteLine("    <ELG_SHAFTNAME Value=\"{0}\" Comment=\"Имя вала, на котором навешен генератор\"/>", NameOfShaft[int.Parse(ELG_StrN1[3 * j]) - 1]);
                                sw.WriteLine("    <ELG_SHAFTNUM Value=\"{0}\" Comment=\"Номер вала на котором навешен генератор\"/>", ELG_StrN1[3 * j]);
                                sw.WriteLine("    <ELG_NETNAME Value=\"{0}\" Comment=\"Имя электрической сети, на которую нагружен генератор\"/>", NameOfNET[int.Parse(ELG_StrN1[3 * j + 1]) - 1]);
                                sw.WriteLine("    <ELG_NETNUM Value=\"{0}\" Comment=\"Номер электрической сети, на которую нагружен генератор\"/>", ELG_StrN1[3 * j + 1]);
                                sw.WriteLine("    <ELG_ASU Value=\"0\" Comment=\"Номер сигнала управления, по которому происходит отключение генератора от сети\"/>");
                                sw.WriteLine("    <ELG_MJGEN Value=\"{0}\" Comment=\"Момент механической инерции генератора, кг * м ^ 2\"/>", ELG_StrN2[3 * j]);
                                sw.WriteLine("    <ELG_NASIN Value=\"{0}\" Comment=\"Изменение механической мощности на валу генератора при отклонении от частоты сети на один радиан в секунду, Вт / (рад / с)\"/>", ELG_StrN2[3 * j + 2]);
                                sw.WriteLine("    <ELG_TAUALT Value=\"{0}\" Comment=\"Характерное время регулятора тока возбуждения, с\"/>", ELG_StrN4[4 * j]);
                                sw.WriteLine("    <ELG_DEDI Value=\"{0}\" Comment=\"Крутизна регулировочной кривой ЭДС генератора от тока возбуждения, В / А\"/>", ELG_StrN4[4 * j + 1]);
                                sw.WriteLine("    <ELG_IMAXA Value=\"{0}\" Comment=\"Ограничение по току возбуждения генератора, А\"/>", ELG_StrN4[4 * j + 2]);
                                sw.WriteLine("    <ELG_UMAXA Value=\"{0}\" Comment=\"Значение напряжения на клеммах генератора, при котором ток возбуждения становится равным нулю, В\"/>", ELG_StrN4[4 * j + 3]);
                                sw.WriteLine("   </GENERAL_ELG>");
                                sw.WriteLine("   <NOMDATA_ELG Comment=\"Номинальные значения\">");
                                sw.WriteLine("    <ELG_NNOM Value=\"{0}\" Comment=\"Электрическая мощность генератора, выдаваемая в сеть, Вт\"/>", ELG_StrN2[3 * j + 1]);
                                sw.WriteLine("   </NOMDATA_ELG>");
                                sw.WriteLine("   <INITDATA_ELG Comment=\"Начальные условия\">");
                                sw.WriteLine("    <ELG_FIN Value=\"{0}\" Comment=\"Начальный угол нагрузки генератора, рад\"/>", ELG_5[2 * j]);
                                sw.WriteLine("    <ELG_IALTER Value=\"{0}\" Comment=\"Начальный ток возбуждения генератора, А\"/>", ELG_5[(2 * j) + 1]);
                                sw.WriteLine("   </INITDATA_ELG>");
                                sw.WriteLine("  </ELG_NAME>");
                            }
                            sw.WriteLine(" </ELG_CNT>");


                            #endregion

                            #region Записали электросети

                            sw.WriteLine(" <NET_CNT Value=\"{0}\" Comment=\"Количество электросетей\">", CountNET[0]);
                            int NumTbl = 0;
                            for (int j = 0; j < int.Parse(CountNET[0]); j++)
                            {
                                sw.WriteLine("  <NET_NAME Value=\"{0}\">", NameOfNET[j]);
                                sw.WriteLine("   <NET_PROP NET_NUM=\"{0}\" Comment=\"Электросеть\" Description=\"Электросеть №{0}\"/>", (j + 1));
                                sw.WriteLine("   <GENERAL_NET Comment=\"ИД для электрической сети\">");
                                sw.WriteLine("    <NET_JSOUR Value=\"{0}\" Comment=\"Размерность таблицы зависимости мощности внешних источников от времени\"/>", NET_StrN1[j]);
                                for (int k = NumTbl; k < int.Parse(NET_StrN1[j]) + NumTbl; k++)
                                {
                                    sw.WriteLine("    <NET_PSOUR_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Момент времени №{1}, с\"/>", NET_StrN2[k], (k + 1 - NumTbl));

                                }
                                for (int k = int.Parse(NET_StrN1[j]) + NumTbl; k < NumTbl + 2 * int.Parse(NET_StrN1[j]); k++)
                                {
                                    sw.WriteLine("    <NET_PSOUR Value=\"{0}\" Numb=\"{1}\" Comment=\"Значение мощности внешних источников №{1}, Вт\"/>", NET_StrN2[k], (k - int.Parse(NET_StrN1[j]) + 1 - NumTbl));

                                }
                                NumTbl += int.Parse(NET_StrN1[j]) * 2;
                                sw.WriteLine("   </GENERAL_NET>");
                                sw.WriteLine("   <INITDATA_NET Comment=\"Начальные условия\">");
                                sw.WriteLine("    <NET_OMNET Value=\"{0}\" Comment=\"Частота электросети, 1/с\"/>", NET_StrN3[j]);
                                sw.WriteLine("   </INITDATA_NET>");
                                sw.WriteLine("  </NET_NAME>");
                            }
                            sw.WriteLine(" </NET_CNT>");

                            #endregion

                            #region Записали электродвигатели

                            NumTbl = 0;
                            sw.WriteLine(" <ELM_CNT Value=\"{0}\" Comment=\"Количество электродвигателей\">", CountELM[0]);
                            for (int j = 0; j < int.Parse(CountELM[0]); j++)
                            {
                                sw.WriteLine("  <ELM_NAME Value=\"{0}\">", NameOfELM[j]);
                                sw.WriteLine("   <ELM_PROP Numb=\"{0}\" Comment=\"Электродвигатель\" Description=\"Электродвигатель №{0}\"/>", (j + 1));
                                sw.WriteLine("   <GENERAL_ELM Comment=\"ИД для электродвигателя\">");
                                sw.WriteLine("    <ELM_JMAC Value=\"{0}\" Comment=\"Тип электродвигателя\"/>", ELM_StrN2[2 * j]);
                                sw.WriteLine("    <ELM_NETNAME Value=\"{0}\" Comment=\"Имя электрической сети, к которой подключен электродвигатель\"/>", NameOfNET[int.Parse(ELM_StrN1[5 * j]) - 1]);
                                sw.WriteLine("    <ELM_NETNUM Value=\"{0}\" Comment=\"Номер электрической сети, к которой подключен электродвигатель\"/>", ELM_StrN1[5 * j]);
                                sw.WriteLine("    <ELM_MJMAC Value=\"{0}\" Comment=\"Механический момент инерции ротора электродвигателя, кг * м ^ 2\"/>", ELM_StrN1[5 * j + 1]);
                                sw.WriteLine("    <ELM_M1MAC Value=\"{0}\" Comment=\"Коэффициент в соотношении момента потерь на трение от частоты(постоянная составляющая), Н * м\"/>", ELM_StrN1[5 * j + 2]);
                                sw.WriteLine("    <ELM_M2MAC Value=\"{0}\" Comment=\"Коэффициент в соотношении момента потерь на трение от частоты(при линейном члене), Н * м\"/>", ELM_StrN1[5 * j + 3]);
                                sw.WriteLine("    <ELM_M3MAC Value=\"{0}\" Comment=\"Коэффициент в соотношении момента потерь на трение от частоты(при квадратичном члене), Н * м\"/>", ELM_StrN1[5 * j + 4]);
                                if (int.Parse(ELM_StrN2[2 * j]) == 1)
                                {
                                    sw.WriteLine("    <ELM_JVFMAC Value=\"{0}\" Comment=\"Размерность таблицы зависимости возмущения по частоте преобразователя от времени\"/>", ELM_StrN2[2 * j + 1]);
                                    for (int k = NumTbl; k < int.Parse(ELM_StrN2[2 * j + 1]) + NumTbl; k++)
                                    {
                                        sw.WriteLine("    <ELM_VFMAC_ARG Value=\"{0}\" Numb=\"{1}\" Comment=\"Моменты времени №{1}, с\"/>", ELM_StrN3[k], (k + 1 - NumTbl));

                                    }
                                    for (int k = int.Parse(ELM_StrN2[2 * j + 1]) + NumTbl; k < NumTbl + 2 * int.Parse(ELM_StrN2[2 * j + 1]); k++)
                                    {
                                        sw.WriteLine("    <ELM_VFMAC Value=\"{0}\" Numb=\"{1}\" Comment=\"Возмущения по частоте №{1}, Гц\"/>", ELM_StrN3[k], (k - int.Parse(ELM_StrN2[2 * j + 1]) + 1 - NumTbl));

                                    }
                                }
                                NumTbl += int.Parse(ELM_StrN2[2 * j + 1]) * 2;
                                sw.WriteLine("   </GENERAL_ELM>");
                                sw.WriteLine("   <INITDATA_ELM Comment=\"Начальные условия\">");
                                sw.WriteLine("    <ELM_OMELMA Value=\"{0}\" Comment=\"Cкорость вращения электродвигателя, Гц\"/>", ELM_StrN4[j]);
                                sw.WriteLine("   </INITDATA_ELM>");
                                sw.WriteLine("  </ELM_NAME>");
                            }
                            sw.WriteLine(" </ELM_CNT>");

                            #endregion

                            #region Записали Насосы

                            sw.WriteLine(" <PUMP_CNT Value=\"{0}\" Comment=\"Количество насосов\">", CountPUMP[0]);
                            for (int j = 0; j < int.Parse(CountPUMP[0]); j++)
                            {
                                sw.WriteLine("  <JUN_PUMPNUM Value=\"{0}\" Comment=\"Номер насоса\">", (j + 1));
                                sw.WriteLine("   <GENERAL_PUMP Comment=\"ИД для насоса\">");
                                sw.WriteLine("    <PUMP_TUREM Value=\"{0}\" Comment=\"Тип привода насоса\"/>", PUMP_StrN1[3 * j]);
                                if (int.Parse(PUMP_StrN1[3 * j]) == 20)
                                {
                                    sw.WriteLine("    <PUMP_ELMNAME Value=\"{0}\" Comment=\"Имя электродвигателя\"/>", NameOfELM[int.Parse(PUMP_StrN1[3 * j + 1]) - 1]);
                                    sw.WriteLine("    <PUMP_ELMNUM Value=\"{0}\" Comment=\"Номер электродвигателя\"/>", int.Parse(PUMP_StrN1[3 * j + 1]));
                                }
                                else if (int.Parse(PUMP_StrN1[3 * j]) == 10)
                                {
                                    sw.WriteLine("    <PUMP_SHAFTNAME Value=\"{0}\" Comment=\"Имя вала на который насажен насос\"/>", NameOfShaft[int.Parse(PUMP_StrN1[3 * j + 1]) - 1]);
                                    sw.WriteLine("    <PUMP_SHAFTNUM Value=\"{0}\" Comment=\"Номер вала на который насажен насос\"/>", int.Parse(PUMP_StrN1[3 * j + 1]));
                                }
                                else
                                {
                                    Console.WriteLine("НЕПРАВИЛЬНЫЙ ТИП НАСОСА");
                                }
                                sw.WriteLine("    <PUMP_MJPUMP Value=\"{0}\" Comment=\"Момент механической инерции насоса, кг * м ^ 2\"/>", PUMP_StrN1[3 * j + 2]);
                                sw.WriteLine("   </GENERAL_PUMP>");
                                sw.WriteLine("  </JUN_PUMPNUM>");
                            }
                            sw.WriteLine(" </PUMP_CNT>");

                            #endregion

                            sw.Write("</EQP_DATA>");

                            #endregion


                            break;

                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл ELPOWS.DAT не был найден");
            }

            #endregion

            #region ASUELK.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathAsuelk, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathAsuelk, false, Encoding.Default))
                    {

                        #region Создаем массивы

                        List<string> Ell_Str1 = new List<string>();
                        List<string> Ell_Str2 = new List<string>();
                        List<string> Ell_Str3 = new List<string>();
                        List<string> Ell_Str4 = new List<string>();

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfAsuelk = new List<string>(); // Массив строк с файла Asuelk
                        string LineOfAsuelk;
                        while ((LineOfAsuelk = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfAsuelk.StartsWith("C") && !LineOfAsuelk.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfAsuelk) && !LineOfAsuelk.StartsWith("!"))
                            {
                                ArrayOfAsuelk.Add(LineOfAsuelk.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfAsuelk.Count; i++)
                        {

                            #region Считали количество потребителей электроэнергии

                            string CountELL = ArrayOfAsuelk[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Считываем каждого потребителя электроэнергии

                            for (int j = 0; j < int.Parse(CountELL); j++)
                            {

                                #region Считали 1 строку

                                string[] Str1 = ArrayOfAsuelk[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelk, ref Str1, i, formatter); // Проверяем на *
                                for (int k = 0; k < 2; k++)
                                {
                                    Ell_Str1.Add(Str1[k]);
                                }
                                i++;

                                #endregion


                                #region Считали 2 строку

                                string[] Str2 = ArrayOfAsuelk[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelk, ref Str2, i, formatter); // Проверяем на *
                                Ell_Str2.Add(Str2[0]);
                                i++;

                                #endregion


                                #region Считали 3 строку

                                string[] Str3 = ArrayOfAsuelk[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelk, ref Str3, i, formatter); // Проверяем на *
                                for (int k = 0; k < 4; k++)
                                {
                                    Ell_Str3.Add(Str3[k]);
                                }
                                i++;

                                #endregion

                            }

                            #endregion

                            #region Считываем начальные условия

                            string[] Str4 = ArrayOfAsuelk[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfAsuelk, ref Str4, i, formatter); // Проверяем на *
                            for (int k = 0; k < int.Parse(CountELL); k++)
                            {
                                Ell_Str4.Add(Str4[k]);
                            }

                            #endregion

                            #region Записываем в файл

                            sw.WriteLine("<ELL_DATA Comment=\"Данные для расчета состояния потребителей электроэнергии\">");
                            sw.WriteLine(" <ELL_CNT Value=\"{0}\" Comment=\"Количество потребителей электроэнергии\">", CountELL);
                            for (int j = 0; j < int.Parse(CountELL); j++)
                            {
                                sw.WriteLine("  <ELL_NAME Value=\"Key{0}\">", (j + 1));
                                sw.WriteLine("   <ELL_PROP ELL_NUM=\"{0}\" Comment=\"Имя группы потребителей электроэнергии\" Description=\"Группа потребителей №{0}\"/>", (j + 1));
                                sw.WriteLine("   <GENERAL_ELL Comment=\"ИД для потребителей электроэнергии\">");
                                sw.WriteLine("    <ELL_NETNAME Value=\"{0}\" Comment=\"Имя электросети, к которой подключена группа потребителей электроэнергии\"/>", NameOfNET[int.Parse(Ell_Str1[2 * j]) - 1]);
                                sw.WriteLine("    <ELL_NETNUM Value=\"{0}\" Comment=\"Номер электросети, к которой подключена группа потребителей электроэнергии\"/>", Ell_Str1[2 * j]);
                                sw.WriteLine("    <ELL_PNKEY Value=\"{0}\" Comment=\"Мощность группы потребителей электроэнергии\"/>", Ell_Str1[2 * j + 1]);
                                sw.WriteLine("    <ELL_FOFF Value=\"{0}\" Comment=\"Уставка на отключение группы потребителей по снижению частоты сети\"/>", Ell_Str3[4 * j]);
                                sw.WriteLine("    <ELL_DELOFF Value=\"{0}\" Comment=\"Задержка на отключение группы потребителей, c\"/>", Ell_Str3[4 * j + 2]);
                                sw.WriteLine("    <ELL_FON Value=\"{0}\" Comment=\"Уставка на подключение группы потребителей при повышении частоты сети\"/>", Ell_Str3[4 * j + 1]);
                                sw.WriteLine("    <ELL_DELON Value=\"{0}\" Comment=\"Задержка на подключение группы потребителей, c\"/>", Ell_Str3[4 * j + 3]);
                                sw.WriteLine("   </GENERAL_ELL>");
                                sw.WriteLine("   <INITDATA_ELL>");
                                sw.WriteLine("    <ELL_VOLKEY Value=\"{0}\" Comment=\"Состояние электрического ключа групп потребителей\"/>", Ell_Str4[j]);
                                sw.WriteLine("   </INITDATA_ELL>");
                                sw.WriteLine("  </ELL_NAME>");
                            }
                            sw.WriteLine(" </ELL_CNT>");
                            sw.Write("</ELL_DATA>");

                            #endregion

                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл ASUELK.DAT не был найден");
            }

            #endregion

            #region ASUELM.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathAsuelm, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathAsuelm, false, Encoding.Default))
                    {

                        List<string> NamesOfAsuelm = new List<string>();
                        List<string> AsuOfAsuelm = new List<string>();
                        List<string> PropsOfAsuelm = new List<string>();

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfAsuelm = new List<string>(); // Массив строк с файла Asuelm
                        string LineOfAsuelm;
                        while ((LineOfAsuelm = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfAsuelm.StartsWith("C") && !LineOfAsuelm.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfAsuelm) && !LineOfAsuelm.StartsWith("!"))
                            {
                                ArrayOfAsuelm.Add(LineOfAsuelm.Trim());
                            }
                        }

                        #endregion


                        for (int i = 0; i < ArrayOfAsuelm.Count; i++)
                        {

                            #region Узнали количество электродвигателей

                            string CountELM = ArrayOfAsuelm[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Считали каждый электродвигатель

                            for (int j = 0; j < int.Parse(CountELM); j++)
                            {
                                #region Считали имена

                                string[] Str1 = ArrayOfAsuelm[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelm, ref Str1, i, formatter); // Проверяем на *
                                NamesOfAsuelm.Add(Str1[1]);
                                i++;

                                #endregion

                                #region Считали сигнал от АСУ на изменение состояния электродвигателя

                                string[] Str2 = ArrayOfAsuelm[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelm, ref Str2, i, formatter); // Проверяем на *
                                AsuOfAsuelm.Add(Str2[3]);
                                i++;

                                #endregion

                                #region Считали PHAND

                                string[] Str3 = ArrayOfAsuelm[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelm, ref Str3, i, formatter); // Проверяем на *
                                PropsOfAsuelm.Add(Str3[3]);
                                i++;

                                #endregion

                                #region Считали MHAND

                                string[] Str4 = ArrayOfAsuelm[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelm, ref Str4, i, formatter); // Проверяем на *
                                PropsOfAsuelm.Add(Str4[3]);
                                i++;

                                #endregion

                                #region Считали SHAND

                                string[] Str5 = ArrayOfAsuelm[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuelm, ref Str5, i, formatter); // Проверяем на *
                                PropsOfAsuelm.Add(Str5[3]);
                                i++;

                                #endregion

                                i++;

                            }

                            #endregion

                            #region Записали в файл

                            sw.WriteLine("<ASUEQP_DATA Comment=\"Данные для расчета параметров системы управления электродвигателями\">");
                            sw.WriteLine(" <ELM_CNT Value=\"{0}\" Comment=\"Количество электродвигателей\">", CountELM);
                            for (int j = 0; j < int.Parse(CountELM); j++)
                            {
                                sw.WriteLine("  <ELM_NAME Value=\"{0}\">", NamesOfAsuelm[j]);
                                sw.WriteLine("   <ELM_PROP Numb=\"{0}\" Comment=\"Электродвигатель\"/>", (j + 1), NamesOfAsuelm[j]);
                                sw.WriteLine("   <GENERAL_ELM Comment=\"ИД для электродвигателя\">");
                                sw.WriteLine("    <ELM_ASU1 Value=\"{0}NO\" Comment=\"Сигнал NO от АСУ на изменение состояния электродвигателя\"/>", NamesOfAsuelm[j]);
                                sw.WriteLine("    <ELM_ASU2 Value=\"{0}00\" Comment=\"Сигнал 00 от АСУ на изменение состояния электродвигателя\"/>", NamesOfAsuelm[j]);
                                sw.WriteLine("    <ELM_ASU3 Value=\"{0}01\" Comment=\"Сигнал 01 от АСУ на изменение состояния электродвигателя\"/>", NamesOfAsuelm[j]);
                                sw.WriteLine("    <ELM_ASU4 Value=\"{0}02\" Comment=\"Сигнал 02 от АСУ на изменение состояния электродвигателя\"/>", NamesOfAsuelm[j]);
                                sw.WriteLine("    <ELM_PHAND Value=\"{0}\" Comment=\"Число пар полюсов обмоток электродвигателя\"/>", PropsOfAsuelm[3 * j]);
                                sw.WriteLine("    <ELM_MHAND Value=\"{0}\" Comment=\"Момент, развиваемый электродвигателем при критическом скольжении\"/>", PropsOfAsuelm[3 * j + 1]);
                                sw.WriteLine("    <ELM_SHAND Value=\"{0}\" Comment=\"Критическое скольжение ротора при подключении обмоток\"/>", PropsOfAsuelm[3 * j + 2]);
                                sw.WriteLine("   </GENERAL_ELM>");
                                sw.WriteLine("  </ELM_NAME>");
                            }
                            sw.WriteLine(" </ELM_CNT>");
                            sw.Write("</ASUEQP_DATA>");

                            #endregion

                            break;

                        }


                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл ASUELM.DAT не был найден");
            }

            #endregion

            #region HSTR.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathHstr, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathHstr, false, Encoding.Default))
                    {

                        #region Инициализируем массивы

                        List<string> GENERAL_HSTR = new List<string>();
                        List<string> STRMAT_HSTR = new List<string>();
                        List<string> HSTR_NAME = new List<string>();
                        List<string> LeftElem_HSTR = new List<string>();
                        List<string> RightElem_HSTR = new List<string>();
                        List<string> StrAlwaysExist_HSTR = new List<string>();
                        List<string> TypeOfElem = new List<string>();
                        string CountHstr = null;

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfHstr = new List<string>(); // Массив строк с файла 
                        string LineOfHstr;
                        while ((LineOfHstr = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfHstr.StartsWith("C") && !LineOfHstr.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfHstr) && !LineOfHstr.StartsWith("!"))
                            {
                                ArrayOfHstr.Add(LineOfHstr.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfHstr.Count; i++)
                        {

                            #region Считали количество элементов

                            CountHstr = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Считываем каждый элемент

                            for (int j = 0; j < int.Parse(CountHstr); j++)
                            {

                                #region Считали "Число расчетных участков по длине" + "длину теплообменной поверхности" + "Тип источника"

                                string[] Str1 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfHstr, ref Str1, i, formatter); // Проверяем на *
                                for (int k = 0; k < 3; k++)
                                {
                                    GENERAL_HSTR.Add(Str1[k]);
                                }
                                i++;

                                #endregion

                                #region Считали "Свойства конструкционных материалов теплообменной поверхности"

                                string[] Str2 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfHstr, ref Str2, i, formatter); // Проверяем на *
                                for (int k = 0; k < 6; k++)
                                {
                                    STRMAT_HSTR.Add(Str2[k]);
                                }
                                i++;

                                #endregion

                                #region Если JHSTIP == 1

                                if (Str1[2] == "1")
                                {
                                    GENERAL_HSTR.Add(ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                    i++;
                                }
                                else
                                {
                                    GENERAL_HSTR.Add("NOLenght");
                                }

                                #endregion

                                #region Считываем строку с ASU

                                string[] Str3 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfHstr, ref Str3, i, formatter); // Проверяем на *
                                for (int k = 0; k < 2; k++)
                                {
                                    GENERAL_HSTR.Add(Str3[k]);
                                }
                                i++;

                                #endregion

                                if (Str3[1] == "NO") // Если это тепловое соединение
                                {
                                    TypeOfElem.Add("Тепловое соединение");

                                    #region Название левого элемента

                                    string[] Str4 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    HSTR_NAME.Add(Str4[0]);
                                    i++;

                                    #endregion

                                    #region Считали параметры элемента из которого выходит тепловая связь (Объем с уровнем)

                                    if (NameAndTypeForHSTR[Str4[0]] == "3" || NameAndTypeForHSTR[Str4[0]] == "31")
                                    {

                                        string[] Str8 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfHstr, ref Str8, i, formatter); // Проверяем на *
                                        for (int k = 0; k < 3; k++)
                                        {
                                            LeftElem_HSTR.Add(Str8[k]);
                                        }
                                        i++;

                                    }
                                    else
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            LeftElem_HSTR.Add("NO");
                                        }
                                    }

                                    #endregion

                                    #region Название правого элемента

                                    string[] Str5 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    HSTR_NAME.Add(Str5[0]);
                                    i++;

                                    #endregion

                                    #region Считали параметры элемента, куда входит тепловая связь (Объем с уровнем)

                                    if (NameAndTypeForHSTR[Str5[0]] == "3" || NameAndTypeForHSTR[Str5[0]] == "31")
                                    {

                                        string[] Str6 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfHstr, ref Str6, i, formatter); // Проверяем на *
                                        for (int k = 0; k < 3; k++)
                                        {
                                            RightElem_HSTR.Add(Str6[k]);
                                        }
                                        i++;

                                    }
                                    else
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            RightElem_HSTR.Add("NO");
                                        }
                                    }

                                    #endregion

                                    #region Считали строку отвечающую за трубы, она есть всегда

                                    string[] Str7 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfHstr, ref Str7, i, formatter); // Проверяем на *
                                    for (int k = 0; k < 4; k++)
                                    {
                                        StrAlwaysExist_HSTR.Add(Str7[k]);
                                    }
                                    i++;

                                    #endregion


                                }
                                else // Если это тепловой источник
                                {
                                    TypeOfElem.Add("Источник тепловыделений");

                                    for (int k = 0; k < 3; k++)
                                    {
                                        LeftElem_HSTR.Add("NO");
                                    }

                                    i = i + 2;

                                    #region Считали названия

                                    string[] Str4 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    HSTR_NAME.Add(Str4[0]);
                                    i++;
                                    string[] Str5 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    HSTR_NAME.Add(Str5[0]);
                                    i++;

                                    #endregion

                                    #region Считали тепловое соединение

                                    if (NameAndTypeForHSTR[HSTR_NAME[2 * j + 1]] == "3" || NameAndTypeForHSTR[HSTR_NAME[2 * j + 1]] == "31")
                                    {
                                        #region Считали параметры элемента, куда входит тепловая связь (Объем с уровнем)

                                        string[] Str6 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfHstr, ref Str6, i, formatter); // Проверяем на *
                                        for (int k = 0; k < 3; k++)
                                        {
                                            RightElem_HSTR.Add(Str6[k]);
                                        }
                                        i++;

                                        #endregion

                                        #region Считали строку отвечающую за трубы, она есть всегда

                                        string[] Str7 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfHstr, ref Str7, i, formatter); // Проверяем на *
                                        for (int k = 0; k < 4; k++)
                                        {
                                            StrAlwaysExist_HSTR.Add(Str7[k]);
                                        }
                                        i++;

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Если справа труба

                                        string[] Str7 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfHstr, ref Str7, i, formatter); // Проверяем на *
                                        for (int k = 0; k < 4; k++)
                                        {
                                            StrAlwaysExist_HSTR.Add(Str7[k]);
                                        }
                                        for (int k = 0; k < 3; k++)
                                        {
                                            RightElem_HSTR.Add("NO");
                                        }
                                        i++;

                                        #endregion
                                    }

                                    //if (NameAndTypeForHSTR[HSTR_NAME[2 * j + 1]] == "2")
                                    //{
                                    //    #region Если справа труба

                                    //    string[] Str7 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //    ReadStar(ref ArrayOfHstr, ref Str7, i, formatter); // Проверяем на *
                                    //    for (int k = 0; k < 4; k++)
                                    //    {
                                    //        StrAlwaysExist_HSTR.Add(Str7[k]);
                                    //    }
                                    //    for (int k = 0; k < 3; k++)
                                    //    {
                                    //        RightElem_HSTR.Add("NO");
                                    //    }
                                    //    i++;

                                    //    #endregion
                                    //}
                                    //else if(NameAndTypeForHSTR[HSTR_NAME[2 * j + 1]] == "3" || NameAndTypeForHSTR[HSTR_NAME[2 * j + 1]] == "31")
                                    //{
                                    //    #region Считали параметры элемента, куда входит тепловая связь (Объем с уровнем)

                                    //    string[] Str6 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //    ReadStar(ref ArrayOfHstr, ref Str6, i, formatter); // Проверяем на *
                                    //    for (int k = 0; k < 3; k++)
                                    //    {
                                    //        RightElem_HSTR.Add(Str6[k]);
                                    //    }
                                    //    i++;

                                    //    #endregion

                                    //    #region Считали строку отвечающую за трубы, она есть всегда

                                    //    string[] Str7 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //    ReadStar(ref ArrayOfHstr, ref Str7, i, formatter); // Проверяем на *
                                    //    for (int k = 0; k < 4; k++)
                                    //    {
                                    //        StrAlwaysExist_HSTR.Add(Str7[k]);
                                    //    }
                                    //    i++;

                                    //    #endregion
                                    //}
                                    //else
                                    //{
                                    //    #region Считали строку отвечающую за трубы, она есть всегда

                                    //    string[] Str7 = ArrayOfHstr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //    ReadStar(ref ArrayOfHstr, ref Str7, i, formatter); // Проверяем на *
                                    //    for (int k = 0; k < 4; k++)
                                    //    {
                                    //        StrAlwaysExist_HSTR.Add(Str7[k]);
                                    //    }
                                    //    for (int k = 0; k < 3; k++)
                                    //    {
                                    //        RightElem_HSTR.Add("NO");
                                    //    }
                                    //    i++;

                                    //    #endregion
                                    //}

                                    #endregion

                                }

                            }
                            break;

                            #endregion

                        }

                        #region Записываем в файл

                        sw.WriteLine("<JTHS Value=\"{0}\" Comment=\"Теплообменных поверхностей\">", CountHstr);
                        for (int i = 0; i < int.Parse(CountHstr); i++)
                        {

                            sw.WriteLine(" <HSTR_NAME Value=\"HeatStructure{0}\">", i + 1);
                            sw.WriteLine("  <HSTR_PROP Numb=\"{0}\" Comment=\"{1}\"/>", i + 1, TypeOfElem[i]);
                            sw.WriteLine("  <HSTR_LEFTNAME Value=\"{0}\" Comment=\"Элемент слева от т/о поверхности\"/>", HSTR_NAME[2 * i]);
                            if (HSTR_NAME[2 * i] != "NO")
                            {
                                sw.WriteLine("  <HSTR_LEFTTYPE Value=\"{0}\" Comment=\"Тип элемента слева от т/о поверхности\"/>", NameAndTypeForHSTR[HSTR_NAME[2 * i]]);
                            }
                            sw.WriteLine("  <HSTR_RIGHTNAME Value=\"{0}\" Comment=\"Элемент справа от т/о поверхности\"/>", HSTR_NAME[2 * i + 1]);
                            if (HSTR_NAME[2 * i + 1] != "NO")
                            {
                                sw.WriteLine("  <HSTR_RIGHTTYPE Value=\"{0}\" Comment=\"Тип элемента справа от т/о поверхности\"/>", NameAndTypeForHSTR[HSTR_NAME[2 * i + 1]]);
                            }
                            sw.WriteLine("  <GENERAL_HSTR Comment=\"Общие\">");
                            sw.WriteLine("   <HSTR_JHHS Value=\"{0}\" Comment=\"Число расчетных участков по длине теплообменной поверхности\"/>", GENERAL_HSTR[6 * i]);
                            sw.WriteLine("   <HSTR_JHSTIP Value=\"{0}\" Comment=\"Задание длины теплообменной поверхности\"/>", GENERAL_HSTR[6 * i + 2]);
                            if (GENERAL_HSTR[6 * i + 3] != "NOLenght")
                            {
                                sw.WriteLine("   <HSTR_ALHS Value=\"{0}\" Comment=\"Длина теплообменной поверхности, м\"/>", GENERAL_HSTR[6 * i + 3]);

                            }
                            sw.WriteLine("   <HSTR_QADHS Value=\"{0}\" Comment=\"Мощность источника тепловыделений, кВт\"/>", GENERAL_HSTR[6 * i + 4]);
                            sw.WriteLine("   <HSTR_ASU Value=\"{0}\" Comment=\"Имя управляющего сигнала АСУ на изменение состояния источника тепловыделений \"/>", GENERAL_HSTR[6 * i + 5]);

                            if (LeftElem_HSTR[3 * i] != "NO")
                            {
                                sw.WriteLine("   <HSTR_HBOTHSL Value=\"{0}\" Comment=\"Нижняя отметка теплообменной поверхности слева, м\"/>", LeftElem_HSTR[3 * i]);
                                sw.WriteLine("   <HSTR_HTOPHSL Value=\"{0}\" Comment=\"Верхняя отметка теплообменной поверхности слева, м\"/>", LeftElem_HSTR[3 * i + 1]);
                                sw.WriteLine("   <HSTR_JDIRHSL Value=\"{0}\" Comment=\"Ориентация теплообменной поверхности слева относительно вертикали\"/>", LeftElem_HSTR[3 * i + 2]);
                            }
                            if (RightElem_HSTR[3 * i] != "NO")
                            {
                                sw.WriteLine("   <HSTR_HBOTHSR Value=\"{0}\" Comment=\"Нижняя отметка теплообменной поверхности справа, м\"/>", RightElem_HSTR[3 * i]);
                                sw.WriteLine("   <HSTR_HTOPHSR Value=\"{0}\" Comment=\"Верхняя отметка теплообменной поверхности справа, м\"/>", RightElem_HSTR[3 * i + 1]);
                                sw.WriteLine("   <HSTR_JDIRHSR Value=\"{0}\" Comment=\"Ориентация теплообменной поверхности справа относительно вертикали\"/>", RightElem_HSTR[3 * i + 2]);
                            }
                            sw.WriteLine("   <HSTR_J1RUL Value=\"{0}\" Comment=\"Номер расчетного т/г участка элемента слева, граничащего с первым расчетным участком т/о поверхности\"/>", StrAlwaysExist_HSTR[4 * i]);
                            sw.WriteLine("   <HSTR_JADDL Value=\"{0}\" Comment=\"Индекс, определяющий номер расчетного т/г участка, граничащего с расчетным участком т/о поверхности слева\"/>", StrAlwaysExist_HSTR[4 * i + 1]);
                            sw.WriteLine("   <HSTR_J1RUR Value=\"{0}\" Comment=\"Номер расчетного т/г участка элемента справа, граничащего с первым расчетным участком т/о поверхности\"/>", StrAlwaysExist_HSTR[4 * i + 2]);
                            sw.WriteLine("   <HSTR_JADDR Value=\"{0}\" Comment=\"Индекс, определяющий номер расчетного т/г участка, граничащего с расчетным участком т/о поверхности справа\"/>", StrAlwaysExist_HSTR[4 * i + 3]);
                            sw.WriteLine("  </GENERAL_HSTR>");
                            sw.WriteLine("  <STRMAT_HSTR Comment=\"Свойства конструкционных материалов теплообменной поверхности\">");
                            sw.WriteLine("   <HSTR_CM Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*К)\"/>", STRMAT_HSTR[6 * i]);
                            sw.WriteLine("   <HSTR_RM Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_HSTR[6 * i + 1]);
                            sw.WriteLine("   <HSTR_ALMD Value=\"{0}\" Comment=\"Теплопроводность, кВт/(м*К)\"/>", STRMAT_HSTR[6 * i + 2]);
                            sw.WriteLine("   <HSTR_DLHS Value=\"{0}\" Comment=\"Диаметр теплообменных трубок слева, м\"/>", STRMAT_HSTR[6 * i + 3]);
                            sw.WriteLine("   <HSTR_DRHS Value=\"{0}\" Comment=\"Диаметр теплообменных трубок справа, м\"/>", STRMAT_HSTR[6 * i + 4]);
                            sw.WriteLine("   <HSTR_JTRHS Value=\"{0}\" Comment=\"Количество теплообменных трубок\"/>", STRMAT_HSTR[6 * i + 5]);
                            sw.WriteLine("  </STRMAT_HSTR>");
                            sw.WriteLine(" </HSTR_NAME>");

                        }
                        sw.Write("</JTHS>");

                        #endregion

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл HSTR.DAT не был найден");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Ошибка в файле hstr.dat (Нет расчетного элемента в файле volid.dat или отсутствует сам файл volid.dat)");
            }

            #endregion

            #region CANENT.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathCanent, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathCanent, false, Encoding.Default))
                    {

                        #region Инициализируем массивы

                        List<string> JCAN = new List<string>();
                        List<string> CORE_JRCTIP = new List<string>();
                        List<string> GENERAL_CORE = new List<string>();

                        List<string> FA_CORE = new List<string>();
                        HashSet<string> CountOfType = new HashSet<string>(); // Записал все типы ТВС
                        List<string> Type = new List<string>();
                        List<string> UNHEAT_CORE = new List<string>();
                        List<string> CORE_KSIMJ = new List<string>();
                        List<string> CORE_GENGEOM = new List<string>();
                        List<string> CORE_GEOM = new List<string>();
                        List<string> FUELROD_CORE = new List<string>();
                        List<string> CORE_RT = new List<string>();
                        List<string> CORE_GAT_DELST = new List<string>();
                        List<string> FUELROD_CORE_COUNT = new List<string>();
                        List<string> FUELROD_CORE_POGR = new List<string>();
                        List<string> CORE_JCTTAB = new List<string>(); // Таблица теплоемкости твэлов
                        List<string> CORE_JCTTAB_COUNT = new List<string>(); // Размерность таблицы теплоемкости твэлов
                        List<string> CORE_JLTTAB = new List<string>(); // Таблица теплопроводности твэлов
                        List<string> CORE_JLTTAB_COUNT = new List<string>(); // Размерность таблицы  теплопроводности твэлов
                        List<string> CORE_JCOTAB = new List<string>(); // Таблица зависимости теплоемкости оболочки от температуры
                        List<string> CORE_JCOTAB_COUNT = new List<string>(); // Размерность таблицы зависимости теплоемкости оболочки от температуры
                        List<string> CORE_JLOTAB = new List<string>(); // Таблица зависимости теплопроводности оболочки от температуры
                        List<string> CORE_JLOTAB_COUNT = new List<string>(); // Размерность таблицы зависимости теплопроводности оболочки от температуры
                        List<string> CORE_JLGTAB = new List<string>(); // Таблица зависимости теплопроводности  газового зазора от температуры
                        List<string> CORE_JLGTAB_COUNT = new List<string>(); // Размерность таблицы зависимости теплопроводности  газового зазора от температуры
                        List<string> CORE_JD0ZAZ = new List<string>(); // Толщина, длина и коэфф. термич. расширения
                        List<string> CORE_JGTVL = new List<string>(); // Расчет давления газов под оболочкой
                        List<string> CORE_JGTVL_1 = new List<string>();
                        List<string> STRMAT_FA_CORE = new List<string>(); // Свойства конструкционных материалов активной части ТВС
                        List<string> STRMAT_UNHEAT_CORE = new List<string>(); // Свойства конструкционных материалов необогреваемой части ТВС
                        List<string> CORE_CROSS_TFT = new List<string>(); // Количество расчетных узлов с обвязкой и Количество теплотехнических каналов
                        List<string> CORE_KSI = new List<string>(); // Расход теплоносителя на входе ТВС
                        List<string> CORE_KV1 = new List<string>(); // Начальное распределение энерговыделений на входе в активную зону ТВС
                        List<string> CORE_KV2 = new List<string>(); // Начальное распределение энерговыделений в активной зоне

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfCanent = new List<string>(); // Массив строк с файла 
                        string LineOfCanent;
                        while ((LineOfCanent = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfCanent.StartsWith("C") && !LineOfCanent.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfCanent) && !LineOfCanent.StartsWith("!"))
                            {
                                ArrayOfCanent.Add(LineOfCanent.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfCanent.Count; i++)
                        {

                            #region Обрабатываем все строки

                            #region Считываем количество конструктивных ТВС в активной зоне

                            for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                            {
                                string[] Str1 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str1, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str1.Length; k++)
                                {
                                    JCAN.Add(Str1[k]);
                                }
                                i++;
                                if (JCAN.Count == int.Parse(REAC_PARAM[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Считываем тип ТВС

                            for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                            {
                                string[] Str2 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str2, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str2.Length; k++)
                                {
                                    CORE_JRCTIP.Add(Str2[k]);
                                    if (CountOfType.Contains(Str2[k]) == false)
                                    {
                                        CountOfType.Add(Str2[k]);
                                        Type.Add(Str2[k]);
                                    }

                                }
                                i++;
                                if (CORE_JRCTIP.Count == int.Parse(REAC_PARAM[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Основные параметры 

                            string[] Str3 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfCanent, ref Str3, i, formatter); // Проверяем на *
                            for (int k = 0; k < 5; k++)
                            {
                                GENERAL_CORE.Add(Str3[k]);
                            }
                            i++;

                            #endregion

                            #region Считываем ограничения модели и методики

                            for (int j = 0; j < 11; j++)
                            {
                                string[] Str4 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str4, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str4.Length; k++)
                                {
                                    MODLIMIT_CORE.Add(Str4[k]);
                                }
                                i++;
                                if (MODLIMIT_CORE.Count == 11)
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Общие свойства для активной части ТВС

                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                string[] Str5 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str5, i, formatter); // Проверяем на *
                                for (int k = 0; k < 11; k++)
                                {
                                    FA_CORE.Add(Str5[k]);
                                }
                                i++;
                            }

                            #endregion

                            #region Общие свойства для необогреваемого участка на выходе ТВС

                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                string[] Str6 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str6, i, formatter); // Проверяем на *
                                for (int k = 0; k < 7; k++)
                                {
                                    UNHEAT_CORE.Add(Str6[k]);
                                }
                                i++;
                            }

                            #endregion

                            #region Коэффициент местных гидравлических сопротивлений на входе в ТВС

                            for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                            {
                                string[] Str7 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str7, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str7.Length; k++)
                                {
                                    CORE_KSIMJ.Add(Str7[k]);
                                }
                                i++;
                                if (CORE_KSIMJ.Count == int.Parse(REAC_PARAM[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Общие геометрические характеристики

                            for (int j = 0; j < 5000; j++)
                            {
                                string[] Str8 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str8, i, formatter); // Проверяем на *
                                for (int k = 0; k < 3; k++)
                                {
                                    CORE_GENGEOM.Add(Str8[k]);
                                }
                                i++;
                                Str8 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (Str8.Length != 3)
                                {
                                    break;
                                }
                            }


                            #endregion

                            #region Геометрические характеристики для ТВС различных типов

                            for (int j = 0; j < 5000; j++)
                            {
                                string[] Str9 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str9, i, formatter); // Проверяем на *
                                for (int k = 0; k < 6; k++)
                                {
                                    CORE_GEOM.Add(Str9[k]);
                                }
                                i++;
                                Str9 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (Str9.Length != 6)
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Количество и пограешность твэлов

                            string[] Str100 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Радиусы
                            ReadStar(ref ArrayOfCanent, ref Str100, i, formatter); // Проверяем на *
                            for (int k = 0; k < CountOfType.Count - 1; k++)
                            {
                                FUELROD_CORE_COUNT.Add(Str100[k]); // Количество
                            }
                            i++;

                            FUELROD_CORE_POGR.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Погрешность
                            i = i + 2; ;

                            #endregion

                            for (int g = 0; g < CountOfType.Count - 1; g++)
                            {
                                #region Свойства твэлов

                                FUELROD_CORE.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Тип
                                i++;

                                string[] Str10 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Радиусы
                                ReadStar(ref ArrayOfCanent, ref Str10, i, formatter); // Проверяем на *
                                for (int k = 0; k < 4; k++)
                                {
                                    CORE_RT.Add(Str10[k]);
                                }
                                i++;

                                string[] Str11 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Радиусы
                                ReadStar(ref ArrayOfCanent, ref Str11, i, formatter); // Проверяем на *
                                for (int k = 0; k < 3; k++)
                                {
                                    CORE_GAT_DELST.Add(Str11[k]);
                                }
                                i++;

                                // Остановился на таблице

                                #endregion

                                #region Таблица зависимости теплоемкости топлива от температуры

                                CORE_JCTTAB_COUNT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Размерность
                                i++;

                                for (int j = 0; j < 2 * int.Parse(CORE_JCTTAB_COUNT[0]); j++)
                                {
                                    string[] Str12 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfCanent, ref Str12, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str12.Length; k++)
                                    {
                                        CORE_JCTTAB.Add(Str12[k]);
                                    }
                                    i++;
                                    if (CORE_JCTTAB.Count >= (2 * int.Parse(CORE_JCTTAB_COUNT[0])))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region Таблица зависимости теплопроводности топлива от температуры

                                CORE_JLTTAB_COUNT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Размерность
                                i++;

                                for (int j = 0; j < int.Parse(CORE_JLTTAB_COUNT[0]) * 2; j++)
                                {
                                    string[] Str13 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfCanent, ref Str13, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str13.Length; k++)
                                    {
                                        CORE_JLTTAB.Add(Str13[k]);
                                    }
                                    i++;
                                    if (CORE_JLTTAB.Count >= (2 * int.Parse(CORE_JLTTAB_COUNT[0])))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region Таблица зависимости теплоемкости оболочки от температуры

                                CORE_JCOTAB_COUNT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Размерность
                                i++;

                                for (int j = 0; j < int.Parse(CORE_JCOTAB_COUNT[0]) * 2; j++)
                                {
                                    string[] Str14 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfCanent, ref Str14, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str14.Length; k++)
                                    {
                                        CORE_JCOTAB.Add(Str14[k]);
                                    }
                                    i++;
                                    if (CORE_JCOTAB.Count >= (int.Parse(CORE_JCOTAB_COUNT[0]) * 2))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region Таблица зависимости теплопроводности оболочки от температуры

                                CORE_JLOTAB_COUNT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Размерность
                                i++;

                                for (int j = 0; j < int.Parse(CORE_JLOTAB_COUNT[0]) * 2; j++)
                                {
                                    string[] Str15 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfCanent, ref Str15, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str15.Length; k++)
                                    {
                                        CORE_JLOTAB.Add(Str15[k]);
                                    }
                                    i++;
                                    if (CORE_JLOTAB.Count >= (int.Parse(CORE_JLOTAB_COUNT[0]) * 2))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region Таблица зависимости теплопроводности  газового зазора от температуры

                                CORE_JLGTAB_COUNT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]); // Размерность
                                i++;

                                for (int j = 0; j < int.Parse(CORE_JLGTAB_COUNT[0]) * 2; j++)
                                {
                                    string[] Str16 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfCanent, ref Str16, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str16.Length; k++)
                                    {
                                        CORE_JLGTAB.Add(Str16[k]);
                                    }
                                    i++;
                                    if (CORE_JLGTAB.Count >= (int.Parse(CORE_JLGTAB_COUNT[0]) * 2))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region Толщина, длина и коэфф. термич. расширения

                                string[] Str17 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str17, i, formatter); // Проверяем на *
                                for (int k = 0; k < 3; k++)
                                {
                                    CORE_JD0ZAZ.Add(Str17[k]);
                                }
                                i++;

                                #endregion

                                #region Расчет давления газов под оболочкой

                                CORE_JGTVL.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                i++;

                                #endregion

                                #region Если CORE_JGTVL(J)=1 вводится:

                                if (int.Parse(CORE_JGTVL[0]) == 1)
                                {
                                    string[] Str18 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfCanent, ref Str18, i, formatter); // Проверяем на *
                                    for (int k = 0; k < 5; k++)
                                    {
                                        CORE_JGTVL_1.Add(Str18[k]);
                                    }
                                    i++;
                                }

                                #endregion

                            }

                            #region Начальное распределение энерговыделений на входе в активную зону ТВС

                            for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                            {
                                string[] Str22 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str22, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str22.Length; k++)
                                {
                                    CORE_KV1.Add(Str22[k]);
                                }
                                i++;
                                if (CORE_KV1.Count >= int.Parse(REAC_PARAM[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Начальное распределение энерговыделений в активной зоне

                            for (int j = 0; j < int.Parse(REAC_PARAM[0]) * int.Parse(REAC_PARAM[1]); j++)
                            {
                                string[] Str26 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str26, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str26.Length; k++)
                                {
                                    CORE_KV2.Add(Str26[k]);
                                }
                                i++;
                                if (CORE_KV2.Count >= int.Parse(REAC_PARAM[0]) * int.Parse(REAC_PARAM[1]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Свойства конструкционных материалов активной части ТВС 

                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                string[] Str18 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str18, i, formatter); // Проверяем на *
                                for (int k = 0; k < 4; k++)
                                {
                                    STRMAT_FA_CORE.Add(Str18[k]);
                                }
                                i++;
                            }

                            #endregion

                            #region Свойства конструкционных материалов необогреваемой части ТВС

                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                string[] Str19 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str19, i, formatter); // Проверяем на *
                                for (int k = 0; k < 4; k++)
                                {
                                    STRMAT_UNHEAT_CORE.Add(Str19[k]);
                                }
                                i++;
                            }

                            #endregion

                            #region Расход теплоносителя на входе ТВС

                            for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                            {
                                string[] Str20 = ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfCanent, ref Str20, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str20.Length; k++)
                                {
                                    CORE_KSI.Add(Str20[k]);
                                }
                                i++;
                                if (CORE_KSI.Count >= int.Parse(REAC_PARAM[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Последние 2 строки 

                            CORE_CROSS_TFT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            CORE_CROSS_TFT.Add(ArrayOfCanent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);

                            #endregion

                            i++;

                            #endregion

                            #region Запись в файл

                            sw.WriteLine("<CORE_DATA Comment=\"Данные для расчета теплогидравлики активной зоны\">");
                            sw.WriteLine(" <GENERAL_CORE Comment=\"Общие\">");
                            sw.WriteLine("  <CORE_DISCR Value=\"Активная зона\"/>");
                            sw.WriteLine("  <CORE_NNOM Value=\"{0}\" Comment=\"Номинальная мощность активной зоны, кВт\"/>", GENERAL_CORE[0]);
                            sw.WriteLine("  <CORE_ALFC Value=\"{0}\" Comment=\"Доля тепла, выделяющегося непосредственно в теплоносителе ТВС, отн.ед.\"/>", GENERAL_CORE[1]);
                            sw.WriteLine("  <CORE_ALFB Value=\"{0}\" Comment=\"Доля тепла, выделяющегося непосредственно в теплоносителе байпаса, отн.ед.\"/>", GENERAL_CORE[2]);
                            sw.WriteLine("  <CORE_P Value=\"{0}\" Comment=\"Начальное значение давления на выходе расчетных каналов, МПа\"/>", GENERAL_CORE[3]);
                            sw.WriteLine("  <CORE_EPSMK Value=\"{0}\" Comment=\"Погрешность вычисления энтальпии байпаса, кДж / кг\"/>", MODLIMIT_CORE[0]);
                            sw.WriteLine("  <CORE_JJRC Value=\"{0}\" Comment=\"Суммарное количство конструктивных ТВС в активной зоне\"/>  ", GENERAL_CORE[4]);
                            sw.WriteLine(" </GENERAL_CORE>");
                            sw.WriteLine(" <SEPARATE_CORE Comment=\"Индивидуальные свойства ТВС\">");
                            sw.WriteLine("  <JCAN Value=\"{0}\" Comment=\"Количство расчетных групп ТВС в активной зоне, включая байпас\">", REAC_PARAM[0]);
                            for (int j = 0; j < JCAN.Count; j++)
                            {
                                sw.WriteLine("   <JCAN_N Value=\"{0}\" Comment=\"Расчетная группа ТВС в активной зоне №{0}\">", j + 1);
                                sw.WriteLine("    <CORE_JRC Value=\"{0}\" Comment=\"Количество конструктивных ТВС в расчетной группе ТВС №{1}\"/>", JCAN[j], j + 1);
                                sw.WriteLine("    <CORE_JRCTIP Value=\"{0}\" Comment=\"Тип геометрии конструктивных ТВС в расчетной группе ТВС №{1}\"/>", CORE_JRCTIP[j], (j + 1));
                                sw.WriteLine("    <CORE_KSIMJ Value=\"{0}\" Comment=\"Коэффициент местных гидравлических сопротивлений на входе в ТВС №{1}\"/>", CORE_KSIMJ[j], (j + 1));
                                sw.WriteLine("    <CORE_KSI Value=\"{0}\" Comment=\"Расход теплоносителя на входе ТВС №{1}\"/>", CORE_KSI[j], (j + 1));
                                sw.WriteLine("    <CORE_KV1 Value=\"{0}\" Comment=\"Начальное распределение энерговыделений на входе в активную зону ТВС №{1}\"/>", CORE_KV1[j], (j + 1));
                                sw.WriteLine("    <JK1 Value=\"{0}\" Comment=\"Число расчётных слоев по высоте в активной зоне\">", REAC_PARAM[1], (j + 1));
                                if (REAC_PARAM[7] == "0")
                                {
                                    for (int k = 0; k < int.Parse(REAC_PARAM[1]); k++)
                                    {
                                        sw.WriteLine("     <CORE_KV2 Value=\"{0}\" Comment=\"Начальное распределение энерговыделений в активной зоне(Слой №{1})\"/>", CORE_KV2[j * int.Parse(REAC_PARAM[1]) + k], (k + 1));
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < int.Parse(REAC_PARAM[1]); k++)
                                    {
                                        sw.WriteLine("     <CORE_KV2 Value=\"{0}\" Comment=\"Начальное распределение энерговыделений в активной зоне(Слой №{1})\"/>", "1", (k + 1));
                                    }
                                }
                                sw.WriteLine("    </JK1>");
                                sw.WriteLine("   </JCAN_N>", j + 1);
                            }
                            sw.WriteLine("  </JCAN>");
                            sw.WriteLine(" </SEPARATE_CORE>");
                            sw.WriteLine(" <MODLIMIT_CORE Comment=\"Ограничения модели и методики\">");
                            sw.WriteLine("  <CORE_JDGDZ Value=\"{0}\" Comment=\"Учет сжимаемости теплоносителя в уравнениях энергии и неразрывности\"/>", MODLIMIT_CORE[1]);
                            sw.WriteLine("  <CORE_JDPDZ Value=\"{0}\" Comment=\"Учет распределенности давления в реакторе\"/>", MODLIMIT_CORE[3]);
                            sw.WriteLine("  <CORE_JDPDT Value=\"{0}\" Comment=\"Учет dP / dt в уравнении сохранения энергии\"/>", MODLIMIT_CORE[4]);
                            sw.WriteLine("  <CORE_JCPROT Value=\"{0}\" Comment=\"Учет байпаса при расчете\"/>", MODLIMIT_CORE[10]);
                            sw.WriteLine("  <CORE_JNF Value=\"{0}\" Comment=\"Методика расчета энтальпии и расхода\"/>", MODLIMIT_CORE[2]);
                            sw.WriteLine("  <CORE_JPRFI Value=\"{0}\" Comment=\"Методика расчета среднего истинного паросодержания на кипящем участке\"/>", MODLIMIT_CORE[5]);
                            sw.WriteLine("  <CORE_JFZ Value=\"{0}\" Comment=\"!!!УТОЧНИТЬ НАЗВАНИЕ!!!\"/>", MODLIMIT_CORE[6]);
                            sw.WriteLine("  <CORE_QPOPR Value=\"{0}\" Comment=\"!!!УТОЧНИТЬ НАЗВАНИЕ!!!\"/>", MODLIMIT_CORE[7]);
                            sw.WriteLine("  <CORE_EPSX Value=\"{0}\" Comment=\"Ограничение на минимальную величину расходного паросодержания при расчете объемного паросодержания\"/>", MODLIMIT_CORE[8]);
                            sw.WriteLine("  <CORE_JCB Value=\"{0}\" Comment=\"Тип расчетной группы ТВС, находящейся в тепловом взаимодействии с байпасом\"/>", MODLIMIT_CORE[9]);
                            sw.WriteLine(" </MODLIMIT_CORE>");
                            sw.WriteLine(" <FA_CORE Comment=\"Общие свойства для активной части ТВС\">");
                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                sw.WriteLine("  <CORE_FATYPE Value=\"{0}\" Comment=\"Тип №{1} геометрии конструктивных ТВС\">", Type[j], j + 1);
                                sw.WriteLine("   <CORE_PMI Value=\"{0}\"  Comment=\"Периметр теплообмена теплоносителя с конструкционными материалами, м\"/>", FA_CORE[11 * j]);
                                sw.WriteLine("   <CORE_PPROT Value=\"{0}\"  Comment=\"Периметр теплообмена теплоносителя канала с теплоносителем байпаса, м\"/>", FA_CORE[11 * j + 1]);
                                sw.WriteLine("   <CORE_DELMD Value=\"{0}\"  Comment=\"Термическое сопротивление кожухов каналов, м ^ 2 / (кВт * С)\"/>", FA_CORE[11 * j + 2]);
                                sw.WriteLine("   <CORE_POPRB Value=\"{0}\"  Comment=\"Поправочный коэффициент при расчете гидравлических потерь на трение\"/>", FA_CORE[11 * j + 3]);
                                sw.WriteLine("   <CORE_ALFKDH Value=\"{0}\"  Comment=\"Поправочный коэффициент при расчете коэффициента теплоотдачи\"/>", FA_CORE[11 * j + 4]);
                                sw.WriteLine("   <CORE_DTCRIT Value=\"{0}\"  Comment=\"Тепловой диаметр, м\"/>", FA_CORE[11 * j + 5]);
                                sw.WriteLine("   <CORE_JUNEQ Value=\"{0}\"  Comment=\"Модель неравновесности\"/>", FA_CORE[11 * j + 6]);
                                sw.WriteLine("   <CORE_JOMEG Value=\"{0}\"  Comment=\"Замыкающее соотношение для коэффициента проскальзывания\"/>", FA_CORE[11 * j + 7]);
                                sw.WriteLine("   <CORE_JQCRIT Value=\"{0}\"  Comment=\"Замыкающее соотношение для критического теплового потока\"/>", FA_CORE[11 * j + 8]);
                                sw.WriteLine("   <CORE_JACRIT Value=\"{0}\"  Comment=\"Замыкающее соотношение для коэффициента теплоотдачи при кризисе теплообмена\"/>", FA_CORE[11 * j + 9]);
                                sw.WriteLine("   <CORE_JBUNDL Value=\"{0}\"  Comment=\"Тип геометрии при расчете критического теплового потока\"/>", FA_CORE[11 * j + 10]);
                                sw.WriteLine("  </CORE_FATYPE>");
                            }
                            sw.WriteLine(" </FA_CORE>");
                            sw.WriteLine(" <UNHEAT_CORE Comment=\"Общие свойства для необогреваемого участка на выходе ТВС\">");
                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                sw.WriteLine("  <CORE_FATYPE Value=\"{0}\" Comment=\"Тип №{1} геометрии конструктивных ТВС\">", Type[j], j + 1);
                                sw.WriteLine("   <CORE_PMI2 Value=\"{0}\"  Comment=\"Периметр теплообмена теплоносителя с конструкционными материалами, м\"/>", UNHEAT_CORE[7 * j]);
                                sw.WriteLine("   <CORE_PPROT2 Value=\"{0}\"  Comment=\"Периметр теплообмена теплоносителя канала с теплоносителем байпаса, м\"/>", UNHEAT_CORE[7 * j + 1]);
                                sw.WriteLine("   <CORE_DELMD2 Value=\"{0}\"  Comment=\"Термическое сопротивление кожухов каналов, м ^ 2 / (кВт * С)\"/>", UNHEAT_CORE[7 * j + 2]);
                                sw.WriteLine("   <CORE_POPRB2 Value=\"{0}\"  Comment=\"Поправочный коэффициент при расчете гидравлических потерь на трение\"/>", UNHEAT_CORE[7 * j + 3]);
                                sw.WriteLine("   <CORE_ALFKDH2 Value=\"{0}\"  Comment=\"Поправочный коэффициент при расчете коэффициента теплоотдачи\"/>", UNHEAT_CORE[7 * j + 4]);
                                sw.WriteLine("   <CORE_JUNEQ2 Value=\"{0}\"  Comment=\"Модель неравновесности\"/>", UNHEAT_CORE[7 * j + 5]);
                                sw.WriteLine("   <CORE_JOMEG2 Value=\"{0}\"  Comment=\"Замыкающее соотношение для коэффициента проскальзывания\"/>", UNHEAT_CORE[7 * j + 6]);
                                sw.WriteLine("  </CORE_FATYPE>");
                            }
                            sw.WriteLine(" </UNHEAT_CORE>");
                            sw.WriteLine(" <CORE_GENGEOM Comment=\"Общие геометрические характеристики\">");
                            sw.WriteLine("  <CORE_K1 Value=\"{0}\" Comment=\"Количество макроучастков с разной геометрией\"/>", CORE_GENGEOM.Count / 3);
                            for (int j = 0; j < CORE_GENGEOM.Count / 3; j++)
                            {
                                sw.WriteLine("  <CORE_K1_N Value=\"{0}\" Comment=\"Макроучасток №{0}\">", j + 1);
                                sw.WriteLine("   <CORE_H Value=\"{0}\" Comment=\"Длина макроучастка, м\"/>", CORE_GENGEOM[3 * j]);
                                sw.WriteLine("   <CORE_COSC Value=\"{0}\" Comment=\"Косинус угла наклона между положительным направлением расхода и вектором силы тяжести\"/>", CORE_GENGEOM[3 * j + 1]);
                                sw.WriteLine("   <CORE_JV Value=\"{0}\" Comment=\"Число расчетных участков, на которые разбивается макроучасток\"/>", CORE_GENGEOM[3 * j + 2]);
                                sw.WriteLine("  </CORE_K1_N>");
                            }
                            sw.WriteLine(" </CORE_GENGEOM>");
                            sw.WriteLine(" <CORE_GEOM Comment=\"Геометрические характеристики для ТВС различных типов\">");
                            sw.WriteLine("  <CORE_K2 Value=\"{0}\" Comment=\"Количество макроучастков с неизменной геометрией\"/>", CORE_GEOM.Count / 6);
                            for (int j = 0; j < CORE_GEOM.Count / 6; j++)
                            {
                                sw.WriteLine("  <CORE_K2_N Value=\"{0}\" Comment=\"Макроучасток №{0}\">", j + 1);
                                sw.WriteLine("   <CORE_VC Value=\"{0}\" Comment=\"Объем, м ^ 3\"/>", CORE_GEOM[6 * j]);
                                sw.WriteLine("   <CORE_SC Value=\"{0}\" Comment=\"Площадь проходного сечения, м ^ 2\"/>", CORE_GEOM[6 * j + 1]);
                                sw.WriteLine("   <CORE_DC Value=\"{0}\" Comment=\"Гидравлический диаметр, м\"/>", CORE_GEOM[6 * j + 2]);
                                sw.WriteLine("   <CORE_KSIM Value=\"{0}\" Comment=\"Коэффициент местных гидравлических сопротивлений\"/>", CORE_GEOM[6 * j + 3]);
                                sw.WriteLine("   <CORE_SHER Value=\"{0}\" Comment=\"Шероховатость, м\"/>", CORE_GEOM[6 * j + 4]);
                                sw.WriteLine("   <CORE_JV2 Value=\"{0}\" Comment=\"Число расчетных участков, на которые разбивается макроучасток\"/>", CORE_GEOM[6 * j + 5]);
                                sw.WriteLine("  </CORE_K2_N>");
                            }
                            sw.WriteLine(" </CORE_GEOM>");
                            sw.WriteLine(" <FUELROD_CORE Comment=\"Свойства твэл\">");
                            sw.WriteLine("  <CORE_FUELRODTYPE Value=\"{0}\" Comment=\"Количество типов твэл\"/>", CountOfType.Count - 1);
                            if (MODLIMIT_CORE[10] == "0")
                            {
                                for (int k = 0; k < CountOfType.Count; k++)
                                {
                                    sw.WriteLine("  <CORE_FUELRODTYPE_N Value=\"{0}\" Comment=\"Тип твэл №{0}\">", k + 1);
                                    sw.WriteLine("   <FUEL_CORE Comment=\"Свойства топлива твэл\">");
                                    sw.WriteLine("    <CORE_TVEL Value=\"{0}\" Comment=\"Количество твэлов в конструктивной ТВС\"/>", FUELROD_CORE_COUNT[k]);
                                    if (k == 0)
                                    {
                                        sw.WriteLine("    <CORE_EPSTF Value=\"{0}\" Comment=\"Погрешность расчета температуры твэлов в исходном состоянии, К\"/>", FUELROD_CORE_POGR[0]);
                                    }
                                    sw.WriteLine("    <CORE_JVVOD Value=\"{0}\" Comment=\"Тип разбиения на расчетные слои в топливе\"/>", FUELROD_CORE[k]);
                                    sw.WriteLine("    <CORE_RT1 Value=\"{0}\" Comment=\"Радиус центрального отверстия топливной таблетки, м\"/>", CORE_RT[4 * k]);
                                    sw.WriteLine("    <CORE_RT2 Value=\"{0}\" Comment=\"Радиус топливной таблетки, м\"/>", CORE_RT[4 * k + 1]);
                                    sw.WriteLine("    <CORE_GAT Value=\"{0}\" Comment=\"Плотность топлива, кг/м^3\"/>", CORE_GAT_DELST[3 * k]);
                                    sw.WriteLine("    <CORE_DELST Value=\"{0}\" Comment=\"Расстояние между центрами соседних твэлов, м\"/>", CORE_GAT_DELST[3 * k + 2]);
                                    sw.WriteLine("    <CORE_JCTTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплоемкости топлива от температуры\">", CORE_JCTTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]); j < int.Parse(CORE_JCTTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JCTTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_CTTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JCTTAB[j], j + 1 - 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JCTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]); j < 2 * int.Parse(CORE_JCTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_CTTAB Value=\"{0}\" Comment=\"Теплоемкость №{1}, кДж/кг\"/>", CORE_JCTTAB[j], j + 1 - int.Parse(CORE_JCTTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JCTTAB>");
                                    sw.WriteLine("    <CORE_JLTTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплопроводности топлива от температуры\">", CORE_JLTTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]); j < int.Parse(CORE_JLTTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JLTTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_LTTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JLTTAB[j], j + 1 - 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JLTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]); j < 2 * int.Parse(CORE_JLTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_LTTAB Value=\"{0}\" Comment=\"Теплопроводность №{1}, кВт/(м*К)\"/>", CORE_JLTTAB[j], j + 1 - int.Parse(CORE_JLTTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JLTTAB>");
                                    sw.WriteLine("   </FUEL_CORE>");
                                    sw.WriteLine("   <GASGAP_CORE Comment=\"Свойства газового зазора\">");
                                    sw.WriteLine("    <CORE_JGTVL Value=\"{0}\" Comment=\"Расчет давления газов под оболочкой\">", CORE_JGTVL[k]);
                                    if (int.Parse(CORE_JGTVL[k]) == 1)
                                    {
                                        sw.WriteLine("     <CORE_PTVL Value=\"{0}\" Comment=\"Давление газа под оболочкой в холодном состоянии, МПа\"/>", CORE_JGTVL_1[5 * k]);
                                        sw.WriteLine("     <CORE_AMGTVL Value=\"{0}\" Comment=\"Масса газа под оболочкой, кг\"/>", CORE_JGTVL_1[5 * k + 1]);
                                        sw.WriteLine("     <CORE_RGTVL Value=\"{0}\" Comment=\"Газовая постоянная, МДж/(кг*К)\"/>", CORE_JGTVL_1[5 * k + 2]);
                                        sw.WriteLine("     <CORE_VGSTVL Value=\"{0}\" Comment=\"Объем газосборника, м^3\"/>", CORE_JGTVL_1[5 * k + 3]);
                                        sw.WriteLine("     <CORE_FTTVL Value=\"{0}\" Comment=\"Площадь поверхности теплообмена между газосборником и теплоносителем, м^2\"/>", CORE_JGTVL_1[5 * k + 4]);
                                    }
                                    sw.WriteLine("    </CORE_JGTVL>");
                                    sw.WriteLine("    <CORE_D0ZAZ Value=\"{0}\" Comment=\"Толщина газового зазора в холодном состоянии, м\"/>", CORE_JD0ZAZ[3 * k]);
                                    sw.WriteLine("    <CORE_JLGTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплопроводности  газового зазора от температуры\">", CORE_JLGTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]); j < int.Parse(CORE_JLGTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JLGTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_LGTAB_ARG Value=\"{0}\" Comment=\"Температура газа №{1}, К\"/>", CORE_JLGTAB[j], j + 1 - 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JLGTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]); j < 2 * int.Parse(CORE_JLGTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_LGTAB Value=\"{0}\" Comment=\"Теплопроводность №{1}, кВт/(м*К)\"/>", CORE_JLGTAB[j], j + 1 - int.Parse(CORE_JLGTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JLGTAB>");
                                    sw.WriteLine("   </GASGAP_CORE>");
                                    sw.WriteLine("   <FUELCLAD_CORE Comment=\"Свойства оболочки твэл\">");
                                    sw.WriteLine("    <CORE_RIO Value=\"{0}\" Comment=\"Внутренний радиус оболочки, м\"/>", CORE_RT[4 * k + 2]);
                                    sw.WriteLine("    <CORE_ROO Value=\"{0}\" Comment=\"Наружный радиус оболочки, м\"/>", CORE_RT[4 * k + 3]);
                                    sw.WriteLine("    <CORE_GAOB Value=\"{0}\" Comment=\"Плотность оболочки, кг/м^3\"/>", CORE_GAT_DELST[3 * k + 1]);
                                    sw.WriteLine("    <CORE_DGZAZ Value=\"{0}\" Comment=\"Суммарная длина температурных скачков у поверхности оболочки и топлива, м\"/>", CORE_JD0ZAZ[3 * k + 1]);
                                    sw.WriteLine("    <CORE_AOZAZ Value=\"{0}\" Comment=\"Коэффициент термического расширения оболочки, м/К\"/>", CORE_JD0ZAZ[3 * k + 2]);
                                    sw.WriteLine("    <CORE_JCOTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплоемкости оболочки от температуры\">", CORE_JCOTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]); j < int.Parse(CORE_JCOTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JCOTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_COTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JCOTAB[j], j + 1 - 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JCOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]); j < 2 * int.Parse(CORE_JCOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_COTAB Value=\"{0}\" Comment=\"Теплоемкость №{1}, кДж/кг\"/>", CORE_JCOTAB[j], j + 1 - int.Parse(CORE_JCOTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JCOTAB>");
                                    sw.WriteLine("    <CORE_JLOTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплопроводности оболочки от температуры\">", CORE_JLOTAB_COUNT[k]);

                                    for (int j = 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]); j < int.Parse(CORE_JLOTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JLOTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_LOTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JLOTAB[j], j + 1 - 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JLOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]); j < 2 * int.Parse(CORE_JLOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_LOTAB Value=\"{0}\" Comment=\"Теплопроводность №{1}, кВт/(м*К)\"/>", CORE_JLOTAB[j], j + 1 - int.Parse(CORE_JLOTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JLOTAB>");
                                    sw.WriteLine("   </FUELCLAD_CORE>");
                                    sw.WriteLine("  </CORE_FUELRODTYPE_N>");
                                }
                            }
                            else if (MODLIMIT_CORE[10] == "1")
                            {
                                for (int k = 0; k < CountOfType.Count - 1; k++)
                                {
                                    sw.WriteLine("  <CORE_FUELRODTYPE_N Value=\"{0}\" Comment=\"Тип твэл №{0}\">", k + 1);
                                    sw.WriteLine("   <FUEL_CORE Comment=\"Свойства топлива твэл\">");
                                    sw.WriteLine("    <CORE_TVEL Value=\"{0}\" Comment=\"Количество твэлов в конструктивной ТВС\"/>", FUELROD_CORE_COUNT[k]);
                                    if (k == 0)
                                    {
                                        sw.WriteLine("    <CORE_EPSTF Value=\"{0}\" Comment=\"Погрешность расчета температуры твэлов в исходном состоянии, К\"/>", FUELROD_CORE_POGR[0]);
                                    }
                                    sw.WriteLine("    <CORE_JVVOD Value=\"{0}\" Comment=\"Тип разбиения на расчетные слои в топливе\"/>", FUELROD_CORE[k]);
                                    sw.WriteLine("    <CORE_RT1 Value=\"{0}\" Comment=\"Радиус центрального отверстия топливной таблетки, м\"/>", CORE_RT[4 * k]);
                                    sw.WriteLine("    <CORE_RT2 Value=\"{0}\" Comment=\"Радиус топливной таблетки, м\"/>", CORE_RT[4 * k + 1]);
                                    sw.WriteLine("    <CORE_GAT Value=\"{0}\" Comment=\"Плотность топлива, кг/м^3\"/>", CORE_GAT_DELST[3 * k]);
                                    sw.WriteLine("    <CORE_DELST Value=\"{0}\" Comment=\"Расстояние между центрами соседних твэлов, м\"/>", CORE_GAT_DELST[3 * k + 2]);
                                    sw.WriteLine("    <CORE_JCTTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплоемкости топлива от температуры\">", CORE_JCTTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]); j < int.Parse(CORE_JCTTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JCTTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_CTTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JCTTAB[j], j + 1 - 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JCTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]); j < 2 * int.Parse(CORE_JCTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_CTTAB Value=\"{0}\" Comment=\"Теплоемкость №{1}, кДж/кг\"/>", CORE_JCTTAB[j], j + 1 - int.Parse(CORE_JCTTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JCTTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JCTTAB>");
                                    sw.WriteLine("    <CORE_JLTTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплопроводности топлива от температуры\">", CORE_JLTTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]); j < int.Parse(CORE_JLTTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JLTTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_LTTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JLTTAB[j], j + 1 - 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JLTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]); j < 2 * int.Parse(CORE_JLTTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_LTTAB Value=\"{0}\" Comment=\"Теплопроводность №{1}, кВт/(м*К)\"/>", CORE_JLTTAB[j], j + 1 - int.Parse(CORE_JLTTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JLTTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JLTTAB>");
                                    sw.WriteLine("   </FUEL_CORE>");
                                    sw.WriteLine("   <GASGAP_CORE Comment=\"Свойства газового зазора\">");
                                    sw.WriteLine("    <CORE_JGTVL Value=\"{0}\" Comment=\"Расчет давления газов под оболочкой\">", CORE_JGTVL[k]);
                                    if (int.Parse(CORE_JGTVL[k]) == 1)
                                    {
                                        sw.WriteLine("     <CORE_PTVL Value=\"{0}\" Comment=\"Давление газа под оболочкой в холодном состоянии, МПа\"/>", CORE_JGTVL_1[5 * k]);
                                        sw.WriteLine("     <CORE_AMGTVL Value=\"{0}\" Comment=\"Масса газа под оболочкой, кг\"/>", CORE_JGTVL_1[5 * k + 1]);
                                        sw.WriteLine("     <CORE_RGTVL Value=\"{0}\" Comment=\"Газовая постоянная, МДж/(кг*К)\"/>", CORE_JGTVL_1[5 * k + 2]);
                                        sw.WriteLine("     <CORE_VGSTVL Value=\"{0}\" Comment=\"Объем газосборника, м^3\"/>", CORE_JGTVL_1[5 * k + 3]);
                                        sw.WriteLine("     <CORE_FTTVL Value=\"{0}\" Comment=\"Площадь поверхности теплообмена между газосборником и теплоносителем, м^2\"/>", CORE_JGTVL_1[5 * k + 4]);
                                    }
                                    sw.WriteLine("    </CORE_JGTVL>");
                                    sw.WriteLine("    <CORE_D0ZAZ Value=\"{0}\" Comment=\"Толщина газового зазора в холодном состоянии, м\"/>", CORE_JD0ZAZ[3 * k]);
                                    sw.WriteLine("    <CORE_JLGTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплопроводности  газового зазора от температуры\">", CORE_JLGTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]); j < int.Parse(CORE_JLGTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JLGTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_LGTAB_ARG Value=\"{0}\" Comment=\"Температура газа №{1}, К\"/>", CORE_JLGTAB[j], j + 1 - 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JLGTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]); j < 2 * int.Parse(CORE_JLGTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_LGTAB Value=\"{0}\" Comment=\"Теплопроводность №{1}, кВт/(м*К)\"/>", CORE_JLGTAB[j], j + 1 - int.Parse(CORE_JLGTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JLGTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JLGTAB>");
                                    sw.WriteLine("   </GASGAP_CORE>");
                                    sw.WriteLine("   <FUELCLAD_CORE Comment=\"Свойства оболочки твэл\">");
                                    sw.WriteLine("    <CORE_RIO Value=\"{0}\" Comment=\"Внутренний радиус оболочки, м\"/>", CORE_RT[4 * k + 2]);
                                    sw.WriteLine("    <CORE_ROO Value=\"{0}\" Comment=\"Наружный радиус оболочки, м\"/>", CORE_RT[4 * k + 3]);
                                    sw.WriteLine("    <CORE_GAOB Value=\"{0}\" Comment=\"Плотность оболочки, кг/м^3\"/>", CORE_GAT_DELST[3 * k + 1]);
                                    sw.WriteLine("    <CORE_DGZAZ Value=\"{0}\" Comment=\"Суммарная длина температурных скачков у поверхности оболочки и топлива, м\"/>", CORE_JD0ZAZ[3 * k + 1]);
                                    sw.WriteLine("    <CORE_AOZAZ Value=\"{0}\" Comment=\"Коэффициент термического расширения оболочки, м/К\"/>", CORE_JD0ZAZ[3 * k + 2]);
                                    sw.WriteLine("    <CORE_JCOTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплоемкости оболочки от температуры\">", CORE_JCOTAB_COUNT[k]);
                                    for (int j = 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]); j < int.Parse(CORE_JCOTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JCOTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_COTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JCOTAB[j], j + 1 - 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JCOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]); j < 2 * int.Parse(CORE_JCOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_COTAB Value=\"{0}\" Comment=\"Теплоемкость №{1}, кДж/кг\"/>", CORE_JCOTAB[j], j + 1 - int.Parse(CORE_JCOTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JCOTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JCOTAB>");
                                    sw.WriteLine("    <CORE_JLOTAB Value=\"{0}\" Comment=\"Размерность таблицы зависимости теплопроводности оболочки от температуры\">", CORE_JLOTAB_COUNT[k]);

                                    for (int j = 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]); j < int.Parse(CORE_JLOTAB_COUNT[k]) + (2 * k * int.Parse(CORE_JLOTAB_COUNT[k])); j++)
                                    {
                                        sw.WriteLine("     <CORE_LOTAB_ARG Value=\"{0}\" Comment=\"Температура №{1}, К\"/>", CORE_JLOTAB[j], j + 1 - 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]));
                                    }
                                    for (int j = int.Parse(CORE_JLOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]); j < 2 * int.Parse(CORE_JLOTAB_COUNT[k]) + 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]); j++)
                                    {
                                        sw.WriteLine("     <CORE_LOTAB Value=\"{0}\" Comment=\"Теплопроводность №{1}, кВт/(м*К)\"/>", CORE_JLOTAB[j], j + 1 - int.Parse(CORE_JLOTAB_COUNT[k]) - 2 * k * int.Parse(CORE_JLOTAB_COUNT[k]));
                                    }
                                    sw.WriteLine("    </CORE_JLOTAB>");
                                    sw.WriteLine("   </FUELCLAD_CORE>");
                                    sw.WriteLine("  </CORE_FUELRODTYPE_N>");
                                }
                            }
                            sw.WriteLine(" </FUELROD_CORE>");
                            sw.WriteLine(" <STRMAT_FA_CORE Comment=\"Свойства конструкционных материалов активной части ТВС\">");
                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                sw.WriteLine("  <CORE_FATYPE Value=\"{0}\" Comment=\"Тип №{1} геометрии конструктивных ТВС\">", Type[j], j + 1);
                                sw.WriteLine("   <CORE_CMI Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*С)\"/>", STRMAT_FA_CORE[4 * j]);
                                sw.WriteLine("   <CORE_RMI Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_FA_CORE[4 * j + 1]);
                                sw.WriteLine("   <CORE_DELMI Value=\"{0}\" Comment=\"Эквивалентная толщина, м\"/>", STRMAT_FA_CORE[4 * j + 2]);
                                sw.WriteLine("   <CORE_ALMI Value=\"{0}\" Comment=\"Теплопроводность, кВт/(м*С) \"/>", STRMAT_FA_CORE[4 * j + 3]);
                                sw.WriteLine("  </CORE_FATYPE>");
                            }
                            sw.WriteLine(" </STRMAT_FA_CORE>");
                            sw.WriteLine(" <STRMAT_UNHEAT_CORE Comment=\"Свойства конструкционных материалов необогреваемой части ТВС\">");
                            for (int j = 0; j < CountOfType.Count; j++)
                            {
                                sw.WriteLine("  <CORE_FATYPE Value=\"{0}\" Comment=\"Тип №{1} геометрии конструктивных ТВС\">", Type[j], j + 1);
                                sw.WriteLine("   <CORE_CMI2 Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*С)\"/>", STRMAT_UNHEAT_CORE[4 * j]);
                                sw.WriteLine("   <CORE_RMI2 Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_UNHEAT_CORE[4 * j + 1]);
                                sw.WriteLine("   <CORE_DELMI2 Value=\"{0}\" Comment=\"Эквивалентная толщина, м\"/>", STRMAT_UNHEAT_CORE[4 * j + 2]);
                                sw.WriteLine("   <CORE_ALMI2 Value=\"{0}\" Comment=\"Теплопроводность, кВт/(м*С) \"/>", STRMAT_UNHEAT_CORE[4 * j + 3]);
                                sw.WriteLine("  </CORE_FATYPE>");
                            }
                            sw.WriteLine(" </STRMAT_UNHEAT_CORE>");
                            sw.WriteLine(" <CORE_CROSS Comment=\"Свойства  расчетных узлов с обвязкой\">");
                            sw.WriteLine("  <CORE_JCROSS Value=\"{0}\" Comment=\"Количество расчетных узлов с обвязкой\"/>", CORE_CROSS_TFT[0]);
                            sw.WriteLine(" </CORE_CROSS>");
                            sw.WriteLine(" <CORE_TFT Comment=\"Свойства теплотехнических каналов\">");
                            sw.WriteLine("  <CORE_JTFT Value=\"{0}\" Comment=\"Количество теплотехнических каналов\"/>", CORE_CROSS_TFT[1]);
                            sw.WriteLine(" </CORE_TFT>");
                            sw.Write("</CORE_DATA>");

                            #endregion

                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл CANENT.DAT не был найден");
            }

            #endregion

            #region ASUVAL.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathAsuval, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathAsuval, false, Encoding.Default))
                    {

                        string VLV_CNT;
                        List<string> JUN_VLVNAM_Name = new List<string>();
                        List<string> JUN_VLVNAM_Discr = new List<string>();
                        List<string> VLV_HHVAL = new List<string>();

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfAsuval = new List<string>(); // Массив строк с файла 
                        string LineOfAsuval;
                        while ((LineOfAsuval = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfAsuval.StartsWith("C") && !LineOfAsuval.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfAsuval) && !LineOfAsuval.StartsWith("!"))
                            {
                                ArrayOfAsuval.Add(LineOfAsuval.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfAsuval.Count; i++)
                        {

                            #region Записываем количество клапанов

                            VLV_CNT = ArrayOfAsuval[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Записываем имена и описание

                            for (int j = 0; j < int.Parse(VLV_CNT); j++)
                            {
                                string[] Str1 = ArrayOfAsuval[i].Split(new string[] { "/USU/", "!" }, StringSplitOptions.RemoveEmptyEntries);
                                if (Str1.Length == 1)
                                {
                                    JUN_VLVNAM_Name.Add(Str1[0].Trim());
                                    JUN_VLVNAM_Discr.Add("");
                                }
                                else if (Str1.Length == 2)
                                {
                                    JUN_VLVNAM_Name.Add(Str1[0].Trim());
                                    JUN_VLVNAM_Discr.Add(Str1[1].Trim());
                                }
                                i++;
                            }

                            #endregion

                            #region Начальная степень открытия клапана, отн.ед.

                            for (int j = 0; j < int.Parse(VLV_CNT); j++)
                            {
                                string[] Str2 = ArrayOfAsuval[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfAsuval, ref Str2, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str2.Length; k++)
                                {
                                    VLV_HHVAL.Add(Str2[k]);
                                }
                                i++;
                                if (VLV_HHVAL.Count >= int.Parse(VLV_CNT))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Запись в файл

                            sw.WriteLine("<VLV_CNT Value=\"{0}\" Comment=\"Количество клапанов\">", VLV_CNT);
                            for (int j = 0; j < int.Parse(VLV_CNT); j++)
                            {
                                sw.WriteLine(" <JUN_VLVNAM Value=\"{0}\" Comment=\"Имя клапана\" Description=\"{1}\">", JUN_VLVNAM_Name[j], JUN_VLVNAM_Discr[j]);
                                sw.WriteLine("  <VLV_HHVAL Value=\"{0}\" Comment=\"Начальная степень открытия клапана, отн.ед.\"/>", VLV_HHVAL[j]);
                                sw.WriteLine(" </JUN_VLVNAM>");
                            }
                            sw.Write("</VLV_CNT>");

                            #endregion

                            break;
                        }



                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл ASUVAL.DAT не был найден");
            }

            #endregion

            #region OTYENT.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathOtyent, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathOtyent, false, Encoding.Default))
                    {

                        #region Инициализируем данные

                        List<string> GENERAL_OTY = new List<string>();
                        List<string> STRMAT_OTY = new List<string>();
                        List<string> GEOM_OTY = new List<string>();
                        List<string> JO_N = new List<string>();

                        List<string> OTY_JMCOUT = new List<string>();

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfOtyent = new List<string>(); // Массив строк с файла 
                        string LineOfOtyent;
                        while ((LineOfOtyent = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfOtyent.StartsWith("C") && !LineOfOtyent.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfOtyent) && !LineOfOtyent.StartsWith("!"))
                            {
                                ArrayOfOtyent.Add(LineOfOtyent.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfOtyent.Count; i++)
                        {

                            i++;

                            #region Считываем каждую строку


                            #region Замыкающее соотношение для коэффициента проскальзывания

                            string[] Str1 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str1, i, formatter); // Проверяем на *
                            GENERAL_OTY.Add(Str1[0]);
                            i++;

                            #endregion

                            #region Периметр теплообмеа теплоносителя с конструкционными материалами

                            string[] Str2 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str2, i, formatter); // Проверяем на *
                            STRMAT_OTY.Add(Str2[0]);
                            i++;

                            #endregion

                            #region Шероховатость и Поправочный коэффициент при расчете гид-равлических потерь на трение

                            string[] Str3 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str3, i, formatter); // Проверяем на *
                            GEOM_OTY.Add(Str3[0]);
                            GENERAL_OTY.Add(Str3[1]);
                            i++;

                            #endregion

                            #region Коэффициент местных гидравлических потерь на входе

                            string[] Str4 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str4, i, formatter); // Проверяем на *
                            GEOM_OTY.Add(Str4[0]);
                            i++;

                            #endregion

                            #region Считываем расчетные слои

                            for (int j = 0; j < int.Parse(REAC_PARAM[3]); j++)
                            {

                                string[] Str5 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfOtyent, ref Str5, i, formatter); // Проверяем на *
                                try
                                {
                                    if (Str5.Length != 3)
                                    {
                                        throw new IndexOutOfRangeException("Файлы main.dat и otyent.dat несовместимы, проверить значение JO в main.dat");
                                    }
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine(e.Message);

                                }
                                for (int k = 0; k < 3; k++)
                                {
                                    JO_N.Add(Str5[k]);
                                }
                                i++;

                            }

                            #endregion

                            #region Проходное сечение

                            string[] Str6 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str6, i, formatter); // Проверяем на *
                            GEOM_OTY.Add(Str6[0]);
                            i++;

                            #endregion

                            #region Гидравлический диаметр

                            string[] Str7 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str7, i, formatter); // Проверяем на *
                            GEOM_OTY.Add(Str7[0]);
                            i++;

                            #endregion

                            #region Свойства конструкционных материалов

                            string[] Str8 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOtyent, ref Str8, i, formatter); // Проверяем на *
                            for (int j = 0; j < 4; j++)
                            {
                                STRMAT_OTY.Add(Str8[j]);
                            }
                            i++;

                            #endregion

                            #region Число камер неперемешивания в выходной ка-мере реактора

                            CountMix = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Массив признаков

                            if (int.Parse(CountMix) > 1)
                            {
                                for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                                {
                                    string[] Str9 = ArrayOfOtyent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfOtyent, ref Str9, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str9.Length; k++)
                                    {
                                        OTY_JMCOUT.Add(Str9[k]);
                                    }
                                    i++;
                                    if (OTY_JMCOUT.Count >= int.Parse(REAC_PARAM[0]))
                                    {
                                        break;
                                    }
                                }
                            }

                            #endregion

                            #endregion

                            #region Записываем в файл

                            sw.WriteLine("<OTY_DATA Comment=\"Данные для теплогидравлического расчета выходной камеры реактора\">");
                            sw.WriteLine(" <GENERAL_OTY Comment=\"Общие\">");
                            sw.WriteLine("  <OTY_JOMEOT Value=\"{0}\" Comment=\"Замыкающее соотношение для коэффициента проскальзывания\"/>", GENERAL_OTY[0]);
                            sw.WriteLine("  <OTY_POPRBO Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете гидравлических потерь на трение\"/>", GENERAL_OTY[1]);
                            sw.WriteLine(" </GENERAL_OTY>");
                            sw.WriteLine(" <GEOM_OTY Comment=\"Геометрические и гидравлические характеристики\">");
                            sw.WriteLine("  <OTY_SOTY Value=\"{0}\" Comment=\"Площадь проходного сечения, м^2\"/>", GEOM_OTY[2]);
                            sw.WriteLine("  <OTY_DOTY Value=\"{0}\" Comment=\"Гидравлический диаметр, м\"/>", GEOM_OTY[3]);
                            sw.WriteLine("  <OTY_AKSI Value=\"{0}\" Comment=\"Коэффициент местных гидравлических потерь на входе\"/>", GEOM_OTY[1]);
                            sw.WriteLine("  <OTY_SHEROT Value=\"{0}\" Comment=\"Шероховатость, м\"/> ", GEOM_OTY[0]);
                            for (int j = 0; j < int.Parse(REAC_PARAM[3]); j++)
                            {
                                sw.WriteLine("  <JO_N Value=\"{0}\" Comment=\"Номер расчетного слоя по высоте в общем подъемном участке\">", j + 1);
                                sw.WriteLine("   <OTY_VO Value=\"{0}\" Comment=\"Объемы, м^3\"/>", JO_N[3 * j + 1]);
                                sw.WriteLine("   <OTY_HO Value=\"{0}\" Comment=\"Высоты, м\"/>", JO_N[3 * j]);
                                sw.WriteLine("   <OTY_KSIMO Value=\"{0}\" Comment=\"Коэффициенты местных гидравлических сопротивлений расчетного участка\"/>", JO_N[3 * j + 2]);
                                sw.WriteLine("  </JO_N>");
                            }
                            sw.WriteLine(" </GEOM_OTY>");
                            sw.WriteLine(" <STRMAT_OTY Comment=\"Свойства конструкционных материалов\">");
                            sw.WriteLine("  <OTY_PMOTY Value=\"{0}\" Comment=\"Периметр теплообмеа теплоносителя с конструкционными материалами, м\"/>", STRMAT_OTY[0]);
                            sw.WriteLine("  <OTY_CMO Value=\"{0}\" Comment=\"Теплоемкость, кДж/(кг*°К)\"/>", STRMAT_OTY[1]);
                            sw.WriteLine("  <OTY_RMO Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_OTY[2]);
                            sw.WriteLine("  <OTY_DELTMO Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_OTY[3]);
                            sw.WriteLine("  <OTY_ALMO Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_OTY[4]);
                            sw.WriteLine(" </STRMAT_OTY>");
                            sw.WriteLine(" <MIX_OTY Comment=\"Свойства камер перемешивания теплоносителя\">");
                            sw.WriteLine("  <OTY_JGROTY Value=\"{0}\" Comment=\"Количество камер частичного перемешивания\"/>", CountMix);
                            if (int.Parse(CountMix) > 1)
                            {
                                sw.WriteLine("  <JCAN Value=\"{0}\" Comment=\"Количество расчетных групп ТВС в активной зоне, исключая байпас\">", REAC_PARAM[0]);
                                if (MODLIMIT_CORE[10] == "1")
                                {
                                    for (int j = 0; j < int.Parse(REAC_PARAM[0]) - 1; j++)
                                    {
                                        sw.WriteLine("   <OTY_JMCOUT Value=\"{0}\" Comment=\"Номер камеры частичного перемешивания, соответствующей расчетной ТВС №{1}\"/>", OTY_JMCOUT[j], j + 1);
                                    }
                                }
                                else if (MODLIMIT_CORE[10] == "0")
                                {
                                    for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                                    {
                                        sw.WriteLine("   <OTY_JMCOUT Value=\"{0}\" Comment=\"Номер камеры частичного перемешивания, соответствующей расчетной ТВС №{1}\"/>", OTY_JMCOUT[j], j + 1);
                                    }
                                }
                                sw.WriteLine("  </JCAN>");
                            }
                            sw.WriteLine(" </MIX_OTY>");
                            sw.WriteLine("</OTY_DATA>");

                            #endregion

                            break;

                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл OTYENT.DAT не был найден");
            }

            #endregion

            #region OOPENT.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathOopent, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathOopent, false, Encoding.Default))
                    {
                        #region Инициализируем массивы


                        List<string> GEOM_OOU = new List<string>(); // V AL S DG AKS AKSIN AKSOUT  SHER ACOS JV
                        List<string> STRMAT_OOU = new List<string>(); // PM CM RM DL ALMD KOS Q TOC
                        List<string> JMCIN = new List<string>();
                        List<string> ALFA0 = new List<string>();
                        string OOU_JMIXOY;

                        List<string> INITDATA_OOU = new List<string>(); //  KCIOOP IOOP PC BOR

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfOopent = new List<string>(); // Массив строк с файла 
                        string LineOfOopent;
                        while ((LineOfOopent = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfOopent.StartsWith("C") && !LineOfOopent.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfOopent) && !LineOfOopent.StartsWith("!"))
                            {
                                ArrayOfOopent.Add(LineOfOopent.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfOopent.Count; i++)
                        {

                            #region Количество камер частичного перемешивания 

                            OOU_JMIXOY = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Расход, энтальпия, давление

                            string[] Str1 = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOopent, ref Str1, i, formatter); // Проверяем на *
                            GENERAL_OOU.Add(Str1[0]);
                            for (int j = 1; j < 4; j++)
                            {
                                INITDATA_OOU.Add(Str1[j]);
                            }
                            i++;

                            #endregion

                            #region Коэффициент местных гидравлических сопро-тивлений на входе

                            GENERAL_OOU.Add(ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            #region Число макроучастков (участки с неизменными геометрическими и гидравлическими характеристиками по длине)

                            GENERAL_OOU.Add(ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            #region Геометрические исходные данные.

                            for (int j = 0; j < int.Parse(GENERAL_OOU[2]); j++)
                            {
                                string[] Str2 = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfOopent, ref Str2, i, formatter); // Проверяем на *
                                for (int k = 0; k < 9; k++)
                                {
                                    GEOM_OOU.Add(Str2[k]);
                                }
                                for (int k = 9; k < 16; k++)
                                {
                                    STRMAT_OOU.Add(Str2[k]);
                                }
                                GEOM_OOU.Add(Str2[16]);
                                i++;
                            }

                            #endregion

                            #region Поправочные коэффициенты

                            string[] Str3 = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfOopent, ref Str3, i, formatter); // Проверяем на *
                            for (int j = 0; j < 3; j++)
                            {
                                GENERAL_OOU.Add(Str3[j]);
                            }
                            i++;

                            #endregion

                            #region Температура окружающей среды, K

                            STRMAT_OOU.Add(ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            if (int.Parse(OOU_JMIXOY) > 1)
                            {

                                #region Количество петель

                                OOU_JL1 = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                i++;

                                #endregion

                                #region Матрица коэффициентов частичного пере-мешивания: последовательно, для каждого сек-тора входной камеры задаются доли расхода из петель теплообмена

                                for (int j = 0; j < int.Parse(OOU_JL1) * int.Parse(OOU_JMIXOY); j++)
                                {
                                    string[] Str4 = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfOopent, ref Str4, i, formatter); // Проверяем на *
                                    for (int k = 0; k < int.Parse(OOU_JMIXOY); k++)
                                    {
                                        ALFA0.Add(Str4[j]);
                                    }
                                    i++;
                                    if (ALFA0.Count >= int.Parse(OOU_JL1) * int.Parse(OOU_JMIXOY))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region Массив признаков, в котором для каждого расчетного канала активной зоны указывается номер соответствующей ему камеры частичного перемешивания входной камеры

                                for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                                {
                                    string[] Str5 = ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfOopent, ref Str5, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str5.Length; k++)
                                    {
                                        JMCIN.Add(Str5[k]);
                                    }
                                    i++;
                                    if (JMCIN.Count >= int.Parse(REAC_PARAM[0]))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                            }

                            #region Признак, учета изменения концентрации бор-ной кислоты в теплоносителе первого контура 

                            GENERAL_OOU.Add(ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            #region Начальная массовая концентрация борной кислоты на входе в активную зону (задается в тех единицах, в которых используется в блоке кинетики)

                            INITDATA_OOU.Add(ArrayOfOopent[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            #region Запись в файл

                            sw.WriteLine("<OOU_DATA Comment=\"Данные для теплогидравлического расчета входной камеры реактора\">");
                            sw.WriteLine(" <GENERAL_OOU Comment=\"Общие\">");
                            sw.WriteLine("  <OOU_JMACRO Value=\"{0}\" Comment=\"Количество гидравлических макроучастков\"/>", GENERAL_OOU[2]);
                            sw.WriteLine("  <OOU_ALFK  Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете коэффициента теплоотдачи\"/>", GENERAL_OOU[3]);
                            sw.WriteLine("  <OOU_POPR  Value=\"{0}\" Comment=\"Поправочный коэффициент при расчете коэффициента трения\"/>", GENERAL_OOU[4]);
                            sw.WriteLine("  <OOU_JALFA  Value=\"{0}\" Comment=\"Методика расчета коэффициента теплоотдачи\"/>", GENERAL_OOU[5]);
                            sw.WriteLine("  <OOU_GOOP  Value=\"{0}\" Comment=\"Нормировочный расход, кг/с\"/>", GENERAL_OOU[0]);
                            sw.WriteLine("  <OOU_KSIEOO  Value=\"{0}\" Comment=\"Коэффициент местных гидравлических сопротивлений на входе\"/>", GENERAL_OOU[1]);
                            sw.WriteLine("  <OOU_TOC Value=\"{0}\" Comment=\"Температура окружающей среды, К\"/>", STRMAT_OOU[7]);
                            sw.WriteLine("  <OOU_JCBOR  Value=\"{0}\" Comment=\"Учет изменения концентрации борной кислоты в теплоносителе первого контура при расчете обратных связей по реактивности\"/>", GENERAL_OOU[6]);
                            sw.WriteLine(" </GENERAL_OOU>");
                            sw.WriteLine(" <GEOM_OOU Comment=\"Геометрические и гидравлические характеристики\">");
                            for (int j = 0; j < int.Parse(GENERAL_OOU[2]); j++)
                            {
                                sw.WriteLine("  <OOU_JMACRO_N Value=\"{0}\" Comment=\"Номер гидравлического макроучастка\">", j + 1);
                                sw.WriteLine("   <OOU_V Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM_OOU[10 * j]);
                                sw.WriteLine("   <OOU_S  Value=\"{0}\" Comment=\"Площадь проходного сечения, м^2\"/>", GEOM_OOU[10 * j + 2]);
                                sw.WriteLine("   <OOU_DG  Value=\"{0}\" Comment=\"Гидравлический диаметр, м\"/>", GEOM_OOU[10 * j + 3]);
                                sw.WriteLine("   <OOU_AL  Value=\"{0}\" Comment=\"Длина, м\"/>", GEOM_OOU[10 * j + 1]);
                                sw.WriteLine("   <OOU_ACOS  Value=\"{0}\" Comment=\"Косинус угла наклона между положительным направлением расхода и вектором силы тяжести\"/>", GEOM_OOU[10 * j + 8]);
                                sw.WriteLine("   <OOU_AKSIN  Value=\"{0}\" Comment=\"Коэффициент местных сопротивлений на входе в макроучасток\"/>", GEOM_OOU[10 * j + 5]);
                                sw.WriteLine("   <OOU_AKS  Value=\"{0}\" Comment=\"Суммарный коэффициент местных сопротивлений макроучастка\"/>", GEOM_OOU[10 * j + 4]);
                                sw.WriteLine("   <OOU_AKSOUT  Value=\"{0}\" Comment=\"Коэффициент местных сопротивлений на выходе макроучастка\"/>", GEOM_OOU[10 * j + 6]);
                                sw.WriteLine("   <OOU_SHER  Value=\"{0}\" Comment=\"Шероховатость, м\"/>", GEOM_OOU[10 * j + 7]);
                                sw.WriteLine("   <OOU_JV  Value=\"{0}\" Comment=\"Количество гидравлических расчетных участков\"/>", GEOM_OOU[10 * j + 9]);
                                sw.WriteLine("  </OOU_JMACRO_N>");
                            }
                            sw.WriteLine(" </GEOM_OOU>");
                            sw.WriteLine(" <STRMAT_OOU Comment=\"Свойства конструкционных материалов\">");
                            for (int j = 0; j < int.Parse(GENERAL_OOU[2]); j++)
                            {
                                sw.WriteLine("  <OOU_JMACRO_N Value=\"{0}\" Comment=\"Номер гидравлического макроучастка\">", j + 1);
                                sw.WriteLine("   <OOU_PM Value=\"{0}\" Comment=\"Периметры теплообмена теплоносителя с конструкционными материалами\"/>", STRMAT_OOU[8 * j]);
                                sw.WriteLine("   <OOU_CM Value=\"{0}\" Comment=\"Удельная теплоемкость, кДж/(кг*°К)\"/>", STRMAT_OOU[8 * j + 1]);
                                sw.WriteLine("   <OOU_RM Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_OOU[8 * j + 2]);
                                sw.WriteLine("   <OOU_DL Value=\"{0}\" Comment=\"Толщина, м\"/>", STRMAT_OOU[8 * j + 3]);
                                sw.WriteLine("   <OOU_ALMD Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_OOU[8 * j + 4]);
                                sw.WriteLine("   <OOU_KOS Value=\"{0}\" Comment=\"Коэффициент теплоотдачи в окружающую среду, кВт/(м^2*°К)\"/>", STRMAT_OOU[8 * j + 5]);
                                sw.WriteLine("   <OOU_Q Value=\"{0}\" Comment=\"Мощность теплоотвода на задаваемом участке, отн.ед.\"/>", STRMAT_OOU[8 * j + 6]);
                                sw.WriteLine("  </OOU_JMACRO_N>");
                            }
                            sw.WriteLine(" </STRMAT_OOU>");
                            sw.WriteLine(" <MIX_OOU Comment=\"Свойства камер перемешивания теплоносителя\">");
                            sw.WriteLine("  <OOU_JMIXOY Value=\"{0}\" Comment=\"Количество камер частичного перемешивания\"/>", OOU_JMIXOY);
                            if (int.Parse(OOU_JMIXOY) != 1)
                            {
                                sw.WriteLine("   <OOU_JL1 Value=\"4\" Comment=\"Количество петель теплообмена\"/>", OOU_JL1);
                                for (int j = 0; j < int.Parse(OOU_JMIXOY); j++)
                                {
                                    sw.WriteLine("   <OOU_JMIXOY_N Value=\"{0}\" Comment=\"Камера частичного перемешивания №{0}\">", j + 1);
                                    for (int k = 0; k < int.Parse(OOU_JL1); k++)
                                    {
                                        sw.WriteLine("    <OOU_ALFA0 Value=\"{0}\" Comment=\"Расход из петли теплообмена №{1}\"/>", ALFA0[k + j * int.Parse(OOU_JL1)], k + 1);
                                    }
                                    sw.WriteLine("   </OOU_JMIXOY_N>");
                                }
                                sw.WriteLine("   <JCAN Value=\"{0}\" Comment=\"Количество расчетных групп ТВС в активной зоне, включая байпас\">", REAC_PARAM[0]);
                                for (int j = 0; j < int.Parse(REAC_PARAM[0]); j++)
                                {
                                    sw.WriteLine("   <OOU_JMCIN Value=\"{0}\" Comment=\"Номер камеры частичного перемешивания, соответствующей расчетной ТВС №{1}\"/>", JMCIN[j], j + 1);
                                }
                                sw.WriteLine("   </JCAN>");
                            }
                            sw.WriteLine(" </MIX_OOU>");
                            sw.WriteLine(" <INITDATA_OOU Comment=\"Начальные условия\">");
                            sw.WriteLine("  <OOU_PC Value=\"{0}\" Comment=\"Давление в реакторе, МПа\"/>", INITDATA_OOU[2]);
                            sw.WriteLine("  <OOU_IOOP Value=\"{0}\" Comment=\"Энтальпия теплоносителя на входе, кДж/кг\"/>", INITDATA_OOU[1]);
                            sw.WriteLine("  <OOU_KCIOOP Value=\"{0}\" Comment=\"Расход на входе, отн.ед.\"/>", INITDATA_OOU[0]);
                            sw.WriteLine("  <OOU_CBBXAZ Value=\"{0}\" Comment=\"Массовая концентрация борной кислоты на входе в активную зону, г/кг\"/>", INITDATA_OOU[3]);
                            sw.WriteLine(" </INITDATA_OOU>");
                            sw.WriteLine("</OOU_DATA>");

                            #endregion

                            break;


                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл OOPENT.DAT не был найден");
            }

            #endregion

            #region UPPER.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathUpper, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathUpper, false, Encoding.Default))
                    {

                        #region Инициализируем массивы

                        List<string> GENERAL_UPP = new List<string>(); // awc bwc aza bza
                        List<string> GEOM_UPP = new List<string>(); // voutr sk3
                        List<string> STRMAT_UPP = new List<string>(); // foutr der1 der2 ckrr gkrr lkrr akkr akokr tor 
                        List<string> MIX_UPP = new List<string>(); // JMIXUP 
                        List<string> INITDATA_UPP = new List<string>(); // PC TM
                        List<string> ALFA0 = new List<string>();
                        List<string> JMHOIN = new List<string>();

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfUpper = new List<string>(); // Массив строк с файла 
                        string LineOfUpper;
                        while ((LineOfUpper = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfUpper.StartsWith("C") && !LineOfUpper.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfUpper) && !LineOfUpper.StartsWith("!"))
                            {
                                ArrayOfUpper.Add(LineOfUpper.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfUpper.Count; i++)
                        {

                            #region Количество расчетных секторов в коллекторе на входе в петли

                            string[] Str1 = ArrayOfUpper[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfUpper, ref Str1, i, formatter); // Проверяем на *
                            MIX_UPP.Add(Str1[0]);
                            i++;

                            #endregion

                            #region Геометр и конструкт харкатеристики

                            string[] Str2 = ArrayOfUpper[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfUpper, ref Str2, i, formatter); // Проверяем на *
                            GEOM_UPP.Add(Str2[0]);
                            for (int j = 1; j < 10; j++)
                            {
                                STRMAT_UPP.Add(Str2[j]);
                            }
                            GEOM_UPP.Add(Str2[10]);
                            for (int j = 11; j < 15; j++)
                            {
                                GENERAL_UPP.Add(Str2[j]);
                            }
                            i++;

                            #endregion

                            #region Начальные условия

                            string[] Str3 = ArrayOfUpper[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfUpper, ref Str3, i, formatter); // Проверяем на *
                            for (int j = 0; j < 2; j++)
                            {
                                INITDATA_UPP.Add(Str3[j]);
                            }
                            i++;

                            #endregion

                            #region при JMIXUP > 1

                            if (int.Parse(MIX_UPP[0]) > 1)
                            {

                                #region ALFA0

                                for (int j = 0; j < int.Parse(CountMix) * int.Parse(MIX_UPP[0]); j++)
                                {
                                    string[] Str4 = ArrayOfUpper[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfUpper, ref Str4, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str4.Length; k++)
                                    {
                                        ALFA0.Add(Str4[k]);
                                    }
                                    i++;
                                    if (ALFA0.Count >= int.Parse(CountMix) * int.Parse(MIX_UPP[0]))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                                #region JMHOIN

                                for (int j = 0; j < int.Parse(OOU_JL1); j++)
                                {
                                    string[] Str5 = ArrayOfUpper[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfUpper, ref Str5, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str5.Length; k++)
                                    {
                                        JMHOIN.Add(Str5[k]);
                                    }
                                    i++;
                                    if (JMHOIN.Count >= int.Parse(OOU_JL1))
                                    {
                                        break;
                                    }
                                }

                                #endregion

                            }

                            #endregion

                            #region Записываем в файл

                            sw.WriteLine("<UPP_DATA Comment=\"Данные для теплогидравлического расчета коллектора реактора на входе в тепли\">");
                            sw.WriteLine(" <GENERAL_UPP Comment=\"Общие\">");
                            sw.WriteLine("  <UPP_AWC Value=\"{0}\" Comment=\"Коэффициенты замыкающего соотношения для расчета скорости всплытия парового пузырька (при свободном члене)\"/>", GENERAL_UPP[0]);
                            sw.WriteLine("  <UPP_BWC Value=\"{0}\" Comment=\"Коэффициенты замыкающего соотношения для расчета скорости всплытия парового пузырька (при линейном члене)\"/>", GENERAL_UPP[1]);
                            sw.WriteLine("  <UPP_AZA Value=\"{0}\" Comment=\"Коэффициенты замыкающего соотношения для расчета захвата пара в петли теплообмена  (при свободном члене)\"/>", GENERAL_UPP[2]);
                            sw.WriteLine("  <UPP_BZA Value=\"{0}\" Comment=\"Коэффициенты замыкающего соотношения для расчета захвата пара в петли теплообмена  (при линейном члене)\"/>", GENERAL_UPP[3]);
                            sw.WriteLine(" </GENERAL_UPP>");
                            sw.WriteLine(" <GEOM_UPP Comment=\"Геометрические и гидравлические характеристики\">");
                            sw.WriteLine("  <UPP_VOUTR Value=\"{0}\" Comment=\"Объем, м^3\"/>", GEOM_UPP[0]);
                            sw.WriteLine("  <UPP_SK3 Value=\"{0}\" Comment=\"Площадь проходного сечения, м^2\"/>", GEOM_UPP[1]);
                            sw.WriteLine(" </GEOM_UPP>");
                            sw.WriteLine(" <STRMAT_UPP Comment=\"Свойства конструкционных материалов\">");
                            sw.WriteLine("  <UPP_FOUTR Value=\"{0}\" Comment=\"Площадь поверхности теплообмена с корпусом реактора, м^2\"/>", STRMAT_UPP[0]);
                            sw.WriteLine("  <UPP_DER1 Value=\"{0}\" Comment=\"Расстояние от внутренней поверхности корпуса до точки, к которой отнесена средняя температура корпуса, м\"/>", STRMAT_UPP[1]);
                            sw.WriteLine("  <UPP_DER2 Value=\"{0}\" Comment=\"Расстояние от внешней поверхности корпуса реактора до точки, к которой отнесена средняя температура корпуса, м\"/>", STRMAT_UPP[2]);
                            sw.WriteLine("  <UPP_CKRR Value=\"{0}\" Comment=\"Теплоемкость, кДж/(кг*°К)\"/>", STRMAT_UPP[3]);
                            sw.WriteLine("  <UPP_GKRR Value=\"{0}\" Comment=\"Плотность, кг/м^3\"/>", STRMAT_UPP[4]);
                            sw.WriteLine("  <UPP_LKRR Value=\"{0}\" Comment=\"Теплопроводность, кДж/(м*°К)\"/>", STRMAT_UPP[5]);
                            sw.WriteLine("  <UPP_AKKR Value=\"{0}\" Comment=\"Коэффициент теплоотдачи от первого контура к корпусу, кВт/(м^2*°К)\"/>", STRMAT_UPP[6]);
                            sw.WriteLine("  <UPP_AKOKR Value=\"{0}\" Comment=\"Мощность теплоотвода на задаваемом участке, кВт/(м^2*°К)\"/>", STRMAT_UPP[7]);
                            sw.WriteLine("  <UPP_TOR Value=\"{0}\" Comment=\"Температура окружающей среды, К\"/>", STRMAT_UPP[8]);
                            sw.WriteLine(" </STRMAT_UPP>");
                            sw.WriteLine(" <MIX_UPP Comment=\"Свойства камер перемешивания теплоносителя\">");
                            sw.WriteLine("  <UPP_JMIXUP Value=\"{0}\" Comment=\"Количество расчетных секторов в коллекторе на входе в петли\">", MIX_UPP[0]);
                            if (int.Parse(MIX_UPP[0]) > 1)
                            {
                                for (int j = 0; j < int.Parse(MIX_UPP[0]); j++)
                                {
                                    sw.WriteLine("  <UPP_JMIXUP_N Value=\"{0}\" Comment=\"Камера частичного перемешивания №{0}\">", j + 1);
                                    for (int k = 0; k < int.Parse(CountMix); k++)
                                    {
                                        sw.WriteLine("   <UPP_ALFA0 Value=\"{0}\" Comment=\"Расход из секторов выходной камеры реактора №{1}\"/>", ALFA0[j * int.Parse(CountMix) + k], j + 1);
                                    }
                                    sw.WriteLine("  </UPP_JMIXUP_N>");
                                }
                            }
                            sw.WriteLine("  </UPP_JMIXUP>");
                            if (int.Parse(MIX_UPP[0]) > 1)
                            {
                                sw.WriteLine("  <OOU_JL1 Value=\"{0}\" Comment=\"Количество петель теплообмена\">", JMHOIN.Count);
                                for (int j = 0; j < JMHOIN.Count; j++)
                                {
                                    sw.WriteLine("   <UPP_JMHOIN Value=\"{0}\" Comment=\"Номер камеры частичного перемешивания, соединенного с расчетной петлей №{1}\"/>", JMHOIN[j], j + 1);
                                }
                                sw.WriteLine("  </OOU_JL1>");
                            }
                            sw.WriteLine(" </MIX_UPP>");
                            sw.WriteLine(" <INITDATA_UPP Comment=\"Начальные условия\">");
                            sw.WriteLine("  <UPP_PC Value=\"{0}\" Comment=\"Давление в реакторе, МПа\"/>", INITDATA_UPP[0]);
                            sw.WriteLine("  <UPP_TM Value=\"{0}\" Comment=\"Температура корпуса, К\"/>", INITDATA_UPP[1]);
                            sw.WriteLine(" </INITDATA_UPP>");
                            sw.Write("</UPP_DATA>");

                            #endregion

                            break;
                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл UPPER.DAT не был найден");
            }

            #endregion

            #region KINET.DAT

            try
            {
                using (StreamReader sr = new StreamReader(ReadPathKinet, Encoding.GetEncoding(1251)))
                {
                    using (StreamWriter sw = new StreamWriter(WritePathKinet, false, Encoding.Default))
                    {

                        #region Инициализируем массивы

                        string FNAME = null;
                        List<string> KIN_JGRKIN = new List<string>();
                        List<string> KIN_JGROST = new List<string>();
                        List<string> KIN_BGAM = new List<string>();
                        List<string> KIN_BLAM = new List<string>();
                        List<string> GENERAL_DATA = new List<string>(); // PNL S0 NN TOST POWFIS 
                        List<string> KIN_JNJOB_COUNT = new List<string>();
                        List<string> KIN_JNJOB = new List<string>();
                        List<string> REACEFF_DATA = new List<string>(); // STEPFT STEPHT STEPHG ALFFT TFT0 DRONE0 DTNOM TAZG TAZX ALFCR 
                        List<string> KIN_JARHT = new List<string>();
                        List<string> KIN_JARHT_COUNT = new List<string>();
                        List<string> JARHT = new List<string>();
                        List<string> JARHT_COUNT = new List<string>();
                        List<string> JARHTM = new List<string>();
                        List<string> JARHTM_COUNT = new List<string>(); // JARHG
                        List<string> JARHG = new List<string>();
                        List<string> JARHG_COUNT = new List<string>();
                        List<string> JARHCB = new List<string>();
                        List<string> JARHCB_COUNT = new List<string>();
                        List<string> JDKT = new List<string>();
                        List<string> JDKT_COUNT = new List<string>();
                        List<string> JDGRUP_COUNT = new List<string>();
                        string JGRUP = null;
                        List<string> KIN_DKGRUP = new List<string>();
                        List<string> KIN_ASUOR = new List<string>();
                        List<string> KIN_ASUHRO0 = new List<string>();
                        List<string> KIN_BE = new List<string>();
                        List<string> KIN_LM = new List<string>();
                        List<string> KIN_NETJOB = new List<string>();
                        List<string> KIN_FKTF = new List<string>();
                        List<string> KIN_ARHT = new List<string>();
                        List<string> KIN_ARHTM = new List<string>();
                        List<string> KIN_ARHG = new List<string>();
                        List<string> KIN_ARHCB = new List<string>();
                        List<string> KIN_DKT = new List<string>();
                        List<string> KIN_DKGRUP_2 = new List<string>();

                        #endregion

                        #region Записали содержимое файла в массив

                        List<string> ArrayOfKinet = new List<string>(); // Массив строк с файла 
                        string LineOfKinet;
                        while ((LineOfKinet = await sr.ReadLineAsync()) != null)
                        {
                            if (!LineOfKinet.StartsWith("C") && !LineOfKinet.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfKinet) && !LineOfKinet.StartsWith("!"))
                            {
                                ArrayOfKinet.Add(LineOfKinet.Trim());
                            }
                        }

                        #endregion

                        for (int i = 0; i < ArrayOfKinet.Count; i++)
                        {

                            #region Считываем имя

                            FNAME = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Количество групп запаздывающих нейтронов и источников остаточных энерговыделений

                            string[] Str1 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str1, i, formatter); // Проверяем на *
                            KIN_JGRKIN.Add(Str1[0]);
                            KIN_JGROST.Add(Str1[1]);
                            i++;

                            #endregion

                            #region Постоянная распада запаздывающих нейт-ронов J-ой группы

                            for (int j = 0; j < int.Parse(KIN_JGRKIN[0]); j++)
                            {
                                string[] Str2 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str2, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str2.Length; k++)
                                {
                                    KIN_LM.Add(Str2[k]);
                                }
                                i++;
                                if (KIN_LM.Count >= int.Parse(KIN_JGRKIN[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Относительная доля запаздывающих нейт-ронов 

                            for (int j = 0; j < int.Parse(KIN_JGRKIN[0]); j++)
                            {
                                string[] Str3 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str3, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str3.Length; k++)
                                {
                                    KIN_BE.Add(Str3[k]);
                                }
                                i++;
                                if (KIN_BE.Count >= int.Parse(KIN_JGRKIN[0]))
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Общие

                            string[] Str4 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str4, i, formatter); // Проверяем на *
                            for (int j = 0; j < 4; j++)
                            {
                                GENERAL_DATA.Add(Str4[j]);
                            }
                            i++;

                            #endregion

                            #region Поправочный множитель при расчете остаточ-ных энерговыделений

                            GENERAL_DATA.Add(ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            #region Размерность таблицы NETJOB

                            KIN_JNJOB_COUNT.Add(ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            i++;

                            #endregion

                            #region таблицы описания предыстории работы реактора

                            for (int j = 0; j < int.Parse(KIN_JNJOB_COUNT[0]) * 2; j++)
                            {
                                string[] Str5 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str5, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str5.Length; k++)
                                {
                                    KIN_JNJOB.Add(Str5[k]);
                                }
                                i++;
                                if (KIN_JNJOB.Count >= int.Parse(KIN_JNJOB_COUNT[0]) * 2)
                                {
                                    break;
                                }
                            }
                            for (int j = int.Parse(KIN_JNJOB_COUNT[0]); j < int.Parse(KIN_JNJOB_COUNT[0]) * 2; j++)
                            {
                                KIN_NETJOB.Add(KIN_JNJOB[j]);
                            }

                            #endregion

                            #region Показатель степени в зависимости весовых коэффициентов

                            string[] Str6 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str6, i, formatter); // Проверяем на *
                            for (int j = 0; j < 3; j++)
                            {
                                REACEFF_DATA.Add(Str6[j]);
                            }
                            i++;

                            #endregion

                            #region Коэффициент реактивности по температуре топлива при температуре TFT0,  $/К Нормировочное значение температуры топлива, К

                            string[] Str7 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str7, i, formatter); // Проверяем на *
                            for (int j = 0; j < 2; j++)
                            {
                                REACEFF_DATA.Add(Str7[j]);
                            }
                            i++;

                            #endregion

                            #region таблицы зависимости реактивности от температуры топлива if (int.Parse(REACEFF_DATA[4]) < 1)

                            if (double.Parse(REACEFF_DATA[4], formatter) < 1)
                            {
                                string[] Str8 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str8, i, formatter); // Проверяем на *
                                KIN_JARHT_COUNT.Add(Str8[0]);
                                if (Str8.Length == 1)
                                {
                                    i++;
                                }
                                for (int j = 0; j < int.Parse(KIN_JARHT_COUNT[0]) * 2; j++)
                                {
                                    string[] Str9 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfKinet, ref Str9, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str9.Length; k++)
                                    {
                                        if (!(Str8.Length != 1 && k == 0 && j == 0))
                                        {
                                            KIN_JARHT.Add(Str9[k]);
                                        }
                                    }
                                    i++;
                                    if (KIN_JARHT.Count >= int.Parse(KIN_JARHT_COUNT[0]) * 2)
                                    {
                                        break;
                                    }
                                }
                                for (int j = int.Parse(KIN_JARHT_COUNT[0]); j < int.Parse(KIN_JARHT_COUNT[0]) * 2; j++)
                                {
                                    KIN_FKTF.Add(KIN_JARHT[j]);
                                }
                            }

                            #endregion

                            #region таблицы зависимости реактивности от температуры теплоносителя

                            string[] Str11 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str11, i, formatter); // Проверяем на *
                            JARHT_COUNT.Add(Str11[0]);
                            if (Str11.Length == 1)
                            {
                                i++;
                            }
                            for (int j = 0; j < int.Parse(JARHT_COUNT[0]) * 2; j++)
                            {
                                string[] Str10 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str10, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str10.Length; k++)
                                {
                                    if (!(Str11.Length != 1 && k == 0 && j == 0))
                                    {
                                        JARHT.Add(Str10[k]);
                                    }

                                }
                                i++;
                                if (JARHT.Count >= int.Parse(JARHT_COUNT[0]) * 2)
                                {
                                    break;
                                }
                            }
                            for (int j = int.Parse(JARHT_COUNT[0]); j < int.Parse(JARHT_COUNT[0]) * 2; j++)
                            {
                                KIN_ARHT.Add(JARHT[j]);
                            }

                            #endregion

                            #region Таблица зависимости реактивности от темпе-ратуры конструкционных материалов

                            string[] Str12 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str12, i, formatter); // Проверяем на *
                            JARHTM_COUNT.Add(Str12[0]);
                            if (Str12.Length == 1)
                            {
                                i++;
                            }
                            for (int j = 0; j < int.Parse(JARHTM_COUNT[0]) * 2; j++)
                            {
                                string[] Str13 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str13, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str13.Length; k++)
                                {
                                    if (!(Str12.Length != 1 && k == 0 && j == 0))
                                    {
                                        JARHTM.Add(Str13[k]);
                                    }

                                }
                                i++;
                                if (JARHTM.Count >= int.Parse(JARHTM_COUNT[0]) * 2)
                                {
                                    break;
                                }
                            }
                            for (int j = int.Parse(JARHTM_COUNT[0]); j < int.Parse(JARHTM_COUNT[0]) * 2; j++)
                            {
                                KIN_ARHTM.Add(JARHTM[j]);
                            }

                            #endregion

                            #region Таблица зависимости реактивности от плотно-сти теплоносителя

                            string[] Str14 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str14, i, formatter); // Проверяем на *
                            JARHG_COUNT.Add(Str14[0]);
                            if (Str14.Length == 1)
                            {
                                i++;
                            }
                            for (int j = 0; j < int.Parse(JARHG_COUNT[0]) * 2; j++)
                            {
                                string[] Str15 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str15, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str15.Length; k++)
                                {
                                    if (!(Str14.Length != 1 && k == 0 && j == 0))
                                    {
                                        JARHG.Add(Str15[k]);
                                    }

                                }
                                i++;
                                if (JARHG.Count >= int.Parse(JARHG_COUNT[0]) * 2)
                                {
                                    break;
                                }
                            }
                            for (int j = int.Parse(JARHG_COUNT[0]); j < int.Parse(JARHG_COUNT[0]) * 2; j++)
                            {
                                KIN_ARHG.Add(JARHG[j]);
                            }

                            #endregion

                            #region Таблица зависимости реактивности от кон-центрации бора

                            try
                            {
                                if (double.Parse(GENERAL_OOU[6], formatter) > 0)
                                {
                                    string[] Str16 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfKinet, ref Str16, i, formatter); // Проверяем на *
                                    JARHCB_COUNT.Add(Str16[0]);
                                    if (Str16.Length == 1)
                                    {
                                        i++;
                                    }
                                    for (int j = 0; j < int.Parse(JARHCB_COUNT[0]) * 2; j++)
                                    {
                                        string[] Str17 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        ReadStar(ref ArrayOfKinet, ref Str17, i, formatter); // Проверяем на *
                                        for (int k = 0; k < Str17.Length; k++)
                                        {
                                            if (!(Str16.Length != 1 && k == 0 && j == 0))
                                            {
                                                JARHCB.Add(Str17[k]);
                                            }

                                        }
                                        i++;
                                        if (JARHCB.Count >= int.Parse(JARHCB_COUNT[0]) * 2)
                                        {
                                            break;
                                        }
                                    }
                                    for (int j = int.Parse(JARHCB_COUNT[0]); j < int.Parse(JARHCB_COUNT[0]) * 2; j++)
                                    {
                                        KIN_ARHCB.Add(JARHCB[j]);
                                    }
                                }
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine("Файл OOPENT.DAT не был найден");
                            }

                            #endregion

                            #region Эффект реактивности от неоднородности параметров теплоносителя и Номинальный подогрев теплоносителя в реакторе

                            string[] Str18 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str18, i, formatter); // Проверяем на *
                            REACEFF_DATA.Add(Str18[0]);
                            REACEFF_DATA.Add(Str18[1]);
                            i++;

                            #endregion

                            #region Реперное значение температуры для контроля эффекта реактивности

                            string[] Str19 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str19, i, formatter); // Проверяем на *
                            REACEFF_DATA.Add(Str19[0]);
                            REACEFF_DATA.Add(Str19[1]);
                            i++;

                            #endregion

                            #region ”Вес” ТВС в суммарном эффекте реактивности по теплоносителю

                            string[] Str20 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str20, i, formatter); // Проверяем на *
                            REACEFF_DATA.Add(Str20[0]);
                            i++;

                            #endregion

                            #region Размерность таблицы для задания возмущения по реактивности от времени

                            string[] Str21 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            ReadStar(ref ArrayOfKinet, ref Str21, i, formatter); // Проверяем на *
                            JDKT_COUNT.Add(Str21[0]);
                            JDGRUP_COUNT.Add(Str21[1]);
                            if (Str21.Length == 2)
                            {
                                i++;
                            }
                            for (int j = 0; j < int.Parse(JDKT_COUNT[0]) * 2; j++)
                            {
                                string[] Str22 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                ReadStar(ref ArrayOfKinet, ref Str22, i, formatter); // Проверяем на *
                                for (int k = 0; k < Str22.Length; k++)
                                {
                                    if (!(Str21.Length != 2 && k == 0 && j == 0))
                                    {
                                        JDKT.Add(Str22[k]);
                                    }

                                }
                                i++;
                                if (JDKT.Count >= int.Parse(JDKT_COUNT[0]) * 2)
                                {
                                    break;
                                }
                            }
                            for (int j = int.Parse(JDKT_COUNT[0]); j < int.Parse(JDKT_COUNT[0]) * 2; j++)
                            {
                                KIN_DKT.Add(JDKT[j]);
                            }

                            #endregion

                            #region Число групп ОР СУЗ

                            JGRUP = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            i++;

                            #endregion

                            #region Зависимости реактивности от положения J-ой группы регулирующих органов

                            for (int g = 0; g < int.Parse(JGRUP); g++)
                            {
                                for (int j = 0; j < int.Parse(JDGRUP_COUNT[0]) * 2; j++)
                                {
                                    string[] Str23 = ArrayOfKinet[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    ReadStar(ref ArrayOfKinet, ref Str23, i, formatter); // Проверяем на *
                                    for (int k = 0; k < Str23.Length; k++)
                                    {
                                        KIN_DKGRUP.Add(Str23[k]);

                                    }
                                    i++;
                                    if (KIN_DKGRUP.Count - g * 2 * int.Parse(JDGRUP_COUNT[0]) >= int.Parse(JDGRUP_COUNT[0]) * 2)
                                    {
                                        break;
                                    }
                                }
                                for (int j = int.Parse(JDGRUP_COUNT[0]) + g * 2 * int.Parse(JDGRUP_COUNT[0]); j < int.Parse(JDGRUP_COUNT[0]) * 2 + g * 2 * int.Parse(JDGRUP_COUNT[0]); j++)
                                {
                                    KIN_DKGRUP_2.Add(KIN_DKGRUP[j]);
                                }
                            }
                            #endregion

                            #region Записываем BGAM и BLAM

                            try
                            {
                                using (StreamReader sr_s = new StreamReader(FNAME, Encoding.GetEncoding(1251)))
                                {

                                    #region Записали содержимое файла в массив

                                    List<string> ArrayOfBG = new List<string>(); // Массив строк с файла 
                                    string LineOfBG;
                                    while ((LineOfBG = await sr_s.ReadLineAsync()) != null)
                                    {
                                        if (!LineOfBG.StartsWith("C") && !LineOfBG.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfBG) && !LineOfBG.StartsWith("!"))
                                        {
                                            ArrayOfBG.Add(LineOfBG.Trim());
                                        }
                                    }

                                    #endregion

                                    for (int nn = 0; nn < ArrayOfBG.Count; nn++)
                                    {

                                        #region KIN_BGAM

                                        for (int j = 0; j < int.Parse(KIN_JGROST[0]); j++)
                                        {
                                            string[] Str30 = ArrayOfBG[nn].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfBG, ref Str30, nn, formatter); // Проверяем на *
                                            for (int k = 0; k < Str30.Length; k++)
                                            {
                                                KIN_BGAM.Add(Str30[k]);

                                            }
                                            nn++;
                                            if (KIN_BGAM.Count >= int.Parse(KIN_JGROST[0]))
                                            {
                                                break;
                                            }
                                        }

                                        #endregion

                                        #region KIN_BLAM

                                        for (int j = 0; j < int.Parse(KIN_JGROST[0]); j++)
                                        {
                                            string[] Str31 = ArrayOfBG[nn].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfBG, ref Str31, nn, formatter); // Проверяем на *
                                            for (int k = 0; k < Str31.Length; k++)
                                            {
                                                KIN_BLAM.Add(Str31[k]);

                                            }
                                            nn++;
                                            if (KIN_BLAM.Count >= int.Parse(KIN_JGROST[0]))
                                            {
                                                break;
                                            }
                                        }

                                        #endregion

                                    }
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("Файл {0} не был найден", FNAME);
                            }

                            #endregion

                            #region Записываем название ОР СУЗ и начальное положение

                            try
                            {
                                using (StreamReader sr_s = new StreamReader("asupo.dat", Encoding.GetEncoding(1251)))
                                {

                                    #region Записали содержимое файла в массив

                                    List<string> ArrayOfAsu = new List<string>(); // Массив строк с файла 
                                    string LineOfAsu;
                                    while ((LineOfAsu = await sr_s.ReadLineAsync()) != null)
                                    {
                                        if (!LineOfAsu.StartsWith("C") && !LineOfAsu.StartsWith("c") && !string.IsNullOrWhiteSpace(LineOfAsu) && !LineOfAsu.StartsWith("!"))
                                        {
                                            ArrayOfAsu.Add(LineOfAsu.Trim());
                                        }
                                    }

                                    #endregion

                                    for (int nn = 0; nn < ArrayOfAsu.Count; nn++)
                                    {

                                        #region Имена

                                        for (int j = 0; j < int.Parse(JGRUP); j++)
                                        {
                                            string[] Str50 = ArrayOfAsu[nn].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfAsu, ref Str50, nn, formatter); // Проверяем на *
                                            KIN_ASUOR.Add(Str50[1]);
                                            nn++;
                                            if (KIN_ASUOR.Count >= int.Parse(JGRUP))
                                            {
                                                break;
                                            }
                                        }

                                        #endregion

                                        #region Начальное положение группы ОР СУЗ

                                        for (int j = 0; j < int.Parse(JGRUP); j++)
                                        {
                                            string[] Str51 = ArrayOfAsu[nn].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            ReadStar(ref ArrayOfAsu, ref Str51, nn, formatter); // Проверяем на *
                                            for (int k = 0; k < Str51.Length; k++)
                                            {
                                                KIN_ASUHRO0.Add(Str51[k]);

                                            }
                                            nn++;
                                            if (KIN_ASUHRO0.Count >= int.Parse(JGRUP))
                                            {
                                                break;
                                            }
                                        }

                                        #endregion

                                    }
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("Файл asupo.dat не был найден");
                            }

                            #endregion

                            #region Запись в файл

                            sw.WriteLine("<KIN_DATA Comment=\"Данные для расчета по точечной модели кинетики\">");

                            #region Общие

                            sw.WriteLine(" <GENERAL_DATA Comment=\"Общие\">");
                            sw.WriteLine("  <KIN_NN Value=\"{0}\" Comment=\"Относительная мощность реактора, отн.ед.\"/>", GENERAL_DATA[2]);
                            sw.WriteLine("  <KIN_S0 Value=\"{0}\" Comment=\"Относительная мощность источника, отн.ед.\"/>", GENERAL_DATA[1]);
                            sw.WriteLine("  <KIN_PNL Value=\"{0}\" Comment=\"Время жизни мгновенных нейтронов, отнесенное к эффективной доле запаздывающих нейтронов,с/$\"/>", GENERAL_DATA[0]);
                            sw.WriteLine("  <KIN_JGRKIN Value=\"{0}\" Comment=\"Количество групп запаздывающих нейтронов\">", KIN_JGRKIN[0]);
                            for (int j = 0; j < int.Parse(KIN_JGRKIN[0]); j++)
                            {
                                sw.WriteLine("   <KIN_LM Value=\"{0}\" Comment=\"Постоянная распада запаздывающих нейтронов группы №{1}\"/>", KIN_LM[j], j + 1);
                                sw.WriteLine("   <KIN_BE Value=\"{0}\" Comment=\"Относительная доля запаздывающих нейтронов группы №{1}\"/>", KIN_BE[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JGRKIN>");
                            sw.WriteLine("  <KIN_JGROST Value=\"{0}\" Comment=\"Количество групп источников остаточных энерговыделений\">", KIN_JGROST[0]);
                            for (int j = 0; j < int.Parse(KIN_JGROST[0]); j++)
                            {
                                sw.WriteLine("   <KIN_BGAM Value=\"{0}\" Comment=\"Выход источников остаточных энерговыделений на один акт деления для группы №{1}\"/>", KIN_BGAM[j], j + 1);
                                sw.WriteLine("   <KIN_BLAM Value=\"{0}\" Comment=\"Постоянная распада источников остаточных энерговыделений группы №{1}\"/>", KIN_BLAM[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JGROST>");
                            sw.WriteLine("  <KIN_POWFIS Value=\"{0}\" Comment=\"Поправочный множитель при расчете остаточных энерговыделений\"/>", GENERAL_DATA[4]);
                            sw.WriteLine("  <KIN_JNJOB Value=\"{0}\" Comment=\"Размерность таблицы описания предыстории работы реактора\">", KIN_JNJOB_COUNT[0]);
                            for (int j = 0; j < int.Parse(KIN_JNJOB_COUNT[0]); j++)
                            {
                                sw.WriteLine("   <KIN_NETJOB_ARG Value=\"{0}\" Comment=\"Период времени №{1} работы реактора на соответствующей мощности, с\"/>", KIN_JNJOB[j], j + 1);
                                sw.WriteLine("   <KIN_NETJOB Value=\"{0}\" Comment=\"Мощность реактора №{1}, отн.ед.\"/>", KIN_NETJOB[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JNJOB>");
                            sw.WriteLine("  <KIN_TOST  Value=\"{0}\" Comment=\"Момент времени, начиная с которого расчет ведется с учетом изменения мощности остаточных энерговыделений, с\"/>", GENERAL_DATA[3]);
                            sw.WriteLine(" </GENERAL_DATA>");

                            #endregion

                            #region Эффекты реактивности

                            sw.WriteLine(" <REACEFF_DATA Comment=\"Эффекты реактивности\">");
                            sw.WriteLine("  <KIN_STEPFT Value=\"{0}\" Comment=\"Показатель степени в зависимости весовых коэффициентов по топливу от энерговыделений\"/>", REACEFF_DATA[0]);
                            sw.WriteLine("  <KIN_STEPHT Value=\"{0}\" Comment=\"Показатель степени в зависимости весовых коэффициентов по температуре теплоносителя от энерговыделений\"/>", REACEFF_DATA[1]);
                            sw.WriteLine("  <KIN_STEPHG Value=\"{0}\" Comment=\"Показатель степени в зависимости весовых коэффициентов по плотности теплоносителя от энерговыделений\"/>", REACEFF_DATA[2]);
                            sw.WriteLine("  <KIN_ALFFT Value=\"{0}\" Comment=\"Коэффициент реактивности по температуре топлива, $/К\"/>", REACEFF_DATA[3]);
                            sw.WriteLine("  <KIN_TFT0 Value=\"{0}\" Comment=\"Нормировочное значение температуры топлива, K\"/>", REACEFF_DATA[4]);
                            if (double.Parse(REACEFF_DATA[4], formatter) < 1)
                            {
                                sw.WriteLine("  <KIN_JFKTF Value=\"{0}\" Comment=\"Размерность таблицы зависимости реактивности от температуры топлива\">", KIN_JARHT_COUNT[0]);
                                for (int j = 0; j < int.Parse(KIN_JARHT_COUNT[0]); j++)
                                {
                                    sw.WriteLine("   <KIN_FKTF_ARG Value=\"{0}\" Comment=\"Температуры топлива №{1}, К\"/>", KIN_JARHT[j], j + 1);
                                    sw.WriteLine("   <KIN_FKTF Value=\"{0}\" Comment=\"Реактивности №{1}, $\"/>", KIN_FKTF[j], j + 1);
                                }
                                sw.WriteLine("  </KIN_JFKTF>");
                            }
                            sw.WriteLine("  <KIN_JARHT Value=\"{0}\" Comment=\"Размерность таблицы зависимости реактивности от температуры теплоносителя\">", JARHT_COUNT[0]);
                            for (int j = 0; j < int.Parse(JARHT_COUNT[0]); j++)
                            {
                                sw.WriteLine("   <KIN_ARHT_ARG Value=\"{0}\" Comment=\"Температуры теплоносителя №{1}, К\"/>", JARHT[j], j + 1);
                                sw.WriteLine("   <KIN_ARHT Value=\"{0}\" Comment=\"Реактивности №{1}, $\"/>", KIN_ARHT[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JARHT>");
                            sw.WriteLine("  <KIN_JARHTM Value=\"{0}\" Comment=\"Размерность таблицы зависимости реактивности от температуры конструкционных материалов\">", JARHTM_COUNT[0]);
                            for (int j = 0; j < int.Parse(JARHTM_COUNT[0]); j++)
                            {
                                sw.WriteLine("   <KIN_ARHTM_ARG Value=\"{0}\" Comment=\"Температуры конструкционных материалов №{1}, К\"/>", JARHTM[j], j + 1);
                                sw.WriteLine("   <KIN_ARHTM Value=\"{0}\" Comment=\"Реактивности №{1}, $\"/>", KIN_ARHTM[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JARHTM>");
                            sw.WriteLine("  <KIN_JARHG Value=\"{0}\" Comment=\"Размерность таблицы зависимости реактивности от плотности теплоносителя\">", JARHG_COUNT[0]);
                            for (int j = 0; j < int.Parse(JARHG_COUNT[0]); j++)
                            {
                                sw.WriteLine("   <KIN_ARHG_ARG Value=\"{0}\" Comment=\"Плотности теплоносителя №{1}, кг/м^3\"/>", JARHG[j], j + 1);
                                sw.WriteLine("   <KIN_ARHG Value=\"{0}\" Comment=\"Реактивности №{1}, $\"/>", KIN_ARHG[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JARHG>");
                            sw.WriteLine("  <KIN_JARHCB Value=\"{0}\" Comment=\"Размерность таблицы зависимости реактивности от концентрации бора\">", JARHCB_COUNT[0]);
                            for (int j = 0; j < int.Parse(JARHCB_COUNT[0]); j++)
                            {
                                sw.WriteLine("   <KIN_ARHCB_ARG Value=\"{0}\" Comment=\"Концентрации бора №{1}, г/кг\"/>", JARHCB[j], j + 1);
                                sw.WriteLine("   <KIN_ARHCB Value=\"{0}\" Comment=\"Реактивности №{1}, $\"/>", KIN_ARHCB[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JARHCB>");
                            sw.WriteLine("  <KIN_JDKT Value=\"{0}\" Comment=\"Размерность таблицы для задания возмущения по реактивности от времени\">", JDKT_COUNT[0]);
                            for (int j = 0; j < int.Parse(JDKT_COUNT[0]); j++)
                            {
                                sw.WriteLine("   <KIN_DKT_ARG Value=\"{0}\" Comment=\"Моменты времени №{1}, с\"/>", JDKT[j], j + 1);
                                sw.WriteLine("   <KIN_DKT Value=\"{0}\" Comment=\"Реактивности №{1}, $\"/>", KIN_DKT[j], j + 1);
                            }
                            sw.WriteLine("  </KIN_JDKT>");
                            sw.WriteLine("  <KIN_DRONE0 Value=\"{0}\" Comment=\"Эффект реактивности от неоднородности параметров теплоносителя, $\"/>", REACEFF_DATA[5]);
                            sw.WriteLine("  <KIN_DTNOM Value=\"{0}\" Comment=\"Номинальный подогрев теплоносителя в реакторе, K\"/>", REACEFF_DATA[6]);
                            sw.WriteLine("  <KIN_ALFCR Value=\"{0}\" Comment=\"'Вес' ТВС в суммарном эффекте реактивности по теплоносителю, отн.ед.\"/>", REACEFF_DATA[9]);
                            sw.WriteLine(" </REACEFF_DATA>");

                            #endregion

                            #region ИД для ОР СУЗ

                            sw.WriteLine(" <CRODS_DATA Comment=\"ИД для ОР СУЗ\">");
                            sw.WriteLine("  <KIN_JGRUP Value=\"10\" Comment=\"Число групп ОР СУЗ\">", JGRUP);
                            for (int j = 0; j < int.Parse(JGRUP); j++)
                            {
                                sw.WriteLine("   <KIN_JGRUP_N Value=\"{0}\" Comment=\"Группа ОР СУЗ №{0}\">", j + 1);
                                sw.WriteLine("    <KIN_ASUOR Value=\"{0}\" Comment=\"Имя группы ОР СУЗ №{1}\"/>", KIN_ASUOR[j], j + 1);
                                sw.WriteLine("    <KIN_ASUHRO0 Value=\"{0}\" Comment=\"Начальное положение группы ОР СУЗ №{1}\"/>", KIN_ASUHRO0[j], j + 1);
                                sw.WriteLine("    <KIN_JDGRUP Value=\"{0}\" Comment=\"Размерности таблиц зависимости реактивности от положения группы ОР СУЗ №{1}\">", JDGRUP_COUNT[0], j + 1);
                                for (int k = 0; k < int.Parse(JDGRUP_COUNT[0]); k++)
                                {
                                    sw.WriteLine("     <KIN_DKGRUP_ARG Value=\"{0}\" Comment=\"Положения группы (слой №{1})\"/>", KIN_DKGRUP[k + int.Parse(JDGRUP_COUNT[0]) * 2 * j], k + 1);
                                    sw.WriteLine("     <KIN_DKGRUP Value=\"{0}\" Comment=\"Реактивности (слой №{1})\"/>", KIN_DKGRUP_2[k + int.Parse(JDGRUP_COUNT[0]) * j], k + 1);

                                }
                                sw.WriteLine("    </KIN_JDGRUP>");
                                sw.WriteLine("   </KIN_JGRUP_N>");
                            }
                            sw.WriteLine("  </KIN_JGRUP>");
                            sw.WriteLine(" </CRODS_DATA>");

                            #endregion

                            sw.WriteLine("</KIN_DATA>");

                            #endregion

                            break;
                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл KINET.DAT не был найден");
            }

            #endregion


            Console.ReadKey(true);
        }
    }
}

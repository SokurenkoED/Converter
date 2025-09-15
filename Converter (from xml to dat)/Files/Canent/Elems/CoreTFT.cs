using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Converter__from_xml_to_dat_.Files.Canent.Elems
{
    class CoreTFT
    {
        public string CORETT_JTFCAN { get; set; }
        public string CORETT_JTFJ { get; set; }

        public List<string> CORETT_JRC = new List<string>();
        public List<string> CORETT_JRCTIP = new List<string>();
        public List<string> CORETT_KSIMJ = new List<string>();
        public List<string> CORETT_KR = new List<string>();
        public List<string> CORETT_JCELL1 = new List<string>();
        public List<string> CORETT_JCELL2 = new List<string>();
        public List<string> CORETT_JCELL3 = new List<string>();
        public List<string> CORETT_JCELL4 = new List<string>();
        public List<string> CORETT_DELT1 = new List<string>();
        public List<string> CORETT_DELT2 = new List<string>();
        public List<string> CORETT_DELT3 = new List<string>();
        public List<string> CORETT_DELT4 = new List<string>();
        public List<string> CORETT_CDELT1 = new List<string>();
        public List<string> CORETT_CDELT2 = new List<string>();
        public List<string> CORETT_CDELT3 = new List<string>();
        public List<string> CORETT_CDELT4 = new List<string>();

        public List<string> CORETT_PMI = new List<string>();
        public List<string> CORETT_PPROT = new List<string>();
        public List<string> CORETT_DELMD = new List<string>();
        public List<string> CORETT_POPRB = new List<string>();
        public List<string> CORETT_ALFKDH = new List<string>();
        public List<string> CORETT_DTCRIT = new List<string>();
        public List<string> CORETT_JUNEQ = new List<string>();
        public List<string> CORETT_JOMEG = new List<string>();
        public List<string> CORETT_JQCRIT = new List<string>();
        public List<string> CORETT_JACRIT = new List<string>();
        public List<string> CORETT_JBUNDL = new List<string>();

        public List<string> CORETT_K2 = new List<string>();
        public List<string> CORETT_VC = new List<string>();
        public List<string> CORETT_SC = new List<string>();
        public List<string> CORETT_DC = new List<string>();
        public List<string> CORETT_KSIM = new List<string>();
        public List<string> CORETT_SHER = new List<string>();
        public List<string> CORETT_JV2 = new List<string>();

        public List<string> CORETT_CMI = new List<string>();
        public List<string> CORETT_RMI = new List<string>();
        public List<string> CORETT_DELMI = new List<string>();
        public List<string> CORETT_ALMI = new List<string>();

        public string CORETT_FSTAND { get; set; }
        public string CORETT_DSTAND { get; set; }
        public List<string> CORETT_TVELCOUNT = new List<string>();
        public List<string> CORETT_JGEOM = new List<string>();

        public void ChangeTTK(int deletedType)
        {
            // Определим индексы ячеек, которые исчезают
            List<int> deleteIndexes = new List<int>();
            List<int> changedIndexes = new List<int>();
            for (int i = 0; i < CORETT_JRCTIP.Count; i++)
            {
                if (CORETT_JRCTIP[i] == deletedType.ToString())
                {
                    deleteIndexes.Add(i);
                }
            }
            // Определим индексы ячеек, для которых меняем геометрию
            int changedIndex = int.Parse(CORETT_JCELL1[deleteIndexes[0]]) - 1;
            int changedType = int.Parse(CORETT_JRCTIP[changedIndex]) - 1;
            for (int i = 0; i < CORETT_JRCTIP.Count; i++)
            {
                if (CORETT_JRCTIP[i] == (changedType + 1).ToString())
                {
                    changedIndexes.Add(i);
                }
            }
            Console.WriteLine($"Меняем геометрию для типа ячеек: {changedType}");


            // Для изменяемых ячеек меняем CORETT_DELT и CORETT_CDELT, если граничит с удаленной
            string newDELT = "0.00076";
            //string newCDELT = "0.00076";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                for (int j = 0; j < CORETT_DELT1.Count; j++)
                {
                    if (int.Parse(CORETT_JCELL1[j]) == deleteIndexes[i] + 1)
                    {
                        CORETT_DELT1[j] = newDELT; // рассчитать DELT
                        //CORETT_CDELT1[j] = "-200000000000"; // расчитать CDELT
                    }

                    if (int.Parse(CORETT_JCELL2[j]) == deleteIndexes[i] + 1)
                    {
                        CORETT_DELT2[j] = newDELT;
                        //CORETT_CDELT2[j] = "-200000000000";
                    }

                    if (int.Parse(CORETT_JCELL3[j]) == deleteIndexes[i] + 1)
                    {
                        CORETT_DELT3[j] = newDELT;
                        //CORETT_CDELT3[j] = "-200000000000";
                    }

                    if (int.Parse(CORETT_JCELL4[j]) == deleteIndexes[i] + 1)
                    {
                        CORETT_DELT4[j] = newDELT;
                        //CORETT_CDELT4[j] = "-200000000000";
                    }
                }
            }


            // Переприсваиваем соседние ячейки JCELL
            foreach (var index in deleteIndexes)
            {
                int firstCellIndex = int.Parse(CORETT_JCELL1[index]) - 1;
                int secondCellIndex = int.Parse(CORETT_JCELL2[index]) - 1;

                // меняем соседнюю ячейку для первой ячейки
                if (CORETT_JCELL1[firstCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL1[firstCellIndex] = (secondCellIndex + 1).ToString();
                }
                else if (CORETT_JCELL2[firstCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL2[firstCellIndex] = (secondCellIndex + 1).ToString();
                }
                else if (CORETT_JCELL3[firstCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL3[firstCellIndex] = (secondCellIndex + 1).ToString();
                }
                else if (CORETT_JCELL4[firstCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL4[firstCellIndex] = (secondCellIndex + 1).ToString();
                }

                // меняем соседнюю ячейку для второй ячейки
                if (CORETT_JCELL1[secondCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL1[secondCellIndex] = (firstCellIndex + 1).ToString();
                }
                else if (CORETT_JCELL2[secondCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL2[secondCellIndex] = (firstCellIndex + 1).ToString();
                }
                else if (CORETT_JCELL3[secondCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL3[secondCellIndex] = (firstCellIndex + 1).ToString();
                }
                else if (CORETT_JCELL4[secondCellIndex] == (index + 1).ToString())
                {
                    CORETT_JCELL4[secondCellIndex] = (firstCellIndex + 1).ToString();
                }
            }

            // Учтем номера соседних ячеек учитывая, что мы удалили 6 ячеек
            for (int i = 0; i < CORETT_JCELL1.Count; i++)
            {
                for (int j = deleteIndexes.Count - 1; j >= 0; j--)
                {
                    if (int.Parse(CORETT_JCELL1[i]) > (deleteIndexes[j] + 1))
                    {
                        CORETT_JCELL1[i] = (int.Parse(CORETT_JCELL1[i]) - 1).ToString();
                    }

                    if (int.Parse(CORETT_JCELL2[i]) > (deleteIndexes[j] + 1))
                    {
                        CORETT_JCELL2[i] = (int.Parse(CORETT_JCELL2[i]) - 1).ToString();
                    }

                    if (int.Parse(CORETT_JCELL3[i]) > (deleteIndexes[j] + 1))
                    {
                        CORETT_JCELL3[i] = (int.Parse(CORETT_JCELL3[i]) - 1).ToString();
                    }

                    if (int.Parse(CORETT_JCELL4[i]) > (deleteIndexes[j] + 1))
                    {
                        CORETT_JCELL4[i] = (int.Parse(CORETT_JCELL4[i]) - 1).ToString();
                    }
                }
            }

            // Сдвинул типы > BadJrctip на 1
            for (int i = 0; i < CORETT_JRCTIP.Count; i++)
            {
                int value = int.Parse(CORETT_JRCTIP[i]);
                if (value > deletedType)
                {
                    CORETT_JRCTIP[i] = (value + 1).ToString();
                }
            }
            Console.WriteLine("Нужно ли нам пересчитывать KR?");


            int changedK2 = int.Parse(CORETT_K2[changedType - 1]);
            int changedStartIndex = 0;
            for (int i = 0; i < changedType - 1; i++)
            {
                changedStartIndex += int.Parse(CORETT_K2[i]);
            }

            for (int i = changedStartIndex; i < changedStartIndex + changedK2 - 2; i++) // 2 это хвостовик
            {
                CORETT_VC[i] = (0.075 * 0.032505 * int.Parse(CORETT_JV2[i])).ToString();
                CORETT_SC[i] = "0.032505";
                CORETT_DC[i] = "0.006604";
                //CORETT_KSIM[i] = "KSIM";
                //CORETT_SHER[i] = "SHER";
                //CORETT_JV2[i] = "JV2";
            }

            // Нужно изменить CORETT_TVELCOUNT
            CORETT_TVELCOUNT[changedType - 1] = "0.41666666663";

            // Удаляем ненужные ячейки
            for (int j = deleteIndexes.Count - 1; j >= 0; j--)
            {
                CORETT_JCELL1.RemoveAt(deleteIndexes[j]);
                CORETT_JCELL2.RemoveAt(deleteIndexes[j]);
                CORETT_JCELL3.RemoveAt(deleteIndexes[j]);
                CORETT_JCELL4.RemoveAt(deleteIndexes[j]);
                CORETT_KR.RemoveAt(deleteIndexes[j]);
                CORETT_KSIMJ.RemoveAt(deleteIndexes[j]);
                CORETT_JRCTIP.RemoveAt(deleteIndexes[j]);
                CORETT_JRC.RemoveAt(deleteIndexes[j]);

                CORETT_CDELT1.RemoveAt(deleteIndexes[j]);
                CORETT_CDELT2.RemoveAt(deleteIndexes[j]);
                CORETT_CDELT3.RemoveAt(deleteIndexes[j]);
                CORETT_CDELT4.RemoveAt(deleteIndexes[j]);

                CORETT_DELT1.RemoveAt(deleteIndexes[j]);
                CORETT_DELT2.RemoveAt(deleteIndexes[j]);
                CORETT_DELT3.RemoveAt(deleteIndexes[j]);
                CORETT_DELT4.RemoveAt(deleteIndexes[j]);
            }

            CORETT_PMI.RemoveAt(deletedType - 2);
            CORETT_PPROT.RemoveAt(deletedType - 2);
            CORETT_DELMD.RemoveAt(deletedType - 2);
            CORETT_POPRB.RemoveAt(deletedType - 2);
            CORETT_ALFKDH.RemoveAt(deletedType - 2);
            CORETT_DTCRIT.RemoveAt(deletedType - 2);
            CORETT_JUNEQ.RemoveAt(deletedType - 2);
            CORETT_JOMEG.RemoveAt(deletedType - 2);
            CORETT_JQCRIT.RemoveAt(deletedType - 2);
            CORETT_JACRIT.RemoveAt(deletedType - 2);
            CORETT_JBUNDL.RemoveAt(deletedType - 2);

            int deleteK2 = int.Parse(CORETT_K2[deletedType - 2]); // -2 потому что -1 это за счет индеккса с 0, -1 за то, что тип передается тигровкский, который больше на 1, чем фактический
            int startIndex = 0;
            for (int i = 0; i < deletedType - 2; i++)
            {
                startIndex += int.Parse(CORETT_K2[i]);
            }
            CORETT_K2.RemoveAt(deletedType - 2);
            CORETT_VC.RemoveRange(startIndex, deleteK2);
            CORETT_SC.RemoveRange(startIndex, deleteK2);
            CORETT_DC.RemoveRange(startIndex, deleteK2);
            CORETT_KSIM.RemoveRange(startIndex, deleteK2);
            CORETT_SHER.RemoveRange(startIndex, deleteK2);
            CORETT_JV2.RemoveRange(startIndex, deleteK2);

            CORETT_CMI.RemoveAt(deletedType - 2);
            CORETT_RMI.RemoveAt(deletedType - 2);
            CORETT_DELMI.RemoveAt(deletedType - 2);
            CORETT_ALMI.RemoveAt(deletedType - 2);

            CORETT_JGEOM.RemoveAt(deletedType - 2);
            CORETT_TVELCOUNT.RemoveAt(deletedType - 2);

            Console.WriteLine("Убедись, что из-за удаленных ячеек не будет проблем с датчиками.");
        }



    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Practicheskaya_3
{
    internal class Program
    {
         public static void Main()
        {

            bool check = true;

            while(check)
            {
                Console.WriteLine("Введите 1 чтобы вывести данные на экран, 2 - заполнить данные и добавить новую запись в конец файла,\n3 - посмотреть одну запись по ID, 4 - удалить запись\n5 - поиск в диапазоне добавления дат, 6 - сортировка, 7 - редактирование\n8 - выход");
                string oneTwo = Console.ReadLine();
                if (oneTwo == "1")
                {
                    Console.WriteLine("\nВывод данных\n");
                    check = false;

                    Repository r = new Repository();
                    r.ShowAllData();
                    
                }
                else if (oneTwo == "2")
                {
                    Console.WriteLine("\nДобавление");
                    check = false;
                    Repository r = new Repository();
                    r.CreateRecord();
                }
                else if (oneTwo == "3")
                {
                    Console.WriteLine("\nПросмотр одной записи\n");
                    check = false;
                    Repository r = new Repository();
                    r.OneRecord();
                }
                else if (oneTwo == "4")
                {
                    Console.WriteLine("\nУдалить запись\n");
                    check = false;
                    Repository r = new Repository();
                    r.DeleteRecord();
                }
                else if (oneTwo == "5")
                {
                    Console.WriteLine("\nДиапазон дат (число.месяц.год, например 01.01.2000)\n");
                    check = false;
                    Repository r = new Repository();
                    r.RecordInDateRange();
                }
                else if (oneTwo == "6")
                {
                    Console.WriteLine("\nСортировка\n");
                    check = false;
                    Repository r = new Repository();
                    r.Sort();
                }
                else if (oneTwo == "7")
                {
                    Console.WriteLine("\nРедактирование\n");
                    check = false;
                    Repository r = new Repository();
                    r.Edit();
                }
                else if (oneTwo == "8")
                {
                    Console.WriteLine("\nВыход\n");
                    check = false;
                }
                else
                {
                    Console.WriteLine("\nНеверное число\n");
                }
            }
            Console.ReadKey();
        }
    }

    public class Worker
    {
        public int ID;
        public DateTime dateTime;
        public string FIO;
        public int age;
        public int height;
        public string dateOfBirth;
        public string placeOfBirth;
        public Worker() { }
        public Worker(int id, DateTime dateTime, string FIO, int age, int height, string dateOfBirth, string placeOfBirth)
        {
            this.ID = id;
            this.dateTime = dateTime;
            this.FIO = FIO;
            this.age = age;
            this.height = height;
            this.dateOfBirth = dateOfBirth;
            this.placeOfBirth = placeOfBirth;
        }
    }

    public class Repository
    {
        private string fileName = "worker.txt";
        string path = Directory.GetCurrentDirectory();
        public void ShowAllData()
        {
            try
            {
                FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs1);
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
            }
            catch
            {
                FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
                StreamWriter sr = new StreamWriter(fs);
                fs.Close();
                Console.WriteLine("\nФайл создан\n");
            }
            Program.Main();
        }
        
        public void OneRecord()
        {
            bool check = true;
            bool check2 = true;
            string[] readText = new string[] { };

            while (check)
            {
                while (check2)
                {
                    try
                    {
                        readText = File.ReadAllLines($@"{path}\{fileName}");
                        check2 = false;
                    }
                    catch
                    {
                        FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
                        StreamWriter sr = new StreamWriter(fs);
                        fs.Close();
                        Console.WriteLine("\nФайл создан\n");
                    }
                }
                Console.WriteLine("\nВведите ID записи\n");
                string id = Console.ReadLine();
                string[] number = new string[readText.Length];
                string[] splitText = new string[6];

                for(int i = 0; i < readText.Length; i++)
                {
                    splitText = readText[i].Split('#');
                    number[i] = splitText[0];
                }
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == id)
                    {
                        check = false;
                        Console.WriteLine($"\n{readText[i]}\n");
                        break;
                    }
                    else if (i == number.Length - 1)
                        Console.WriteLine("\nЗапись не найдена\n");
                }
            }
            Program.Main();
        }

        public void CreateRecord()
        {
            string[] readText = new string[] {};
            try
            {
                readText = File.ReadAllLines($@"{path}\{fileName}");
            }
            catch
            {
                FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Append);
                StreamWriter sr = new StreamWriter(fs1);
                fs1.Close();
                Console.WriteLine("\nФайл создан\n");
            }
            string[] number = new string[readText.Length];
            string[] splitText = new string[6];
            int id = 1;
            try
            {
                for (int i = 0; i < readText.Length; i++)
                {
                    splitText = readText[i].Split('#');
                    number[i] = splitText[0];
                }
                id = Convert.ToInt32(number.Max()) + 1;
            }
            catch
            {
                id = 1;
            }
            Console.WriteLine("\nВведите ФИО\n");
            string fio = Console.ReadLine();
            Console.WriteLine("\nВведите возраст\n");
            bool check = true;
            int age = 0;
            while(check)
            {
                try
                {
                    age = Convert.ToInt32(Console.ReadLine());
                    check = false;
                }
                catch
                {
                    Console.WriteLine("Неправильно набран возраст");
                }
            }
            Console.WriteLine("\nВведите рост\n");
            int height = 0;
            while (!check)
            {
                try
                {
                    height = Convert.ToInt32(Console.ReadLine());
                    check = true;
                }
                catch
                {
                    Console.WriteLine("Неправильно набран рост");
                }
            }
            Console.WriteLine("\nВведите дату рождения(день.месяц.год)\n");
            string dateOfBirth = Console.ReadLine();
            Console.WriteLine("\nВведите место рождения\n");
            string placeOfBirth = Console.ReadLine();

            Worker worker = new Worker(id, DateTime.Now, fio, age, height, dateOfBirth, placeOfBirth);

            FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"{worker.ID}#{worker.dateTime}#{worker.FIO}#{worker.age}#{worker.height}#{worker.dateOfBirth}#{worker.placeOfBirth}");
            sw.Close();

            Console.WriteLine("\nЗапись добавлена\n");

            Program.Main();
        }

        public void DeleteRecord()
        {
            string[] readText = new string[] { };
            try
            {
                readText = File.ReadAllLines($@"{path}\{fileName}");
            }
            catch
            {
                FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Append);
                StreamWriter sr = new StreamWriter(fs1);
                fs1.Close();
                Console.WriteLine("\nФайл создан\n");
            }
            bool check = true;
            while(check)
            {
                try
                {
                    readText = File.ReadAllLines($@"{path}\{fileName}");
                    Console.WriteLine("\nНомер строки которую удалить\n");
                    foreach(string line in readText)
                        Console.WriteLine(line);
                    Console.WriteLine();
                    int a = Convert.ToInt32(Console.ReadLine());
                    string[] text = new string[readText.Length - 1];
                    int b = 0;
                    for (int i = 0; i < readText.Length ; i++)
                    {
                        if (i + 1 == a)
                            continue;
                        else
                            text[b] = readText[i];
                        b++;
                    }
                    b = 0;
                    readText = text;
                    File.Delete($@"{path}\{fileName}");
                    FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    for (int i = 0; i < readText.Length; i++)
                        sw.WriteLine(readText[i]);
                    sw.Close();
                    Console.WriteLine("\nСтрока удалена\n");
                    check = false;
                }
                catch
                {
                    Console.WriteLine("\nТакой строки нет\n");
                }
            }
            Program.Main();
        }

        public void RecordInDateRange()
        {
            string[] readText = new string[] { };
            try
            {
                readText = File.ReadAllLines($@"{path}\{fileName}");
            }
            catch
            {
                FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Append);
                StreamWriter sr = new StreamWriter(fs1);
                fs1.Close();
                Console.WriteLine("\nФайл создан\n");
            }
            Console.WriteLine("От какой даты");
            string date1 = Console.ReadLine();
            Console.WriteLine("До какой даты");
            string date2 = Console.ReadLine();

            string[] date_1 = date1.Split('.');
            string[] date_2 = date2.Split('.');

            int day1 = int.Parse(date_1[0]);
            int month1 = int.Parse(date_1[1]);
            int year1 = Convert.ToInt32(date_1[2]);
            
            int day2 = int.Parse(date_2[0]);
            int month2 = int.Parse(date_2[1]);
            int year2 = Convert.ToInt32(date_2[2]);

            readText = File.ReadAllLines($@"{path}\{fileName}");
            string[] splitText = new string[6];
            string[] splitText1 = new string[10];
            string[] splitText2 = new string[10];
            string[] number = new string[readText.Length];
            string[] number1 = new string[readText.Length];
            string[] day = new string[readText.Length];
            string[] month = new string[readText.Length];
            string[] year = new string[readText.Length];
            for (int i = 0; i < readText.Length; i++)
            {
                splitText = readText[i].Split('#');
                number[i] = splitText[1];
            }
            for (int i = 0; i < readText.Length; i++)
            {
                splitText1 = number[i].Split(' ');
                number1[i] = splitText1[0];
            }
            for (int i = 0; i < readText.Length; i++)
            {
                splitText2 = number1[i].Split('.');
                day[i] = splitText2[0];
                month[i] = splitText2[1];
                year[i] = splitText2[2];
            }
            for (int i = 0; i < readText.Length; i++)
            {
                if (Convert.ToInt32(year[i]) > year1 && Convert.ToInt32(year[i]) < year2)
                {
                    Console.WriteLine(readText[i]);
                }
                else if (Convert.ToInt32(year[i]) >= year1 && Convert.ToInt32(year[i]) <= year2)
                {
                    if (Convert.ToInt32(month[i]) >= month1 && Convert.ToInt32(month[i]) <= month2)
                    {
                        Console.WriteLine(readText[i]);
                    }
                    else if(Convert.ToInt32(month[i]) >= month1 && Convert.ToInt32(year[i]) < year2)
                    {
                        Console.WriteLine(readText[i]);
                    }
                    else if(Convert.ToInt32(month[i]) > month1 && Convert.ToInt32(month[i]) < month2)
                    {
                        if (Convert.ToInt32(day[i]) >= day1 && Convert.ToInt32(day[i]) <= day2)
                        {
                            Console.WriteLine(readText[i]);
                        }
                    }
                }
            }
            Program.Main();
        }

        public void Sort()
        {
            string[] readText = new string[] { };
            try
            {
                readText = File.ReadAllLines($@"{path}\{fileName}");
            }
            catch
            {
                FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Append);
                StreamWriter sr = new StreamWriter(fs1);
                fs1.Close();
                Console.WriteLine("\nФайл создан\n");
            }
            readText = File.ReadAllLines($@"{path}\{fileName}");
            string[] splitText = new string[6];
            string[] number = new string[readText.Length];
            bool check = true;

            while (check)
            {
                Console.WriteLine("Введите 1 чтобы сортировать по ID, 2 - сортировать по возрасту,\n3 - сортировать по росту");
                string oneTwo = Console.ReadLine();
                if (oneTwo == "1")
                {
                    Console.WriteLine("\nСортровка по ID:\n");
                    check = false;

                    for (int i = 0; i < readText.Length; i++)
                    {
                        splitText = readText[i].Split('#');
                        number[i] = splitText[0];
                    }
                    Array.Sort(number, readText);
                        
                    foreach (string i in readText)
                        Console.WriteLine(i);

                    File.Delete($@"{path}\{fileName}");
                    FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (string line in readText)
                        sw.WriteLine(line);
                    sw.Close();
                }
                else if (oneTwo == "2")
                {
                    Console.WriteLine("\nСортровка по возрасту:");
                    check = false;

                    for (int i = 0; i < readText.Length; i++)
                    {
                        splitText = readText[i].Split('#');
                        number[i] = splitText[3];
                    }
                    Array.Sort(number, readText);
                    foreach (string i in readText)
                        Console.WriteLine(i);

                    File.Delete($@"{path}\{fileName}");
                    FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (string line in readText)
                        sw.WriteLine(line);
                    sw.Close();
                }
                else if (oneTwo == "3")
                {
                    Console.WriteLine("\nСортровка по росту:\n");
                    check = false;

                    for (int i = 0; i < readText.Length; i++)
                    {
                        splitText = readText[i].Split('#');
                        number[i] = splitText[4];
                    }
                    Array.Sort(number, readText);
                    foreach (string i in readText)
                        Console.WriteLine(i);

                    File.Delete($@"{path}\{fileName}");
                    FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (string line in readText)
                        sw.WriteLine(line);
                    sw.Close();
                }
                else
                {
                    Console.WriteLine("\nНеверное число\n");
                }
            }
            Console.WriteLine();
            Program.Main();
        }

        public void Edit()
        {
            Console.WriteLine("Номер записи у которой редактировать\n");
            
            int oneFive = 0;
            bool check = true;
            string[] readText = new string[] { };
            try
            {
                readText = File.ReadAllLines($@"{path}\{fileName}");
            }
            catch
            {
                FileStream fs1 = new FileStream($@"{path}\{fileName}", FileMode.Append);
                StreamWriter sr = new StreamWriter(fs1);
                fs1.Close();
                Console.WriteLine("\nФайл создан\n");
            }
            foreach(string i in readText)
                Console.WriteLine(i);
            Console.WriteLine();
            int num = Convert.ToInt32(Console.ReadLine()) - 1;
            while (check)
            {
                Console.WriteLine("Введите 1 чтобы редактировать ФИО, 2 - возраст, 3 - рост, 4 - дату рождения, 5 - место рождения");
                string oneTwo = Console.ReadLine();
                if (oneTwo == "1")
                {
                    Console.WriteLine("\nРедактирование ФИО:\n");
                    check = false;
                    oneFive = 2;
                }
                else if (oneTwo == "2")
                {
                    Console.WriteLine("\nРедактирование возраста:");
                    check = false;
                    oneFive = 3;
                }
                else if (oneTwo == "3")
                {
                    Console.WriteLine("\nРедактирование роста:\n");
                    check = false;
                    oneFive = 4;
                }
                else if (oneTwo == "4")
                {
                    Console.WriteLine("\nРедактирование даты рождения:\n");
                    check = false;
                    oneFive = 5;
                }
                else if (oneTwo == "5")
                {
                    Console.WriteLine("\nРедактирование места рождения:\n");
                    check = false;
                    oneFive = 6;
                }
                else
                {
                    Console.WriteLine("\nНеверное число\n");
                }
            }
            readText = File.ReadAllLines($@"{path}\{fileName}");
            string[] splitText = new string[6];
            string[] number = new string[readText.Length];

            splitText = readText[num].Split('#');

            Console.WriteLine("\nВведите новое значение\n");
            string _new = Console.ReadLine();
            splitText[oneFive] = _new;
            string newRecord = "";
            for (int i = 0; i < splitText.Length; i++)
            {
                newRecord += splitText[i];
                if (i == splitText.Length - 1)
                {
                    continue;
                }
                else
                {
                    newRecord += "#";
                }
            }
            readText[num] = newRecord;

            File.Delete($@"{path}\{fileName}");
            FileStream fs = new FileStream($@"{path}\{fileName}", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            foreach(string line in readText)
                sw.WriteLine(line);
            sw.Close();

            Program.Main();
        }
    }
}

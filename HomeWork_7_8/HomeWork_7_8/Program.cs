﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_7_8
{
    internal class Program
    {
        private static string filePath = "employees.txt";
        private static string choice = "";
        private static char separator = '#';
        private static Repository repository = new Repository();
        
        static void Main(string[] args)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            while (!"3".Equals(choice))
            {
                Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine("Введите 1 — для вывода данных о сотрудниках на экран" +
                " \nВведите 2 — что бы заполнить данные и добавить новую запись в конец файла" +
                "\nВведите 3 - что бы выйти из программы");
                choice = Console.ReadLine();

                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                switch (choice)
                {
                    case "1":
                        ReadEmployeeInfoFile();
                        Console.ReadKey();
                        break;
                    case "2":
                        SaveEmployeeInfoToFile();
                        break;
                    case "3":
                        DeleteWorker();
                        break;
                    case "4":
                        SelectWorkerBetweenDates();
                        break;
                    case "5":
                        Console.WriteLine("До свидания!");
                        break;
                    default:
                        Console.WriteLine("Введено не правильное число, нажмите клавишу Enter и попробуйте еще раз!");
                        Console.ReadKey();
                        break;
                }
            }          

        }

        /// <summary>
        /// Метод проверяет существует ли текстовый файл, если да, то выводит его на экран,
        /// иначе выдает соответсвующее сообщение и возвращает в главное меню.
        /// </summary>
        private static void ReadEmployeeInfoFile()
        {
            if (File.Exists(filePath))
            {
                //using (StreamReader streamReader = new StreamReader(filePath, Encoding.Unicode))
                //{
                //    StringBuilder stringBuilder = new StringBuilder(streamReader.ReadToEnd());
                //    Console.Write(stringBuilder.Replace(separator, ' '));
                //    Console.WriteLine("Нажмите любую кнопку что бы продолжить...");
                //}

                string content = File.ReadAllText(filePath);

                content = content.Replace("#", " ");

                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Файл не существует! Пожалуйста, заполните хотя бы одну запись.");
                Console.WriteLine("Нажмите любую кнопку что бы продолжить...");
            }
        }

        /// <summary>
        /// Метод проверяет существует ли файл. Если нет, то он создает его и записывает первую строку.
        /// Далее уже добавляет новые строки в файл.
        /// </summary>
        private static void SaveEmployeeInfoToFile()
        {
            Console.WriteLine("Введите имя сотрудника:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст сотрудника:");
            int age = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Введите рост сотрудника:");
            int height = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату рождения сотрудника (пример:1992.12.31):");
            string dateOfBirth = Console.ReadLine();

            string[] dateParts = dateOfBirth.Split('.');

            int year = Int32.Parse(dateParts[0]);
            int month = Int32.Parse(dateParts[1]);
            int day = Int32.Parse(dateParts[2]);    

            Console.WriteLine("Введите город проживания сотрудника:");
            string city = Console.ReadLine();

            Worker worker = new Worker
            {
                infoCreated = DateTime.Now,
                FullName = name,
                Age = age,
                Height = height,
                Birthday = new DateTime(year, month, day),
                CityOfBirth = city
            };

            repository.AddWorker(worker);

            Console.WriteLine("Нажмите любу клавишу...");
            Console.ReadLine();
        }
        
        private static void DeleteWorker()
        {
            Console.WriteLine("Введите ID сотрудника для удаления:");
            int id = Int32.Parse(Console.ReadLine());
            repository.DeleteWorker(id);
        }

        private static void SelectWorkerBetweenDates()
        {
            Console.WriteLine("Введите первую дату:");
            DateTime afterDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите вторую дату:");
            DateTime beforeDate = DateTime.Parse(Console.ReadLine());
            
            Worker[] workers = repository.GetWorkersBetweenTwoDates(afterDate, beforeDate);

            foreach (Worker worker in workers)
            {
                Console.WriteLine(worker);
            }

            Console.WriteLine("Нажмите любу клавишу...");
            Console.ReadLine();
        }
    }    
}

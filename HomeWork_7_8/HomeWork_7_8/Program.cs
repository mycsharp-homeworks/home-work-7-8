using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_7_8
{
    internal class Program
    {
        private static Repository repository = new Repository();

        private static string filePath = "employees.txt";
        private static string choice = "";
        private static string sortingChoice = "";
        private static char separator = '#';

        static void Main(string[] args)
        {
            //if (!File.Exists(filePath))
            //{
            //    File.Create(filePath);
            //}

            while (!"6".Equals(choice))
            {
                Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine("Введите 1 — для вывода данных о сотрудниках на экран" +
                "\nВведите 2 — что бы заполнить данные и добавить новую запись в конец файла" +
                "\nВведите 3 - что бы удалить сотрудника" +
                "\nВведите 4 - что бы загрузить записи в определенном диапазоне дат" +
                "\nВведите 5 - что бы отсортировать сотрудников по определенным полям" +
                "\nВведите 6 - что бы выйти из программы");
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
                        Console.WriteLine("Как вы хотите отсортировать работников? Введите цифру:" +
                            "\n1 - По ID" +
                            "\n2 - Ф.И.О" +
                            "\n3 - Возраст" +
                            "\n4 - Рост" +
                            "\n5 - Дата Рождения" +
                            "\n6 - Место рождения");
                        SortWorkers();
                        break;
                    case "6":
                        Console.WriteLine("До свидания!");
                        break;
                    default:
                        Console.WriteLine("Введено не правильное число, нажмите на клавишу Enter и попробуйте еще раз!");
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
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    StringBuilder stringBuilder = new StringBuilder(streamReader.ReadToEnd());
                    Console.Write(stringBuilder.Replace(separator, ' '));
                    Console.WriteLine();
                    Console.WriteLine("Нажмите на любую клавишу что бы продолжить...");
                }
            }
            else
            {
                Console.WriteLine("Файл не существует! Пожалуйста, заполните хотя бы одну запись.");
                Console.WriteLine();
                Console.WriteLine("Нажмите на любую клавишу что бы продолжить...");
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

            Console.WriteLine();
            Console.WriteLine("Нажмите на любую клавишу что бы продолжить...");
            Console.ReadLine();
        }

        /// <summary>
        /// Метод принимает ID работника и создает новый массив работников, только без него.
        /// Далее подчищает файл и уже добавляет строки из обнвленого массива в файл.
        /// </summary>
        private static void DeleteWorker()
        {
            Console.WriteLine("Введите ID сотрудника для удаления:");
            int id = Int32.Parse(Console.ReadLine());
            repository.DeleteWorker(id);
        }

        /// <summary>
        /// Метод принимает две даты и выводит на экран сотрудников,
        /// чьи дни рождения попадают в данный диапазон дат.
        /// </summary>
        private static void SelectWorkerBetweenDates()
        {
            Console.WriteLine("Введите первую дату:");
            string firstDateInput = Console.ReadLine();

            string[] firstDateParts = firstDateInput.Split('.');

            int firstYear = Int32.Parse(firstDateParts[0]);
            int firstMonth = Int32.Parse(firstDateParts[1]);
            int firstDay = Int32.Parse(firstDateParts[2]);

            Console.WriteLine("Введите вторую дату:");
            string secondDateInput = Console.ReadLine();

            Console.WriteLine(">>>>>>>>>>>> Сотрудники в выбранном диапазоне дат <<<<<<<<<<<");

            string[] secondDateParts = secondDateInput.Split('.');

            int secondYear = Int32.Parse(secondDateParts[0]);
            int secondMonth = Int32.Parse(secondDateParts[1]);
            int secondDay = Int32.Parse(secondDateParts[2]);

            DateTime dateFrom = new DateTime(firstYear, firstMonth, firstDay);
            DateTime dateTo = new DateTime(secondYear, secondMonth, secondDay);

            Worker[] workers = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
            string[] workersToPrint = new string[workers.Length];
            int workerIndex = 0;

            for (int i = 0; i < workers.Length - 1; i++)
            {
                workersToPrint[i] = $"{workers[i].Id} {workers[i].infoCreated} {workers[i].FullName} {workers[i].Age} {workers[i].Height} {workers[i].Birthday} {workers[i].CityOfBirth}";
                Console.WriteLine(workersToPrint[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите на любую клавишу что бы продолжить...");
            Console.ReadLine();
        }

        public static void SortWorkers()
        {
            Worker[] workersToSort = repository.GetAllWorkers();
            Worker[] afterSorting = new Worker[workersToSort.Length];
            sortingChoice = Console.ReadLine();

            switch (sortingChoice)
            {
                case "1":
                    afterSorting = workersToSort.OrderBy(worker => worker.Id).ToArray();                    
                    break;
                case "2":
                    afterSorting = workersToSort.OrderBy(worker => worker.FullName).ToArray();                    
                    break;
                case "3":
                    afterSorting = workersToSort.OrderBy(worker => worker.Age).ToArray();                   
                    break;
                case "4":                    
                    afterSorting = workersToSort.OrderBy(worker => worker.Height).ToArray();
                    break;
                case "5":
                    afterSorting = workersToSort.OrderBy(worker => worker.Birthday).ToArray();
                    break;
                case "6":
                    afterSorting = workersToSort.OrderBy(worker => worker.CityOfBirth).ToArray();
                    break;
            }
            
            Console.WriteLine("Результат сортировки:");
            
            foreach (Worker worker in afterSorting)
            {
                string sortedWorker = $"{worker.Id} {worker.infoCreated} {worker.FullName} {worker.Age} {worker.Height} {worker.Birthday} {worker.CityOfBirth}";
                Console.WriteLine(sortedWorker);
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите на любую клавишу что бы продолжить...");
            Console.ReadKey();
        }
    }
}

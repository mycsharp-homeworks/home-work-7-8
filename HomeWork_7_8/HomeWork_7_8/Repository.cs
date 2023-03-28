using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_7_8
{
    internal class Repository
    {
        private string filePath = "employees.txt";
        private static int idCounter = 1;

        public void AddWorker(Worker worker)
        {
            // присваиваем worker уникальный ID,
            worker.Id = idCounter++;

            // дописываем нового worker в файл
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                string newWorker = $"{worker.Id}#{worker.infoCreated}#{worker.FullName}#{worker.Age}#{worker.Height}#{worker.Birthday}#{worker.CityOfBirth}";
                writer.WriteLine(newWorker);
            }
        }

        public Worker[] GetAllWorkers() 
        {
            // здесь происходит чтение из файла
            string[] linesOfWorkers = File.ReadAllLines(filePath);
            Worker[] workers = new Worker[linesOfWorkers.Length];

            for (int i = 0; i < workers.Length; i++)
            {
                string[] fields = linesOfWorkers[i].Split('#');
                Worker worker = new Worker
                {
                    Id = Int32.Parse(fields[0]),
                    FullName = fields[1],
                    Age = Int32.Parse(fields[2]),
                    Height = Int32.Parse(fields[3]),
                    Birthday = DateTime.Parse(fields[4]),
                    CityOfBirth = fields[5]
                };
                worker = workers[i];
            }

            // и возврат массива считанных экземпляров
            return workers; 
        }

        public Worker GetWorkerById(int id)
        {
            // происходит чтение из файла, возвращается Worker
            // с запрашиваемым ID
            Worker[] allWorkers = GetAllWorkers();
            Worker workerToReturn = new Worker();
            
            for (int i = 0; i < allWorkers.Length; i++)
            {
                if (allWorkers[i].Id == id)
                {
                    workerToReturn = allWorkers[i];
                }
            }
            return workerToReturn;
        }        

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            // здесь происходит чтение из файла
            // фильтрация нужных записей
            // и возврат массива считанных экземпляров
            int count = 0;
            Worker[] allWorkers = GetAllWorkers();
            Worker[] workersInBetween = new Worker[allWorkers.Length];

            foreach (Worker worker in allWorkers)
            {
                if (worker.Birthday >= dateFrom && worker.Birthday <= dateTo)
                {
                    workersInBetween[count] = worker;
                    count++;
                }
            }

            return workersInBetween;
        }

        public void DeleteWorker(int id)
        {
            // считывается файл, находится нужный Worker
            // происходит запись в файл всех Worker,
            // кроме удаляемого
            Worker[] allWorkers = GetAllWorkers();

            string emptyContent = "";

            File.WriteAllText(filePath, emptyContent);

            foreach (Worker worker in allWorkers)
            {
                if (worker.Id == id)
                {
                    continue;
                }
                AddWorker(worker);
            }
        }
    }
}

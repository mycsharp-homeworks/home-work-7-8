using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_7_8
{
    internal class Repository
    {
        private string filePath = "employees.txt";
        private char separator = '#';

        public void AddWorker(Worker worker)
        {
            // присваиваем worker уникальный ID,
            // дописываем нового worker в файл
        }

        public Worker[] GetAllWorkers() 
        {
            // здесь происходит чтение из файла
            // и возврат массива считанных экземпляров
            return new Worker[0]; 
        }

        public Worker GetWorkerById(int id)
        {
            // происходит чтение из файла, возвращается Worker
            // с запрашиваемым ID
            Worker[] workers = GetAllWorkers();
            return workers[0];
        }        

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            // здесь происходит чтение из файла
            // фильтрация нужных записей
            // и возврат массива считанных экземпляров
            Worker[] workers = GetAllWorkers();
            return workers;
        }

        public void DeleteWorker(int id)
        {
            // считывается файл, находится нужный Worker
            // происходит запись в файл всех Worker,
            // кроме удаляемого
        }
    }
}

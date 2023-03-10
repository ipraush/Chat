using System;
using System.IO;
using System.Linq;

namespace Chat.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Подключаю файл data.txt
            // Необходимо в свойствах выбрать параметр "Копировать в выходной коталог" и установить занчение "Всегда копировать"
            // Необхожимо указать кодировку файла для правильного отображения кирилических символов
            // Выбираю "Файл" -> "Сохранить как" -> "Юнокод (UTF-8 с сигнатурой), кодовая страница 65001"
            var lines = File.ReadAllLines("data.txt");
            // Разбиваю строку на части 
            var questions = lines
                // в данном примере Split('|') выступает в качестве разделителя
                // т.е. до | вопрос, после ответ
                .Select(line => line.Split('|'))
                .Select(line => (line[0], line[1]))
                .ToList();
            // подключаю метод Random
            Random random = new();
            var count = questions.Count;
            while (true)
            {
                // эти 2 строчки изменят порядок строк на случайный
                // т.е. вопросы будут задаваться не по порядку
                var index = random.Next(count - 1);
                var question = questions[index];
                Console.WriteLine(question.Item1);
                var tryAnswer = Console.ReadLine();
                
                if (tryAnswer == question.Item2)
                {
                    Console.WriteLine("Правильно!");
                }
                else
                {
                    Console.WriteLine("Не правильно!");
                }
            }
            Console.WriteLine("Hello World!");

            // 01:11:51
        }
    }
}

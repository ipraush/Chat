using System;
using System.IO;
using System.Linq;

namespace Chat.Bot
{
    class Program
    {
        /// <summary>
        /// Чат бот работа в офлайн режиме.
        /// </summary>
        /// <param name="args"> Отсутствует. </param>
        static void Main(string[] args)
        {
            // Подключаю файл data.txt.
            // Необходимо в свойствах выбрать параметр "Копировать в выходной коталог" и установить занчение "Всегда копировать".
            // Необхожимо указать кодировку файла для правильного отображения кирилических символов.
            // Выбираю "Файл" -> "Сохранить как" -> "Юнокод (UTF-8 с сигнатурой), кодовая страница 65001".
            var lines = File.ReadAllLines("data.txt");
            // Разбиваю строку на части.
            var questions = lines
                // в данном примере Split('|') выступает в качестве разделителя.
                // т.е. до | вопрос, после ответ.
                .Select(line => line.Split('|'))
                // TODO: свойство этой строки.
                .Select(line => (line[0], line[1]))
                .ToList();
            // Подключаю метод Random.
            Random random = new();
            // Номер строки из файла data.txt.
            var count = questions.Count;
            // Количество отчков.
            var Score = 0;
            while (true)
            {
                // эти 2 строчки изменят порядок строк на случайный.
                // т.е. вопросы будут задаваться не по порядку.
                var index = random.Next(count - 1);
                // Вопрос в случайном порядке.
                var question = questions[index];

                // Инициализация подсчета открытых букв.
                var opened = 0;
                // Цыкл обработки открытых символов ответа на вопрос.
                while (opened < question.Item2.Length)
                {
                    // Открывает буквы по порядку при каждом не правильном ответе.
                    var answer = question.Item2
                        .Substring(0, opened)
                        .PadRight(question.Item2.Length, '*');
                    Console.WriteLine($"  {question.Item1}:\n    {question.Item2.Length} букв  {answer}");
                    // Если в ответе присутствует беква ё, заменяем ее на е.
                    var tryAnswer = Console.ReadLine().ToLower().Replace('ё', 'е');

                    // Условие обработки ответов.
                    if (tryAnswer == question.Item2)
                    {
                        // Увеличивает количество очков за каждый правильный ответ.
                        Score++;
                        // При верном ответе вывод в консоль.
                        Console.WriteLine($"  Правильно!  У Вас {Score} очков.");
                        break;
                    }
                    else
                    {
                        // При не верном ответе вывод в консоль.
                        Console.WriteLine("  Не правильно!");
                        // Увеличивает счетчик если ответ неправильный.
                        opened++;
                    }
                }
                // Условие срабатывает когда подсказки закончились.
                if (opened == question.Item2.Length)
                {
                    // Вывод в консоль.
                    Console.WriteLine($"  Печально!!! ответ: {question.Item2}\n");
                }
            }
        }
    }
}

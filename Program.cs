using System.Diagnostics.Metrics; 

namespace GuessNumber 
{
    internal class Program 
    {
        // Точка входа в приложение
        static void Main(string[] args) 
        {
            // Инициализация переменных для сбора статистики сессии
            int minAttempt = 0;   // Наилучший результат (минимум попыток)
            int maxAttempt = 0;   // Худший результат (максимум попыток)
            int totalAttempts = 0; // Суммарное количество попыток
            int gamesPlayed = 0;   // Общее число завершенных партий
            
            Random randomGenerator = new Random(); 
            char userChoice = 'Y'; 

            do 
            {
                // Запуск игрового цикла и обновление глобальной статистики
                PlayGame(randomGenerator, ref minAttempt, ref maxAttempt, ref totalAttempts, ref gamesPlayed);
                
                Console.WriteLine("Хотите сыграть еще раз? (Y/N)");
                userChoice = Console.ReadKey().KeyChar; // Считывание символа без нажатия Enter
                Console.WriteLine(); 
            }
            while (char.ToUpper(userChoice) == 'Y'); // Проверка условия продолжения

            // Вывод итоговых показателей за все игры
            Console.WriteLine($"\nСтатистика: Мин. попыток = {minAttempt}, Макс. попыток = {maxAttempt}, Среднее = {(double)totalAttempts / gamesPlayed:F2}");
        }

        /// <summary>
        /// Логика одного игрового раунда
        /// </summary>
        static int PlayGame(Random rnd, ref int min, ref int max, ref int count, ref int countGame)
        {
            int currentAttempts = 0; 
            int targetNumber = rnd.Next(1, 100); // Загаданное число в диапазоне [1, 99]
            
            Console.WriteLine("Компьютер загадал число. Попробуйте угадать!");

            while (true) 
            {
                currentAttempts++; 
                int userMove = ReadUserNumber();

                if (userMove > targetNumber)
                    Console.WriteLine("Загаданное число меньше вашего."); 
                else if (userMove < targetNumber)
                    Console.WriteLine("Загаданное число больше вашего."); 
                else
                {
                    Console.WriteLine($"Победа! Угадано за {currentAttempts} попыток.");
                    
                    // Обновление рекордов
                    if (min == 0 || currentAttempts < min) min = currentAttempts;
                    if (currentAttempts > max) max = currentAttempts;
                    
                    count += currentAttempts;
                    countGame++;
                    break; 
                }
            }
            return currentAttempts;
        }

        /// <summary>
        /// Обработка ввода пользователя с валидацией
        /// </summary>
        static int ReadUserNumber()
        {
            int result = 0;
            const int maxRetries = 3; // Лимит ошибок при вводе

            for (int i = 0; i < maxRetries; i++)
            {
                Console.Write("Введите число от 1 до 100: ");
                string input = Console.ReadLine();

                // Проверка на числовой формат и вхождение в диапазон
                if (int.TryParse(input, out result) && result >= 1 && result <= 100)
                {
                    return result;
                }

                Console.WriteLine("Ошибка! Нужно ввести целое число в указанном диапазоне.");
            }

            // Экстренное завершение при многократных ошибках ввода
            Console.WriteLine("Превышено количество попыток ввода. Выход из программы.");
            Environment.Exit(0);
            return 0;
        }
    }
}

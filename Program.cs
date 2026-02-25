using System;

namespace AlgorithmAndDataStructures
{
    /// <summary>
    /// Основной класс программы для игры "Угадай число".
    /// </summary>
    internal class Program
    {
        // Глобальные переменные для хранения статистики между играми
        static int min = 0;
        static int max = 0;
        static int count = 0;
        static int countGame = 0;

        /// <summary>
        /// Точка входа: управляет циклом повтора игры.
        /// </summary>
        static void Main(string[] args)
        {
            char answer = 'Y';
            do
            {
                StartGameRound();

                Console.WriteLine("Do you want play again? (Y/N)");
                // Читаем строку и берем первый символ в верхнем регистре
                string input = Console.ReadLine();
                answer = !string.IsNullOrEmpty(input) ? input.ToUpper()[0] : 'N';

            } while (answer == 'Y');

            PrintFinalStats();
        }

        /// <summary>
        /// Метод отвечает за логику одного раунда игры
        /// </summary>
        static void StartGameRound()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 101);
            int counter = 0;

            while (true)
            {
                int userNumber = GetValidUserInput();
                counter++;

                if (userNumber > number)
                {
                    Console.WriteLine("Your number is greater");
                }
                else if (userNumber < number)
                {
                    Console.WriteLine("Your number is less");
                }
                else
                {
                    Console.WriteLine($"You are win! Attempts: {counter}");
                    UpdateStatistics(counter);
                    break;
                }
            }
        }

        /// <summary>
        /// Метод для безопасного получения числа от пользователя (3 попытки).
        /// </summary>
        /// <returns>Введенное пользователем число.</returns>
        static int GetValidUserInput()
        {
            int userNumber;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Input number from [1;100]");
                if (int.TryParse(Console.ReadLine(), out userNumber) && userNumber <= 100 && userNumber >= 1)
                {
                    return userNumber;
                }

                if (i == 2)
                {
                    Console.WriteLine("You are stupid");
                    Environment.Exit(0); // Выход из программы при 3 ошибках
                }
            }
            return -1;
        }

        /// <summary>
        /// Обновляет значения min, max и среднее количество попыток.
        /// </summary>
        /// <param name="counter">Количество попыток в текущей игре.</param>
        static void UpdateStatistics(int counter)
        {
            if (min == 0 || min > counter) min =counter;
            if (max < counter) max = counter;
            count += counter;
            countGame++;
        }

        /// <summary>
        /// Выводит итоговую статистику на экран
        /// </summary>
        static void PrintFinalStats()
        {
            if (countGame > 0)
            {
                double avg = (double)count / countGame;
                Console.WriteLine($"min={min} max={max} avg={avg:F2}");
            }
        }
    }
}
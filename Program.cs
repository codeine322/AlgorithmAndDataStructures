using System.Diagnostics.Metrics;

namespace GuessNumber
{
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу. Управляет игровым циклом и выводит итоговую статистику.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            int min = 0;
            int max = 0;
            int count = 0;
            int countGame = 0;
            Random rnd = new Random();
            char answer = 'Y';
            do
            {
                int couneter = PlayGame(rnd, ref min, ref max, ref count, ref countGame);
                Console.WriteLine("Do you want play game? (Y/N)");
                // Читаем строку и берем первый символ, чтобы избежать проблем с буфером ввода
                string input = Console.ReadLine() ?? "N";
                answer = input.Length > 0 ? char.ToUpper(input[0]) : 'N';
            }
            while (answer == 'Y');
            
            if (countGame > 0)
                Console.WriteLine($"min = {min} max = {max} avg = {(double)count / countGame}");
        }

        /// <summary>
        /// Проводит одну игровую сессию по угадыванию числа.
        /// </summary>
        /// <param name="rnd">Экземпляр генератора случайных чисел.</param>
        /// <param name="min">Минимальное количество попыток за все игры (обновляется по ссылке).</param>
        /// <param name="max">Максимальное количество попыток за все игры (обновляется по ссылке).</param>
        /// <param name="count">Общее количество попыток за все игры (обновляется по ссылке).</param>
        /// <param name="countGame">Общее количество проведенных игр (обновляется по ссылке).</param>
        /// <returns>Количество попыток, затраченных в текущей сессии.</returns>
        static int PlayGame(Random rnd, ref int min, ref int max, ref int count, ref int countGame)
        {
            int couneter = 0;
            int number = rnd.Next(1, 101); // 101, чтобы включить число 100
            Console.WriteLine("Try guess number?");
            while (true)
            {
                couneter++;
                int userNumber = ReadUserNumber();
                if (userNumber > number)
                    Console.WriteLine("Your number is less!");
                else if (userNumber < number)
                    Console.WriteLine("Your number is great!");
                else
                {
                    Console.WriteLine("You are Win!!!");
                    if (min == 0 || min > couneter)
                        min = couneter;
                    max = max < couneter ? couneter : max;
                    count += couneter;
                    countGame++;
                    break;
                }
            }
            return couneter;
        }

        /// <summary>
        /// Считывает число пользователя из консоли с валидацией диапазона [1;100].
        /// Предоставляет 3 попытки на корректный ввод, иначе завершает программу.
        /// </summary>
        /// <returns>Валидное целое число, введенное пользователем.</returns>
        static int ReadUserNumber()
        {
            int userNumber = 0;
            Console.WriteLine("Input number from [1;100]");

            for (int i = 0; i < 3; i++)
            {
                if (!int.TryParse(Console.ReadLine(), out userNumber)
                    || userNumber > 100 || userNumber < 1)
                {
                    if (i < 2) Console.WriteLine("Invalid input. Try again. Input number from [1;100]");
                }
                else
                {
                    return userNumber;
                }
            }
            
            Console.WriteLine("You are stupid");
            Environment.Exit(0);
            return 0;
        }
    }
}

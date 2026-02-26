using System.Diagnostics.Metrics; 

namespace GuessNumber 
{
    internal class Program 
    {
        // Точка входа в приложение
        static void Main(string[] args) 
        {
            int minAttempt = 0;
            int maxAttempt = 0;
            int totalAttempts = 0;
            int gamesPlayed = 0;
            
            Random randomGenerator = new Random(); 
            char userChoice = 'Y'; 

            do 
            {
                PlayGame(randomGenerator, ref minAttempt, ref maxAttempt, ref totalAttempts, ref gamesPlayed);
                
                Console.WriteLine("Хотите сыграть еще раз? (Y/N)");
                userChoice = Console.ReadKey().KeyChar;
                Console.WriteLine(); 
            }
            while (char.ToUpper(userChoice) == 'Y');
            Console.WriteLine($"\nСтатистика: Мин. попыток = {minAttempt}, Макс. попыток = {maxAttempt}, Среднее = {(double)totalAttempts / gamesPlayed:F2}");
        }

        // Логика одного игрового раунда
        static int PlayGame(Random rnd, ref int min, ref int max, ref int count, ref int countGame)
        {
            int currentAttempts = 0; 
            int targetNumber = rnd.Next(1, 100);
            
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
                    
                    if (min == 0 || currentAttempts < min) min = currentAttempts;
                    if (currentAttempts > max) max = currentAttempts;
                    
                    count += currentAttempts;
                    countGame++;
                    break; 
                }
            }
            return currentAttempts;
        }

        // Обработка ввода пользователя с валидацией
        static int ReadUserNumber()
        {
            int result = 0;
            const int maxRetries = 3;

            for (int i = 0; i < maxRetries; i++)
            {
                Console.Write("Введите число от 1 до 100: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result >= 1 && result <= 100)
                {
                    return result;
                }

                Console.WriteLine("Ошибка! Нужно ввести целое число в указанном диапазоне.");
           }
            Console.WriteLine("Превышено количество попыток ввода. Выход из программы.");
            Environment.Exit(0);
            return 0;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
namespace LogExchangeMidili
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.Seq("http://localhost:5341/", apiKey: "SvqPwQzhheCNqeJHpEl4")
                        .CreateLogger();
            var money = 0.00d;
            var TryParseResult = false;
            Console.WriteLine("<Обменник валют>");
            while (!TryParseResult)
            {
                Console.Write("Введите кол-во долларов: ");
                var input = Console.ReadLine();
                TryParseResult = double.TryParse(input, out money);
                if (!TryParseResult)
                {
                    Log.Error($"Неверный формат ввода!");
                }
            }
            Log.Information($"Пользователь сделал депозит в размере: {money}$");
            if (money <= 500.00 / 60.47)
            {
                Console.WriteLine("Курс доллара: 60.47 рублей");
                Console.WriteLine("Комиссия банка составляет 8 рублей");
                Console.WriteLine($"Итого: {money * 60.47 - 8:#.##}P.");
                Log.Information($"Перевод прошёл успешно, выдано {money * 60.47 - 8:#.##}P.");
            }
            else
            {
                Console.WriteLine("Курс доллара: 60.47 рублей");
                Console.WriteLine("Комиссия банка составляет 0.37%");
                Console.WriteLine($"Итого: {money * 60.47 * 0.9963:#.##}P.");
                Log.Information($"Перевод прошёл успешно, выдано {money * 60.47 * 0.9963:#.##}P.");
            }
            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TestDb.Interfaces;
using TestDb.Models;
using TestDB.MsSql;
using TestDB.MsSql.Services;


namespace TestDbApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region --------------------Подключение БД--------------------

            var conectionString = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build().GetConnectionString("TestDB");

            SqlConnection connection = new SqlConnection(conectionString);

            connection.Open();

            if (connection.State == ConnectionState.Open) Console.WriteLine("Соединение с БД установленно"); //HACK временная проверка!

            #endregion --------------------Подключение БД--------------------

            #region --------------------Контейнер и сервисы--------------------

            var services = new ServiceCollection();

            services.AddDbServices();

            var serviceProvider = services.BuildServiceProvider();

            var DbService = serviceProvider.GetService<IDbService>();

            #endregion --------------------Контейнер и сервисы--------------------

            #region --------------------Обработка аргументов--------------------

            if (args.Length == 0)
            {
                Console.WriteLine("Отсутствуют аргументы, запуск не возможен!");
                Console.ReadKey();
                return;
            }

            try
            {
                int intArg;
                if (!int.TryParse(args[0], out intArg))
                {
                    Console.WriteLine("Первый аргумент не корректный, запуск не возможен!");
                    Console.ReadKey();
                    return;
                }

                switch (intArg)
                {
                    case 1:
                        DbService.AddTable("Добавлена таблица 1");
                        Console.WriteLine(intArg);
                        Console.ReadKey();
                        break;

                    case 2:

                        if (args.Length != 6)
                        {
                            Console.WriteLine("Не верное количество или формат введённых аргументов, запуск не возможен!\n"
                                + "Изпользуйте формат: TestDbApp 2 Фамилия Имя Отчество ДатаРождения Пол");
                            Console.ReadKey();
                        }
                        else
                        {
                            foreach (var el in args)
                                Console.Write(el + " ");
                            Console.ReadKey();
                        }

                        break;

                    case 3:
                        Console.WriteLine(intArg);
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine(intArg);
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine(intArg);
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Введено не верное значение первого аргумента, запуск не возможен!");
                        Console.ReadKey();
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            #endregion --------------------Обработка аргументов--------------------
            
            if(connection.State  == ConnectionState.Open) connection.Dispose();
        }
    }
}
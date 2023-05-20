using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TestData;
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
            var conectionString = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build().GetConnectionString("TestDB");

            if (args.Length == 0)
            {
                Console.WriteLine("Отсутствуют аргументы, запуск не возможен!");
                Console.ReadKey();
                return;
            }

            #region --------------------Контейнер и сервисы--------------------

            var services = new ServiceCollection();

            services.AddDbServices()
                    .AddTestDataServices();

            var serviceProvider = services.BuildServiceProvider();

            var dbService = serviceProvider.GetService<IDbService>();
            var testDataService = serviceProvider.GetService<ITestData>();

            #endregion --------------------Контейнер и сервисы--------------------

            #region --------------------Обработка аргументов--------------------

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
                        dbService.AddTable(conectionString);
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
                            var person = new Person
                            {
                                LastName = args[1],
                                FirstName = args[2],
                                Patronymic = args[3],
                                BirthDate = DateTime.Parse(args[4]),
                                Gender = args[5]
                            };

                            dbService.AddRow(conectionString, person);
                        }
                        break;

                    case 3:
                        Console.WriteLine(intArg);
                        Console.ReadKey();
                        break;

                    case 4:
                        var persons = testDataService.GetRandomPersons(1000);
                        dbService.AddRows(conectionString, persons);
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
        }
    }
}
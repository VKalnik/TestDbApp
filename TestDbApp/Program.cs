using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TestData;
using TestDb.Interfaces;
using TestDb.Models;
using TestDB.MsSql;


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
                if (!int.TryParse(args[0], out int intArg))
                {
                    Console.WriteLine("Первый аргумент не корректный, запуск не возможен!");
                    Console.ReadKey();
                    return;
                }

                switch (intArg)
                {
                    case 1:
                        dbService.AddTable(conectionString);
                        Console.WriteLine("Таблица [Persons] создана в БД");
                        break;

                    case 2:
                        if (args.Length != 6)
                        {
                            Console.WriteLine("Не верное количество или формат введённых аргументов, запуск не возможен!\n"
                                + "Изпользуйте формат: TestDbApp 2 Фамилия Имя Отчество ДатаРождения Пол");
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

                        Console.WriteLine("Строка добавлена в БД.");

                        break;

                    case 3:
                        string sqlCommandText = "SELECT id, last_name,  first_name,  patronymic,  birthdate,  gender FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY last_name,  first_name,  patronymic,  birthdate ORDER BY last_name) as row_num FROM Persons) AS subquery WHERE row_num = 1 ORDER BY last_name;";
                        
                        var peoples = dbService.Select(conectionString, sqlCommandText);

                        foreach (Person p in peoples.persons)
                            Console.WriteLine(p + $"Полных лет: {p.GetAge()}");
                        break;

                    case 4:

                        int amount = 1000000;
                        var persons = testDataService.GetRandomPersons(amount);
                        dbService.AddRows(conectionString, persons);

                        Console.WriteLine($"В таблицу [Persons] в БД добавлено {amount} строк");

                        break;

                    case 5:
                        string sqlCommandText1 = "SELECT * FROM Persons WHERE gender = 'M' AND last_name LIKE 'F%';";

                        foreach (Person p in dbService.Select(conectionString, sqlCommandText1).persons)
                            Console.WriteLine(p);

                        Console.WriteLine(dbService.Select(conectionString, sqlCommandText1).msg);

                        break;

                    default:
                        Console.WriteLine("Введено не верное значение первого аргумента, запуск не возможен!");
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
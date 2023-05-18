namespace TestDbApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
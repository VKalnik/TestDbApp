using TestDb.Models;
using TestDb.Interfaces;

namespace TestData
{
    /// <summary>
    /// Класс с тестовыми данными
    /// </summary>
    internal class TestData : ITestData
    {
        #region Переменные и словари

        private static readonly Random rnd = new();

        /// <summary>
        /// Список значений фамилия
        /// </summary>
        private readonly string[] lastNames = 
        {
            "Adams", "Anderson", "Allen", "Armstrong", // A
            "Brown", "Baker", "Bell", "Bennett", // B
            "Clark", "Carter", "Cooper", "Campbell", // C
            "Davis", "Diaz", "Dixon", "Duncan", // D
            "Edwards", "Evans", "Ellis", "Elliott", // E
            "Ford", "Fisher", "Foster", "Fletcher", // F
            "Garcia", "Gray", "Green", "Gonzalez", // G
            "Harris", "Hall", "Hill", "Hughes", // H
            "Jackson", "Johnson", "Jones", "James", // I
            "King", "Kelly", "Kim", "Kennedy", // J
            "Lee", "Lewis", "Lopez", "Long", // K
            "Miller", "Moore", "Mitchell", "Murphy", // L
            "Nelson", "Nguyen", "Neal", "Newton", // M
            "O'Connor", "Owens", "Olson", "Ortega", // N
            "Parker", "Peterson", "Price", "Powell", // O
            "Quinn", "Quick", "Quintero", "Quezada", // P
            "Roberts", "Rodriguez", "Reed", "Richardson", // Q
            "Smith", "Sanders", "Scott", "Sullivan", // R
            "Taylor", "Thomas", "Thompson", "Turner", // S
            "Upton", "Underwood", "Urban", "Ulrich", // T
            "Vargas", "Vaughn", "Vega", "Valdez", // U
            "Williams", "Wilson", "Walker", "White", // V
            "Xiong", "Xu", "Xiao", "Xu", // W
            "Young", "Yang", "Yu", "Yates", // X
            "Zhang", "Zimmerman", "Zimmer", "Zavala" // Y
        };

        /// <summary>
        /// Список значений - имя
        /// </summary>
        private readonly string[] firstNames =
        {
            "Adam",   // A
            "Benjamin",   // B
            "Christopher",   // C
            "Daniel",   // D
            "Ethan",   // E
            "Frank",   // F
            "George",   // G
            "Henry",   // H
            "Isaac",   // I
            "Jacob",   // J
            "Kevin",   // K
            "Liam",   // L
            "Matthew",   // M
            "Noah",   // N
            "Oliver",   // O
            "Patrick",   // P
            "Quentin",   // Q
            "Ryan",   // R
            "Samuel",   // S
            "Thomas",   // T
            "Ulysses",   // U
            "Victor",   // V
            "William",   // W
            "Xavier",   // X
            "Yusuf",   // Y
            "Zachary"   // Z
        };

        /// <summary>
        /// Список значений- отчество
        /// </summary>
        private readonly string[] patronymics =
        {
            "Adamovich",   // A
            "Benjaminovich",   // B
            "Christopherovich",   // C
            "Danielovich",   // D
            "Ethanovich",   // E
            "Frankovich",   // F
            "Georgeovich",   // G
            "Henryovich",   // H
            "Isaacovich",   // I
            "Jacobovich",   // J
            "Kevinovich",   // K
            "Liamovich",   // L
            "Matthewovich",   // M
            "Noahovich",   // N
            "Oliverovich",   // O
            "Patrickovich",   // P
            "Quentinovich",   // Q
            "Ryanovich",   // R
            "Samuelovich",   // S
            "Thomasovich",   // T
            "Ulyssesovich",   // U
            "Victorovich",   // V
            "Williamovich",   // W
            "Xavierovich",   // X
            "Yusufovich",   // Y
            "Zacharyovich"   // Z
        };

        /// <summary>
        /// Список значений - пол
        /// </summary>
        private readonly string[] genders =
        {
            "M",
            "F"
        };

        /// <summary>
        /// Список латинских букв
        /// </summary>
        private readonly string letters = "abcdefghijklmnopqrstuvwxyz";
        //private readonly string letters = "abcdeghijklmnopqrstuvwxyz";

        #endregion Переменные и словари

        /// <summary>
        /// Создаёт массив случайных экземпляров <see cref="Person"/> в указанном количестве.
        /// </summary>
        /// <param name="amount">Количество</param>
        /// <returns>Массив <see cref="Person"/></returns>
        public Person[] GetRandomPersons(int amount)
        {
            var persons = new Person[amount];

            for (int i = 0; i < amount; i++)
            {
                persons[i] = new Person
                {
                    LastName = GetRandomLastName(),
                    FirstName = GetRandomFirstName(),
                    Patronymic = GetRandomPatronymic(),
                    BirthDate = GetRandomBirthDate(),
                    Gender = GetRandomGender(),
                    //Gender = "M",
                };
            }

            return persons;
        }

        #region Вспомогательные методы

        /// <summary>
        /// Возвращает случайное значение имени
        /// </summary>
        /// <returns>Случайное Имя</returns>
        private string GetRandomLastName() => lastNames[rnd.Next(firstNames.Length)];

        /// <summary>
        /// Возвращает случайное значение имени
        /// </summary>
        /// <returns>Случайное Имя</returns>
        private string GetRandomFirstName() => firstNames[rnd.Next(firstNames.Length)];

        /// <summary>
        /// Возвращает случайное значение отчества
        /// </summary>
        /// <returns>Случайное Отчество</returns>
        private string GetRandomPatronymic() => patronymics[rnd.Next(patronymics.Length)];

        /// <summary>
        /// Возвращает полностью случайный набор букв от 5 до 10 штук начиная с заглавной буквы.
        /// </summary>
        /// <returns>Случайная значение фамилия (случайный набор букв)</returns>
        private string GetFullRandonLastName()
        {
            int length = rnd.Next(5, 10);
            char[] chars = new char[length];
            chars[0] = char.ToUpper(letters[rnd.Next(letters.Length)]);
            //chars[0] = 'F';

            for (int i = 1; i < length; i++)
            {
                chars[i] = letters[rnd.Next(letters.Length)];
            }

            return new string(chars);
        }
        /// <summary>
        /// Возвращает случайное значение даты рождения
        /// </summary>
        /// <returns></returns>
        private DateTime GetRandomBirthDate() => DateTime.Parse($"{rnd.Next(1, 28)}.{rnd.Next(1, 12)}.{rnd.Next(1940, 2000)}");

        /// <summary>
        /// Возвращает случайное значение пола
        /// </summary>
        /// <returns>Случайное значение пола</returns>
        private string GetRandomGender() => genders[rnd.Next(genders.Length)];
    }

    #endregion Вспомогательные методы
}
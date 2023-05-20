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
                    LastName = GetRandonLastName(),
                    FirstName = GetRandomFirstName(),
                    Patronymic = GetRandomPatronymic(),
                    BirthDate = GetRandomBirthDate(),
                    Gender = GetRandomGender(),
                };
            }

            return persons;
        }

        #region Вспомогательные методы

        /// <summary>
        /// Возвращает случайное значение имени
        /// </summary>
        /// <returns>Случайное Имя</returns>
        private string GetRandomFirstName() => firstNames[rnd.Next(0, 25)];

        /// <summary>
        /// Возвращает случайное значение отчества
        /// </summary>
        /// <returns>Случайное Отчество</returns>
        private string GetRandomPatronymic() => patronymics[rnd.Next(0, 25)];

        /// <summary>
        /// Возвращает случайное значение фамилии
        /// </summary>
        /// <returns>Случайная Фамилия (случайный набор букв)</returns>
        private string GetRandonLastName()
        {
            int length = rnd.Next(5, 10);
            char[] chars = new char[length];
            chars[0] = char.ToUpper(letters[rnd.Next(letters.Length)]);

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
        private string GetRandomGender() => genders[rnd.Next(0, 1)];
    }

    #endregion Вспомогательные методы
}
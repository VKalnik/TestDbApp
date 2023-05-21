using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TestDb.Interfaces;
using TestDb.Models;
using TestDB.MsSql.Properties;

namespace TestDB.MsSql.Services
{
    /// <summary>
    /// Класс взаимодействия с БД MS SQL.
    /// </summary>
    /// <seealso cref="TestDb.Interfaces.IDbService" />
    internal class DbService : IDbService
    {
        /// <summary>
        /// Метод добавляет таблицу [Persons] в БД.
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД.</param>
        public void AddTable(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = Resources.Sql_AddPersonsTable;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Метод добавляет строку в таблицу [Persons] в БД.
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД.</param>
        /// <param name="person">Объект <see cref="Person"/>.</param>
        public void AddRow(string connectionString, Person person)
        {
            if (person is null) throw new ArgumentNullException(nameof(person));
            if (string.IsNullOrEmpty(person.LastName)) throw new ArgumentNullException(nameof(person.LastName));
            if (string.IsNullOrEmpty(person.FirstName)) throw new ArgumentNullException(nameof(person.FirstName));
            if (string.IsNullOrEmpty(person.Patronymic)) throw new ArgumentNullException(nameof(person.Patronymic));
            if (string.IsNullOrEmpty(person.BirthDate.ToString("dd.MM.yyyy"))) throw new ArgumentNullException(nameof(person.BirthDate));
            if (string.IsNullOrEmpty(person.Gender)) throw new ArgumentNullException(nameof(person.Gender));

            using var connection = new SqlConnection(connectionString);
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = Resources.Sql_Insert;
                    cmd.Parameters.AddWithValue("last_name", person.LastName);
                    cmd.Parameters.AddWithValue("first_name", person.FirstName);
                    cmd.Parameters.AddWithValue("patronymic", person.Patronymic);
                    cmd.Parameters.AddWithValue("birthdate", person.BirthDate.ToString("dd.MM.yyyy"));
                    cmd.Parameters.AddWithValue("gender", person.Gender);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Метод пакетного добавления строк в таблицу [Persons] в БД.
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД.</param>
        /// <param name="persons">Массив объектов <see cref="Person"/></param>
        public void AddRows(string connectionString, Person[] persons)
        {
            var table = new DataTable();

            table.Columns.Add("id");
            table.Columns.Add("last_name");
            table.Columns.Add("first_name");
            table.Columns.Add("patronymic");
            table.Columns.Add("birthdate");
            table.Columns.Add("gender");

            foreach (var person in persons)
            {
                if (person is null) throw new ArgumentNullException(nameof(person));
                if (string.IsNullOrEmpty(person.LastName)) throw new ArgumentNullException(nameof(person.LastName));
                if (string.IsNullOrEmpty(person.FirstName)) throw new ArgumentNullException(nameof(person.FirstName));
                if (string.IsNullOrEmpty(person.Patronymic)) throw new ArgumentNullException(nameof(person.Patronymic));
                if (string.IsNullOrEmpty(person.BirthDate.ToString("dd.MM.yyyy"))) throw new ArgumentNullException(nameof(person.BirthDate));
                if (string.IsNullOrEmpty(person.Gender)) throw new ArgumentNullException(nameof(person.Gender));

                table.Rows.Add
                (
                    person.Id,
                    person.LastName,
                    person.FirstName,
                    person.Patronymic,
                    person.BirthDate.ToString("dd.MM.yyyy"),
                    person.Gender
                );
            }

            using (var connection = new SqlConnection(connectionString))
            {
                using (var sqlBulkCopy = new SqlBulkCopy(connection))
                {
                    sqlBulkCopy.DestinationTableName = "[dbo].[Persons]";
                    connection.Open();
                    sqlBulkCopy.WriteToServer(table);
                }
            }
        }

        /// <summary>
        /// Метод возвращает массив объектов <see cref="Person"/> по заданному условию.
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД.</param>
        /// <param name="sqlCommandText">Текст SQL-запроса(при отсутствии возвращает результаты в соответствии с запросом по умолчанию).</param>
        /// <returns></returns>
        public Person[] Select(string connectionString, string sqlCommandText)
        {
            var sw = new Stopwatch();

            SqlDataReader reader;

            using var connection = new SqlConnection(connectionString);
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sqlCommandText ?? Resources.Sql_BaseSelectPersons;

                    connection.Open();
                    sw.Start();
                    reader = cmd.ExecuteReader();
                    sw.Stop();
                }
            }

            Console.WriteLine($"Затраченное время: {sw.ElapsedMilliseconds} миллисекунд");
            
            var persons = new List<Person>();

            while (reader.Read())
            {
                var person = new Person();
                person.Id = reader.GetInt32(0);
                person.LastName = reader.GetString(1);
                person.FirstName = reader.GetString(2);
                person.Patronymic = reader.GetString(3);
                person.BirthDate = Convert.ToDateTime(reader.GetString(4));
                person.Gender = reader.GetString(5);
                persons.Add(person);
            }

            reader.Close();

            return persons.ToArray();
        }
    }
}

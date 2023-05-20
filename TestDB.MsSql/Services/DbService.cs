using System.Data;
using System.Data.SqlClient;
using TestDb.Interfaces;
using TestDb.Models;
using TestDB.MsSql.Properties;

namespace TestDB.MsSql.Services
{
    internal class DbService : IDbService
    {
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

        public void AddRow(string connectionString, Person person)
        {
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
                table.Rows.Add
                (
                    person.Id,
                    person.LastName,
                    person.FirstName,
                    person.Patronymic,
                    person.BirthDate.ToString("dd.MM.yyyy"),
                    person.Gender
                );

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

        public Person[] Select(string connectionString, string expression)
        {
            throw new NotImplementedException();
        }
    }
}

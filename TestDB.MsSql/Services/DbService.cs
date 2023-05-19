using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = Resources.Sql_AddPersonsTable;
                    cmd.ExecuteNonQuery();
                }
            };
        }

        public void AddRow(string connectionString, Person person)
        {
            using var connection = new SqlConnection(connectionString);
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = Resources.Sql_Insert;
                    cmd.Parameters.AddWithValue("last_name", person.LastName);
                    cmd.Parameters.AddWithValue("first_name", person.FirstName);
                    cmd.Parameters.AddWithValue("patronymic", person.Patronymic);
                    cmd.Parameters.AddWithValue("birthdate", person.BirthDate.ToString("dd.MM.yyyy"));
                    cmd.Parameters.AddWithValue("gender", person.Gender);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddRows(string connectionString, Person[] persons)
        {
            throw new NotImplementedException();
        }

        public Person[] Select(string connectionString, string expression)
        {
            throw new NotImplementedException();
        }
    }
}

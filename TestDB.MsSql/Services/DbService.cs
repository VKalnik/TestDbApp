using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDb.Interfaces;
using TestDb.Models;
using TestDB.MsSql.Properties;

namespace TestDB.MsSql.Services
{
    internal class DbService : IDbService
    {
        public void AddTable(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(Resources.Sql_AddPersonsTable, connection);
            command.ExecuteNonQuery();
        }

        public void AddRow(SqlConnection connection, Person person)
        {
            SqlCommand command = new SqlCommand(Resources.Sql_Insert, connection);

            command.Parameters.AddWithValue("last_name", person.LastName);
            command.Parameters.AddWithValue("first_name", person.FirstName);
            command.Parameters.AddWithValue("patronymic", person.Patronymic);
            command.Parameters.AddWithValue("birthdate", person.BirthDate.ToString("dd.MM.yyyy"));
            command.Parameters.AddWithValue("gender", person.Gender);
            command.ExecuteNonQuery();
        }

        public void AddRows(SqlConnection connection, Person[] persons)
        {
            throw new NotImplementedException();
        }

        public Person[] Select(SqlConnection connection, string expression)
        {
            throw new NotImplementedException();
        }
    }
}

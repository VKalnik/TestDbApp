using System.Data.SqlClient;
using TestDb.Models;

namespace TestDb.Interfaces
{
    public interface IDbService
    {
        void AddTable(SqlConnection connection);

        void AddRow(SqlConnection connection, Person person);

        void AddRows(SqlConnection connection, Person[] persons);
        Person[] Select(SqlConnection connection, string expression);
    }
}

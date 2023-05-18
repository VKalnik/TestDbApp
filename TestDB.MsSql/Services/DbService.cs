using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDb.Interfaces;
using TestDb.Models;

namespace TestDB.MsSql.Services
{
    internal class DbService : IDbService
    {
        public void AddTable(string connectionString)
        {
            Console.WriteLine(connectionString);
        }

        public void AddRow(string connectionString, Person person)
        {
            throw new NotImplementedException();
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

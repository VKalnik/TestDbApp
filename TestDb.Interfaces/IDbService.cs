﻿using TestDb.Models;

namespace TestDb.Interfaces
{
    public interface IDbService
    {
        void AddTable(string connectionString);

        void AddRow(string connectionString, Person person);

        void AddRows(string connectionString, Person[] persons);
        (Person[] persons, string msg) Select(string connectionString, string sqlCommandText = null);
    }
}

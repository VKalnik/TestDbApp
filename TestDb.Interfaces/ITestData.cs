using TestDb.Models;

namespace TestDb.Interfaces
{
    public interface ITestData
    {
        Person[] GetRandomPersons(int amount);
    }
}

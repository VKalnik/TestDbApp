namespace TestDb.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
    }

    public static class PersonExtension
    {
        /// <summary>
        /// Converts to age.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns>Persons age </returns>
        public static int GetAge(this Person person) => DateTime.Now.Year - person.BirthDate.Year;
    }
}

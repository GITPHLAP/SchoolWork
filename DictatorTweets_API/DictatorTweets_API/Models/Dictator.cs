namespace DictatorTweets_API.Models
{
    public class Dictator
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _birthYear;
        private int _deadYear;
        private string _shortDescription;
        private string _mappningText;

        public Dictator(int id, string firstName, string lastName, int birthYear, int deadYear, string shortDescription, string mappningText)
        {
            this._id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthYear = birthYear;
            DeadYear = deadYear;
            ShortDescription = shortDescription;
            MappningText = mappningText;
        }

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public int BirthYear { get => _birthYear; set => _birthYear = value; }
        public int DeadYear { get => _deadYear; set => _deadYear = value; }
        public string ShortDescription { get => _shortDescription; set => _shortDescription = value; }
        public string MappningText { get => _mappningText; set => _mappningText = value; }
        public int Id { get => _id; }
    }
}

namespace KontaktbuchApi.Model
{
    public class Contacts
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        public Contacts()
        {
            
        }
        public Contacts(int id, string firstName, string lastName, string phoneNumber, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Age = age;

        }


    }
    public class ContactCollection
    {
        public List<Contacts> ContactList { get; set; }
        public Contacts Contact { get; set; }
    }

}

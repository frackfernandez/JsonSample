namespace Json.Net_Sample.Model
{
    public class Contact
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phones { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

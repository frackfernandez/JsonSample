using Json.Net_Sample.Model;
using Newtonsoft.Json;

string _path = @"C:\Users\User\Desktop\Contacts.json";

//var contacts = GetContacts();
//SerializeJsonFile(contacts);

var contacts = GetContactsJsonFromFile();
//DeserializeJsonFile(contacts);

ReadingJsonWithJSONTextReader(contacts);


#region "Writing JSON"
List<Contact> GetContacts()
{
    List<Contact> contacts = new List<Contact>
    {
        new Contact
        {
            Name = "Jhon Peace",
            DateOfBirth = new DateTime(1990, 05, 05),
            Address = new Address
            {
                Street = "Centennial",
                Number = 15,
                City = new City
                {
                    Name = "Chicago",
                    Country = new Country { Code = "USA", Name = "United States"}
                }
            },
            Phones = new List<Phone>
            {
                new Phone { Name = "Personal", Number = "09347813"},
                new Phone { Name = "Work", Number = "894579847"}
            }
        },
        new Contact
        {
            Name = "Sherlock Holmes",
            DateOfBirth = new DateTime(1890, 05, 05),
            Address = new Address
            {
                Street = "Baker",
                Number = 22,
                City = new City
                {
                    Name = "London",
                    Country = new Country { Code = "ENG", Name = "England"}
                }
            },
            Phones = new List<Phone>
            {
                new Phone { Name = "Personal", Number = "0837437"},
                new Phone { Name = "Work", Number = "8956565555"}
            }
        },
        new Contact
        {
            Name = "Paul Anderson",
            DateOfBirth = new DateTime(1990, 06, 25),
            Address = new Address
            {
                Street = "Av. 9 de Julio",
                Number = 22,
                City = new City
                {
                    Name = "Buenos Aires",
                    Country = new Country { Code = "ARG", Name = "Argentina"}
                }
            },
            Phones = new List<Phone>
            {
                new Phone { Name = "Personal", Number = "66564645666"},
                new Phone { Name = "Work", Number = "77473737"}
            }
        }
    };

    return contacts;
}

void SerializeJsonFile(List<Contact> contacts)
{
    string contactsJson = JsonConvert.SerializeObject(contacts.ToArray(), Formatting.Indented);

    File.WriteAllText(_path, contactsJson);
}
#endregion

#region "Reading JSON"
string GetContactsJsonFromFile()
{
    string contactsJsonFromFile;
    using (var reader = new StreamReader(_path))
    {
        contactsJsonFromFile = reader.ReadToEnd();
    }

    return contactsJsonFromFile;
}

void DeserializeJsonFile(string contacsJsonFromFile)
{
    Console.WriteLine(contacsJsonFromFile);
    
    var contacts = JsonConvert.DeserializeObject<List<Contact>>(contacsJsonFromFile);

    Console.WriteLine(string.Format("Paul Anderson's address is: {0} {1}",
        contacts[2].Address.Street, contacts[2].Address.Number));

    Console.WriteLine(string.Format("Jhon Peace's date of birth is: {0}",
        contacts[0].DateOfBirth.ToShortDateString()));
}
#endregion

void ReadingJsonWithJSONTextReader(string contactsJsonFromFile)
{
    JsonTextReader reader = new JsonTextReader(new StringReader(contactsJsonFromFile));

    //while (reader.Read())
    //{
    //    if (reader.Value != null)
    //    {
    //        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
    //    }
    //    else
    //    {
    //        Console.WriteLine("Token: {0}", reader.TokenType);
    //    }
    //}

    string dateofBirth = string.Empty;
    while (reader.Read())
    {
        if (string.IsNullOrEmpty(dateofBirth))
        {
            if (reader.Value != null && reader.TokenType == JsonToken.Date) 
            {
                dateofBirth = DateTime.Parse(reader.Value.ToString()).ToShortDateString();
            }
        }
    }

    Console.WriteLine("Jhon Peak's birthday is on "+ dateofBirth);

}
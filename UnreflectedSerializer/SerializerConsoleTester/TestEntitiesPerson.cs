namespace SerializerConsoleTester
{
    internal class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    internal class Country
    {
        public string Name { get; set; }
        public int AreaCode { get; set; }
    }

    internal class PhoneNumber
    {
        public Country Country { get; set; }
        public int Number { get; set; }
    }

    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; } //if mapped, will cause a MissingSetterException during mapping (deserialization problem without setter)
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }
        public Country CitizenOf { get; set; }
        public PhoneNumber MobilePhone { get; set; }
    }
}

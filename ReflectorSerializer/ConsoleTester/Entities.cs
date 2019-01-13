using System.Collections.Generic;

namespace ConsoleTester
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

        public static Country TestInstance { get; } = new Country
        {
            Name = "Czech Republic",
            AreaCode = 420
        };
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
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }
        public List<Person> Employees { get; } = new List<Person>();
        public Country CitizenOf { get; set; }
        public PhoneNumber MobilePhone { get; set; }

        public static Person TestInstance { get; } = new Person
        {
            FirstName = "Pavel",
            LastName = "Jezek",
            HomeAddress = new Address
            {
                Street = "Patkova",
                City = "Prague"
            },
            WorkAddress = new Address
            {
                Street = "Malostranske namesti",
                City = "Prague"
            },
            CitizenOf = Country.TestInstance,
            MobilePhone = new PhoneNumber
            {
                Country = Country.TestInstance,
                Number = 123456789
            },
            Employees =
            {
                new Person
                {
                    FirstName = "Jiri",
                    LastName = "Vesely",
                    HomeAddress = new Address
                    {
                        Street = "Ctvrtkova",
                        City = "Prague"
                    },
                    WorkAddress = new Address
                    {
                        Street = "Dvorakova",
                        City = "Horni Slavkov"
                    },
                    CitizenOf = Country.TestInstance,
                    MobilePhone = new PhoneNumber
                    {
                        Country = Country.TestInstance,
                        Number = 111222333
                    }
                },
                new Person
                {
                    FirstName = "Jan",
                    LastName = "Kofron",
                    HomeAddress = new Address
                    {
                        Street = "Stredecni",
                        City = "Beroun"
                    },
                    WorkAddress = new Address
                    {
                        Street = "Husitska",
                        City = "Brno"
                    },
                    CitizenOf = Country.TestInstance,
                    MobilePhone = new PhoneNumber
                    {
                        Country = Country.TestInstance,
                        Number = 777666555
                    }
                },
            }
        };
    }
}

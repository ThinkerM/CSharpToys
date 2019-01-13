using System;
using System.IO;
using System.Text;
using UnreflectedSerializer;

namespace SerializerConsoleTester
{
    /// <summary>
    /// https://github.com/ThinkerM/School-Tasks.git
    /// </summary>
    /// <remarks>
    /// Could still use some architectural as well as technical tweaks - e.g. unsure how to handle different mappings being used for serialization and then deserialization -
    /// you'll usually end up with an exception about an unknown xml element, but I guess I could also just ignore them in such case. Also missing checks for whether all the mapped properties 
    /// were found and populated, not sure if that would actually be a meaningful thing to do.
    /// </remarks>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            PersonTest();
        }

        private static IDescriptor<Person> GetPersonDescriptor_FluentlyConfigured()
        {
            return FluentConfigurationSample.GetPersonDescriptor();
        }

        private static IDescriptor<Person> GetPersonDescriptor_ClassBased()
        {
            return new PersonDescriptor(); //from ClassBasedConfigSample
        }

        private static void PersonTest()
        {
            var czechRepublic = new Country
            {
                Name = "Czech Republic",
                AreaCode = 420
            };
            var person = new Person
            {
                FirstName = null, //null will be ignored during (de)serialization
                LastName = string.Empty,
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
                CitizenOf = czechRepublic,
                MobilePhone = new PhoneNumber
                {
                    Country = czechRepublic,
                    Number = 123456789
                }
            };
            var output = new StringBuilder();
            GetPersonDescriptor_FluentlyConfigured().Serialize(person, new StringWriter(output));
            Console.WriteLine(output.ToString());

            var deserialized = GetPersonDescriptor_ClassBased().Deserialize(new StringReader(output.ToString()));

            var outputRoundtrip = new StringBuilder(output.Length);
            GetPersonDescriptor_FluentlyConfigured().Serialize(deserialized, new StringWriter(outputRoundtrip));
            if (output.ToString() != outputRoundtrip.ToString())
                throw new Exception("Houston, We got a serialouz problem.");
        }
    }
}

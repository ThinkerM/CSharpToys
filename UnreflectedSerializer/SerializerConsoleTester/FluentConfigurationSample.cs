using UnreflectedSerializer;

namespace SerializerConsoleTester
{
    internal static class FluentConfigurationSample
    {
        /// <summary>
        /// One way to configure is using a fluent-ish syntax, less boilerplate than mapping through defined classes
        /// </summary>
        /// <returns></returns>
        public static IDescriptor<Person> GetPersonDescriptor()
        {
            var adressDescriptor = new Descriptor<Address>() //address will be mapped multiple times, might wanna define it just once or explicitly create a Descriptor class for it
                .WithProperty(x => x.City)
                .WithProperty(x => x.Street);

            var personDescriptor = new Descriptor<Person>()
                .WithProperty(x => x.FirstName)
                .WithProperty(x => x.FirstName, propertyName: "NotFirstName")
                //.WithProperty(x => x.FirstName) //DuplicateMappingException
                .WithProperty(x => x.LastName) //can re-define the serialization tag if you want, defining a duplicate identifier will cause a DuplicateMappingException
                //.WithProperty(x => x.Age) //no setter on a property throws MissingSetterException (can't deserialize well)
                .WithComponent(x => x.HomeAddress, adressDescriptor)
                .WithComponent(x => x.WorkAddress, adressDescriptor)
                .WithComponent(x => x.MobilePhone, phoneMapping => //mapping a component with no pre-defined descriptor
                {
                    phoneMapping.Property(x => x.Number);
                    phoneMapping.Component(x => x.Country, countryMapping => //nested component
                    {
                        countryMapping.Property(x => x.Name);
                        countryMapping.Property(x => x.AreaCode);
                    });
                })
                .WithComponent(x => x.CitizenOf, new CountryDescriptor()); //using a Descriptor from ClassBased sample
            return personDescriptor;
        }

        public static IDescriptor<Planet> GetPlanetDescriptor()
        {
            return new Descriptor<Planet>()
                .WithProperty(planet => planet.Mass)
                .WithProperty(planet => planet.Name)
                .WithComponent(planet => planet.MostCommonElement, m =>
                {
                    m.Property(element => element.AtomicNumberOrWhatever, propertyName: "A");
                    m.Property(element => element.Id);
                });
        }

        public static IDescriptor<King> GetKingDescriptor() //even though King and Servant reference each other, the cycle can be ommitted in the mapping allowing for partial serialization
        {
            return new Descriptor<King>()
                .WithProperty(x => x.Name)
                .WithProperty(x => x.Age)
                .WithComponent(x => x.Servant, m =>
                {
                    m.Property(x => x.Name);
                });
        }

        public static IDescriptor<Servant> GetServantDescriptor()
        {
            return new Descriptor<Servant>()
                .WithProperty(x => x.Name)
                .WithProperty(x => x.Age)
                .WithComponent(x => x.Liege, m =>
                {
                    m.Property(x => x.Name);
                    m.Property(x => x.Age);
                });
        }
    }
}

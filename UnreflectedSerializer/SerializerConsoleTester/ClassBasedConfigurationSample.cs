
using UnreflectedSerializer;

namespace SerializerConsoleTester
{
    //creating class descriptors by defining individual classes for them
    
    internal class PersonDescriptor : Descriptor<Person>
    {
        public PersonDescriptor()
        {
            Property(x => x.FirstName);
            Property(x => x.FirstName, "NotFirstName"); //valid, same value will just get serialized under multiple tags
            Property(x => x.LastName);
            //Property(x => x.Age); //no setter => MissingSetterException
            Component(x => x.CitizenOf, new CountryDescriptor());
            Component(x => x.HomeAddress, new AddressDescriptor());
            Component(x => x.WorkAddress, new AddressDescriptor()); //could also create a single AddressDesc instance and pass it to all mappings, no problemo
            Component(x => x.MobilePhone, phone => //not gonna define a new descriptor class for this one to demo another way to map components
            {
                phone.Property(x => x.Number);
                phone.Component(x => x.Country, country => 
                // we already have a country descriptor which would normally be reused, just a demo of nested components 
                //(could just map everything this way with no additional classes, but especially if some types get mapped more frequently, you''ll want to make descriptors for them too)
                {
                    country.Property(x => x.Name);
                    country.Property(x => x.AreaCode);
                });
            });
        }
    }

    internal class CountryDescriptor : Descriptor<Country>
    {
        public CountryDescriptor()
        {
            Property(x => x.Name);
            Property(x => x.AreaCode);
        }
    }

    internal class CityOnlyAddressDescriptor : Descriptor<Address>
    {
        public CityOnlyAddressDescriptor()
        {
            Property(x => x.City);
        }
    }

    internal class StreetOnlyAddressDescriptor : Descriptor<Address>
    {
        public StreetOnlyAddressDescriptor()
        {
            Property(x => x.Street);
        }
    }

    internal class AddressDescriptor : Descriptor<Address>
    {
        public AddressDescriptor()
        {
            Property(x => x.City);
            Property(x => x.Street);
        }
    }

    //..etc
}

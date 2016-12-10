using System.Collections.Generic;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml.Tests
{
    public static class TypeDefinitionTestFixtures
    {
        public static List<TypeDefinition> GetTypeDefinitionsSimpleExample()
        {
            var someArrayListMember1 = new Attribute("elementData", "Object[]", VisibilityType.Public);
            var someArrayListMember2 = new Attribute("size()");
            var someArrayListMembers = new List<Attribute>
            {
                someArrayListMember1,
                someArrayListMember2
            };
            var someArrayListSuperClass = new SuperClass("SomeObject");
            var someArrayTypeDefinition = new TypeDefinition(
                "SomeArrayList", 
                someArrayListSuperClass,
                someArrayListMembers
                );

            var someObjectMember1 = new Attribute("equals()");
            var someObjectMembers = new List<Attribute>
            {
                someObjectMember1
            };
            var someObjectTypeDefinition = new TypeDefinition("SomeObject", someObjectMembers);

            return new List<TypeDefinition>
            {
                someArrayTypeDefinition,
                someObjectTypeDefinition
            };
        }

        public static List<TypeDefinition> GetGeneralizationsExample()
        {
            var superType = new SuperClass("SuperType", UmlEntityType.Abstract);
            var subType1 = new TypeDefinition("SubType1", superType);
            var subType2 = new TypeDefinition("SubType2", superType);

            return new List<TypeDefinition>
            {
                subType1, subType2
            };
        }

        public static List<TypeDefinition> GetDependencyExample()
        {
            var dependencies = new List<Dependency>
            {
                new Dependency("Dependency", UmlEntityType.Enum)
            };
            var dependentType = new TypeDefinition("DependendType", dependencies);
            return new List<TypeDefinition>
            {
                dependentType
            };
        }

        public static List<TypeDefinition> GetRealizationExample()
        {
            var realizations = new List<Realization>
            {
                new Realization("Interface", UmlEntityType.Interface)
            };
            var interfaceRealization = new TypeDefinition("InterfaceRealization", realizations);
            return new List<TypeDefinition>
            {
                interfaceRealization
            };
        }
    }
}
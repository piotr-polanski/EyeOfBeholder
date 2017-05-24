using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml.Tests
{
    public static class TypeDefinitionTestFixtures
    {
        public static List<UmlClass> GetTypeDefinitionsSimpleExample()
        {
            var someArrayListSuperClass = new SuperClass("SomeObject",UmlClassType.Abstract);

            var someArrayListAttribute1 = new Attribute("elementData", "SomeObject", VisibilityType.Public);
            var someArrayListAttribute2 = new Attribute("someAttribute", "string", VisibilityType.Private);
            var someArrayListAttributes = new List<Attribute>
            {
                someArrayListAttribute1, someArrayListAttribute2
            };

            var association1 = new Association("elementData", "SomeObject", UmlClassType.Class);
            var association2 = new Association("someAttribute", "string", UmlClassType.Class);
            var associations = new List<Association>
            {
                association1, association2
            };

            var someArrayTypeDefinition = new UmlClass(
                "SomeArrayList",
                VisibilityType.Public,
                associations,
                someArrayListAttributes,
                new List<Realization>(),
                null,
                new List<Dependency>(),
                new List<Operation>());

            var someObjectMember1 = new Operation("equals", "bool", VisibilityType.Public);
            var someObjectOperations = new List<Operation>
            {
                someObjectMember1
            };
            var someObjectTypeDefinition = new UmlClass(
                "SomeObject",
                VisibilityType.Public,
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                null,
                new List<Dependency>(),
                someObjectOperations);

            var someOtherObject = new UmlClass(
                "string",
                VisibilityType.Package,
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                someArrayListSuperClass,
                new List<Dependency>(),
                new List<Operation>());


            return new List<UmlClass>
            {
                someArrayTypeDefinition,
                someObjectTypeDefinition,
                someOtherObject
            };
        }

        public static List<UmlClass> GetGeneralizationsExample()
        {
            var superType = new SuperClass("SuperType", UmlClassType.Abstract);
            var subType1 = new UmlClass(
                "SubType1",
                VisibilityType.Public,
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                superType,
                new List<Dependency>(),
                new List<Operation>());
            var subType2 = new UmlClass(
                "SubType2",
                VisibilityType.Public,
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                superType,
                new List<Dependency>(),
                new List<Operation>());

            return new List<UmlClass>
            {
                subType1, subType2
            };
        }

        public static List<UmlClass> GetDependencyExample()
        {
            var dependencies = new List<Dependency>
            {
                new Dependency("Dependency", UmlClassType.Enum, "relationName")
            };
            var dependentType = new UmlClass(
                "DependendType",
                VisibilityType.Public,
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                null,
                dependencies,
                new List<Operation>());
            return new List<UmlClass>
            {
                dependentType
            };
        }

        public static List<UmlClass> GetRealizationExample()
        {
            var realizations = new List<Realization>
            {
                new Realization("Interface", UmlClassType.Interface)
            };
            var interfaceRealization = new UmlClass(
                "InterfaceRealization",
                VisibilityType.Public,
                new List<Association>(),
                new List<Attribute>(),
                realizations,
                null,
                new List<Dependency>(),
                new List<Operation>());

            return new List<UmlClass>
            {
                interfaceRealization
            };
        }

        public static List<UmlClass> GetAttributesExample()
        {
            var attributes = new List<Attribute>
            {
                new Attribute("publicAttribute", "Object[]", VisibilityType.Public),
                new Attribute("privateAttribute", "String", VisibilityType.Private),
                new Attribute("protectedAttribute", "int", VisibilityType.Protected),
                new Attribute("packagePrivateAttribute", "dupa", VisibilityType.Package),
            };
            var typeDefinitions = new UmlClass(
                "SomeArrayList",
                VisibilityType.Public,
                new List<Association>(),
                attributes,
                new List<Realization>(),
                null,
                new List<Dependency>(),
                new List<Operation>());

            return new List<UmlClass>
            {
                typeDefinitions
            };
        }

        public static List<UmlClass> GetOperationsExample()
        {
            var operations = new List<Operation>
            {
                new Operation("publicOperation", "Object[]", VisibilityType.Public),
                new Operation("privateOperation", "String", VisibilityType.Private),
                new Operation("protectedOperation", "int", VisibilityType.Protected),
                new Operation("packagePrivateOperation", "dupa", VisibilityType.Package),
            };
            var typeDefinitions = new UmlClass(
                "SomeArrayList",
                VisibilityType.Public,
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                null,
                new List<Dependency>(),
                operations);
            return new List<UmlClass>
            {
                typeDefinitions
            };
        }

        public static List<UmlClass> GetAssociationsExample()
        {
            var association1 = new Association("associationAttribute1Name",
                "Association1", UmlClassType.Class);
            var association2 = new Association("associationAttribute2Name",
                "Association2", UmlClassType.Class);
            var associations = new List<Association>
            {
                association1, association2
            };
            return new List<UmlClass>
            {
                new UmlClass(
                    "Class",
                    VisibilityType.Public,
                    associations,
                    new List<Attribute>(),
                    new List<Realization>(),
                    null,
                    new List<Dependency>(),
                    new List<Operation>())
            };
        }
    }
}
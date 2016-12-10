using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml.Tests
{
    public static class TypeDefinitionTestFixtures
    {
        public static List<TypeDefinition> GetTypeDefinitionsSimpleExample()
        {
            var someArrayListSuperClass = new SuperClass("SomeObject",UmlEntityType.Abstract);

            var someArrayListAttribute1 = new Attribute("elementData", "SomeObject", VisibilityType.Public);
            var someArrayListAttribute2 = new Attribute("someAttribute", "string", VisibilityType.Private);
            var someArrayListAttributes = new List<Attribute>
            {
                someArrayListAttribute1, someArrayListAttribute2
            };

            var association1 = new Association("elementData", "SomeObject", VisibilityType.Package, UmlEntityType.Class);
            var association2 = new Association("someAttribute", "string", VisibilityType.Package, UmlEntityType.Class);
            var associations = new List<Association>
            {
                association1, association2
            };

            var someArrayTypeDefinition = new TypeDefinition(
                "SomeArrayList", 
                VisibilityType.Public, 
                associations, 
                someArrayListAttributes, 
                new List<Realization>(), 
                null,
                new List<Dependency>(), 
                new List<Operation>());

            var someObjectMember1 = new Operation("equals()", "bool", VisibilityType.Public);
            var someObjectOperations = new List<Operation>
            {
                someObjectMember1
            };
            var someObjectTypeDefinition = new TypeDefinition(
                "SomeObject",
                VisibilityType.Public, 
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                null,
                new List<Dependency>(),
                someObjectOperations);
            
            var someOtherObject = new TypeDefinition(
                "string", 
                VisibilityType.Package,
                new List<Association>(),
                new List<Attribute>(), 
                new List<Realization>(),
                someArrayListSuperClass,
                new List<Dependency>(), 
                new List<Operation>());


            return new List<TypeDefinition>
            {
                someArrayTypeDefinition,
                someObjectTypeDefinition,
                someOtherObject
            };
        }

        public static List<TypeDefinition> GetGeneralizationsExample()
        {
            var superType = new SuperClass("SuperType", UmlEntityType.Abstract);
            var subType1 = new TypeDefinition(
                "SubType1",
                VisibilityType.Public, 
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                superType,
                new List<Dependency>(),
                new List<Operation>());
            var subType2 = new TypeDefinition(
                "SubType2",
                VisibilityType.Public, 
                new List<Association>(),
                new List<Attribute>(),
                new List<Realization>(),
                superType,
                new List<Dependency>(),
                new List<Operation>());

            return new List<TypeDefinition>
            {
                subType1, subType2
            };
        }

        public static List<TypeDefinition> GetDependencyExample()
        {
            var dependencies = new List<Dependency>
            {
                new Dependency("Dependency", UmlEntityType.Enum, "relationName")
            };
            var dependentType = new TypeDefinition(
                "DependendType",
                VisibilityType.Public, 
                new List<Association>(),
                new List<Attribute>(),  
                new List<Realization>(), 
                null,
                dependencies,
                new List<Operation>());
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
            var interfaceRealization = new TypeDefinition(
                "InterfaceRealization",
                VisibilityType.Public, 
                new List<Association>(), 
                new List<Attribute>(), 
                realizations,
                null,
                new List<Dependency>(),
                new List<Operation>());

            return new List<TypeDefinition>
            {
                interfaceRealization
            };
        }

        public static List<TypeDefinition> GetAttributesExample()
        {
            var attributes = new List<Attribute>
            {
                new Attribute("publicAttribute", "Object[]", VisibilityType.Public),
                new Attribute("privateAttribute", "String", VisibilityType.Private),
                new Attribute("protectedAttribute", "int", VisibilityType.Protected),
                new Attribute("packagePrivateAttribute", "dupa", VisibilityType.Package),
            };
            var typeDefinitions = new TypeDefinition(
                "SomeArrayList", 
                VisibilityType.Public, 
                new List<Association>(), 
                attributes,
                new List<Realization>(),
                null,
                new List<Dependency>(),
                new List<Operation>());

            return new List<TypeDefinition>
            {
                typeDefinitions
            };
        }

        public static List<TypeDefinition> GetOperationsExample()
        {
            var operations = new List<Operation>
            {
                new Operation("publicOperation()", "Object[]", VisibilityType.Public),
                new Operation("privateOperation()", "String", VisibilityType.Private),
                new Operation("protectedOperation()", "int", VisibilityType.Protected),
                new Operation("packagePrivateOperation()", "dupa", VisibilityType.Package),
            };
            var typeDefinitions = new TypeDefinition(
                "SomeArrayList", 
                VisibilityType.Public, 
                new List<Association>(), 
                new List<Attribute>(), 
                new List<Realization>(), 
                null,
                new List<Dependency>(), 
                operations);
            return new List<TypeDefinition>
            {
                typeDefinitions
            };
        }

        public static List<TypeDefinition> GetAssociationsExample()
        {
            var association1 = new Association("associationAttribute1Name",
                "Association1", VisibilityType.Public, UmlEntityType.Class);
            var association2 = new Association("associationAttribute2Name",
                "Association2", VisibilityType.Private, UmlEntityType.Class);
            var associations = new List<Association>
            {
                association1, association2
            };
            return new List<TypeDefinition>
            {
                new TypeDefinition(
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
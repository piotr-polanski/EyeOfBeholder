﻿using System.Collections.Generic;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml.Tests
{
    public static class TypeDefinitionTestFixtures
    {
        public static List<TypeDefinition> GetTypeDefinitionsSimpleExample()
        {
            var someArrayListMember1 = new Member("elementData", "Object[]");
            var someArrayListMember2 = new Member("size()");
            var someArrayListMembers = new List<Member>
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

            var someObjectMember1 = new Member("equals()");
            var someObjectMembers = new List<Member>
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
            var superType = new SuperClass("SuperType", PlantUmlEntityType.Abstract);
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
                new Dependency("Dependency", PlantUmlEntityType.Enum)
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
                new Realization("Interface", PlantUmlEntityType.Interface)
            };
            var interfaceRealization = new TypeDefinition("InterfaceRealization", realizations);
            return new List<TypeDefinition>
            {
                interfaceRealization
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using EyeOfBeholder.Uml.UmlType;
using Ploeh.AutoFixture;
using Xunit;

namespace EyeOfBeholder.Uml.Tests
{
    public class PlantUmlGeneratorTests
    {
        [Fact]
        public void GenerateUmlString_Given_ValidTypeDefinitions_Returns_ValidPlantUmlString()
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();
            var expectedUmlString = File.ReadAllText(@"testData\GeneralPlantUml.puml");
            var typeDefinitions = GetTypeDefinitionsSimpleExample();

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions).ToString();
            var outputWithFixedNewLines = ConvertNewLineCode(plantUmlString, Environment.NewLine);

            //assert
            Assert.Equal(expectedUmlString, outputWithFixedNewLines);
        }

        private static List<TypeDefinition> GetTypeDefinitionsSimpleExample()
        {
            var someArrayListMember1 = new Member("elementData", "Object[]");
            var someArrayListMember2 = new Member("size()");
            var someArrayListMembers = new List<Member>
            {
                someArrayListMember1,
                someArrayListMember2
            };
            var someArrayListSuperClass = new SuperClass("SomeObject");
            var someArrayTypeDefinition = new TypeDefinition("SomeArrayList", someArrayListMembers,
                someArrayListSuperClass);

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

        private string ConvertNewLineCode(string text, string newline)
        {
            var reg = new System.Text.RegularExpressions.Regex("\r\n|\r|\n");
            return reg.Replace(text, newline);
        }
    }
}

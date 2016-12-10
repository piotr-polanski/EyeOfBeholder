using System;
using System.Collections.Generic;
using System.IO;
using EyeOfBeholder.Uml.UmlType;
using Xunit;
using Xunit.Extensions;

namespace EyeOfBeholder.Uml.Tests
{
    public class PlantUmlGeneratorTests
    {
        [Theory, MemberData("GenerateUmlStringTestData")]
        public void GenerateUmlString_Given_ValidTypeDefinitions_Returns_ValidPlantUmlString(List<TypeDefinition> typeDefinitions, string expectedUmlString )
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions);
            var outputWithFixedNewLines = ConvertNewLineCode(plantUmlString, Environment.NewLine);

            //assert
            Assert.Equal(expectedUmlString, outputWithFixedNewLines);
        }

        public static object[] GenerateUmlStringTestData()
        {
            return new object[]
            {
                //new object[] {TypeDefinitionTestFixtures.GetTypeDefinitionsSimpleExample(), File.ReadAllText(@"testData\GeneralPlantUml.puml")},
                new object[] {TypeDefinitionTestFixtures.GetGeneralizationsExample(), File.ReadAllText(@"testData\Generalization.puml")},
                new object[] {TypeDefinitionTestFixtures.GetDependencyExample(), File.ReadAllText(@"testData\Dependency.puml")},
                new object[] {TypeDefinitionTestFixtures.GetRealizationExample(), File.ReadAllText(@"testData\Realization.puml")},
                new object[] {TypeDefinitionTestFixtures.GetAttributesExample(), File.ReadAllText(@"testData\Attributes.puml")},
                new object[] {TypeDefinitionTestFixtures.GetOperationsExample(), File.ReadAllText(@"testData\Operations.puml")},
                new object[] {TypeDefinitionTestFixtures.GetAssociationsExample(), File.ReadAllText(@"testData\Association.puml")},
            };
        }
        
        private string ConvertNewLineCode(string text, string newline)
        {
            var reg = new System.Text.RegularExpressions.Regex("\r\n|\r|\n");
            return reg.Replace(text, newline);
        }
    }
}

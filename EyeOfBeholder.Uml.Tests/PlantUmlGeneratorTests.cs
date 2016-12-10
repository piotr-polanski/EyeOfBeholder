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
            var typeDefinitions = TypeDefinitionFixtures.GetTypeDefinitionsSimpleExample();

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions).ToString();
            var outputWithFixedNewLines = ConvertNewLineCode(plantUmlString, Environment.NewLine);

            //assert
            Assert.Equal(expectedUmlString, outputWithFixedNewLines);
        }
        [Fact]
        public void Generalization_Given_ValidTypeDefinitions_Returns_ValidPlantUmlString()
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();
            var expectedUmlString = File.ReadAllText(@"testData\Generalization.puml");
            var typeDefinitions = TypeDefinitionFixtures.GetGeneralizationsExample();

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions);
            var outputWithFixedNewLines = ConvertNewLineCode(plantUmlString, Environment.NewLine);

            //assert
            Assert.Equal(expectedUmlString, outputWithFixedNewLines);
        }

        [Fact]
        public void Dependency_Given_ValidTypeDefinitions_Returns_ValidPlantUmlString()
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();
            var expectedUmlString = File.ReadAllText(@"testData\Dependency.puml");
            var typeDefinitions = TypeDefinitionFixtures.GetDependencyExample();

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions);
            var outputWithFixedNewLines = ConvertNewLineCode(plantUmlString, Environment.NewLine);

            //assert
            Assert.Equal(expectedUmlString, outputWithFixedNewLines);
        }
        [Fact]
        public void Realization_Given_ValidTypeDefinitions_Returns_ValidPlantUmlString()
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();
            var expectedUmlString = File.ReadAllText(@"testData\Realization.puml");
            var typeDefinitions = TypeDefinitionFixtures.GetRealizationExample();

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions);
            var outputWithFixedNewLines = ConvertNewLineCode(plantUmlString, Environment.NewLine);

            //assert
            Assert.Equal(expectedUmlString, outputWithFixedNewLines);
        }
        private string ConvertNewLineCode(string text, string newline)
        {
            var reg = new System.Text.RegularExpressions.Regex("\r\n|\r|\n");
            return reg.Replace(text, newline);
        }
    }
}

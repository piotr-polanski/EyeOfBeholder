using System;
using System.Collections.Generic;
using System.IO;
using EyeOfBeholder.Uml.UmlType;
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
            var expectedUmlString = File.ReadAllText(@"testData\GeneralPlantUml.txt");
            var typeDefinitions = new List<TypeDefinition>
            {
                new TypeDefinition()
                {
                    Name = "SomeArrayList",
                    Associations = new List<Association>(),
                    Members = new List<Member>
                    {
                        new Member
                        {
                            Name = "elementData",
                            ReturnType = "Object[]"
                        },
                        new Member
                        {
                            Name = "size()"
                        }
                    },
                    Realizations = new List<Realization>(),
                    SuperClass = new SuperClass
                    {
                        Name = "SomeObject"
                    }
                },
                new TypeDefinition
                {
                    Name = "SomeObject",
                    Members = new List<Member>
                    {
                        new Member
                        {
                            Name = "equals()"
                        }
                    }
                }
            };

            //act
            var plantUmlString = plantUmlGenerator.GenerateUmlString(typeDefinitions).ToString();
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

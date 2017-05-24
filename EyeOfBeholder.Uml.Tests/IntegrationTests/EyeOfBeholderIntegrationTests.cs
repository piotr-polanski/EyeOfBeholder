using System.IO;
using Xunit;

namespace EyeOfBeholder.Uml.Tests.IntegrationTests
{
    public class EyeOfBeholderIntegrationTests
    {
        [Fact]
        public void PlantUmlGenerator_Given_ValidCsharpCode_Should_GeneratePlantUmlFile()
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();
            var classesExtractor= new ClassesExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var umlClasses = classesExtractor.GetFrom(codeString);
            var umlString = plantUmlGenerator.GenerateUmlString(umlClasses);

            //assert
            Assert.NotEmpty(umlString);
        }

        [Fact]
        public void PlantUmlGenerator_Given_ValidSlnPath_Should_GeneratePlantUmlFile()
        {
            //arrange
            var plantUmlGenerator = new PlantUmlGenerator();
            var classesExtractor= new ClassesExtractor();
            var slnPath = @"..\..\..\EyeOfBeholder.Uml\EyeOfBeholder.Uml.sln";

            //act
            var umlClasses = classesExtractor.GetFromSolution(slnPath);
            var umlString = plantUmlGenerator.GenerateUmlString(umlClasses);

            //assert
            Assert.NotEmpty(umlString);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using EyeOfBeholder.Uml.UmlStringGenerators;
using FakeItEasy;
using Xunit;

namespace EyeOfBeholder.Uml.Tests.IntegrationTests
{
    public class EyeOfBeholderIntegrationTests
    {
        [Fact]
        public void PlantUmlGenerator_Given_ValidCsharpCode_Should_GeneratePlantUmlFile()
        {
            //arrange
			var umlStringGenerator = new PlantUmlStringGenerator();
            var diagramGenerator = new DiagramGenerator(umlStringGenerator);
            var umlEntitiesExtractor= new UmlEntitiesExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var umlClasses = umlEntitiesExtractor.GetFrom(codeString);
            var umlString = diagramGenerator.GenerateUmlString(umlClasses);

            //assert
            Assert.NotEmpty(umlString);
        }

        [Fact]
        public void PlantUmlGenerator_Given_ValidSlnPath_Should_GeneratePlantUmlFile()
        {
            //arrange
			var umlStringGenerator = new PlantUmlStringGenerator();
            var diagramGenerator = new DiagramGenerator(umlStringGenerator);
            var umlEntitiesExtractor = new UmlEntitiesExtractor();
			var slnPath = @"..\..\..\EyeOfBeholder.Uml\EyeOfBeholder.Uml.sln";
			//var slnPath = @"C:\Sources\Repos\b2b_platform\B2BPlatform.sln";
			var projectsToExtract = new List<string>()
			{
				//"B2BPlatform.Orders"
				"EyeOfBeholder.Uml"
			};

            //act
            var umlContainers = umlEntitiesExtractor.GetFromSolution(slnPath, projectsToExtract);
            var umlString = diagramGenerator.GenerateUmlString(umlContainers.SelectMany(c => c.UmlClasses).ToList());

            //assert
            Assert.NotEmpty(umlString);
        }

	    [Fact]
	    public void NomnomlStringGenerator_Given_ValidSlnPath_Should_GenerateNomnomlFile()
	    {
		    //arrange
		    var umlStringGenerator = new NomnomlStringGenerator();
		    var diagramGenerator = new DiagramGenerator(umlStringGenerator);
		    var umlEntitiesExtractor = new UmlEntitiesExtractor();
		    //var slnPath = @"..\..\..\EyeOfBeholder.Uml\EyeOfBeholder.Uml.sln";
		    var slnPath = @"C:\Sources\Repos\b2b_platform\B2BPlatform.sln";
			var projectsToExtract = new List<string>()
			{
				"B2BPlatform.Orders"
			};

		    //act
		    var umlContainers = umlEntitiesExtractor.GetFromSolution(slnPath, projectsToExtract);
		    var umlString = diagramGenerator.GenerateUmlString(umlContainers.SelectMany(c => c.UmlClasses).ToList());

		    //assert
		    Assert.NotEmpty(umlString);
	    }
	}
}

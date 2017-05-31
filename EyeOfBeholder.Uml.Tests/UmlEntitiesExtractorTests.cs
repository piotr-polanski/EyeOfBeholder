using System.Collections.Generic;
using System.IO;
using System.Linq;
using EyeOfBeholder.Uml.UmlType;
using Xunit;

namespace EyeOfBeholder.Uml.Tests
{
    public class UmlEntitiesExtractorTests
    {
        [Fact]
        public void GetFrom_Given_validCSharpCode_Return_UmlClasses()
        {
            //arrange
            var umlEntitiesExtractor= new UmlEntitiesExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var umlClasses = umlEntitiesExtractor.GetFrom(codeString);

            //assert
            Assert.NotEmpty(umlClasses);
        }

        [Fact]
        public void GetFrom_Given_SimpleClass_Return_UmlClassRepresentation()
        {
            //arrange
            var umlEntitiesExtractor= new UmlEntitiesExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var umlClasses = umlEntitiesExtractor.GetFrom(codeString);

            //assert
            Assert.Equal(3, umlClasses.Count());
            var simpleClass = umlClasses.First();
            Assert.Equal("SimpleClass", simpleClass.Name);

            Assert.Equal(2, simpleClass.Attributes.Count);
            var firstAttribute = simpleClass.Attributes.First();
            Assert.Equal("name", firstAttribute.Name);
            Assert.Equal("SomeBaseType", firstAttribute.Type);
            Assert.Equal(VisibilityType.Private, firstAttribute.VisibilityType);
            var secondAttribute = simpleClass.Attributes.Last();
            Assert.Equal("Number", secondAttribute.Name);
            Assert.Equal("Int32", secondAttribute.Type);
            Assert.Equal(VisibilityType.Public, secondAttribute.VisibilityType);


            Assert.Equal(2, simpleClass.Associations.Count);
            var firstAssociation = simpleClass.Associations.First();
            Assert.Equal("SomeBaseType", firstAssociation.TypeName);
            Assert.Equal("name", firstAssociation.Name);
            Assert.Equal(UmlClassType.Class, firstAssociation.UmlClassType);

            var secondAssociation = simpleClass.Associations.Last();
            Assert.Equal("ISomeInterface", secondAssociation.TypeName);
            Assert.Equal("GenericMethod", secondAssociation.Name);
            Assert.Equal(UmlClassType.Class, secondAssociation.UmlClassType);

            Assert.Equal(1, simpleClass.Dependencies.Count);
            var firstDependency = simpleClass.Dependencies.First();
            Assert.Equal("SomeBaseType", firstDependency.TypeName);
            Assert.Equal("newName", firstDependency.RelationName);
            Assert.Equal(UmlClassType.Class, firstDependency.Type);

            Assert.Equal(2, simpleClass.Operations.Count);
            var firstOperation = simpleClass.Operations.First();
            Assert.Equal("SomeMethod", firstOperation.Name);
            Assert.Equal("Boolean", firstOperation.ReturnType);
            Assert.Equal(VisibilityType.Public, firstOperation.VisibilityType);

	        var secondOperation = simpleClass.Operations.Last();
			Assert.Equal("GenericMethod", secondOperation.Name);
            Assert.Equal("ISomeInterface", secondOperation.ReturnType);
            Assert.Equal(VisibilityType.Public, secondOperation.VisibilityType);

            Assert.Equal(1, simpleClass.Realizations.Count);
            var firstRealization = simpleClass.Realizations.First();
            Assert.Equal("ISomeInterface", firstRealization.Name);
            Assert.Equal(UmlClassType.Interface, firstRealization.Type);

            Assert.Equal("SomeBaseType", simpleClass.SuperClass.Name);
            Assert.Equal(UmlClassType.Class, simpleClass.Type);
            Assert.Equal(VisibilityType.Public, simpleClass.VisibilityType);

            var secondClass = umlClasses.Skip(1).First();
            Assert.Equal("SomeBaseType", secondClass.Name);
            Assert.Empty(secondClass.Associations);
            Assert.Empty(secondClass.Attributes);
            Assert.Empty(secondClass.Dependencies);
            Assert.Empty(secondClass.Operations);
            Assert.Empty(secondClass.Realizations);
            Assert.Null(secondClass.SuperClass);

            var @interface = umlClasses.Skip(2).First();
            Assert.Equal("ISomeInterface", @interface.Name);
            Assert.Empty(@interface.Associations);
            Assert.Empty(@interface.Attributes);
            Assert.Empty(@interface.Dependencies);
            Assert.Empty(@interface.Operations);
            Assert.Empty(@interface.Realizations);
            Assert.Null(@interface.SuperClass);

        }

	    [Fact]
	    public void GetFromSolution_Given_ValidSolutionPath_Returns_UmlClasses()
	    {
		    //arrange
			var umlEntitiesExtractor = new UmlEntitiesExtractor();
		    var solutionPath = @"../../../EyeOfBeholder.Uml/EyeOfBeholder.Uml.sln";
			var projectNames = new List<string>()
			{
				"EyeOfBeholder.Uml", "EyeOfBeholder.Uml.Tests"
			};


		    //act
		    var umlClasses = umlEntitiesExtractor.GetFromSolution(solutionPath, projectNames);

		    //assert
			Assert.NotEmpty(umlClasses);
	    }

	    [Fact]
	    public void GetFromSolution_Given_ProjectNames_Returns_UmlClassesOnlyFromThoseProjects()
	    {
		    //arrange
			var umlEntitiesExtractor = new UmlEntitiesExtractor();
		    var solutionPath = @"../../../EyeOfBeholder.Uml/EyeOfBeholder.Uml.sln";
			var projectNames = new List<string>()
			{
				"EyeOfBeholder.Uml"
			};

		    //act
		    var umlContainers = umlEntitiesExtractor.GetFromSolution(solutionPath, projectNames);

		    //assert
			Assert.NotEmpty(umlContainers);
			Assert.Equal(1, umlContainers.Count);
	    }
    }
}

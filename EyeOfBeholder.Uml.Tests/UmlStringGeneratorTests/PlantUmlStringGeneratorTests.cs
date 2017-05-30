using System;
using System.Collections.Generic;
using EyeOfBeholder.Uml.UmlStringGenerators;
using EyeOfBeholder.Uml.UmlType;
using Xunit;
using Attribute = EyeOfBeholder.Uml.UmlType.Attribute;

namespace EyeOfBeholder.Uml.Tests.UmlStringGeneratorTests
{
	public class PlantUmlStringGeneratorTests
	{
		[Fact]
		public void GenerataTypeDefinition_Given_ValidTypeDefinition_Return_PlantUmlTypeDefinitionString()
		{
			//arrange
			var plantUmlStringGenerator = new PlantUmlStringGenerator();

			var umlClass = new UmlClass("TypeDefinition", VisibilityType.Public, new List<Association>(), new List<Attribute>()
			{
				new Attribute("attribute", "", VisibilityType.Public)
			}, 
			new List<Realization>(), 
			null, 
			new List<Dependency>(), 
			new List<Operation>()
			{
				new Operation("operation", "", VisibilityType.Public)
			});
			var expectedString = @"class TypeDefinition{
+ attribute : 
+ operation() : 
}
";

			//act
			var typeDefinitionString = plantUmlStringGenerator.GetTypeDefinitionString(umlClass);

			//assert
			Assert.Equal(expectedString, typeDefinitionString);
		}

		[Fact]
		public void GetRealizations_Given_ValidUmlClass_Returns_UmlRealizationString()
		{
			//arrange
			var plantUmlStringGenerator = new PlantUmlStringGenerator();
			var realizations = new List<Realization>()
			{
				new Realization("realization1", UmlClassType.Class),
				new Realization("realization2", UmlClassType.Abstract),
				new Realization("realization3", UmlClassType.Enum),
				new Realization("realization4", UmlClassType.Interface),
			};
			var umlClass = new UmlClass(
				"ClassName",
				VisibilityType.Public,
				new List<Association>(),
				new List<Attribute>(),
				realizations,
				null,
				new List<Dependency>(), 
				new List<Operation>()
			);

			var expectedString = @"class realization1
realization1 <|.. ClassName
abstract realization2
realization2 <|.. ClassName
enum realization3
realization3 <|.. ClassName
interface realization4
realization4 <|.. ClassName
";

			//act
			var realizationsUmlString = plantUmlStringGenerator.GetRealizations(umlClass);

			//assert
			Assert.Equal(expectedString, realizationsUmlString);
		}

		[Fact]
		public void GetAssociations_Given_ValidUmlClass_Returns_AssoctiationUmlString()
		{
			//arrange
			var plantUmlStringGenerator = new PlantUmlStringGenerator();
			var associations = new List<Association>()
			{
				new Association("association1", "TypeName1", UmlClassType.Class),
				new Association("association2", "TypeName2", UmlClassType.Abstract),
				new Association("association3", "TypeName3", UmlClassType.Enum),
				new Association("association4", "TypeName4", UmlClassType.Interface),
			};
			var umlClass = new UmlClass(
				"ClassName",
				VisibilityType.Public,
				associations,
				new List<Attribute>(),
				new List<Realization>(), 
				null,
				new List<Dependency>(),
				new List<Operation>()
			);

			var expectedString = @"class TypeName1
ClassName --> TypeName1 : association1
abstract TypeName2
ClassName --> TypeName2 : association2
enum TypeName3
ClassName --> TypeName3 : association3
interface TypeName4
ClassName --> TypeName4 : association4
";

			//act
			var associationUmlString = plantUmlStringGenerator.GetAssociations(umlClass);

			//assert
			Assert.Equal(expectedString, associationUmlString);
		}

		[Fact]
		public void GetSuperClass_Given_ValidUmlClassWithSuperClass_Returns_SuperClassUmlString()
		{
			//arrange
			var plantUmlStringGenerator = new PlantUmlStringGenerator();
			var superClass = new SuperClass("SuperClass", UmlClassType.Class);
			var umlClass = new UmlClass(
				"ClassName",
				VisibilityType.Public,
				new List<Association>(), 
				new List<Attribute>(),
				new List<Realization>(), 
				superClass,
				new List<Dependency>(),
				new List<Operation>()
			);

			var expectedString = @"class SuperClass
SuperClass <|-- ClassName
";

			//act
			var superClassUmlString = plantUmlStringGenerator.GetSuperClass(umlClass);

			//assert
			Assert.Equal(expectedString, superClassUmlString);
		}

		[Fact]
		public void GetSuperClass_Given_ValidUmlClassWithoutSuperClass_Returns_EmptyString()
		{
			//arrange
			var plantUmlStringGenerator = new PlantUmlStringGenerator();
			var umlClass = new UmlClass(
				"ClassName",
				VisibilityType.Public,
				new List<Association>(), 
				new List<Attribute>(),
				new List<Realization>(), 
				null,
				new List<Dependency>(),
				new List<Operation>()
			);

			var expectedString = String.Empty;

			//act
			var superClassUmlString = plantUmlStringGenerator.GetSuperClass(umlClass);

			//assert
			Assert.Equal(expectedString, superClassUmlString);
		}

		[Fact]
		public void GetDependecies_Given_ValidUmlClass_Returns_DependenciesUmlString()
		{
			//arrange
			var plantUmlStringGenerator = new PlantUmlStringGenerator();
			var dependencies = new List<Dependency>()
			{
				new Dependency( "TypeName1", UmlClassType.Class, "dependency1"),
			};
			var umlClass = new UmlClass(
				"ClassName",
				VisibilityType.Public,
				new List<Association>(), 
				new List<Attribute>(),
				new List<Realization>(),
				null,
				dependencies,
				new List<Operation>()
			);

			var expectedString = @"class TypeName1
TypeName1 <.. ClassName : dependency1
";

			//act
			var dependenciesUmlString = plantUmlStringGenerator.GetDependencies(umlClass);

			//assert
			Assert.Equal(expectedString, dependenciesUmlString);
		}
	}
}

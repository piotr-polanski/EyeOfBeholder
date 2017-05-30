using System;
using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlStringGenerators;
using EyeOfBeholder.Uml.UmlType;
using Xunit;
using Attribute = EyeOfBeholder.Uml.UmlType.Attribute;

namespace EyeOfBeholder.Uml.Tests.UmlStringGeneratorTests
{
	public class NomnomlStringGeneratorTests
	{
		[Fact]
		public void GenerataTypeDefinition_Given_ValidTypeDefinition_Return_NomnomlTypeDefinitionString()
		{
			//arrange
			var nomnomlStringGenerator = new NomnomlStringGenerator();

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
			var expectedString = @"[<class> TypeDefinition|+ attribute : |+ operation() : ]
";

			//act
			var typeDefinitionString = nomnomlStringGenerator.GetTypeDefinitionString(umlClass);

			//assert
			Assert.Equal(expectedString, typeDefinitionString);
		}

		[Fact]
		public void GetRealizations_Given_ValidUmlClass_Returns_UmlRealizationString()
		{
			//arrange
			var nomnomlStringGenerator = new NomnomlStringGenerator();
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

			var expectedString = @"[<class> realization1]
[realization1] <:-- [ClassName]
[<abstract> realization2]
[realization2] <:-- [ClassName]
[<enum> realization3]
[realization3] <:-- [ClassName]
[<interface> realization4]
[realization4] <:-- [ClassName]
";

			//act
			var realizationsUmlString = nomnomlStringGenerator.GetRealizations(umlClass);

			//assert
			Assert.Equal(expectedString, realizationsUmlString);
		}

		[Fact]
		public void GetAssociations_Given_ValidUmlClass_Returns_AssoctiationUmlString()
		{
			//arrange
			var nomnomlStringGenerator = new NomnomlStringGenerator();
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

			var expectedString = @"[<class> TypeName1]
[ClassName] -> association1 [TypeName1]
[<abstract> TypeName2]
[ClassName] -> association2 [TypeName2]
[<enum> TypeName3]
[ClassName] -> association3 [TypeName3]
[<interface> TypeName4]
[ClassName] -> association4 [TypeName4]
";

			//act
			var associationUmlString = nomnomlStringGenerator.GetAssociations(umlClass);

			//assert
			Assert.Equal(expectedString, associationUmlString);
		}

		[Fact]
		public void GetSuperClass_Given_ValidUmlClassWithSuperClass_Returns_SuperClassUmlString()
		{
			//arrange
			var nomnomlStringGenerator = new NomnomlStringGenerator();
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

			var expectedString = @"[<class> SuperClass]
[SuperClass] <:- [ClassName]
";

			//act
			var superClassUmlString = nomnomlStringGenerator.GetSuperClass(umlClass);

			//assert
			Assert.Equal(expectedString, superClassUmlString);
		}

		[Fact]
		public void GetSuperClass_Given_ValidUmlClassWithoutSuperClass_Returns_EmptyString()
		{
			//arrange
			var nomnomlStringGenerator = new NomnomlStringGenerator();
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
			var superClassUmlString = nomnomlStringGenerator.GetSuperClass(umlClass);

			//assert
			Assert.Equal(expectedString, superClassUmlString);
		}

		[Fact]
		public void GetDependecies_Given_ValidUmlClass_Returns_DependenciesUmlString()
		{
			//arrange
			var nomnomlStringGenerator = new NomnomlStringGenerator();
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

			var expectedString = @"[<class> TypeName1]
[TypeName1] <-- dependency1 [ClassName]
";

			//act
			var dependenciesUmlString = nomnomlStringGenerator.GetDependencies(umlClass);

			//assert
			Assert.Equal(expectedString, dependenciesUmlString);
		}
	}

	public class NomnomlStringGenerator : IUmlStringGenerator
	{
		public string StartString { get; } = new StringBuilder().AppendLine("@startuml").ToString();
		public string EndString { get; } = new StringBuilder().Append("@enduml").ToString();

		public string GetTypeDefinitionString(UmlClass umlClass)
		{
			var nomnomlString = new StringBuilder();

			var typeDefinitionType = GetUmlClassTypeName(umlClass.Type);
			nomnomlString.Append(
				$"[<{typeDefinitionType}> {umlClass.Name}|");


			foreach (var attribute in umlClass.Attributes)
			{
				var operationVisibility = GetVisibility(attribute.VisibilityType);
				nomnomlString.Append($"{operationVisibility} {attribute.Name} : " +
				                          $"{attribute.Type}");

			}
			nomnomlString.Append("|");
			foreach (var operation in umlClass.Operations)
			{
				var operationVisibility = GetVisibility(operation.VisibilityType);
				nomnomlString.Append($"{operationVisibility} {operation.Name}() : " +
				                          $"{operation.ReturnType}");
			}
			nomnomlString.AppendLine("]");

			return nomnomlString.ToString();
		}

		public string GetAssociations(UmlClass umlClass)
		{
			var umlString = new StringBuilder();
			foreach (var association in umlClass.Associations)
			{
				var umlClassType = GetUmlClassTypeName(association.UmlClassType);
				umlString.AppendLine($"[<{umlClassType}> {association.TypeName}]");
				umlString.AppendLine(
					$"[{umlClass.Name}] -> {association.Name} [{association.TypeName}]");
			}
			return umlString.ToString();
		}

		public string GetSuperClass(UmlClass umlClass)
		{
			var umlString = new StringBuilder();
			if (umlClass.SuperClass != null)
			{
				var superClassTypeName = GetUmlClassTypeName(umlClass.SuperClass.Type);
				umlString.AppendLine($"[<{superClassTypeName}> {umlClass.SuperClass.Name}]");
				umlString.AppendLine($"[{umlClass.SuperClass.Name}] <:- [{umlClass.Name}]");
			}
			return umlString.ToString();
		}

		public string GetDependencies(UmlClass umlClass)
		{
			var umlString = new StringBuilder();
			foreach (var typeDefinitionDependency in umlClass.Dependencies)
			{
				var dependencyTypeName = GetUmlClassTypeName(typeDefinitionDependency.Type);
				umlString.AppendLine($"[<{dependencyTypeName}> {typeDefinitionDependency.TypeName}]");
				umlString.AppendLine(
					$"[{typeDefinitionDependency.TypeName}] <-- {typeDefinitionDependency.RelationName} [{umlClass.Name}]");
			}
			return umlString.ToString();
		}

		public string GetRealizations(UmlClass umlClass)
		{
			var nomnomlString = new StringBuilder();
			foreach (var typeDefinitionRealization in umlClass.Realizations)
			{
				var realizationTypeName = GetUmlClassTypeName(typeDefinitionRealization.Type);
				nomnomlString.AppendLine($"[<{realizationTypeName}> {typeDefinitionRealization.Name}]");
				nomnomlString.AppendLine($"[{typeDefinitionRealization.Name}] <:-- [{umlClass.Name}]");
			}
			return nomnomlString.ToString();
		}

		private string GetVisibility(VisibilityType visibilityType)
		{
			switch (visibilityType)
			{
				case VisibilityType.Public:
					return "+";
				case VisibilityType.Package:
					return "~";
				case VisibilityType.Private:
					return "-";
				case VisibilityType.Protected:
					return "#";
				default:
					return "+";
			}
		}

		private string GetUmlClassTypeName(UmlClassType type)
		{
			switch (type)
			{
				case UmlClassType.Interface:
					return "interface";
				case UmlClassType.Abstract:
					return "abstract";
				case UmlClassType.Class:
					return "class";
				case UmlClassType.Enum:
					return "enum";
				default:
					return "class";
			}

		}
	}
}

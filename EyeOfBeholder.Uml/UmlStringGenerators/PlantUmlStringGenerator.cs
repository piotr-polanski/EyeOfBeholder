using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml.UmlStringGenerators
{
	public class PlantUmlStringGenerator : IUmlStringGenerator 
	{
		public string StartString { get; } = new StringBuilder().AppendLine("@startuml").ToString();
		public string EndString { get; } = new StringBuilder().Append("@enduml").ToString();

		public string GetTypeDefinitionString(UmlClass umlClass)
		{
			var plantUmlString = new StringBuilder();

			var typeDefinitionType = GetUmlClassTypeName(umlClass.Type);
			plantUmlString.AppendLine(
				$"{typeDefinitionType} {umlClass.Name}{{");


			foreach (var attribute in umlClass.Attributes)
			{
				var operationVisibility = GetVisibility(attribute.VisibilityType);
				plantUmlString.AppendLine($"{operationVisibility} {attribute.Name} : " +
				                          $"{attribute.Type}");

			}
			foreach (var operation in umlClass.Operations)
			{
				var operationVisibility = GetVisibility(operation.VisibilityType);
				plantUmlString.AppendLine($"{operationVisibility} {operation.Name}() : " +
				                          $"{operation.ReturnType}");
			}
			plantUmlString.AppendLine("}");

			return plantUmlString.ToString();
		}

		public string GetAssociations(UmlClass umlClass)
		{
			var umlString = new StringBuilder();
			foreach (var association in umlClass.Associations)
			{
				var umlClassType = GetUmlClassTypeName(association.UmlClassType);
				umlString.AppendLine($"{umlClassType} {association.TypeName}");
				umlString.AppendLine(
					$"{umlClass.Name} --> {association.TypeName} : {association.Name}");
			}
			return umlString.ToString();
		}

		public string GetSuperClass(UmlClass umlClass)
		{
			var umlString = new StringBuilder();
			if (umlClass.SuperClass != null)
			{
				var superClassTypeName = GetUmlClassTypeName(umlClass.SuperClass.Type);
				umlString.AppendLine($"{superClassTypeName} {umlClass.SuperClass.Name}");
				umlString.AppendLine($"{umlClass.SuperClass.Name} <|-- {umlClass.Name}");
			}
			return umlString.ToString();
		}

		public string GetDependencies(UmlClass umlClass)
		{
			var umlString = new StringBuilder();
			foreach (var typeDefinitionDependency in umlClass.Dependencies)
			{
				var dependencyTypeName = GetUmlClassTypeName(typeDefinitionDependency.Type);
				umlString.AppendLine($"{dependencyTypeName} {typeDefinitionDependency.TypeName}");
				umlString.AppendLine(
					$"{typeDefinitionDependency.TypeName} <.. {umlClass.Name} : {typeDefinitionDependency.RelationName}");
			}
			return umlString.ToString();
		}

		public string GetRealizations(UmlClass umlClass)
		{
			var plantUmlString = new StringBuilder();
			foreach (var typeDefinitionRealization in umlClass.Realizations)
			{
				var realizationTypeName = GetUmlClassTypeName(typeDefinitionRealization.Type);
				plantUmlString.AppendLine($"{realizationTypeName} {typeDefinitionRealization.Name}");
				plantUmlString.AppendLine($"{typeDefinitionRealization.Name} <|.. {umlClass.Name}");
			}
			return plantUmlString.ToString();
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
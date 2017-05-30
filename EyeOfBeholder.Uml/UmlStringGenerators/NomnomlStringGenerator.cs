using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml.UmlStringGenerators
{
	public class NomnomlStringGenerator : IUmlStringGenerator
	{
		public string StartString { get;} 
		public string EndString { get;} 

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
				nomnomlString.Append(";");

			}
			RemoveLastCharacterFrom(nomnomlString); 

			nomnomlString.Append("|");
			foreach (var operation in umlClass.Operations)
			{
				var operationVisibility = GetVisibility(operation.VisibilityType);
				nomnomlString.Append($"{operationVisibility} {operation.Name}() : " +
				                     $"{operation.ReturnType}");
				nomnomlString.Append(";");
			}
			RemoveLastCharacterFrom(nomnomlString);
			nomnomlString.AppendLine("]");

			return nomnomlString.ToString();
		}

		private static void RemoveLastCharacterFrom(StringBuilder nomnomlString)
		{
			nomnomlString.Length--;
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
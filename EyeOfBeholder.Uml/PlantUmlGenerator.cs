using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml
{
    public class PlantUmlGenerator
    {
        public string GenerateUmlString(List<TypeDefinition> typeDefinitions)
        {
            var umlString = new StringBuilder();
            umlString.Append("@startuml").AppendLine();
            foreach (var typeDefinition in typeDefinitions)
            {
                AppendSuperClassIfExist(typeDefinition, umlString);
                var typeDefinitionType = GetEntityTypeName(typeDefinition.Type);
                umlString.Append(
                    $"{typeDefinitionType} {typeDefinition.Name} {{").AppendLine();
                foreach (var typeDefinitionAttribute in typeDefinition.Attributes)
                {
                    var attributeVisibility = GetAttributeVisibilityFrom(typeDefinitionAttribute.VisibilityType);
                    umlString.Append($"{attributeVisibility} {typeDefinitionAttribute.Name} : " +
                                     $"{typeDefinitionAttribute.TypeName}")
                        .AppendLine();
                }
                foreach (var typeDefinitionOperation in typeDefinition.Operations)
                {
                    var operationVisibility = GetAttributeVisibilityFrom(typeDefinitionOperation.VisibilityType);
                    umlString.Append($"{operationVisibility} {typeDefinitionOperation.Name} : " +
                                     $"{typeDefinitionOperation.ReturnTypeName}")
                        .AppendLine();
                }
                umlString.Append("}").AppendLine();
                foreach (var typeDefinitionDependency in typeDefinition.Dependencies)
                {
                    var dependencyTypeName = GetEntityTypeName(typeDefinitionDependency.Type);
                    umlString.Append($"{dependencyTypeName} {typeDefinitionDependency.ClassName} <.. {typeDefinition.Name} : {typeDefinitionDependency.RelationName}")
                        .AppendLine();
                }
                foreach (var typeDefinitionRealization in typeDefinition.Realizations)
                {
                    var realizationTypeName = GetEntityTypeName(typeDefinitionRealization.Type);
                    umlString.Append($"{realizationTypeName} {typeDefinitionRealization.Name} <|.. {typeDefinition.Name}")
                        .AppendLine();
                }

            }
            umlString.Append("@enduml");
            return umlString.ToString();
        }

        private string GetAttributeVisibilityFrom(VisibilityType visibilityType)
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

        private string GetEntityTypeName(UmlEntityType type)
        {
            switch (type)
            {
                case UmlEntityType.Interface:
                    return "interface";
                case UmlEntityType.Abstract:
                    return "abstract";
                case UmlEntityType.Class:
                    return "class";
                case UmlEntityType.Enum:
                    return "enum";
                default:
                    return "class";
            }
        }

        private void AppendSuperClassIfExist(TypeDefinition typeDefinition, StringBuilder umlString)
        {
            if (typeDefinition.SuperClass != null)
            {
                var superClassTypeName = GetEntityTypeName(typeDefinition.SuperClass.Type);
                umlString.Append($"{superClassTypeName} {typeDefinition.SuperClass.Name} <|-- {typeDefinition.Name}")
                    .AppendLine();
            }
        }
    }
}
using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml
{
    public class PlantUmlGenerator
    {
        public string GenerateUmlString(List<UmlClass> typeDefinitions)
        {
            var umlString = new StringBuilder();
            umlString.Append("@startuml").AppendLine();
            foreach (var typeDefinition in typeDefinitions)
            {
                foreach (var association in typeDefinition.Associations)
                {
                    var associationTypeName = GetEntityTypeName(association.Type);
                    umlString.Append($"{associationTypeName} {typeDefinition.Name} --> {association.BaseTypeName} : {association.AttributeName}")
                        .AppendLine();
                }
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

        private string GetEntityTypeName(UmlClassType type)
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

        private void AppendSuperClassIfExist(UmlClass umlClass, StringBuilder umlString)
        {
            if (umlClass.SuperClass != null)
            {
                var superClassTypeName = GetEntityTypeName(umlClass.SuperClass.Type);
                umlString.Append($"{superClassTypeName} {umlClass.SuperClass.Name} <|-- {umlClass.Name}")
                    .AppendLine();
            }
        }
    }
}
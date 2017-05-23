using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml
{
    public class PlantUmlGenerator
    {
        private readonly StringBuilder umlString = new StringBuilder();
        public string GenerateUmlString(List<UmlClass> typeDefinitions)
        {
            umlString.Append("@startuml").AppendLine();

            foreach (var typeDefinition in typeDefinitions)
            {
                AppendAssociations(typeDefinition);
                AppendSuperClassIfExist(typeDefinition);
                AppendTypeDefinitionName(typeDefinition);
                AppendAttributes(typeDefinition);
                AppendOperations(typeDefinition);
                AppendDependecies(typeDefinition);
                AppendRealizations(typeDefinition);

            }

            umlString.Append("@enduml");
            return umlString.ToString();
        }

        private void AppendRealizations(UmlClass typeDefinition)
        {
            foreach (var typeDefinitionRealization in typeDefinition.Realizations)
            {
                var realizationTypeName = GetEntityTypeName(typeDefinitionRealization.Type);
                umlString.AppendLine($"{realizationTypeName} {typeDefinitionRealization.Name}");
                umlString.AppendLine($"{typeDefinitionRealization.Name} <|.. {typeDefinition.Name}");
            }
        }

        private void AppendDependecies(UmlClass typeDefinition)
        {
            umlString.Append("}").AppendLine();
            foreach (var typeDefinitionDependency in typeDefinition.Dependencies)
            {
                var dependencyTypeName = GetEntityTypeName(typeDefinitionDependency.Type);
                umlString.AppendLine($"{dependencyTypeName} {typeDefinitionDependency.ClassName}");
                umlString.AppendLine(
                    $"{typeDefinitionDependency.ClassName} <.. {typeDefinition.Name} : {typeDefinitionDependency.RelationName}");
            }
        }

        private void AppendOperations(UmlClass typeDefinition)
        {
            foreach (var typeDefinitionOperation in typeDefinition.Operations)
            {
                var operationVisibility = GetAttributeVisibilityFrom(typeDefinitionOperation.VisibilityType);
                umlString.AppendLine($"{operationVisibility} {typeDefinitionOperation.Name} : " +
                                 $"{typeDefinitionOperation.ReturnTypeName}");
            }
        }

        private void AppendAttributes(UmlClass typeDefinition)
        {
            foreach (var typeDefinitionAttribute in typeDefinition.Attributes)
            {
                var attributeVisibility = GetAttributeVisibilityFrom(typeDefinitionAttribute.VisibilityType);
                umlString.AppendLine($"{attributeVisibility} {typeDefinitionAttribute.Name} : " +
                                     $"{typeDefinitionAttribute.TypeName}");
            }
        }

        private void AppendTypeDefinitionName(UmlClass typeDefinition)
        {
            var typeDefinitionType = GetEntityTypeName(typeDefinition.Type);
            umlString.AppendLine(
                $"{typeDefinitionType} {typeDefinition.Name} {{");
        }

        private void AppendAssociations(UmlClass typeDefinition)
        {
            foreach (var association in typeDefinition.Associations)
            {
                var associationTypeName = GetEntityTypeName(association.Type);
                umlString.AppendLine($"{associationTypeName} {typeDefinition.Name}");
                umlString.AppendLine(
                    $"{typeDefinition.Name} --> {association.BaseTypeName} : {association.AttributeName}");
            }
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

        private void AppendSuperClassIfExist(UmlClass umlClass)
        {
            if (umlClass.SuperClass != null)
            {
                var superClassTypeName = GetEntityTypeName(umlClass.SuperClass.Type);
                umlString.AppendLine($"{superClassTypeName} {umlClass.SuperClass.Name}");
                umlString.AppendLine($"{umlClass.SuperClass.Name} <|-- {umlClass.Name}");
            }
        }
    }
}
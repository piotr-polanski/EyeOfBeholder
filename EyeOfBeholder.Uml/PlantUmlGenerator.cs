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
                umlString.Append($"{realizationTypeName} {typeDefinitionRealization.Name} <|.. {typeDefinition.Name}")
                    .AppendLine();
            }
        }

        private void AppendDependecies(UmlClass typeDefinition)
        {
            umlString.Append("}").AppendLine();
            foreach (var typeDefinitionDependency in typeDefinition.Dependencies)
            {
                var dependencyTypeName = GetEntityTypeName(typeDefinitionDependency.Type);
                umlString.Append(
                        $"{dependencyTypeName} {typeDefinitionDependency.ClassName} <.. {typeDefinition.Name} : {typeDefinitionDependency.RelationName}")
                    .AppendLine();
            }
        }

        private void AppendOperations(UmlClass typeDefinition)
        {
            foreach (var typeDefinitionOperation in typeDefinition.Operations)
            {
                var operationVisibility = GetAttributeVisibilityFrom(typeDefinitionOperation.VisibilityType);
                umlString.Append($"{operationVisibility} {typeDefinitionOperation.Name} : " +
                                 $"{typeDefinitionOperation.ReturnTypeName}")
                    .AppendLine();
            }
        }

        private void AppendAttributes(UmlClass typeDefinition)
        {
            foreach (var typeDefinitionAttribute in typeDefinition.Attributes)
            {
                var attributeVisibility = GetAttributeVisibilityFrom(typeDefinitionAttribute.VisibilityType);
                umlString.Append($"{attributeVisibility} {typeDefinitionAttribute.Name} : " +
                                 $"{typeDefinitionAttribute.TypeName}")
                    .AppendLine();
            }
        }

        private void AppendTypeDefinitionName(UmlClass typeDefinition)
        {
            var typeDefinitionType = GetEntityTypeName(typeDefinition.Type);
            umlString.Append(
                $"{typeDefinitionType} {typeDefinition.Name} {{").AppendLine();
        }

        private void AppendAssociations(UmlClass typeDefinition)
        {
            foreach (var association in typeDefinition.Associations)
            {
                var associationTypeName = GetEntityTypeName(association.Type);
                umlString.Append(
                        $"{associationTypeName} {typeDefinition.Name} --> {association.BaseTypeName} : {association.AttributeName}")
                    .AppendLine();
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
                umlString.Append($"{superClassTypeName} {umlClass.SuperClass.Name} <|-- {umlClass.Name}")
                    .AppendLine();
            }
        }
    }
}
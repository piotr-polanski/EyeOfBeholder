﻿using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml
{
    public class PlantUmlGenerator
    {
        public PlantUmlGenerator()
        {
        }

        public string GenerateUmlString(List<TypeDefinition> typeDefinitions)
        {
            var umlString = new StringBuilder();
            umlString.Append("@startuml\n");
            foreach (var typeDefinition in typeDefinitions)
            {
                AppendSuperClassIfExist(typeDefinition, umlString);
                foreach (var typeDefinitionMember in typeDefinition.Members)
                {
                    umlString.Append(
                        $"{typeDefinition.Name} : {typeDefinitionMember.TypeName} {typeDefinitionMember.Name}")
                        .AppendLine();
                }
                foreach (var typeDefinitionDependency in typeDefinition.Dependencies)
                {
                    var dependencyTypeName = GetEntityTypeName(typeDefinitionDependency.Type);
                    umlString.Append($"{dependencyTypeName} {typeDefinitionDependency.Name} <.. {typeDefinition.Name}")
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
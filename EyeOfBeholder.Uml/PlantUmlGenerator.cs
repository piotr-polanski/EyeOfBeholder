using System.Collections.Generic;
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
                        $"{typeDefinition.Name} : {typeDefinitionMember.ReturnType} {typeDefinitionMember.Name}")
                        .AppendLine();
                }
                foreach (var typeDefinitionDependency in typeDefinition.Dependencies)
                {
                    umlString.Append($"{typeDefinitionDependency.Name} <.. {typeDefinition.Name}")
                        .AppendLine();
                }
            }
            umlString.Append("@enduml");
            return umlString.ToString();
        }

        private static void AppendSuperClassIfExist(TypeDefinition typeDefinition, StringBuilder umlString)
        {
            if (typeDefinition.SuperClass != null)
            {
                umlString.Append($"{typeDefinition.SuperClass.Name} <|-- {typeDefinition.Name}\n");
            }
        }
    }
}
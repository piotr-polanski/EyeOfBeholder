using System.Collections.Generic;

namespace EyeOfBeholder.Uml.UmlType
{
    public class TypeDefinition
    {
        public TypeDefinition(
            string name,
            VisibilityType visibilityType,
            List<Association> associations, 
            List<Attribute> attributes, 
            List<Realization> realizations, 
            SuperClass superClass,
            List<Dependency> dependencies,
            List<Operation> operations
            )
        {
            Associations = associations;
            Attributes = attributes;
            Realizations = realizations;
            SuperClass = superClass;
            Dependencies = dependencies;
            Operations = operations;
            Name = name;
        }

        
        public List<Association> Associations { get;}
        public List<Attribute> Attributes { get;}
        public List<Realization> Realizations { get;}
        public SuperClass SuperClass { get;}
        public string Name { get;}
        public VisibilityType VisibilityType { get; set; }
        public List<Dependency> Dependencies { get;}
        public List<Operation> Operations { get;}
        public UmlEntityType Type { get;}
    }
}
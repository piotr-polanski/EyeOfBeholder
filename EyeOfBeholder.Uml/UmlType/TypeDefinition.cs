using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace EyeOfBeholder.Uml.UmlType
{
    public class TypeDefinition
    {
        public TypeDefinition(string name)
        {
            Name = name;
            Associations = new List<Association>();
            Attributes = new List<Attribute>();
            Operations = new List<Operation>();
            Realizations = new List<Realization>();
        }

        public TypeDefinition(string name, List<Dependency> dependencies)
        {
            Name = name;
            Dependencies = dependencies;
            Associations = new List<Association>();
            Attributes = new List<Attribute>();
            Realizations = new List<Realization>();
            Operations = new List<Operation>();
        }

        public TypeDefinition(string name, SuperClass superClass)
        {
            Name = name;
            SuperClass = superClass;
            Associations = new List<Association>();
            Attributes = new List<Attribute>();
            Realizations = new List<Realization>();
            Dependencies = new List<Dependency>();
            Operations = new List<Operation>();

        }
        public TypeDefinition(string name, List<Attribute> attributes)
        {
            Name = name;
            Attributes = attributes;
            Associations = new List<Association>();
            Realizations = new List<Realization>();
            Dependencies = new List<Dependency>();
            Operations = new List<Operation>();
        }

        public TypeDefinition(string name, SuperClass superClass, List<Attribute> attributes)
        {
            Name = name;
            Attributes = attributes;
            SuperClass = superClass;
            Associations = new List<Association>();
            Realizations = new List<Realization>();
            Dependencies = new List<Dependency>();
            Operations = new List<Operation>();
        }
        public TypeDefinition(
            string name,
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

        public TypeDefinition(string name, List<Realization> realizations)
        {
            Name = name;
            Realizations = realizations;
            Associations = new List<Association>();
            Attributes = new List<Attribute>();
            Dependencies = new List<Dependency>();
            Operations = new List<Operation>();
        }

        public TypeDefinition(string name, List<Operation> operations)
        {
            Name = name;
            Operations = operations;
            Associations = new List<Association>();
            Attributes = new List<Attribute>();
            Dependencies = new List<Dependency>();
            Realizations = new List<Realization>();
        }


        public List<Association> Associations { get;}
        public List<Attribute> Attributes { get;}
        public List<Realization> Realizations { get;}
        public SuperClass SuperClass { get;}
        public string Name { get;}
        public List<Dependency> Dependencies { get;}
        public List<Operation> Operations { get;}
        public UmlEntityType Type { get;}
    }
}
using System.Collections.Generic;

namespace EyeOfBeholder.Uml.UmlType
{
    public class TypeDefinition
    {
        public TypeDefinition(
            string name,
            List<Association> associations, 
            List<Member> members, 
            List<Realization> realizations, 
            SuperClass superClass
            )
        {
            Associations = associations;
            Members = members;
            Realizations = realizations;
            SuperClass = superClass;
            Name = name;
        }

        public TypeDefinition(string name, List<Member> members)
        {
            Name = name;
            Members = members;
            Associations = new List<Association>();
            Realizations = new List<Realization>();
        }

        public TypeDefinition(string name, List<Member> members, SuperClass superClass)
        {
            Name = name;
            Members = members;
            SuperClass = superClass;
        }

        public List<Association> Associations { get; private set; }
        public List<Member> Members { get; private set; }
        public List<Realization> Realizations { get; private set; }
        public SuperClass SuperClass { get; private set; }
        public string Name { get; private set; }
    }
}
using System.Collections.Generic;

namespace EyeOfBeholder.Uml.UmlType
{
    public class TypeDefinition
    {
        public List<Association> Associations { get; set; }
        public List<Member> Members { get; set; }
        public List<Realization> Realizations { get; set; }
        public SuperClass SuperClass { get; set; }
        public string Name { get; set; }
    }
}
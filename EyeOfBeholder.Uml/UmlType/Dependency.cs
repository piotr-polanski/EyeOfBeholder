namespace EyeOfBeholder.Uml.UmlType
{
    public class Dependency
    {
        public Dependency(string typeName, UmlClassType type, string relationName)
        {
            TypeName = typeName;
            Type = type;
            RelationName = relationName;
        }

        public string TypeName { get;}
        public UmlClassType Type { get; }
        public string RelationName { get; }
    }
}
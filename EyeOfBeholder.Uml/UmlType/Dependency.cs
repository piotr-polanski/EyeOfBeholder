namespace EyeOfBeholder.Uml.UmlType
{
    public class Dependency
    {
        public Dependency(string className, UmlEntityType type, string relationName)
        {
            ClassName = className;
            Type = type;
            RelationName = relationName;
        }

        public string ClassName { get;}
        public UmlEntityType Type { get; }
        public string RelationName { get; }
    }
}
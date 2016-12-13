namespace EyeOfBeholder.Uml.UmlType
{
    public class Dependency
    {
        public Dependency(string className, UmlClassType type, string relationName)
        {
            ClassName = className;
            Type = type;
            RelationName = relationName;
        }

        public string ClassName { get;}
        public UmlClassType Type { get; }
        public string RelationName { get; }
    }
}
namespace EyeOfBeholder.Uml.UmlType
{
    public class Dependency
    {
        public Dependency(string name, UmlEntityType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get;}
        public UmlEntityType Type { get; }
    }
}
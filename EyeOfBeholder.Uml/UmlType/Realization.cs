namespace EyeOfBeholder.Uml.UmlType
{
    public class Realization
    {
        public string Name { get; private set; }
        public UmlEntityType Type { get; private set; }

        public Realization(string name, UmlEntityType type)
        {
            Name = name;
            Type = type;
        }
    }
}
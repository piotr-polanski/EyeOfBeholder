namespace EyeOfBeholder.Uml.UmlType
{
    public class Realization
    {
        public string Name { get; private set; }
        public UmlClassType Type { get; private set; }

        public Realization(string name, UmlClassType type)
        {
            Name = name;
            Type = type;
        }
    }
}
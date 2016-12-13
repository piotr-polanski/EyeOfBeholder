namespace EyeOfBeholder.Uml.UmlType
{
    public class SuperClass
    {
        public SuperClass(string name, UmlClassType type)
        {
            Name = name;
            Type = type;
        }

        public SuperClass(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public UmlClassType Type { get; private set; }
    }
}
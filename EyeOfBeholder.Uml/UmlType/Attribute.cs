namespace EyeOfBeholder.Uml.UmlType
{
    public class Attribute
    {
        public Attribute(
            string name, 
            string returnType, 
            VisibilityType visibilityType)
        {
            Name = name;
            Type = returnType;
            VisibilityType = visibilityType;
        }

        public Attribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public VisibilityType VisibilityType { get; private set; }
    }
}
namespace EyeOfBeholder.Uml.UmlType
{
    public class Attribute
    {
        public Attribute(
            string name, 
            string attributeTypeName, 
            VisibilityType visibilityType)
        {
            Name = name;
            TypeName = attributeTypeName;
            VisibilityType = visibilityType;
        }

        public Attribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public VisibilityType VisibilityType { get; private set; }
    }
}
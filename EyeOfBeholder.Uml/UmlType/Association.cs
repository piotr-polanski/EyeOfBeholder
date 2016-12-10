namespace EyeOfBeholder.Uml.UmlType
{
    public class Association
    {
        public string AttributeName { get;}
        public string BaseTypeName { get;}
        public VisibilityType VisibilityType { get;}
        public UmlEntityType Type { get;}

        public Association(string attributeName, string baseTypeName, 
            VisibilityType visibilityType, UmlEntityType type)
        {
            AttributeName = attributeName;
            BaseTypeName = baseTypeName;
            VisibilityType = visibilityType;
            Type = type;
        }
    }
}
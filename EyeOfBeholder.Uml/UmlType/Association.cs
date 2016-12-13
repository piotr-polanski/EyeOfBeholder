namespace EyeOfBeholder.Uml.UmlType
{
    public class Association
    {
        public string AttributeName { get;}
        public string BaseTypeName { get;}
        public UmlClassType Type { get;}

        public Association(string attributeName, string baseTypeName, 
             UmlClassType type)
        {
            AttributeName = attributeName;
            BaseTypeName = baseTypeName;
            Type = type;
        }
    }
}
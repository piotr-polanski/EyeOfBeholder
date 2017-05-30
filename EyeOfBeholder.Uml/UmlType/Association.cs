namespace EyeOfBeholder.Uml.UmlType
{
    public class Association
    {
        public string Name { get;}
        public string TypeName { get;}
        public UmlClassType UmlClassType { get;}

        public Association(string associationName, string associationTypeName, 
             UmlClassType umlClassUmlClassType)
        {
            Name = associationName;
            TypeName = associationTypeName;
            UmlClassType = umlClassUmlClassType;
        }
    }
}
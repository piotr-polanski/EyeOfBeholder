namespace EyeOfBeholder.Uml.UmlType
{
    public class Operation
    {
        public string Name { get; private set; }
        public string ReturnTypeName { get; private set; }
        public VisibilityType VisibilityType { get; private set; }

        public Operation(string name, 
            string returnTypeName, 
            VisibilityType visibilityType)
        {
            Name = name;
            ReturnTypeName = returnTypeName;
            VisibilityType = visibilityType;
        }
    }
}
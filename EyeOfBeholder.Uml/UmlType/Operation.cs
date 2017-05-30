namespace EyeOfBeholder.Uml.UmlType
{
    public class Operation
    {
        public string Name { get; private set; }
        public string ReturnType { get; private set; }
        public VisibilityType VisibilityType { get; private set; }

        public Operation(string name, 
            string returnType, 
            VisibilityType visibilityType)
        {
            Name = name;
            ReturnType = returnType;
            VisibilityType = visibilityType;
        }
    }
}
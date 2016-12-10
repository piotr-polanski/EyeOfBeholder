namespace EyeOfBeholder.Uml.UmlType
{
    public class Member
    {
        public Member(string name, string returnType)
        {
            Name = name;
            ReturnType = returnType;
        }

        public Member(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public string ReturnType { get; private set; }
    }
}
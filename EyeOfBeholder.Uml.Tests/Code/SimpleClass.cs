namespace EyeOfBeholder.Uml.Tests.Code
{
    public class SimpleClass : SomeBaseType, ISomeInterface
    {
        private SomeBaseType name;
    
        public int Number { get; set; }

        public bool SomeMethod()
        {
            return true;
        }

        public SimpleClass(SomeBaseType newName)
        {
            name = newName;
        }
    }

    public class SomeBaseType
    {
    }

    public interface ISomeInterface
    {
    }
}

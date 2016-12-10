namespace EyeOfBeholder.Uml.UmlType
{
    public class Dependency
    {
        public Dependency(string name, PlantUmlEntityType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get;}
        public PlantUmlEntityType Type { get; }
    }
}
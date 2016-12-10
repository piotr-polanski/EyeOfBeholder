namespace EyeOfBeholder.Uml.UmlType
{
    public class Realization
    {
        public string Name { get; private set; }
        public PlantUmlEntityType Type { get; private set; }

        public Realization(string name, PlantUmlEntityType type)
        {
            Name = name;
            Type = type;
        }
    }
}
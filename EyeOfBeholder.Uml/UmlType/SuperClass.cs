﻿namespace EyeOfBeholder.Uml.UmlType
{
    public class SuperClass
    {
        public SuperClass(string name, UmlEntityType type)
        {
            Name = name;
            Type = type;
        }

        public SuperClass(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public UmlEntityType Type { get; private set; }
    }
}
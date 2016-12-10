﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EyeOfBeholder.Uml.UmlType
{
    public class TypeDefinition
    {
        public TypeDefinition(string name)
        {
            Name = name;
            Associations = new List<Association>();
            Members = new List<Member>();
        }

        public TypeDefinition(string name, List<Dependency> dependencies)
        {
            Dependencies = dependencies;
            Associations = new List<Association>();
            Members = new List<Member>();
            Realizations = new List<Realization>();
        }

        public TypeDefinition(string name, SuperClass superClass)
        {
            Name = name;
            SuperClass = superClass;
            Associations = new List<Association>();
            Members = new List<Member>();
            Realizations = new List<Realization>();
            Dependencies = new List<Dependency>();

        }
        public TypeDefinition(string name, List<Member> members)
        {
            Name = name;
            Members = members;
            Associations = new List<Association>();
            Realizations = new List<Realization>();
            Dependencies = new List<Dependency>();
        }

        public TypeDefinition(string name, SuperClass superClass, List<Member> members)
        {
            Name = name;
            Members = members;
            SuperClass = superClass;
            Associations = new List<Association>();
            Realizations = new List<Realization>();
            Dependencies = new List<Dependency>();
        }
        public TypeDefinition(
            string name,
            List<Association> associations, 
            List<Member> members, 
            List<Realization> realizations, 
            SuperClass superClass,
            List<Dependency> dependencies
            )
        {
            Associations = associations;
            Members = members;
            Realizations = realizations;
            SuperClass = superClass;
            Dependencies = dependencies;
            Name = name;
        }

        public TypeDefinition(string name, List<Realization> realizations)
        {
            Name = name;
            Realizations = realizations;
            Associations = new List<Association>();
            Members = new List<Member>();
            Dependencies = new List<Dependency>();
        }


        public List<Association> Associations { get; private set; }
        public List<Member> Members { get; private set; }
        public List<Realization> Realizations { get; private set; }
        public SuperClass SuperClass { get; private set; }
        public string Name { get; private set; }
        public List<Dependency> Dependencies { get; private set; }
    }
}
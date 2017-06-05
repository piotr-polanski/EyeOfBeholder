using System;
using System.Collections.Generic;
using System.Linq;
using EyeOfBeholder.Uml.UmlType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Attribute = EyeOfBeholder.Uml.UmlType.Attribute;

namespace EyeOfBeholder.Uml
{
    public class UmlEntitiesExtractor
    {

        public List<UmlContainer> GetFromSolution(string pathToSolution, List<string> projectNames)
        {
			var umlContainers = new List<UmlContainer>();

            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(pathToSolution).Result;
			workspace.WorkspaceFailed += (o, e) =>
			{
				var ee = e;
			};

	        foreach (var project in solution.Projects)
	        {
		        if (!projectNames.Contains(project.Name))
		        {
					continue;
				}
				var umlContainer = new UmlContainer();
				var classes = new List<UmlClass>();
				foreach (var document in project.Documents)
				{
					var rootNode = document.GetSyntaxRootAsync().Result;
					var semanticModel = document.GetSemanticModelAsync().Result;

					var myClasses = rootNode.DescendantNodes().OfType<ClassDeclarationSyntax>();
					classes.AddRange(GetClassess(project.Name, myClasses, semanticModel, (CompilationUnitSyntax)rootNode));

					var enums = rootNode.DescendantNodes().OfType<EnumDeclarationSyntax>();
					foreach (var @enum in enums)
					{
						var e = semanticModel.GetDeclaredSymbol(@enum);
						var visibiltiy = GetVisibilityType(e.DeclaredAccessibility);
						var members = @enum.Members;
						var atts = new List<Attribute>();
						foreach (var member in members)
						{
							atts.Add(new Attribute(member.Identifier.ValueText));
						}
						classes.Add(new UmlClass(@enum.Identifier.ValueText, visibiltiy, new List<Association>(), atts, new List<Realization>(), null, new List<Dependency>(), new List<Operation>()));
					}
					var structs = rootNode.DescendantNodes().OfType<StructDeclarationSyntax>();
					classes.AddRange(GetClassess(project.Name, structs, semanticModel, (CompilationUnitSyntax)rootNode));

				}
				umlContainer.UmlClasses = classes;
				umlContainers.Add(umlContainer);
		        
	        }
            return umlContainers;
        }
        public List<UmlClass> GetFrom(string codeString)
        {
            var classes = new List<UmlClass>();
            var tree = CSharpSyntaxTree.ParseText(codeString);
            tree.GetRoot();
            var syntaxRoot = (CompilationUnitSyntax)tree.GetRoot();

            var compilation = CSharpCompilation.Create("SimpleClass")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree);

            var model = compilation.GetSemanticModel(tree);

            var myClasses = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();

            classes.AddRange(GetClassess("",myClasses, model, syntaxRoot));

            return classes;
        }

        private IEnumerable<UmlClass> GetClassess(string projectName, IEnumerable<TypeDeclarationSyntax> myClasses, SemanticModel model, CompilationUnitSyntax syntaxRoot)
        {
            List<UmlClass> classes = new List<UmlClass>();
            foreach (var classDeclarationSyntax in myClasses)
            {
                var classSymbol = model.GetDeclaredSymbol(classDeclarationSyntax);
	            if (classSymbol.ContainingAssembly.Name != projectName)
	            {
		            continue;
	            }
                var visibility = classSymbol.DeclaredAccessibility;
                var visibilityType = GetVisibilityType(visibility);
                var name = classDeclarationSyntax.Identifier.ValueText;

                var attributes = new List<Attribute>();
                var associations = new List<Association>();
                AddAttributesAndAssociations(projectName, classSymbol, attributes, associations, model);

                var realizations = new List<Realization>();
                AddRealizations(classSymbol, realizations);

                var operations = new List<Operation>();
                AddOperations(classSymbol, operations, associations);

                SuperClass superClass = null;
                superClass = SetSuperClass(classSymbol, superClass);


                var deps = new List<Dependency>();
                deps = GetDependecies(classSymbol, deps);

                var umlClass = new UmlClass(
                    name,
                    visibilityType,
                    associations,
                    attributes,
                    realizations,
                    superClass,
                    deps,
                    operations
                );

                classes.Add(umlClass);
            }

            var interfaces = syntaxRoot.DescendantNodes().OfType<InterfaceDeclarationSyntax>();
            foreach (var @interface in interfaces)
            {
                var interfaceSymbol = model.GetDeclaredSymbol(@interface);
                var visibility = interfaceSymbol.DeclaredAccessibility;
                var visibilityType = GetVisibilityType(visibility);
                var name = @interface.Identifier.ValueText;

                var attributes = new List<Attribute>();
                var associations = new List<Association>();
                AddAttributesAndAssociations(projectName, interfaceSymbol, attributes, associations, model);

                var realizations = new List<Realization>();
                AddRealizations(interfaceSymbol, realizations);

                var operations = new List<Operation>();
                AddOperations(interfaceSymbol, operations, associations);

                SuperClass superClass = null;
                superClass = SetSuperClass(interfaceSymbol, superClass);


                var deps = new List<Dependency>();
                deps = GetDependecies(interfaceSymbol, deps);

                var umlClass = new UmlClass(
                    name,
                    visibilityType,
                    associations,
                    attributes,
                    realizations,
                    superClass,
                    deps,
                    operations
                );

                classes.Add(umlClass);
            }


            return classes;
        }

        private static List<Dependency> GetDependecies(INamedTypeSymbol classSymbol, List<Dependency> deps)
        {
            var ctors = classSymbol.Constructors;
            var ctorArgumentsDistinct = new Dictionary<string, Dependency>();
            foreach (var ctor in ctors)
            {
                foreach (var parameter in ctor.Parameters)
                {
                    if (!ctorArgumentsDistinct.ContainsKey(parameter.Name) &&
                        !parameter.Type.ContainingNamespace.Name.StartsWith("System"))
                    {
                        var depName = parameter.Name;
                        var depTypeName = parameter.Type.Name;
						if (!((INamedTypeSymbol) parameter.Type).TypeArguments.IsEmpty)
						{
							depTypeName = ((INamedTypeSymbol)parameter.Type).TypeArguments[0].Name;
						}
                        var depType = UmlClassType.Class;
                        if (parameter.Type.IsReferenceType)
                        {
                            depType = UmlClassType.Class;
                        }

                        ctorArgumentsDistinct[parameter.Name] = new Dependency(depTypeName, depType, depName);
                    }
                }
            }

            deps = ctorArgumentsDistinct.Select(a => a.Value).ToList();
            return deps;
        }

        private SuperClass SetSuperClass(INamedTypeSymbol classSymbol, SuperClass superClass)
        {
            if (classSymbol.BaseType != null && !classSymbol.BaseType.ContainingNamespace.Name.StartsWith("System"))
            {
                var baseClassName1 = classSymbol.BaseType.Name;
                var superClassType = classSymbol.BaseType.TypeKind;

                superClass = new SuperClass(baseClassName1, GetUmlClassType(superClassType));
            }
            return superClass;
        }

        private void AddOperations(INamedTypeSymbol classSymbol, List<Operation> operations, List<Association> associations)
        {
            var methods = classSymbol.GetMembers().OfType<IMethodSymbol>()
                .Where(m => m.MethodKind != MethodKind.Constructor
                            && m.MethodKind != MethodKind.PropertyGet && m.MethodKind != MethodKind.PropertySet);
            foreach (var method in methods)
            {
                //var op = semanticModel.GetDeclaredSymbol(method);
                var opname = method.Name;
                var opVisibility = method.DeclaredAccessibility;
                var returnType = method.ReturnType.Name;

	            //generics
	    //        if (method.ReturnType.TypeKind != TypeKind.TypeParameter && !((INamedTypeSymbol) method.ReturnType).TypeArguments.IsEmpty)
	    //        {
		   //         returnType = ((INamedTypeSymbol) method.ReturnType).TypeArguments[0].Name;
		   //         if (!((INamedTypeSymbol) method.ReturnType).ContainingNamespace.Name.StartsWith("System"))
		   //         {
			  //          var propAss = new Association(opname, returnType, UmlClassType.Class);
			  //          //associations.Add(propAss);
		   //         }
	    //        }
	    //        else
	    //        {
					//if (!method.ReturnType.ContainingNamespace.Name.StartsWith("System"))
					//{
					//	var propAss = new Association(opname, returnType, UmlClassType.Class);
					//	//associations.Add(propAss);
					//}
	    //        }
				//rawOperation.DescendantNodes().OfType<PredefinedTypeSyntax>().First().Keyword.ValueText;
				var opvisibility = GetVisibilityType(opVisibility);
                var operation = new Operation(opname, returnType, opvisibility);
                operations.Add(operation);
			}
        }

        private static void AddRealizations(INamedTypeSymbol classSymbol, List<Realization> realizations)
        {
            foreach (var @interface in classSymbol.Interfaces)
            {
                var realizationName = @interface.Name;
                realizations.Add(new Realization(realizationName, UmlClassType.Interface));
            }
        }

        private void AddAttributesAndAssociations(string projectName, INamedTypeSymbol classSymbol, List<Attribute> attributes, List<Association> associations, SemanticModel semanticModel)
        {
            var fids = classSymbol.GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsImplicitlyDeclared);
            foreach (var field in fids)
            {
                //var f = semanticModel.GetDeclaredSymbol(field);
                var attributeName = field.Name;
                var attributeType = field.Type.Name; //.Declaration.DescendantNodes().OfType<IdentifierNameSyntax>().First()
                var attributeVisibility = field.DeclaredAccessibility;
                var vis = GetVisibilityType(attributeVisibility);
                var attribute = new Attribute(attributeName, attributeType, vis);
                attributes.Add(attribute);

                if (field.Type.ContainingNamespace != null && !field.Type.ContainingNamespace.Name.StartsWith("System")
					&& field.Type.ContainingNamespace.Name != string.Empty)
                {
	                if (!associations.Any(a => a.TypeName == attributeType))
	                {
						var association = new Association(attributeName, attributeType, UmlClassType.Class);
						associations.Add(association);
	                }
                }
            }

            var props = classSymbol.GetMembers().OfType<IPropertySymbol>();
            foreach (var property in props)
            {
				var propertyName = property.Name;
	            var propertyType = property.Type.Name;
				//generics
	            try
	            {
					if (!((INamedTypeSymbol) property.Type).TypeArguments.IsEmpty)
					{
						propertyType = ((INamedTypeSymbol)property.Type).TypeArguments[0].Name;
					}

	            }
	            catch (Exception e)
	            {
					//TODO log exception
		            //throw;
	            }
                var propVisibility = property.DeclaredAccessibility;
                var visType = GetVisibilityType(propVisibility);
                var prop = new Attribute(propertyName, propertyType, visType);
                attributes.Add(prop);

                if (property.Type.ContainingNamespace != null && !property.Type.ContainingNamespace.Name.StartsWith("System")
					)
                {
	                if (!associations.Any(a => a.TypeName == propertyType))
	                {
		                var propAss = new Association(propertyName, propertyType, UmlClassType.Class);
		                associations.Add(propAss);
	                }
                }
            }
        }

        private UmlClassType GetUmlClassType(TypeKind typeKind)
        {
            return UmlClassType.Class;
        }

        private VisibilityType GetVisibilityType(Accessibility visibility)
        {
            switch (visibility)
            {
                case Accessibility.Private:
                    return VisibilityType.Private;
                case Accessibility.Protected:
                    return VisibilityType.Protected;
                case Accessibility.Internal:
                    return VisibilityType.Package;
                default: return VisibilityType.Public;
            }
        }
    }
}
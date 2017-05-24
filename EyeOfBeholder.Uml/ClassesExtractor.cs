using System.Collections.Generic;
using System.Linq;
using EyeOfBeholder.Uml.UmlType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EyeOfBeholder.Uml
{
    public class ClassesExtractor
    {
        public IEnumerable<UmlClass> GetFrom(string codeString)
        {
            var classes = new List<UmlClass>();
            var tree = CSharpSyntaxTree.ParseText(codeString);
            tree.GetRoot();
            var syntaxRoot = (CompilationUnitSyntax)tree.GetRoot();

            var compilation = CSharpCompilation.Create("HelloWorld")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree);

            var model = compilation.GetSemanticModel(tree);

            var myClasses = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();



            foreach (var classDeclarationSyntax in myClasses)
            {
                var classSymbol = model.GetDeclaredSymbol(classDeclarationSyntax);
                var visibility = classSymbol.DeclaredAccessibility;
                var visibilityType = GetVisibilityType(visibility);
                var name = classDeclarationSyntax.Identifier.ValueText;

                var attributes = new List<Attribute>();
                var associations = new List<Association>();
                AddAttributesAndAssociations(classSymbol, attributes, associations);

                var realizations = new List<Realization>();
                AddRealizations(classSymbol, realizations);

                var operations = new List<Operation>();
                AddOperations(classSymbol, operations);

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

            var @interfaces = syntaxRoot.DescendantNodes().OfType<InterfaceDeclarationSyntax>();
            foreach (var @interface in @interfaces)
            {
                var @interfaceSymbol = model.GetDeclaredSymbol(@interface);
                var visibility = @interfaceSymbol.DeclaredAccessibility;
                var visibilityType = GetVisibilityType(visibility);
                var name = @interface.Identifier.ValueText;

                var attributes = new List<Attribute>();
                var associations = new List<Association>();
                AddAttributesAndAssociations(@interfaceSymbol, attributes, associations);

                var realizations = new List<Realization>();
                AddRealizations(@interfaceSymbol, realizations);

                var operations = new List<Operation>();
                AddOperations(@interfaceSymbol, operations);

                SuperClass superClass = null;
                superClass = SetSuperClass(@interfaceSymbol, superClass);


                var deps = new List<Dependency>();
                deps = GetDependecies(@interfaceSymbol, deps);

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

        private void AddOperations(INamedTypeSymbol classSymbol, List<Operation> operations)
        {
            var methods = classSymbol.GetMembers().OfType<IMethodSymbol>()
                .Where(m => m.MethodKind != MethodKind.Constructor
                            && m.MethodKind != MethodKind.PropertyGet && m.MethodKind != MethodKind.PropertySet);
            foreach (var method in methods)
            {
                //var op = model.GetDeclaredSymbol(method);
                var opname = method.Name;
                var opVisibility = method.DeclaredAccessibility;
                var returnType = method.ReturnType.Name;
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

        private void AddAttributesAndAssociations(INamedTypeSymbol classSymbol, List<Attribute> attributes, List<Association> associations)
        {
            var fids = classSymbol.GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsImplicitlyDeclared);
            foreach (var field in fids)
            {
                //var f = model.GetDeclaredSymbol(field);
                var attributeName = field.Name;
                var attributeType = field.Type.Name; //.Declaration.DescendantNodes().OfType<IdentifierNameSyntax>().First()
                var attributeVisibility = field.DeclaredAccessibility;
                var vis = GetVisibilityType(attributeVisibility);
                var attribute = new Attribute(attributeName, attributeType, vis);
                attributes.Add(attribute);

                var association = new Association(attributeName, attributeType, UmlClassType.Class);
                associations.Add(association);
            }

            var props = classSymbol.GetMembers().OfType<IPropertySymbol>();
            foreach (var property in props)
            {
                //var pr = model.GetDeclaredSymbol(property);
                var propertyName = property.Name;
                var propertyType = property.Type.Name;
                var propVisibility = property.DeclaredAccessibility;
                var visType = GetVisibilityType(propVisibility);
                var prop = new Attribute(propertyName, propertyType, visType);
                attributes.Add(prop);

                var propAss = new Association(propertyName, propertyType, UmlClassType.Class);
                associations.Add(propAss);
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
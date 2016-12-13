using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EyeOfBeholder.Uml.UmlType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EyeOfBeholder.Uml
{
    public class SyntaxRootsExtractor
    {
        public IEnumerable<SyntaxNode> GetFrom(string codeString)
        {
            var tree = CSharpSyntaxTree.ParseText(codeString);
            tree.GetRoot();
            var syntaxRoot = tree.GetRoot();
            var MyClass = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classDeclarationSyntax in MyClass)
            {
                var umlClassType = classDeclarationSyntax.Keyword.Text;
                var visibility = classDeclarationSyntax.Modifiers[0].ValueText;

                var visibilityType = GetVisibilityType(visibility);
                var name = classDeclarationSyntax.Identifier.ValueText;
                //var baseClass1 = classDeclarationSyntax.BaseList.Types[1];
                var fields = classDeclarationSyntax.DescendantNodes().OfType<FieldDeclarationSyntax>();
                var attributes = new List<Attribute>();
                var associations = new List<Association>();
                foreach (var field in fields)
                {
                    var attributeName = field.Declaration.Variables[0].Identifier.ValueText;
                    var attributeType = field.Declaration.Type;
                    var attributeVisibility = field.Modifiers[0].ValueText;
                    var vis = GetVisibilityType(attributeVisibility);
                    var attribute = new Attribute(attributeName, "jakiśTyp", vis);
                    attributes.Add(attribute);

                    var association = new Association(attributeName, "baseType", UmlClassType.Class);
                    associations.Add(association);
                }
                var interfaces = syntaxRoot.DescendantNodes().OfType<InterfaceDeclarationSyntax>();
                var realizations = new List<Realization>();
                foreach (var interfaceDeclarationSyntax in interfaces)
                {
                    var realizationName = interfaceDeclarationSyntax.Identifier.ValueText;
                    realizations.Add(new Realization(realizationName, UmlClassType.Interface));
                }

                var umlClass = new UmlClass(
                    name, 
                    visibilityType, 
                    associations, 
                    attributes,  
                    realizations,
                    null,
                    new List<Dependency>(), 
                    new List<Operation>()
                    );
            }

            var className = MyClass.First().Identifier.ValueText;
            var members = syntaxRoot.DescendantNodes().OfType<MemberDeclarationSyntax>();

            //var variables = fields.First().DescendantNodes().OfType<VariableDeclarationSyntax>();
            var MyMethod = syntaxRoot.DescendantNodes().OfType<MethodDeclarationSyntax>();
            var properties = syntaxRoot.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            var constructors = syntaxRoot.DescendantNodes().OfType<ConstructorDeclarationSyntax>();
            var dependencies = constructors.First().ParameterList.Parameters;
            var firstParameter = dependencies[0];
            var baseClass =  syntaxRoot.DescendantNodes().OfType<BaseTypeDeclarationSyntax>(); 
            return new List<SyntaxNode>();
        }

        private VisibilityType GetVisibilityType(string visibility)
        {
            switch (visibility)
            {
                case "public":
                    return VisibilityType.Public;
                case "private":
                    return VisibilityType.Private;
                case "protected":
                    return VisibilityType.Protected;
                case "internal":
                    return VisibilityType.Package;
                default: return VisibilityType.Public;
            }
        }
    }
}
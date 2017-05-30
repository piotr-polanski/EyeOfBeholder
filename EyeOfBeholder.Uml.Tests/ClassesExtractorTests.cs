using System.IO;
using System.Linq;
using EyeOfBeholder.Uml.UmlType;
using Xunit;

namespace EyeOfBeholder.Uml.Tests
{
    public class ClassesExtractorTests
    {
        [Fact]
        public void GetFrom_Given_validCSharpCode_Return_UmlClasses()
        {
            //arrange
            var classesExtractor= new ClassesExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var umlClasses = classesExtractor.GetFrom(codeString);

            //assert
            Assert.NotEmpty(umlClasses);
        }

        [Fact]
        public void GetFrom_Given_SimpleClass_Return_UmlClassRepresentation()
        {
            //arrange
            var classesExtractor= new ClassesExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var umlClasses = classesExtractor.GetFrom(codeString);

            //assert
            Assert.Equal(3, umlClasses.Count());
            var simpleClass = umlClasses.First();
            Assert.Equal("SimpleClass", simpleClass.Name);

            Assert.Equal(2, simpleClass.Attributes.Count);
            var firstAttribute = simpleClass.Attributes.First();
            Assert.Equal("name", firstAttribute.Name);
            Assert.Equal("SomeBaseType", firstAttribute.Type);
            Assert.Equal(VisibilityType.Private, firstAttribute.VisibilityType);
            var secondAttribute = simpleClass.Attributes.Last();
            Assert.Equal("Number", secondAttribute.Name);
            Assert.Equal("Int32", secondAttribute.Type);
            Assert.Equal(VisibilityType.Public, secondAttribute.VisibilityType);


            Assert.Equal(1, simpleClass.Associations.Count);
            var firstAssociation = simpleClass.Associations.First();
            Assert.Equal("SomeBaseType", firstAssociation.TypeName);
            Assert.Equal("name", firstAssociation.Name);
            Assert.Equal(UmlClassType.Class, firstAssociation.UmlClassType);

            Assert.Equal(1, simpleClass.Dependencies.Count);
            var firstDependency = simpleClass.Dependencies.First();
            Assert.Equal("SomeBaseType", firstDependency.TypeName);
            Assert.Equal("newName", firstDependency.RelationName);
            Assert.Equal(UmlClassType.Class, firstDependency.Type);

            Assert.Equal(1, simpleClass.Operations.Count);
            var firstOperation = simpleClass.Operations.First();
            Assert.Equal("SomeMethod", firstOperation.Name);
            Assert.Equal("Boolean", firstOperation.ReturnType);
            Assert.Equal(VisibilityType.Public, firstOperation.VisibilityType);

            Assert.Equal(1, simpleClass.Realizations.Count);
            var firstRealization = simpleClass.Realizations.First();
            Assert.Equal("ISomeInterface", firstRealization.Name);
            Assert.Equal(UmlClassType.Interface, firstRealization.Type);

            Assert.Equal("SomeBaseType", simpleClass.SuperClass.Name);
            Assert.Equal(UmlClassType.Class, simpleClass.Type);
            Assert.Equal(VisibilityType.Public, simpleClass.VisibilityType);

            var secondClass = umlClasses.Skip(1).First();
            Assert.Equal("SomeBaseType", secondClass.Name);
            Assert.Empty(secondClass.Associations);
            Assert.Empty(secondClass.Attributes);
            Assert.Empty(secondClass.Dependencies);
            Assert.Empty(secondClass.Operations);
            Assert.Empty(secondClass.Realizations);
            Assert.Null(secondClass.SuperClass);

            var @interface = umlClasses.Skip(2).First();
            Assert.Equal("ISomeInterface", @interface.Name);
            Assert.Empty(@interface.Associations);
            Assert.Empty(@interface.Attributes);
            Assert.Empty(@interface.Dependencies);
            Assert.Empty(@interface.Operations);
            Assert.Empty(@interface.Realizations);
            Assert.Null(@interface.SuperClass);

        }
    }
}

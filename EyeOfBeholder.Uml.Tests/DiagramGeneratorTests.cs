using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlType;
using FakeItEasy;
using Ploeh.AutoFixture;
using Xunit;

namespace EyeOfBeholder.Uml.Tests
{
    public class DiagramGeneratorTests
    {
		[Fact]
        public void GenerateUmlString_Given_ValidTypeDefinition_Builds_UmlString()
        {
            //arrange
			var fixture = new Fixture();
			var umlClasses = new List<UmlClass>()
			{
				fixture.Create<UmlClass>()
			};
	        var umlStringGenerator = A.Fake<IUmlStringGenerator>();
	        A.CallTo(() => umlStringGenerator.StartString)
		        .Returns(new StringBuilder().Append("Start").ToString());
	        A.CallTo(() => umlStringGenerator.EndString)
		        .Returns(new StringBuilder().Append("End").ToString());
	        A.CallTo(() => umlStringGenerator.GetTypeDefinitionString(A<UmlClass>.Ignored))
		        .Returns(new StringBuilder().Append("TypeDefinition").ToString());
	        A.CallTo(() => umlStringGenerator.GetAssociations(A<UmlClass>.Ignored))
		        .Returns(new StringBuilder().Append("Associations").ToString());
	        A.CallTo(() => umlStringGenerator.GetSuperClass(A<UmlClass>.Ignored))
		        .Returns(new StringBuilder().Append("SuperClass").ToString());
	        A.CallTo(() => umlStringGenerator.GetDependencies(A<UmlClass>.Ignored))
		        .Returns(new StringBuilder().Append("Dependencies").ToString());
	        A.CallTo(() => umlStringGenerator.GetRealizations(A<UmlClass>.Ignored))
		        .Returns(new StringBuilder().Append("Realizations").ToString());

            var umlGenerator = new DiagramGenerator(umlStringGenerator);
	        var expectedUmlString =
		        "StartAssociationsSuperClassTypeDefinitionDependenciesRealizationsEnd";

            //act
            var plantUmlString = umlGenerator.GenerateUmlString(umlClasses);

            //assert
            Assert.Equal(expectedUmlString, plantUmlString);
        }
    }
}

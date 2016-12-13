using System.IO;
using Xunit;

namespace EyeOfBeholder.Uml.Tests
{
    public class SyntaxRootExtractorTests
    {
        [Fact]
        public void GetSyntaxRoots_Given_validCSharpCode_ReturnsSyntaxRootCollection()
        {
            //arrange
            var syntaxRootsExtractor = new SyntaxRootsExtractor();
            var codeString = File.ReadAllText(@"Code\SimpleClass.cs");

            //act
            var syntaxRoots = syntaxRootsExtractor.GetFrom(codeString);

            //assert
            Assert.NotEmpty(syntaxRoots);
        }
    }
}

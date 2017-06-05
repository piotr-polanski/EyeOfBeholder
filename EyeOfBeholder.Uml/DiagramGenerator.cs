using System.Collections.Generic;
using System.Text;
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml
{
    public class DiagramGenerator
    {
	    private readonly IUmlStringGenerator _umlStringGenerator;

	    private readonly StringBuilder umlString = new StringBuilder();

	    public DiagramGenerator(IUmlStringGenerator umlStringGenerator)
	    {
		    _umlStringGenerator = umlStringGenerator;
	    }
        public string GenerateUmlString(List<UmlClass> umlClasses)
        {
            umlString.Append(_umlStringGenerator.StartString);


            foreach (var umlClass in umlClasses)
            {
				//umlString.Append(_umlStringGenerator.GetAssociations(umlClass));
				//umlString.Append(_umlStringGenerator.GetSuperClass(umlClass));
	            umlString.Append(_umlStringGenerator.GetTypeDefinitionString(umlClass));
				umlString.Append(_umlStringGenerator.GetDependencies(umlClass));
				//umlString.Append(_umlStringGenerator.GetRealizations(umlClass));
            }

            umlString.Append(_umlStringGenerator.EndString);
            return umlString.ToString();
        }
    }
}
using EyeOfBeholder.Uml.UmlType;

namespace EyeOfBeholder.Uml
{
	public interface IUmlStringGenerator
	{
		string StartString { get; }
		string EndString { get;}
		string GetTypeDefinitionString(UmlClass umlClass);
		string GetAssociations(UmlClass umlClass);
		string GetSuperClass(UmlClass umlClass);
		string GetDependencies(UmlClass umlClass);
		string GetRealizations(UmlClass umlClass);
	}
}
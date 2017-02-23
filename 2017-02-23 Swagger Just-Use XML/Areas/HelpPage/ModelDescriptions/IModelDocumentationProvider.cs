using System;
using System.Reflection;

namespace _2017_02_23_Swagger_Just_Use_XML.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
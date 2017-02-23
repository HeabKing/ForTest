using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _2017_02_23_Swagger_Just_Use_XML.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}
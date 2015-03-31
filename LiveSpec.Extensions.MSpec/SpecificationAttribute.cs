using System;

namespace LiveSpec.Extensions.MSpec
{
    public class SpecificationAttribute : ScenarioAttribute
    {
        public SpecificationAttribute(string subject) : base(subject)
        {
        }

        public SpecificationAttribute(string subject, Type associatedStory) : base(subject, associatedStory)
        {
        }
    }
}
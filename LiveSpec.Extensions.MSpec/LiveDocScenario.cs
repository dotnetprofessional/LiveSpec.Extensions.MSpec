using System;

namespace LiveSpec.Extensions.MSpec
{
    /// <summary>
    /// The LiveDocScenario class is used by tests to gain access to the contents of the 
    /// defined StepAttribute attribute. This can be used to pass the StepAttribute context into the test without
    /// the need to hard code it again.
    /// </summary>
    public class LiveDocScenario
    {
        object instance = null;

        public LiveDocScenario(Type instance)
        {
            this.Given = new Given(instance);
            if (instance.DeclaringType != null) this.Background = new Background(instance.DeclaringType);
        }

        public Given Given { get; private set; }

        public Background Background { get; private set; }
    }
}
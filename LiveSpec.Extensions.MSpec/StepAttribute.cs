using System;
using System.Linq;
using Machine.Specifications.Annotations;

namespace LiveSpec.Extensions.MSpec
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public class StepAttribute : Attribute
    {
        public string Narration { get; set; }

        public StepAttribute(string narration)
        {
            this.Narration = narration;
        }   
    }

    public class Given : Step<GivenAttribute>
    {
        public Given(Type me) : base(me)
        {
        }
    }
    public abstract class Step<T>
        where T: StepAttribute
    {
        Type instance = null;


        protected Step(Type me)
        {
            this.instance = me;
            this.SettNarration();
        }


        void SettNarration()
        {
            var attribute = this.instance.GetCustomAttributes(typeof(T), false).SingleOrDefault() as T;
            if (attribute != null)
            {
                var narration = attribute.Narration;

                var docStringIndex = narration.IndexOf("'''", System.StringComparison.Ordinal);
                if (docStringIndex > 0)
                {
                    this.Narration = narration.Substring(0, docStringIndex);
                    // Now remove the last carriage return produced by the docString
                    this.Narration = this.Narration.TrimEnd(new []{'\n','\r', ' '});
                    this.DocString = this.ProcessDocString(narration.Substring(docStringIndex));
                }
                else
                {
                    this.Narration = narration;
                }

            }
            else
            {
                //throw new InstanceNotFoundException("Unable to find a StepAttribute attribute for this scenario");
            }
        }

        string ProcessDocString(string rawDocString)
        {
            // Split into lines for processing
            rawDocString = rawDocString.Replace("\r", ""); // Stripping out /r as it seems VS sometimes only has /n not /n/r
            var lines = rawDocString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            // Determine the actual last line which contains the terminator
            var terminatorIndex = 0;
            for (int i = lines.Length - 1; i > 0; i--)
            {
                if (lines[i].Contains("'''"))
                {
                    terminatorIndex = i;
                    break;
                }
            }
            // determine how many spaces to trim off lines by counting the number of spaces on the last line before terminator '''
            var padding = lines[terminatorIndex].IndexOf("'''");
            // create new array for returning
            var processedLines = new string[terminatorIndex - 1]; // First and Last lines not needed
            for (int i = 1; i < terminatorIndex; i++)
            {
                if (lines[i].Length < padding)
                    processedLines[i - 1] = "";
                else
                    processedLines[i - 1] = lines[i].Substring(padding);
            }

            var docString = string.Join(Environment.NewLine, processedLines);
            // Determine if the found DocString is refering to an embedded resource
            if (processedLines.Length == 1)
            {
                if (docString.ToLower().StartsWith("resource:"))
                {
                    var resourcename = docString.Substring(9).Trim();
                    // Load the resource 
                    docString = EmbeddedResources.GetResourceString(resourcename, this.instance);
                }
            }
            return docString;
        }

        public string Narration { get; private set; }

        public string DocString { get; private set; }


    }
}

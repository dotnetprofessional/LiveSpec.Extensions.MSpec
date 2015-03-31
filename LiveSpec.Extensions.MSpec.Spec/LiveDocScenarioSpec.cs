using FluentAssertions;
using Machine.Specifications;

namespace LiveSpec.Extensions.MSpec.Spec
{
    [Specification("LiveDocScenario")]
    [Background(@"This specification assumes the following:
            '''
            Background doc string will be available to everyone!!!
            '''    
            ")]
    public class LiveDocScenarioSpec
    {
        [Given(@"StepAttribute some thing
            And something else")]
        public class When_a_given_attribute_is_specified
        {
            static LiveDocScenario Scenario;

            Establish given = () =>
            {
                Scenario = new LiveDocScenario(typeof(When_a_given_attribute_is_specified));
            };
            It should_return_given_text = () =>
            {
                var text = @"StepAttribute some thing
            And something else";

                Scenario.Given.Narration.Should().Be(text);
            };
        }

        [Given(@"StepAttribute some thing
            And something else
            '''
            This is a sample DocString
            '''")]
        public class When_a_given_attribute_is_specified_including_a_Doc_String
        {
            static LiveDocScenario Scenario;

            Establish given = () =>
            {
                Scenario = new LiveDocScenario(typeof(When_a_given_attribute_is_specified_including_a_Doc_String));
            };

            It should_return_given_text = () =>
            {
                var text = @"StepAttribute some thing
            And something else";

                Scenario.Given.Narration.Should().Be(text);
            };

            It should_return_the_doc_string = () =>
            {
                var text = @"This is a sample DocString";

                Scenario.Given.DocString.Should().Be(text);
            };
        }

        [Given(@"StepAttribute some thing
            And something else
            '''
            This is a sample DocString
            '''

            ")]
        public class When_a_given_attribute_is_specified_including_a_Doc_String_with_additional_carriage_returns
        {
            static LiveDocScenario Scenario;

            Establish given = () =>
            {
                Scenario = new LiveDocScenario(typeof(When_a_given_attribute_is_specified_including_a_Doc_String_with_additional_carriage_returns));
            };

            It should_return_given_text = () =>
            {
                var text = @"StepAttribute some thing
            And something else";

                Scenario.Given.Narration.Should().Be(text);
            };

            It should_return_the_doc_string = () =>
            {
                var text = @"This is a sample DocString";

                Scenario.Given.DocString.Should().Be(text);
            };
        }

        [Given(@"StepAttribute some thing
            And something else
            '''
            This is a sample DocString
    
            '''

            ")]
        public class When_a_given_attribute_is_specified_including_a_Doc_String_with_empty_lines_less_than_padding
        {
            static LiveDocScenario Scenario;

            Establish given = () =>
            {
                Scenario = new LiveDocScenario(typeof(When_a_given_attribute_is_specified_including_a_Doc_String_with_empty_lines_less_than_padding));
            };

    
            It should_return_the_doc_string = () =>
            {
                var text = "This is a sample DocString\r\n";

                Scenario.Given.DocString.Should().Be(text);
            };
        }

        [Given(@"StepAttribute some thing
            And something else
            '''
            Resource: SampleDocString.txt
            '''

            ")]
        public class When_a_given_attribute_is_specified_including_a_Doc_String_that_refernces_an_embedded_resource
        {
            static LiveDocScenario Scenario;

            Establish given = () =>
            {
                Scenario = new LiveDocScenario(typeof(When_a_given_attribute_is_specified_including_a_Doc_String_that_refernces_an_embedded_resource));
            };


            It should_return_the_doc_string_from_resource_file = () =>
            {
                var text = "A sample DocString in a resource file";

                Scenario.Given.DocString.Should().Be(text);
            };
        }

        [Given(@"StepAttribute some thing
            And something else
            '''
            Resource: SampleDocString.txt
            '''

            ")]
        public class When_a_background_attribute_is_specified_including_a_Doc_String
        {
            static LiveDocScenario Scenario;

            Establish given = () =>
            {
                Scenario = new LiveDocScenario(typeof(When_a_background_attribute_is_specified_including_a_Doc_String));
            };


            It should_return_the_doc_string_of_the_background_attribute = () =>
            {
                var text = "Background doc string will be available to everyone!!!";

                Scenario.Background.DocString.Should().Be(text);
            };
        }
    }
}

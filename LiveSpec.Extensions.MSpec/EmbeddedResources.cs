using System;
using System.IO;
using System.Reflection;

namespace LiveSpec.Extensions.MSpec
{
    internal class EmbeddedResources
    {
        /// <summary>
        /// Returns the contents of an embedded resource file as a string
        /// </summary>
        /// <param name="resourceName">The name of the resource file to return. The filename is matched using EndsWith allowing for partial filenames to be used.</param>
        /// <param name="typeFromResourceAssembly">a type from the assembly that contains the embedded resource</param>
        /// <returns></returns>
        public static string GetResourceString(string resourceName, Type typeFromResourceAssembly)
        {
            StreamReader objStream;
            string strText = "";

            var assembly = typeFromResourceAssembly.Assembly;
            var resource = GetResourceStream(resourceName, assembly);
            if(resource == null)
                throw new FileNotFoundException("Unable to find the resource: " + resourceName);

            using (objStream = new StreamReader(resource))
            {
                strText = objStream.ReadToEnd();
            }

            return strText;
        }

        private static Stream GetResourceStream(string resourceName, Assembly assembly)
        {
            string strFullResourceName = "";
            foreach (string r in assembly.GetManifestResourceNames())
            {
                if (r.EndsWith(resourceName))
                {
                    strFullResourceName = r;
                    break;
                }
            }

            if (strFullResourceName != "")
                return assembly.GetManifestResourceStream(strFullResourceName);
            else
                return null;
        }

    }
}

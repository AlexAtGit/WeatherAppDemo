using System;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace alex.home.WeatherApp.Shared
{
    /// <summary>
    /// Contains general purpose methods
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Load an object from an XML file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T LoadXML<T>(string filePath)
        {
            T theObject = default(T);

            if (File.Exists(filePath))
            {
                using (TextReader textReader = new StreamReader(filePath))
                {
                    //var xmlSerializer = new XmlSerializer(typeof(T)); 
                    // Instead of the above, use the below to avoid (FileNotFoundException of the form ThisNamespace.XmlSerializers)
                    var xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];

                    theObject = (T)xmlSerializer.Deserialize(textReader);
                }
            }

            return theObject;
        }

        /// <summary>
        /// Save an object into an XML file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="theObject"></param>
        public static void SaveXML<T>(string filePath, T theObject)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(filePath);
            if (theObject == null) throw new ArgumentNullException("theObject");

            using (TextWriter textWriter = new StreamWriter(filePath))
            {
                //var xmlSerializer = new XmlSerializer(typeof(T));
                // Instead of the above, use the below to avoid (FileNotFoundException of the form ThisNamespace.XmlSerializers)
                var xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
                xmlSerializer.Serialize(textWriter, theObject);
            }
        }

        public static string GetVersionNo()
        {
            try
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return version.Major + "." + version.Minor + "." + version.Build;
            }
            catch (Exception)
            {
                return "?.?.?";
            }
        }
    }
}

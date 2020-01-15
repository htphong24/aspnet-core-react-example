using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AspnetCoreSPATemplate.Utils
{
    /// <summary>
    /// Deserializes string data into XML or JSON depending on the requested format as specified in the HTTP request <c>Content-Type</c> header
    /// </summary>
    public static class ApiDeserializer
    {
        /// <summary>
        /// Deserializes an object from XML or JSON
        /// </summary>
        /// <returns>Deserialized object</returns>
        public static object Deserialize(Type type, HttpRequest contextRequest)
        {
            string contentType = contextRequest.ContentType;
            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = ApiActionResult.JSON_CONTENT_TYPE;
            }
            else if (contentType.ToLower().Contains(ApiActionResult.JSON_CONTENT_TYPE))
            {
                contentType = ApiActionResult.JSON_CONTENT_TYPE;
            }
            else if (contentType.ToLower().Contains(ApiActionResult.XML_CONTENT_TYPE))
            {
                contentType = ApiActionResult.XML_CONTENT_TYPE;
            }
            else
            {
                contentType = ApiActionResult.JSON_CONTENT_TYPE;
            }

            // The InputStream was already parsed by the controller let's reset the inputstream position to 0
            //contextRequest.Body.Position = 0;

            if (contentType == ApiActionResult.XML_CONTENT_TYPE)
            {
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(contextRequest.Body);
            }
            else
            {
                using (TextReader textReader = new HttpRequestStreamReader(contextRequest.Body, Encoding.UTF8))
                using (JsonReader jsonReader = new JsonTextReader(textReader))
                {
                    JsonSerializer serializer = new JsonSerializer()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new ApiActionResult.CustomDateTimeFormatResolver(),
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    };

                    object o = serializer.Deserialize(jsonReader, type);
                    if (o == null)
                    {
                        // If  null, then get a empty instance
                        o = Activator.CreateInstance(type);
                    }
                    return o;
                }
            }
        }
    }
}

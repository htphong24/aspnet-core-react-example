using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AspnetCoreSPATemplate.Utils
{
    /// <summary>
    /// Serializes data into XML or JSON depending on the requested format as specified in the HTTP request <c>Content-Type</c> or <c>Accept</c> header
    /// </summary>
    /// <remarks>
    /// See <a href="http://james.newtonking.com/archive/2008/10/16/asp-net-mvc-and-json-net">here</a> for details.
    /// </remarks>
    public class ApiActionResult : ActionResult
    {
        private HttpRequest _contextRequest { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public Newtonsoft.Json.Formatting JsonFormatting { get; set; }

        public const string XML_CONTENT_TYPE = "application/xml";
        public const string JSON_CONTENT_TYPE = "application/json";

        /// <summary>
        /// Basic constructor
        /// </summary>
        public ApiActionResult(HttpRequest contextRequest)
        {
            _contextRequest = contextRequest;

            JsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CustomDateTimeFormatResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            this.JsonFormatting = Newtonsoft.Json.Formatting.None;
            this.ContentEncoding = Encoding.UTF8;

            string dataType = _contextRequest.ContentType;
            if (string.IsNullOrWhiteSpace(dataType))
            {
                dataType = _contextRequest.Headers["Accept"];
            }

            if (string.IsNullOrWhiteSpace(dataType))
            {
                this.ContentType = JSON_CONTENT_TYPE;
            }
            else
            {
                dataType = dataType.ToLower();
                if (dataType.ToLower().Contains("application/json"))
                {
                    this.ContentType = JSON_CONTENT_TYPE;
                }
                else if (dataType.ToLower().Contains("application/xml"))
                {
                    this.ContentType = XML_CONTENT_TYPE;
                }
                else if (dataType.ToLower().Contains("multipart/form-data")
                    && !string.IsNullOrEmpty(_contextRequest.Headers["Accept"])
                    && _contextRequest.Headers["Accept"] == XML_CONTENT_TYPE)
                {
                    this.ContentType = XML_CONTENT_TYPE;
                }
                else
                {
                    this.ContentType = JSON_CONTENT_TYPE;
                }
            }
        }

        /// <summary>
        /// Constructor specifying the data to serialize
        /// </summary>
        /// <param name="data">Object to serialize</param>
        /// <remarks>
        /// If <c>data</c> is an <see cref="Exception"/>, the exception will be encapsulated in <see cref="ApiError"/>.
        /// </remarks>
        public ApiActionResult(HttpRequest contextRequest, object data)
            : this(contextRequest)
        {
            if (data is Exception)
            {
                this.Data = new ApiError((Exception)data);
            }
            else
            {
                this.Data = data;
            }
        }

        /// <summary>
        /// Write our JSON
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponse response = context.HttpContext.Response;
            response.ContentType = this.ContentType;

            // StatusCodes:
            // - Status200OK
            // - Status201Created
            // - Status204
            // - Status400BadRequest
            // - Status400NotFound
            // - Status500InternalServerError 

            if (Data == null)
            {
                // NO Content
                response.StatusCode = StatusCodes.Status204NoContent;
            }
            else
            {
                response.StatusCode = (this.Data is ApiError) 
                                        ? StatusCodes.Status500InternalServerError 
                                        : StatusCodes.Status200OK;
                using (StreamWriter sw = new StreamWriter(response.Body))
                {
                    if (this.ContentType == XML_CONTENT_TYPE)
                    {
                        using (XmlTextWriter writer = new XmlTextWriter(sw))
                        {
                            writer.Formatting = System.Xml.Formatting.Indented;
                            XmlSerializer serializer = new XmlSerializer(this.Data.GetType());
                            serializer.Serialize(writer, this.Data);
                        }
                    }
                    else
                    {
                        using (JsonTextWriter writer = new JsonTextWriter(sw))
                        {
                            writer.Formatting = JsonFormatting;
                            JsonSerializer serializer = JsonSerializer.Create(JsonSerializerSettings);
                            serializer.Serialize(writer, this.Data);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Custom JSON datetime serializer for date only properties with no timestamp
        /// </summary>
        /// <remarks>
        /// <para>
        /// The default format for <see cref="DateTime"/> is <c>2009-02-15T00:00:00Z</c>. For date only property, we do not want 
        /// to serialize the time portion.
        /// </para>
        /// <para>
        /// This formater serializes dates to <c>2009-02-15</c> if the property name ends to "Date".
        /// </para>
        /// See http://stackoverflow.com/questions/22858993/override-json-net-property-serialization-formatting.
        /// See http://www.newtonsoft.com/json/help/html/DatesInJSON.htm
        /// </remarks>
        public class CustomDateTimeFormatResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                // skip if the property is not a DateTime
                if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?) &&
                    property.PropertyType != typeof(DateTimeOffset) && property.PropertyType != typeof(DateTimeOffset?))
                {
                    return property;
                }

                if (property.Converter != null && property.Converter.GetType() == typeof(CustomDateTimeFormatConverter))
                {
                    return property;
                }

                IsoDateTimeConverter converter = new IsoDateTimeConverter();
                if (member.Name.EndsWith("Date"))
                {
                    converter.DateTimeFormat = "yyyy-MM-dd";
                }
                else
                {
                    // For serialization ... this converts to UTC 
                    // It gets ignored for deserialization
                    converter.DateTimeStyles = DateTimeStyles.AdjustToUniversal;
                }
                property.Converter = converter;

                return property;
            }
        }
    }
}

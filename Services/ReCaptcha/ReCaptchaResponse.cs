using System.Collections.Generic;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ReCaptchaResponse
    {
        public ReCaptchaResponse()
        {
        }

        /// <summary>
        /// Results of the search
        /// </summary>
        public List<ContactModel> Results { get; set; }
    }
}
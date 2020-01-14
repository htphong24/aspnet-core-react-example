using System.Collections.Generic;
using AspnetCoreSPATemplate.Models;

namespace AspnetCoreSPATemplate.Services.ReCaptcha
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

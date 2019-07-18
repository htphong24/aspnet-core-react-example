using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public abstract class ServiceBase<RQ, RS>
    {
        protected abstract Task<RS> DoRunAsync(RQ request);

        /// <summary>
        /// Service context for security and user info
        /// </summary>
        protected ServiceContext ServiceContext
        { get; private set; }

        public ServiceBase(ServiceContext context)
        {
            this.ServiceContext = context;
        }

        private void Authorize()
        {
        }

        public async Task<RS> RunAsync(RQ request)
        {
            try
            {
                Authorize();
                RS result = await DoRunAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                DoExceptionHandling(ex);
                throw;
            }
        }

        protected virtual void DoExceptionHandling(Exception ex)
        {
            return;
        }
    }
}

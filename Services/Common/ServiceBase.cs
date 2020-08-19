using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
{
    public abstract class ServiceBase<RQ, RS>
    {
        protected abstract Task<RS> DoRunAsync(RQ request);

        /// <summary>
        /// Service context for security and user info
        /// </summary>
        protected ServiceContext ServiceContext { get; private set; }

        protected ServiceBase(ServiceContext context)
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
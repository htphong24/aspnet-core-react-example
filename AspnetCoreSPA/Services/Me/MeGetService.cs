using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Me
{
    public class MeGetService : ServiceBase<MeGetRequest, MeGetResponse>
    {
        private readonly IMeRepository _meRepo;
        private readonly ServiceContext _context;

        public MeGetService(ServiceContext context, IMeRepository userRepo)
            : base(context)
        {
            _meRepo = userRepo;
            _context = context;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<MeGetResponse> DoRunAsync(MeGetRequest rq)
        {
            MeGetResponse rs = new MeGetResponse
            {
                User = await _meRepo.GetAsync(rq)
            };

            return rs;
        }
    }
}

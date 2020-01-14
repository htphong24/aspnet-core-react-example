using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserCreateService : ServiceBase<UserCreateRequest, UserCreateResponse>
    {
        private readonly IUserRepository _userRepo;

        public UserCreateService(ServiceContext context, IUserRepository userRepo)
            : base(context)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<UserCreateResponse> DoRunAsync(UserCreateRequest rq)
        {
            UserCreateResponse rs = new UserCreateResponse();
            await _userRepo.CreateAsync(rq);
            return rs;
        }
    }
}

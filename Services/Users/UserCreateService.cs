using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
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
            var rs = new UserCreateResponse();
            await _userRepo.CreateAsync(rq);
            return rs;
        }
    }
}
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserListService : ServiceBase<UserListRequest, UserListResponse>
    {
        private readonly IUserRepository _userRepo;

        public UserListService(ServiceContext context, IUserRepository userRepo)
            : base(context)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<UserListResponse> DoRunAsync(UserListRequest rq)
        {
            UserListResponse rs = new UserListResponse
            {
                Results = await _userRepo.ListAsync(rq),
                RecordCount = await _userRepo.ListRecordCountAsync()
            };

            return rs;
        }
    }
}

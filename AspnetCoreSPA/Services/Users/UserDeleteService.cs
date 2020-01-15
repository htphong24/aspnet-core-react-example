using AspnetCoreSPATemplate.Services.Common;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserDeleteService : ServiceBase<UserDeleteRequest, UserDeleteResponse>
    {
        private readonly IUserModificationRepository _userModRepo;

        public UserDeleteService(ServiceContext context, IUserModificationRepository userModRepo)
            : base(context)
        {
            _userModRepo = userModRepo;
        }

        /// <summary>
        /// Inserts a new contact.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<UserDeleteResponse> DoRunAsync(UserDeleteRequest rq)
        {
            UserDeleteResponse rs = new UserDeleteResponse();
            await _userModRepo.DeleteAsync(rq);
            return rs;
        }
    }
}
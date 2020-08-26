using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
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
            var rs = new UserDeleteResponse();
            await _userModRepo.DeleteAsync(rq);
            return rs;
        }
    }
}
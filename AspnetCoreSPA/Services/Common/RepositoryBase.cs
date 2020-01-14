using AutoMapper;
using SqlServerDataAccess.EF;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class RepositoryBase
    {
        protected readonly ContactsMgmtContext _db;
        protected readonly ContactsMgmtIdentityContext _idDb;
        protected readonly IMapper _mapper;

        public RepositoryBase(ContactsMgmtContext db, ContactsMgmtIdentityContext idDb, IMapper mapper)
        {
            _db = db;
            _idDb = idDb;
            _mapper = mapper;
        }

    }
}

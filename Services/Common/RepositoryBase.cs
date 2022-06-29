using AutoMapper;
using SqlServerDataAccess.EF;
// ReSharper disable CheckNamespace

namespace Services
{
    public class RepositoryBase
    {
        protected readonly ContactsMgmtContext Db;
        protected readonly ContactsMgmtIdentityContext IdDb;
        protected readonly IMapper Mapper;

        public RepositoryBase(ContactsMgmtContext db, ContactsMgmtIdentityContext idDb, IMapper mapper)
        {
            Db = db;
            IdDb = idDb;
            Mapper = mapper;
        }
    }
}
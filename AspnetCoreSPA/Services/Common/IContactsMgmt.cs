using AutoMapper;
using SqlServerDataAccess.EF;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IContactsMgmt
    {
        IMapper Mapper { get; set; }

        ContactsMgmtContext Db { get; set; }
    }
}

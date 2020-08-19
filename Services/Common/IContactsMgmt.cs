// ReSharper disable CheckNamespace

using AutoMapper;
using SqlServerDataAccess.EF;

namespace Services
{
    public interface IContactsMgmt
    {
        IMapper Mapper { get; set; }

        ContactsMgmtContext Db { get; set; }
    }
}
using AutoMapper;
using SqlServerDataAccess;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class RepositoryBase
    {
        public ContactsMgmtContext Db { get; set; }

        public IMapper Mapper { get; set; }

    }
}

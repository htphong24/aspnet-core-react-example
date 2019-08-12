using AspnetCoreSPATemplate.Models;
using AutoMapper;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IContactsMgmt
    {
        IMapper Mapper { get; set; }

        ContactsMgmtContext Db { get; set; }
    }
}

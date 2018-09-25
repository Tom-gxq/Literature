using Lib.Application.Services;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User
{
    public interface IAssociatorAppService : IApplicationService
    {
        bool AddAssociator(AssociatorDto associator);
        bool DeleteAssociator(string associatorId);
        List<AssociatorDto> GetAssociatorList(int pageIndex, int pageSize);
        int GetAssociatorListCount();
        List<AssociatorDto> SearchAssociatorByAccountId(string accountId, int pageIndex, int pageSize);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTO;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersByCompanyAsync(Guid companyId);
        Task<UserDto> GetByIdAsync(Guid id);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTO;

namespace TaskManagement.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetByIdAsync(Guid id);
        Task<CompanyDto> CreateAsync(CompanyCreateDto dto);
    }

}

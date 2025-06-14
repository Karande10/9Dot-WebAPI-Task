using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTO;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksAsync(TaskQueryParameters parameters, Guid companyId);
        Task<TaskDto> GetByIdAsync(Guid id, Guid companyId);
        Task<TaskDto> CreateAsync(TaskCreateDto dto, Guid companyId, Guid createdBy);
        Task<TaskDto> UpdateAsync(TaskUpdateDto dto, Guid companyId, Guid updatedBy);
        Task DeleteAsync(Guid id, Guid companyId);
    }

}

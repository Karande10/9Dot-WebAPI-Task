using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTO;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskCommentService
    {
        Task<IEnumerable<TaskCommentDto>> GetCommentsByTaskIdAsync(Guid taskId, Guid companyId);
        Task<TaskCommentDto> AddCommentAsync(TaskCommentCreateDto dto, Guid taskId, Guid userId, Guid companyId);
    }

}

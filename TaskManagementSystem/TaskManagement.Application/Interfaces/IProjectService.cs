using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTO;

namespace TaskManagement.Application.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllProjectsAsync(Guid companyId);
        Task<ProjectDto> GetProjectByIdAsync(Guid id, Guid companyId);
        Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto, Guid userId, Guid companyId);
        Task<bool> UpdateProjectAsync(Guid id, ProjectUpdateDto dto, Guid userId, Guid companyId);
        Task<bool> DeleteProjectAsync(Guid id, Guid userId, Guid companyId);


    }

}

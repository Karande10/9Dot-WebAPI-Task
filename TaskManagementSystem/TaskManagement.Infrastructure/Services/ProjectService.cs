using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.DTO;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entites;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> GetAllProjectsAsync(Guid companyId)
        {
            var projects = await _context.Projects
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();

            return _mapper.Map<List<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(Guid id, Guid companyId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.CompanyId == companyId);

            if (project == null)
                throw new Exception("Project not found.");

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto, Guid userId, Guid companyId)
        {
            var project = _mapper.Map<Project>(dto);
            project.Id = Guid.NewGuid();
            project.CompanyId = companyId;
            project.CreatedByUserId = userId;
            project.CreatedAt = DateTime.UtcNow;

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> UpdateProjectAsync(Guid id, ProjectUpdateDto dto, Guid userId, Guid companyId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.CompanyId == companyId);

            if (project == null)
                throw new Exception("Project not found.");

            _mapper.Map(dto, project);
            project.UpdatedByUserId = userId;
            project.UpdatedAt = DateTime.UtcNow;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProjectAsync(Guid id, Guid userId, Guid companyId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.CompanyId == companyId);

            if (project == null)
                throw new Exception("Project not found.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

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
    public class TaskService : ITaskService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync(TaskQueryParameters parameters, Guid companyId)
        {
            var query = _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .Where(t => t.Project.CompanyId == companyId && !t.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(t =>
                    t.Title.Contains(parameters.Search) ||
                    t.Project.Name.Contains(parameters.Search));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                switch (parameters.SortBy.ToLower())
                {
                    case "title":
                        query = parameters.IsDescending
                            ? query.OrderByDescending(t => t.Title)
                            : query.OrderBy(t => t.Title);
                        break;
                    case "createdat":
                        query = parameters.IsDescending
                            ? query.OrderByDescending(t => t.CreatedAt)
                            : query.OrderBy(t => t.CreatedAt);
                        break;
                      
                }
            }

            query = query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            var tasks = await query.ToListAsync();

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetByIdAsync(Guid id, Guid companyId)
        {
            var task = await _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.CompanyId == companyId && !t.IsDeleted);

            if (task == null)
                throw new Exception("Task not found or does not belong to your company.");

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> CreateAsync(TaskCreateDto dto, Guid companyId, Guid createdBy)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.CompanyId == companyId && !p.IsDeleted);

            if (project == null)
                throw new Exception("Project not found or not accessible.");

            var task = _mapper.Map<TaskItem>(dto);
            task.Id = Guid.NewGuid();
            task.CreatedBy = createdBy;
            task.CreatedAt = DateTime.UtcNow;

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> UpdateAsync(TaskUpdateDto dto, Guid companyId, Guid updatedBy)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == dto.Id && t.Project.CompanyId == companyId && !t.IsDeleted);

            if (task == null)
                throw new Exception("Task not found or access denied.");

           

            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDto>(task);
        }

        public async Task DeleteAsync(Guid id, Guid companyId)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.CompanyId == companyId && !t.IsDeleted);

            if (task == null)
                throw new Exception("Task not found or already deleted.");

            task.IsDeleted = true;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}

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
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskCommentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskCommentDto>> GetCommentsByTaskIdAsync(Guid taskId, Guid companyId)
        {
            var comments = await _context.Tasks
                .Where(c => c.Id == taskId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TaskCommentDto>>(comments);
        }

        public async Task<TaskCommentDto> AddCommentAsync(TaskCommentCreateDto dto, Guid taskId, Guid userId, Guid companyId)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.CompanyId == companyId);

            if (task == null)
                throw new Exception("Task not found or does not belong to your company.");

            var comment = new TaskComment
            {
                Id = Guid.NewGuid(),
                Comment = dto.Comment,
                TaskItemId = taskId,
                CreatedByUserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

          

            return _mapper.Map<TaskCommentDto>(comment);
        }
    
}
}

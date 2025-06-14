using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTO;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/tasks/{taskId}/comments")]
    [Authorize]
    public class TaskCommentsController : ControllerBase
    {
        private readonly ITaskCommentService _commentService;

        public TaskCommentsController(ITaskCommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid taskId, [FromHeader] Guid companyId)
        {
            var comments = await _commentService.GetCommentsByTaskIdAsync(taskId, companyId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid taskId, [FromBody] TaskCommentCreateDto dto, [FromHeader] Guid userId, [FromHeader] Guid companyId)
        {
            var comment = await _commentService.AddCommentAsync(dto, taskId, userId, companyId);
            return Ok(comment);
        }
    }
}

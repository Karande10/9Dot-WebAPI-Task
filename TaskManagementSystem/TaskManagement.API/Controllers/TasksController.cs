using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTO;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] TaskQueryParameters parameters, [FromHeader] Guid companyId)
        {
            var tasks = await _taskService.GetTasksAsync(parameters, companyId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromHeader] Guid companyId)
        {
            var task = await _taskService.GetByIdAsync(id, companyId);
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDto dto, [FromHeader] Guid companyId, [FromHeader] Guid userId)
        {
            var task = await _taskService.CreateAsync(dto, companyId, userId);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskUpdateDto dto, [FromHeader] Guid companyId, [FromHeader] Guid userId)
        {
            var task = await _taskService.UpdateAsync(dto, companyId, userId);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromHeader] Guid companyId)
        {
            await _taskService.DeleteAsync(id, companyId);
            return NoContent();
        }
    }
}

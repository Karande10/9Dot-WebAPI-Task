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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetAllProjects(Guid companyId)
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync(companyId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("{id}/company/{companyId}")]
        public async Task<IActionResult> GetById(Guid id, Guid companyId)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id, companyId);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto dto, [FromQuery] Guid userId, [FromQuery] Guid companyId)
        {
            try
            {
                var createdProject = await _projectService.CreateProjectAsync(dto, userId, companyId);
                return CreatedAtAction(nameof(GetById), new { id = createdProject.Id, companyId }, createdProject);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProjectUpdateDto dto, [FromQuery] Guid userId, [FromQuery] Guid companyId)
        {
            try
            {
                var success = await _projectService.UpdateProjectAsync(id, dto, userId, companyId);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid userId, [FromQuery] Guid companyId)
        {
            try
            {
                var success = await _projectService.DeleteProjectAsync(id, userId, companyId);
                if (!success)
                    return NotFound();

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        }
    }


using AutoMapper;
using IssueService.Api.Contracts;
using IssueService.DAL;
using IssueService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.WebHost.Controllers
{
    /// <summary>
    /// Контроллер сущности Проекты
    /// </summary>
    [ApiController]
    [Route("api/issues")]
    public class IssuesController
        : ControllerBase
    {
        private readonly IssueRepository _issues;
        private readonly IMapper _mapper;

        public IssuesController(
            IssueRepository issues,
            IMapper mapper)
        {
            _issues = issues;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueResponse>>> GetIssuesAsync(string? projectId = null)
        {
            IEnumerable<Issue> projects;
            if (projectId != null)
            {
                Guid projectGuid = Guid.Parse(projectId);
                projects = await _issues.GetAllAsync(q =>
                {
                    return q.Where(i => i.ProjectId == projectGuid);
                });
            }
            else
            {
                projects = await _issues.GetAllAsync();
            }

            IEnumerable<IssueResponse> projectsResponse = projects.Select(p => MapProject(p));
            return Ok(projectsResponse);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IssueResponse>> GetIssueAsync(Guid id)
        {
            var issue =  await _issues.GetAsync(id);
            if (issue == null)
                return NotFound();

            IssueResponse response = MapProject(issue);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<IssueResponse>> CreateIssuesAsync(IssueRequest request)
        {
            Issue issue = new();
            MapProject(request, issue);
            await _issues.AddAsync(issue);

            return Ok(issue);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIssuesAsync(Guid id, IssueRequest request)
        {
            var issue = await _issues.GetAsync(id);
            if (issue == null)
                return NotFound();

            MapProject(request, issue);

            await _issues.UpdateAsync(issue);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIssuesAsync(Guid id)
        {
            var project = await _issues.GetAsync(id);

            if (project == null)
                return NotFound();

            await _issues.DeleteAsync(id);
            return NoContent();
        }

        #region Helpers

        private IssueResponse MapProject(Issue issue)
        {
            IssueResponse response = _mapper.Map<Issue, IssueResponse>(issue);
            return response;
        }

        private void MapProject(IssueRequest request, Issue issue)
        {
            _mapper.Map(request, issue);
        }

        #endregion
    }
}

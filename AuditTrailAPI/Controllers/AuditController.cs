using AuditTrailService.Interface;
using AuditTrailShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditTrailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;
        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost("log")]
        public IActionResult LogAudit([FromBody] AuditRequest request)
        {
            var response = _auditService.GenerateAuditTrail(request);
            return Ok(response);
        }

        [HttpGet("all")]
        public IActionResult GetAllAudits() => Ok(_auditService.GetAll());

        [HttpGet("entity/{name}")]
        public IActionResult GetByEntityName(string name) => Ok(_auditService.GetByEntityName(name));

        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(string userId) => Ok(_auditService.GetByUserId(userId));

        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            _auditService.Clear();
            return NoContent();
        }
    }
}

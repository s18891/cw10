using System;
using cw10.DTOs.Requests;
using cw10.Models;
using cw10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                Enrollment enrollment = _service.EnrollStudent(request);
                return Ok(enrollment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            try
            {
                Enrollment enrollment = _service.PromoteStudent(request);
                return Ok(enrollment);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

    }
}
